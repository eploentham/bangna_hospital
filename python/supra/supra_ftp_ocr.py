from io import BytesIO
from typing import Optional
try:
    import pdfplumber
except ImportError:
    print("กรุณาติดตั้ง: pip install pdfplumber --break-system-packages")

try:
    from PIL import Image
except ImportError:
    print("กรุณาติดตั้ง: pip install pillow --break-system-packages")

class OCRMixin:
    def extract_text_from_pdf_stream(self, stream: BytesIO) -> str:
        """
        แยกข้อความจาก PDF stream
        
        Args:
            stream: BytesIO object ที่มี PDF data
            
        Returns:
            ข้อความที่แยกได้
        """
        text = ""
        try:
            # pdfplumber สามารถอ่านจาก BytesIO ได้โดยตรง
            with pdfplumber.open(stream) as pdf:
                print(f"PDF มี {len(pdf.pages)} หน้า")
                
                for page_num, page in enumerate(pdf.pages, 1):
                    page_text = page.extract_text()
                    if page_text:
                        text += f"\n{'='*60}\n"
                        text += f"หน้า {page_num}\n"
                        text += '='*60 + '\n'
                        text += page_text
            
            print(f"✓ แยกข้อความจาก PDF: {len(text)} ตัวอักษร")
            
        except Exception as e:
            print(f"✗ ไม่สามารถแยกข้อความจาก PDF: {e}")
        
        return text
    def extract_text_from_image_stream(self, stream: BytesIO) -> str:
        """
        แยกข้อความจาก image stream ด้วย OCR
        
        Args:
            stream: BytesIO object ที่มี image data
            
        Returns:
            ข้อความที่แยกได้
        """
        text = ""
        try:
            # PIL สามารถอ่านจาก BytesIO ได้โดยตรง
            stream.seek(0)
            image = Image.open(stream)
            
            print(f"Image: {image.size[0]} x {image.size[1]} pixels, {image.mode}")
            print("กำลัง OCR...")
            
            # EasyOCR รับ PIL Image ได้โดยตรง
            results = self.reader.readtext(image)
            
            # รวมข้อความ
            text_lines = [result[1] for result in results]
            text = "\n".join(text_lines)
            
            print(f"✓ OCR เสร็จสิ้น: {len(results)} บรรทัด, {len(text)} ตัวอักษร")
            
        except Exception as e:
            print(f"✗ ไม่สามารถ OCR image: {e}")
        
        return text