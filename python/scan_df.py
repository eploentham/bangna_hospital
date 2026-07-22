#!/usr/bin/env python3
"""
==============================================================
  Batch OCR for DF Surgery Forms — Claude API
  v3: Multi-page PDF support
==============================================================
  Reads images + PDFs from INPUT_DIR. Splits multi-page PDFs
  into individual pages. Each page = 1 form = 1 API call.

  SUPPORTED FORMATS:
    - PDF (.pdf)         ← multi-page supported, splits per page
    - Images (.jpg/.jpeg/.png/.gif/.webp)

  ASSUMPTIONS:
    - 1 page = 1 DF form (no forms span multiple pages)
    - If a PDF has 50 pages → 50 form extractions

  SETUP:
    pip install anthropic python-dotenv pypdf

  ENV vars (.env file):
    ANTHROPIC_API_KEY=sk-ant-...
    INPUT_DIR=./df_forms
    OUTPUT_CSV=./df_results.csv
    MODEL=claude-sonnet-4-6
==============================================================
"""

import os
import sys
import json
import base64
import logging
import csv
from io import BytesIO
from pathlib import Path
from datetime import datetime
from dataclasses import dataclass, asdict
from typing import NamedTuple

import anthropic
from dotenv import load_dotenv
from pypdf import PdfReader, PdfWriter


# ============================================================
#  CONFIG
# ============================================================
load_dotenv()

INPUT_DIR = Path(os.getenv("INPUT_DIR", "./df_forms"))
OUTPUT_CSV = Path(os.getenv("OUTPUT_CSV", f"./df_results_{datetime.now():%Y%m%d_%H%M}.csv"))
RAW_DIR = Path(os.getenv("RAW_DIR", "./df_raw_responses"))
PROCESSED_LOG = Path(os.getenv("PROCESSED_LOG", "./df_processed.log"))

MODEL = os.getenv("MODEL", "claude-sonnet-4-6")
MAX_TOKENS = 800

IMAGE_EXTS = {".jpg", ".jpeg", ".png", ".gif", ".webp"}
PDF_EXTS = {".pdf"}
SUPPORTED_EXTS = IMAGE_EXTS | PDF_EXTS

COST_PER_PAGE = {
    "claude-haiku-4-5": 0.001,
    "claude-sonnet-4-6": 0.005,
    "claude-opus-4-7": 0.020,
    "claude-opus-4-8": 0.020,
}


# ============================================================
#  LOGGING
# ============================================================
logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s [%(levelname)s] %(message)s",
    handlers=[
        logging.FileHandler("df_ocr.log", encoding="utf-8"),
        logging.StreamHandler(sys.stdout),
    ],
)
log = logging.getLogger("df_ocr")


# ============================================================
#  DATA STRUCTURES
# ============================================================
class Page(NamedTuple):
    """One form to process (= one image or one PDF page)."""
    source_file: str       # original filename, e.g. 'batch_2026_06.pdf'
    page_num: int          # 1 for images, 1+ for PDF pages
    data: bytes            # raw bytes (image bytes or single-page PDF bytes)
    media_type: str        # 'image/jpeg' or 'application/pdf'
    content_type: str      # 'image' or 'document' (for Claude API content block)

    @property
    def page_id(self) -> str:
        """Unique identifier: 'batch_2026_06.pdf#3' or 'scan.jpg#1'."""
        return f"{self.source_file}#{self.page_num}"


@dataclass
class DFFormResult:
    """One row of extracted data (1 row per page)."""
    source_file: str = ""
    page_num: int = 1
    hn: str = ""
    date: str = ""
    patient_age: str = ""
    rights: str = ""
    dx: str = ""
    operation: str = ""
    anesthesia_fee: str = ""
    treatment_fee: str = ""
    surgery_fee: str = ""
    assistant_fee: str = ""
    rw: str = ""
    time: str = ""
    confidence_notes: str = ""
    status: str = "PENDING"
    error: str = ""
    processed_at: str = ""


