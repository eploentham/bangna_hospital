import os
import cv2
import json
import numpy as np
import logging
from ftplib import FTP
from io import BytesIO
from pyzbar.pyzbar import decode
import pytesseract
from logo_detector import LogoDetector

class FTPLogoDetector:
    def __init__(self, ftp_host, ftp_user, ftp_pass, ftp_folder, db_path, 
                 min_matches=10, match_ratio=0.7, debug=False):
        self.ftp_host = ftp_host
        self.ftp_user = ftp_user
        self.ftp_pass = ftp_pass
        self.ftp_folder = ftp_folder
        self.db_path = db_path
        self.min_matches = min_matches
        self.match_ratio = match_ratio
        self.debug = debug
        
        # Setup logging
        logging.basicConfig(level=logging.INFO if not debug else logging.DEBUG,
                          format='%(asctime)s - %(levelname)s - %(message)s')
        self.logger = logging.getLogger(__name__)
        
        # Initialize logo detector
        self.detector = LogoDetector(db_path, min_matches, match_ratio)
        
        # Create results directory
        self.results_dir = os.path.join(os.path.dirname(os.path.dirname(os.path.abspath(__file__))), 'results')
        os.makedirs(self.results_dir, exist_ok=True)
        
        # Create debug directory if needed
        if debug:
            self.debug_dir = os.path.join(self.results_dir, 'debug')
            os.makedirs(self.debug_dir, exist_ok=True)
    
    def connect_ftp(self):
        """Connect to FTP server"""
        try:
            ftp = FTP(self.ftp_host)
            ftp.login(self.ftp_user, self.ftp_pass)
            ftp.cwd(self.ftp_folder)
            self.logger.info(f"Connected to FTP server: {self.ftp_host}")
            return ftp
        except Exception as e:
            self.logger.error(f"Failed to connect to FTP: {str(e)}")
            raise
    
    def download_image_to_memory(self, ftp, filename):
        """Download image from FTP to memory"""
        try:
            # Create BytesIO object to store image data
            image_data = BytesIO()
            
            # Download file to memory
            ftp.retrbinary(f'RETR {filename}', image_data.write)
            image_data.seek(0)
            
            # Convert to numpy array
            image_array = np.frombuffer(image_data.getvalue(), dtype=np.uint8)
            image = cv2.imdecode(image_array, cv2.IMREAD_COLOR)
            
            self.logger.info(f"Downloaded {filename} to memory")
            return image
        except Exception as e:
            self.logger.error(f"Failed to download {filename}: {str(e)}")
            return None
    
    def read_qr_code(self, image):
        """Read QR code from image"""
        try:
            # Convert to grayscale
            gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
            
            # Decode QR code
            decoded = decode(gray)
            if decoded:
                return decoded[0].data.decode('utf-8')
            return None
        except Exception as e:
            self.logger.error(f"Failed to read QR code: {str(e)}")
            return None
    
    def process_image(self, image, filename):
        """Process a single image"""
        try:
            # Read QR code first
            qr_data = self.read_qr_code(image)
            if qr_data:
                self.logger.info(f"Found QR code in {filename}: {qr_data}")
                return {
                    'filename': filename,
                    'qr_data': qr_data,
                    'logos': []
                }
            
            # If no QR code, try logo detection
            detections = self.detector.detect_logos(image)
            
            # Find number template location
            number_location = self.find_number_location(detections)
            if number_location:
                # Extract number region
                number_image = self.extract_number_region(image, number_location)
                # Perform OCR
                number = self.ocr_number(number_image)
                if number:
                    self.logger.info(f"Found number in {filename}: {number}")
                    return {
                        'filename': filename,
                        'qr_data': number,
                        'logos': detections
                    }
            
            # Save debug image if needed
            if self.debug:
                debug_path = os.path.join(self.debug_dir, f"debug_{filename}")
                cv2.imwrite(debug_path, image)
            
            return {
                'filename': filename,
                'qr_data': None,
                'logos': detections
            }
        except Exception as e:
            self.logger.error(f"Error processing {filename}: {str(e)}")
            return {
                'filename': filename,
                'qr_data': None,
                'logos': []
            }
    
    def process_folder(self):
        """Process all images in FTP folder"""
        try:
            ftp = self.connect_ftp()
            results = []
            
            # Get list of files
            files = ftp.nlst()
            image_files = [f for f in files if f.lower().endswith(('.jpg', '.jpeg', '.png'))]
            
            for filename in image_files:
                self.logger.info(f"Processing {filename}")
                
                # Download image to memory
                image = self.download_image_to_memory(ftp, filename)
                if image is None:
                    continue
                
                # Process image
                result = self.process_image(image, filename)
                results.append(result)
            
            ftp.quit()
            return results
        except Exception as e:
            self.logger.error(f"Error processing folder: {str(e)}")
            return []
    
    def find_number_location(self, detections):
        """Find the location of the number template in detections"""
        for detection in detections:
            if detection['name'] == 'number':
                return detection['location']
        return None
    
    def extract_number_region(self, image, location):
        """Extract and resize the number region"""
        x, y, w, h = location
        number_region = image[y:y+h, x:x+w]
        return cv2.resize(number_region, (340, 62))
    
    def ocr_number(self, image):
        """Perform OCR on the number region"""
        try:
            # Convert to grayscale
            gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
            
            # Apply thresholding
            _, thresh = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
            
            # Perform OCR
            number = pytesseract.image_to_string(thresh, config='--psm 6 digits')
            return number.strip()
        except Exception as e:
            self.logger.error(f"OCR failed: {str(e)}")
            return None 