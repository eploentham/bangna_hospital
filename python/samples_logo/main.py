from src.ftp_logo_detector import FTPLogoDetector
import logging
import pytesseract
import os

# Setup logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

# กำหนด path สำหรับ Tesseract (สำหรับ Windows)
if os.name == 'nt':  # Windows
    pytesseract.pytesseract.tesseract_cmd = r'C:\Tesseract-OCR\tesseract.exe'

def main():
    """ฟังก์ชันหลักสำหรับการตรวจจับโลโก้และ OCR"""
    try:
        # FTP configuration
        ftp_host = '172.25.10.3'
        ftp_user = 'u_cert_med'  # เปลี่ยนเป็น username จริง
        ftp_pass = 'u_cert_med'  # เปลี่ยนเป็น password จริง
        ftp_folder = 'cert_med'
        
        # Initialize detector
        detector = FTPLogoDetector(
            ftp_host=ftp_host,
            ftp_user=ftp_user,
            ftp_pass=ftp_pass,
            ftp_folder=ftp_folder,
            database_path='logo_database_bangna5.json'
        )
        
        # Process folder
        results = detector.process_folder()
        
        if results:
            # Print results
            for result in results:
                print(f"\nFile: {result['filename']}")
                if result['qr_data']:
                    print(f"QR Code/Number: {result['qr_data']}")
                if result['detections']:
                    print("Detected Logos:")
                    for detection in result['detections']:
                        print(f"- {detection['filename']} (Confidence: {detection['confidence']:.2f})")
        else:
            logger.error("No results found or error processing folder")
            
    except Exception as e:
        logger.error(f"Error in main: {str(e)}")

if __name__ == "__main__":
    main()