# ============================================================
#  PROMPT
# ============================================================
EXTRACTION_PROMPT = """You are a precise OCR system reading a Thai medical form.

This is a "แบบฟอร์มใบ DF ผ่าตัด" (DF Surgery Form) from a Thai hospital.
The input contains ONE form. Extract its data.

Extract these fields. Return ONLY valid JSON, no markdown, no extra text.

Required JSON structure:
{
  "hn": "<Hospital Number, digits only>",
  "date": "<วันที่ - keep Thai format e.g. '26 พ.ค. 2568' or empty if unclear>",
  "patient_age": "<age e.g. '73 ปี 11 ด 22 ว' or empty>",
  "rights": "<สิทธิ - which checkbox is marked: ปกส/กท/บริษัท/ทั่วไป/พรบ/อื่นๆ>",
  "dx": "<diagnosis text or empty if unclear>",
  "operation": "<operation name or empty>",
  "anesthesia_fee": "<ค่าดมยา - number only, e.g. '500'>",
  "treatment_fee": "<ค่ารักษา - number only>",
  "surgery_fee": "<ค่าผ่าตัด - number only>",
  "assistant_fee": "<ค่าผู้ช่วยผ่าตัด - number only>",
  "rw": "<RW value, decimal number e.g. '0.6501'>",
  "time": "<time of operation e.g. '08:10-08:55'>",
  "confidence_notes": "<brief notes on any unclear/illegible fields>"
}

RULES:
- For numbers (fees, RW): extract digits and decimal point only, no Thai or units
- If a field is unclear or empty, return empty string ""
- If you can't read handwriting, note it in confidence_notes
- DO NOT include any text outside the JSON object
- DO NOT use markdown code fences (no ```json)
"""


# ============================================================
#  FILE LOADING & PAGE SPLITTING
# ============================================================
def load_pages_from_file(file_path: Path) -> list[Page]:
    """
    Load a file and return list of Page objects ready to process.

    - Image file       → 1 Page
    - Single-page PDF  → 1 Page
    - Multi-page PDF   → N Pages
    """
    ext = file_path.suffix.lower()
    pages: list[Page] = []

    if ext == ".pdf":
        # Open multi-page PDF
        reader = PdfReader(str(file_path))
        total_pages = len(reader.pages)
        log.info(f"   📄 {file_path.name} has {total_pages} page(s)")

        # Extract each page as its own single-page PDF in memory
        for i, src_page in enumerate(reader.pages, start=1):
            writer = PdfWriter()
            writer.add_page(src_page)
            buf = BytesIO()
            writer.write(buf)

            pages.append(Page(
                source_file=file_path.name,
                page_num=i,
                data=buf.getvalue(),
                media_type="application/pdf",
                content_type="document",
            ))

    elif ext in IMAGE_EXTS:
        # Single image = 1 page
        media_type_map = {
            ".jpg": "image/jpeg",
            ".jpeg": "image/jpeg",
            ".png": "image/png",
            ".gif": "image/gif",
            ".webp": "image/webp",
        }
        media_type = media_type_map.get(ext, "image/jpeg")

        with open(file_path, "rb") as f:
            img_bytes = f.read()

        pages.append(Page(
            source_file=file_path.name,
            page_num=1,
            data=img_bytes,
            media_type=media_type,
            content_type="image",
        ))

    return pages


# ============================================================
#  HELPERS
# ============================================================
def parse_response(response_text: str) -> dict:
    """Parse JSON from Claude, tolerant of accidental markdown fences."""
    text = response_text.strip()
    if text.startswith("```"):
        start = text.find("{")
        end = text.rfind("}")
        if start != -1 and end != -1:
            text = text[start:end + 1]
    return json.loads(text)


def load_processed_pages() -> set:
    """Return set of page_ids already processed (e.g. 'file.pdf#3')."""
    if not PROCESSED_LOG.exists():
        return set()
    with open(PROCESSED_LOG, encoding="utf-8") as f:
        return {line.strip() for line in f if line.strip()}


def mark_processed(page_id: str):
    with open(PROCESSED_LOG, "a", encoding="utf-8") as f:
        f.write(page_id + "\n")


