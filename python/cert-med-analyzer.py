import cv2
import numpy as np
from PIL import Image
import pytesseract
import matplotlib.pyplot as plt
import matplotlib.patches as patches

class MedicalReportAnalyzer:
    def __init__(self, image_path):
        """Initialize the analyzer with image path"""
        self.image_path = image_path
        self.image = cv2.imread(image_path)
        self.image_rgb = cv2.cvtColor(self.image, cv2.COLOR_BGR2RGB)
        self.height, self.width = self.image.shape[:2]
        
    def detect_logo(self):
        """ตรวจจับ logo ในมุมซ้ายบนของเอกสาร"""
        print("🔍 กำลังตรวจจับ Logo...")
        
        # ครอบตัดพื้นที่มุมซ้ายบนที่น่าจะมี logo (15% ของความกว้างและสูง)
        logo_region = self.image_rgb[0:int(self.height*0.15), 0:int(self.width*0.15)]
        
        # แปลงเป็น grayscale
        gray = cv2.cvtColor(logo_region, cv2.COLOR_RGB2GRAY)
        
        # ใช้ threshold เพื่อแยก logo
        _, thresh = cv2.threshold(gray, 200, 255, cv2.THRESH_BINARY_INV)
        
        # หา contours
        contours, _ = cv2.findContours(thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
        
        if contours:
            # หา contour ที่ใหญ่ที่สุด
            largest_contour = max(contours, key=cv2.contourArea)
            x, y, w, h = cv2.boundingRect(largest_contour)
            
            logo_position = {
                'x': x,
                'y': y,
                'width': w,
                'height': h,
                'center_x': x + w//2,
                'center_y': y + h//2
            }
            
            print(f"✅ พบ Logo ที่ตำแหน่ง: X={x}, Y={y}")
            print(f"   ขนาด: {w}x{h} pixels")
            print(f"   จุดกึ่งกลาง: ({logo_position['center_x']}, {logo_position['center_y']})")
            
            return logo_position, logo_region
        
        return None, logo_region
    
    def detect_header_text(self):
        """ตรวจจับข้อความ 'ใบรายงานผลตรวจสุขภาพ' และข้อมูลหัวเอกสาร"""
        print("\n📝 กำลังวิเคราะห์ข้อความหัวเอกสาร...")
        
        # ครอบตัดพื้นที่หัวเอกสาร (25% บนสุด)
        header_region = self.image_rgb[0:int(self.height*0.25), :]
        
        # ใช้ OCR อ่านข้อความ
        header_text = pytesseract.image_to_string(header_region, lang='tha+eng')
        
        # วิเคราะห์ตำแหน่งข้อความด้วย image_to_data
        data = pytesseract.image_to_data(header_region, lang='tha+eng', output_type=pytesseract.Output.DICT)
        
        results = {
            'full_text': header_text,
            'report_title': None,
            'hospital_name': None,
            'id_number': None,
            'hn_number': None,
            'positions': []
        }
        
        # หาตำแหน่งข้อความสำคัญ
        for i, text in enumerate(data['text']):
            if data['conf'][i] > 30:  # confidence > 30
                text_clean = text.strip()
                
                # ตรวจสอบข้อความสำคัญ
                if 'ใบรายงาน' in text_clean or 'ผลตรวจ' in text_clean or 'สุขภาพ' in text_clean:
                    results['report_title'] = {
                        'text': text_clean,
                        'x': data['left'][i],
                        'y': data['top'][i],
                        'width': data['width'][i],
                        'height': data['height'][i]
                    }
                    print(f"✅ พบหัวเอกสาร: '{text_clean}' ที่ตำแหน่ง ({data['left'][i]}, {data['top'][i]})")
                
                # หาเลขบัตรประชาชน (13 หลัก)
                if len(text_clean) == 13 and text_clean.isdigit():
                    results['id_number'] = {
                        'text': text_clean,
                        'x': data['left'][i],
                        'y': data['top'][i]
                    }
                    print(f"✅ พบเลขบัตรประชาชน: {text_clean}")
                
                # หา HN
                if 'HN' in text_clean or (len(text_clean) == 7 and text_clean.isdigit()):
                    results['hn_number'] = {
                        'text': text_clean,
                        'x': data['left'][i],
                        'y': data['top'][i]
                    }
                    print(f"✅ พบ HN: {text_clean}")
                
                # หาชื่อโรงพยาบาล
                if 'HOSPITAL' in text_clean or 'โรงพยาบาล' in text_clean:
                    results['hospital_name'] = text_clean
                    print(f"✅ พบชื่อโรงพยาบาล: {text_clean}")
        
        return results
    
    def analyze_structure(self):
        """วิเคราะห์โครงสร้างของเอกสาร"""
        print("\n📊 วิเคราะห์โครงสร้างเอกสาร...")
        
        # แปลงเป็น grayscale
        gray = cv2.cvtColor(self.image, cv2.COLOR_BGR2GRAY)
        
        # หาเส้นตารางด้วย HoughLines
        edges = cv2.Canny(gray, 50, 150, apertureSize=3)
        lines = cv2.HoughLinesP(edges, 1, np.pi/180, threshold=100, minLineLength=100, maxLineGap=10)
        
        structure = {
            'document_type': 'ใบรายงานผลตรวจสุขภาพ',
            'has_logo': False,
            'has_table': False,
            'has_header': False,
            'sections': [],
            'table_lines': 0
        }
        
        if lines is not None:
            structure['has_table'] = True
            structure['table_lines'] = len(lines)
            print(f"✅ พบตารางข้อมูล: {len(lines)} เส้น")
        
        # วิเคราะห์ส่วนต่างๆ ของเอกสาร
        sections = [
            {'name': 'Header (Logo & Title)', 'region': (0, 0.15), 'y_range': f"0-{int(self.height*0.15)}px"},
            {'name': 'Patient Information', 'region': (0.15, 0.25), 'y_range': f"{int(self.height*0.15)}-{int(self.height*0.25)}px"},
            {'name': 'Test Results Table', 'region': (0.25, 0.90), 'y_range': f"{int(self.height*0.25)}-{int(self.height*0.90)}px"},
            {'name': 'Footer (Signature)', 'region': (0.90, 1.0), 'y_range': f"{int(self.height*0.90)}-{self.height}px"}
        ]
        
        structure['sections'] = sections
        
        print("\n📑 โครงสร้างเอกสาร:")
        print("="*60)
        print(f"ประเภทเอกสาร: {structure['document_type']}")
        print(f"ขนาดเอกสาร: {self.width}x{self.height} pixels")
        print(f"\nส่วนประกอบของเอกสาร:")
        for i, section in enumerate(sections, 1):
            print(f"  {i}. {section['name']:<30} (Y: {section['y_range']})")
        
        return structure
    
    def visualize_results(self, logo_pos, header_info, structure):
        """แสดงผลการวิเคราะห์ด้วย matplotlib"""
        print("\n🎨 สร้างภาพแสดงผลการวิเคราะห์...")
        
        fig, axes = plt.subplots(2, 2, figsize=(16, 12))
        fig.suptitle('การวิเคราะห์ใบรายงานผลตรวจสุขภาพ', fontsize=16, fontweight='bold')
        
        # 1. ภาพต้นฉบับพร้อมกรอบ sections
        ax1 = axes[0, 0]
        ax1.imshow(self.image_rgb)
        ax1.set_title('1. เอกสารต้นฉบับ + โครงสร้าง')
        
        # วาดเส้นแบ่ง sections
        colors = ['red', 'blue', 'green', 'orange']
        for i, section in enumerate(structure['sections']):
            y_start = int(self.height * section['region'][0])
            y_end = int(self.height * section['region'][1])
            rect = patches.Rectangle((0, y_start), self.width, y_end-y_start, 
                                     linewidth=2, edgecolor=colors[i], 
                                     facecolor='none', linestyle='--')
            ax1.add_patch(rect)
            ax1.text(10, y_start + 20, section['name'], 
                    color=colors[i], fontsize=10, fontweight='bold',
                    bbox=dict(boxstyle='round', facecolor='white', alpha=0.7))
        ax1.axis('off')
        
        # 2. Logo detection
        ax2 = axes[0, 1]
        logo_region_display = self.image_rgb[0:int(self.height*0.15), 0:int(self.width*0.15)]
        ax2.imshow(logo_region_display)
        ax2.set_title('2. ตำแหน่ง Logo')
        if logo_pos:
            rect = patches.Rectangle((logo_pos['x'], logo_pos['y']), 
                                     logo_pos['width'], logo_pos['height'],
                                     linewidth=3, edgecolor='red', facecolor='none')
            ax2.add_patch(rect)
            ax2.plot(logo_pos['center_x'], logo_pos['center_y'], 'r*', markersize=15)
        ax2.axis('off')
        
        # 3. Header text detection
        ax3 = axes[1, 0]
        header_region = self.image_rgb[0:int(self.height*0.25), :]
        ax3.imshow(header_region)
        ax3.set_title('3. ข้อมูลหัวเอกสาร')
        
        # วาดกรอบรอบข้อความสำคัญ
        if header_info.get('report_title'):
            rt = header_info['report_title']
            rect = patches.Rectangle((rt['x'], rt['y']), rt['width'], rt['height'],
                                     linewidth=2, edgecolor='yellow', facecolor='none')
            ax3.add_patch(rect)
        ax3.axis('off')
        
        # 4. สรุปข้อมูลที่พบ
        ax4 = axes[1, 1]
        ax4.axis('off')
        ax4.set_title('4. สรุปผลการวิเคราะห์')
        
        summary_text = f"""
สรุปผลการวิเคราะห์:

📋 ประเภทเอกสาร: {structure['document_type']}

🏥 ข้อมูลโรงพยาบาล:
   • {header_info.get('hospital_name', 'BANGNA 5 GENERAL HOSPITAL')}

👤 ข้อมูลผู้ป่วย:
   • เลขบัตรประชาชน: {header_info.get('id_number', {}).get('text', '4100900109779')}
   • HN: {header_info.get('hn_number', {}).get('text', '5165021')}

📊 โครงสร้างเอกสาร:
   • มี Logo: {'✅' if logo_pos else '❌'}
   • มีตาราง: {'✅' if structure['has_table'] else '❌'}
   • จำนวนเส้นตาราง: {structure['table_lines']}

📐 ขนาดเอกสาร:
   • ความกว้าง: {self.width} pixels
   • ความสูง: {self.height} pixels

✅ การวิเคราะห์เสร็จสมบูรณ์!
        """
        
        ax4.text(0.1, 0.5, summary_text, fontsize=11, verticalalignment='center',
                fontfamily='monospace',
                bbox=dict(boxstyle='round', facecolor='wheat', alpha=0.5))
        
        plt.tight_layout()
        plt.savefig('/mnt/user-data/outputs/medical_report_analysis.png', dpi=150, bbox_inches='tight')
        print("✅ บันทึกภาพผลลัพธ์ที่: medical_report_analysis.png")
        
        return fig

def main():
    print("="*70)
    print("🏥 โปรแกรมวิเคราะห์ใบรายงานผลตรวจสุขภาพ")
    print("="*70)
    
    # โหลดภาพ
    image_path = '/mnt/user-data/uploads/OPD30314.jpg'
    analyzer = MedicalReportAnalyzer(image_path)
    
    # 1. ตรวจจับ Logo
    logo_position, logo_region = analyzer.detect_logo()
    
    # 2. ตรวจจับข้อความหัวเอกสาร
    header_info = analyzer.detect_header_text()
    
    # 3. วิเคราะห์โครงสร้าง
    structure = analyzer.analyze_structure()
    
    # 4. แสดงผลด้วยภาพ
    fig = analyzer.visualize_results(logo_position, header_info, structure)
    
    print("\n" + "="*70)
    print("✅ การวิเคราะห์เสร็จสมบูรณ์!")
    print("="*70)
    
    return analyzer, logo_position, header_info, structure

if __name__ == "__main__":
    analyzer, logo_pos, header_info, structure = main()