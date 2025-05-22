import os
import cv2
import numpy as np
from ftplib import FTP
from pyzbar.pyzbar import decode
import tempfile
from datetime import datetime
import logging
from src.logo_detector import LogoDetector

# Setup logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

class FTPLogoDetector:
    def __init__(self, ftp_host, ftp_user, ftp_pass, ftp_folder, database_path):
        """
        Initialize FTP Logo Detector
        
        Args:
            ftp_host: FTP server host
            ftp_user: FTP username
            ftp_pass: FTP password
            ftp_folder: Folder path on FTP server
            database_path: Path to logo database
        """
        self.ftp_host = ftp_host
        self.ftp_user = ftp_user
        self.ftp_pass = ftp_pass
        self.ftp_folder = ftp_folder
        self.logo_detector = LogoDetector(database_path)
        self.temp_dir = tempfile.mkdtemp()
        
    def connect_ftp(self):
        """Connect to FTP server"""
        logger.info(f"Connecting to FTP server: {self.ftp_host}")
        try:
            ftp = FTP(self.ftp_host)
            ftp.login(self.ftp_user, self.ftp_pass)
            ftp.cwd(self.ftp_folder)
            return ftp
        except Exception as e:
            logger.error(f"FTP connection error: {str(e)}")
            return None
            
    def download_image(self, ftp, filename):
        """Download image from FTP to temporary directory"""
        try:
            local_path = os.path.join(self.temp_dir, filename)
            with open(local_path, 'wb') as f:
                ftp.retrbinary(f'RETR {filename}', f.write)
            return local_path
        except Exception as e:
            logger.error(f"Error downloading {filename}: {str(e)}")
            return None
            
    def read_qr_code(self, image):
        """Read QR code from image"""
        try:
            # Convert to grayscale
            gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
            
            # Decode QR code
            decoded_objects = decode(gray)
            
            if decoded_objects:
                return decoded_objects[0].data.decode('utf-8')
            return None
            
        except Exception as e:
            logger.error(f"Error reading QR code: {str(e)}")
            return None
            
    def process_image(self, image_path):
        """Process single image"""
        try:
            # Read image
            image = cv2.imread(image_path)
            if image is None:
                logger.error(f"Could not read image: {image_path}")
                return None
                
            # Detect logos
            detections = self.logo_detector.detect(image)
            
            # Read QR code
            qr_data = self.read_qr_code(image)
            
            # Draw detections
            result = self.logo_detector.draw_detections(image, detections)
            
            return {
                'detections': detections,
                'qr_data': qr_data,
                'result_image': result
            }
            
        except Exception as e:
            logger.error(f"Error processing image: {str(e)}")
            return None
            
    def process_folder(self):
        """Process all images in FTP folder"""
        logger.info("Starting folder processing")
        try:
            # Connect to FTP
            ftp = self.connect_ftp()
            if ftp is None:
                return
                
            # Get list of files
            files = ftp.nlst()
            jpg_files = [f for f in files if f.lower().endswith('.jpg')]
            
            results = []
            for filename in jpg_files:
                logger.info(f"Processing {filename}")
                
                # Download image
                local_path = self.download_image(ftp, filename)
                if local_path is None:
                    continue
                    
                # Process image
                result = self.process_image(local_path)
                if result is not None:
                    results.append({
                        'filename': filename,
                        'detections': result['detections'],
                        'qr_data': result['qr_data']
                    })
                    
                    # Save result image
                    output_path = os.path.join('results', f'result_{filename}')
                    os.makedirs('results', exist_ok=True)
                    cv2.imwrite(output_path, result['result_image'])
                    
                # Clean up
                os.remove(local_path)
                
            # Close FTP connection
            ftp.quit()
            
            return results
            
        except Exception as e:
            logger.error(f"Error processing folder: {str(e)}")
            return None
            
def main():
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
                print(f"QR Code: {result['qr_data']}")
            if result['detections']:
                print("Detected Logos:")
                for detection in result['detections']:
                    print(f"- {detection['filename']} (Confidence: {detection['confidence']:.2f})")
                    
if __name__ == "__main__":
    main() 