# ============================================================
#  CORE OCR FUNCTION
# ============================================================
def ocr_one_page(client: anthropic.Anthropic, page: Page) -> DFFormResult:
    """Send one page (image or single PDF page) to Claude API."""
    result = DFFormResult(
        source_file=page.source_file,
        page_num=page.page_num,
        processed_at=datetime.now().isoformat(),
    )

    try:
        b64_data = base64.standard_b64encode(page.data).decode("utf-8")

        log.info(f"🔍 Processing: {page.page_id} ({page.content_type})")
        response = client.messages.create(
            model=MODEL,
            max_tokens=MAX_TOKENS,
            messages=[{
                "role": "user",
                "content": [
                    {
                        "type": page.content_type,   # 'image' or 'document'
                        "source": {
                            "type": "base64",
                            "media_type": page.media_type,
                            "data": b64_data,
                        },
                    },
                    {
                        "type": "text",
                        "text": EXTRACTION_PROMPT,
                    },
                ],
            }],
        )

        raw_text = response.content[0].text

        # Save raw response for debugging (filename-safe id)
        RAW_DIR.mkdir(exist_ok=True)
        safe_id = page.page_id.replace("/", "_").replace("#", "_p")
        raw_file = RAW_DIR / f"{safe_id}.json"
        with open(raw_file, "w", encoding="utf-8") as f:
            f.write(raw_text)

        # Parse JSON
        data = parse_response(raw_text)
        result.hn = data.get("hn", "")
        result.date = data.get("date", "")
        result.patient_age = data.get("patient_age", "")
        result.rights = data.get("rights", "")
        result.dx = data.get("dx", "")
        result.operation = data.get("operation", "")
        result.anesthesia_fee = data.get("anesthesia_fee", "")
        result.treatment_fee = data.get("treatment_fee", "")
        result.surgery_fee = data.get("surgery_fee", "")
        result.assistant_fee = data.get("assistant_fee", "")
        result.rw = data.get("rw", "")
        result.time = data.get("time", "")
        result.confidence_notes = data.get("confidence_notes", "")

        if result.hn and result.rw:
            result.status = "SUCCESS"
        elif result.hn or result.rw:
            result.status = "PARTIAL"
        else:
            result.status = "FAILED"
            result.error = "No HN or RW extracted"

        log.info(f"   ✅ HN={result.hn} RW={result.rw} status={result.status}")

    except json.JSONDecodeError as e:
        result.status = "FAILED"
        result.error = f"JSON parse error: {e}"
        log.error(f"   ❌ JSON parse failed for {page.page_id}: {e}")

    except anthropic.APIError as e:
        result.status = "FAILED"
        result.error = f"API error: {e}"
        log.error(f"   ❌ API error for {page.page_id}: {e}")

    except Exception as e:
        result.status = "FAILED"
        result.error = f"{type(e).__name__}: {e}"
        log.error(f"   ❌ Failed {page.page_id}: {e}")

    return result


# ============================================================
#  BATCH PROCESSING
# ============================================================
def process_batch(skip_processed: bool = True) -> list[DFFormResult]:
    if not INPUT_DIR.exists():
        raise FileNotFoundError(f"Input directory not found: {INPUT_DIR}")

    files = sorted([
        p for p in INPUT_DIR.iterdir()
        if p.is_file() and p.suffix.lower() in SUPPORTED_EXTS
    ])

    if not files:
        log.warning(f"No files found in {INPUT_DIR}")
        return []

    # Step 1: scan and split all files into pages
    log.info(f"📂 Scanning {len(files)} file(s) in {INPUT_DIR}/...")
    all_pages: list[Page] = []
    for f in files:
        try:
            pages = load_pages_from_file(f)
            all_pages.extend(pages)
        except Exception as e:
            log.error(f"   ❌ Failed to load {f.name}: {e}")

    # Step 2: filter out already-processed pages
    if skip_processed:
        processed = load_processed_pages()
        original_count = len(all_pages)
        all_pages = [p for p in all_pages if p.page_id not in processed]
        skipped = original_count - len(all_pages)
        if skipped > 0:
            log.info(f"⏭️  Skipping {skipped} already-processed pages")

    if not all_pages:
        log.info("Nothing to process")
        return []

    # Step 3: confirm cost
    cost_per = COST_PER_PAGE.get(MODEL, 0.005)
    est_cost = len(all_pages) * cost_per
    log.info(f"\n📊 Pages to process: {len(all_pages)}")
    log.info(f"🤖 Model: {MODEL}")
    log.info(f"💰 Estimated cost: ${est_cost:.2f} USD")

    if len(all_pages) > 50:
        confirm = input(f"\n⚠️  Processing {len(all_pages)} pages. Continue? (y/N): ")
        if confirm.lower() != "y":
            log.info("Cancelled by user")
            return []

    # Step 4: run OCR
    client = anthropic.Anthropic()
    results: list[DFFormResult] = []
    for i, page in enumerate(all_pages, 1):
        log.info(f"\n[{i}/{len(all_pages)}]")
        result = ocr_one_page(client, page)
        results.append(result)
        mark_processed(page.page_id)

    return results


def save_to_csv(results: list[DFFormResult], output_path: Path):
    if not results:
        log.warning("No results to save")
        return
    fieldnames = list(asdict(results[0]).keys())
    with open(output_path, "w", encoding="utf-8-sig", newline="") as f:
        writer = csv.DictWriter(f, fieldnames=fieldnames)
        writer.writeheader()
        for r in results:
            writer.writerow(asdict(r))
    log.info(f"💾 Saved {len(results)} rows to {output_path}")


def print_summary(results: list[DFFormResult]):
    total = len(results)
    success = sum(1 for r in results if r.status == "SUCCESS")
    partial = sum(1 for r in results if r.status == "PARTIAL")
    failed = sum(1 for r in results if r.status == "FAILED")
    cost_per = COST_PER_PAGE.get(MODEL, 0.005)
    actual_cost = total * cost_per
    source_files = len({r.source_file for r in results})

    print("\n" + "=" * 60)
    print("  BATCH OCR SUMMARY")
    print("=" * 60)
    print(f"  Source files:  {source_files}")
    print(f"  Pages total:   {total}")
    print(f"  ✅ Success:    {success}  ({success/max(total,1)*100:.0f}%)")
    print(f"  ⚠️  Partial:    {partial}  ({partial/max(total,1)*100:.0f}%)")
    print(f"  ❌ Failed:     {failed}  ({failed/max(total,1)*100:.0f}%)")
    print(f"  💰 Total cost: ${actual_cost:.2f} USD")
    print("=" * 60)


# ============================================================
#  TEST MODE — process all pages of 1 file
# ============================================================
def test_one_file(filename: str):
    file_path = INPUT_DIR / filename
    if not file_path.exists():
        print(f"❌ File not found: {file_path}")
        return

    pages = load_pages_from_file(file_path)
    print(f"📄 Loaded {len(pages)} page(s) from {filename}")

    client = anthropic.Anthropic()
    for page in pages:
        result = ocr_one_page(client, page)
        print(f"\n🧪 RESULT for {page.page_id}")
        print("-" * 50)
        for key, value in asdict(result).items():
            print(f"  {key:18}: {value}")
        print("-" * 50)


# ============================================================
#  MAIN
# ============================================================
def main():
    if not os.getenv("ANTHROPIC_API_KEY"):
        print("❌ ANTHROPIC_API_KEY not set. Add it to .env file.")
        return 1

    if len(sys.argv) > 1:
        if sys.argv[1] == "test" and len(sys.argv) > 2:
            test_one_file(sys.argv[2])
            return 0
        elif sys.argv[1] == "redo":
            results = process_batch(skip_processed=False)
        else:
            print("Usage:")
            print("  python df_ocr.py              # Process new pages only")
            print("  python df_ocr.py redo         # Re-process everything")
            print("  python df_ocr.py test <file>  # Test single file (all pages)")
            return 1
    else:
        results = process_batch(skip_processed=True)

    if results:
        save_to_csv(results, OUTPUT_CSV)
        print_summary(results)

    return 0


if __name__ == "__main__":
    sys.exit(main())