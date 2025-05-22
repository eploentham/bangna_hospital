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
from datetime import datetime
import easyocr

class FTPCertmedDetector:
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
        self.detector = LogoDetector(db_path)
        
        # Create results directory
        self.results_dir = os.path.join(os.path.dirname(os.path.dirname(os.path.abspath(__file__))), 'results')
        os.makedirs(self.results_dir, exist_ok=True)
        
        # Create debug directory if needed
        if debug:
            self.debug_dir = os.path.join(self.results_dir, 'debug')
            os.makedirs(self.debug_dir, exist_ok=True)
            
        # Create cert_med directory with date
        self.cert_med_dir = os.path.join(self.results_dir, 'cert_med', datetime.now().strftime('%Y_%m_%d'))
        os.makedirs(self.cert_med_dir, exist_ok=True)
    
    def log_same_line(self, message, level='info'):
        """
        Log a message without newline
        
        Args:
            message (str): Message to log
            level (str): Log level ('info', 'debug', 'warning', 'error')
        """
        if level == 'info':
            self.logger.info(message, extra={'end': ''})
        elif level == 'debug':
            self.logger.debug(message, extra={'end': ''})
        elif level == 'warning':
            self.logger.warning(message, extra={'end': ''})
        elif level == 'error':
            self.logger.error(message, extra={'end': ''})
    
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
    
    def read_qr_code_cv2(self, image):
        """Read QR code from image"""
        try:
            # Convert to grayscale
            gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
            
            # Apply thresholding to improve QR code detection
            _, thresh = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
            
            # Try to decode QR code
            try:
                decoded = decode(thresh)
                if decoded:
                    return decoded[0].data.decode('utf-8')
            except Exception as e:
                self.logger.warning(f"QR code decoding error: {str(e)}")
                # If first attempt fails, try with original grayscale image
                try:
                    decoded = decode(gray)
                    if decoded:
                        return decoded[0].data.decode('utf-8')
                except Exception as e:
                    self.logger.warning(f"Second QR code decoding attempt failed: {str(e)}")
            
            return None
        except Exception as e:
            self.logger.error(f"Failed to read QR code: {str(e)}")
            return None

    def read_qr_code_easyocr(self, image):
        """Read QR code from image using easyocr"""
        try:
            # Convert to grayscale
            gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
            
            # Apply thresholding
            _, thresh = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
            
            # Read text using easyocr
            reader = easyocr.Reader(['en'])
            result = reader.readtext(thresh)
            
            # Extract text from result
            if result:
                return result[0][1]  # Return the first detected text
            return None
        except Exception as e:
            self.logger.error(f"Failed to read QR code with easyocr: {str(e)}")
            return None

    def save_processed_image(self, image, filename, number=None, not_found=False):
        """
        Save processed image according to conditions
        
        Args:
            image: Image to save
            filename: Original filename
            number: Number from OCR (if available)
            not_found: Whether the image was not found in templates
        """
        try:
            # Get file extension
            _, ext = os.path.splitext(filename)
            
            # Create new filename based on conditions
            if number:
                self.logger.info(f"+++++++++Found number: {number}")
                new_filename = f"{number}{ext}"
            elif not_found:
                self.logger.info(f"---------Not found: {filename}")
                new_filename = f"{os.path.splitext(filename)[0]}_notfound{ext}"
            else:
                self.logger.info(f"**********Found number: {number}")
                new_filename = filename
                
            # Save image
            save_path = os.path.join(self.cert_med_dir, new_filename)
            cv2.imwrite(save_path, image)
            self.logger.info(f"Saved image to: {save_path}")
            
        except Exception as e:
            self.logger.error(f"Error saving image: {str(e)}")

    def read_number_easyocr(self, image):
        """Read number from image using easyocr"""
        try:
            # Convert to grayscale
            gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
            
            # Apply thresholding
            _, thresh = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
            
            # Read text using easyocr
            reader = easyocr.Reader(['th', 'en'], gpu=False)
            result = reader.readtext(thresh)
            
            # Extract text from result
            if result:
                # Get all detected text
                all_text = ' '.join([text[1] for text in result])
                # Try to find numbers in the text
                numbers = ''.join(filter(str.isdigit, all_text))
                if numbers:
                    return numbers
            return None
        except Exception as e:
            self.logger.error(f"Failed to read number with easyocr: {str(e)}")
            return None

    def process_image(self, image, filename):
        """Process a single image"""
        try:
            # Log image shape
            self.logger.info(f"Processing image: {filename}")
            self.logger.info(f"Image shape: {image.shape}")
            
            # Rotate image if height > width
            height, width = image.shape[:2]
            if height > width:
                self.logger.info("Rotating image 90 degrees")
                image90 = cv2.rotate(image, cv2.ROTATE_90_CLOCKWISE)
            
            # Try logo detection first
            self.log_same_line("Checking logo templates... ", level='info')
            detections = self.detector.detect(image, threshold=self.match_ratio)
            
            if detections:
                self.logger.info(f"Found {len(detections)} logos")
                #ใบรับรองแพทย certmed
                # Find number template location
                qr_data = self.read_qr_code_cv2(image)
                if qr_data:
                    self.logger.info(f"Found QR code if detections: {qr_data}")
                    # แยกตัวเลขตัวสุดท้ายจาก qr_data
                    last_number = qr_data.split()[-1] if qr_data else None
                    #อาจเป็นภาพ rotate
                    hei,wei, _ = image.shape    #ขนาดภาพ1700x2400
                    if(hei<1800):       #scan มาถูกต้อง หรือ ภาพกลับหัว
                        
                        self.save_processed_image(imagero180, filename, number=last_number)
                    else:   
                        imagero90 = cv2.rotate(image, cv2.ROTATE_90_CLOCKWISE)
                        self.save_processed_image(imagero90, filename, number=last_number)
                    return {
                        'filename': filename,
                        'qr_data': qr_data,
                        'logos': detections
                    }
                number_location = self.find_number_location(detections)
                if number_location:
                    # Extract number region
                    number_image = self.extract_number_region(image, number_location)
                    # Perform OCR
                    number = self.ocr_number_cv2(number_image)
                    if number:
                        self.logger.info(f"Found number: {number}")
                        # Save image with number as filename
                        self.logger.info(f"+++++++++Found number: {number}")
                        self.save_processed_image(image, filename, number=number)
                        return {
                            'filename': filename,
                            'qr_data': number,
                            'logos': detections
                        }
                    else:
                        # Save image with original filename
                        self.debug = True   #ให้เก็บ file ที่folder debug
                        #self.save_processed_image(image, filename)
                else:
                    # Save image with original filename
                    self.debug = True   #ให้เก็บ file ที่folder debug
                    #self.save_processed_image(image, filename)
            else:
                self.logger.info("///// No logos found /////")
                # Save image with _notfound suffix
                #self.save_processed_image(image, filename, not_found=True)
                self.debug = True   #ให้เก็บ file ที่folder debug
            
            # If no logos found or no number detected, try QR code
            self.log_same_line("No logos found, trying QR code... ", level='info')
            qr_data = self.read_qr_code_cv2(image)
            if qr_data:
                self.logger.info(f"Found QR code if qr_data: {qr_data}")
                last_number = qr_data.split()[-1] if qr_data else None
                self.save_processed_image(image, filename, number=last_number)
                return {
                    'filename': filename,
                    'qr_data': qr_data,
                    'logos': detections
                }
            self.logger.info("No QR code found")
            x1=2120
            y1=200
            x2=x1+150
            y2=y1+110
            hei,wei, _ = image.shape    #ขนาดภาพ1700x2400
            if(hei<1800):       #scan มาถูกต้อง หรือ ภาพกลับหัว
                
                number_image = image[y1:y2, x1:x2]
                number = self.ocr_number_cv2(number_image)
                if number:
                    self.logger.info(f"Found number if(hei<1800): {number}")
                    self.save_processed_image(image, filename, number=number)
                    return {
                        'filename': filename,
                        'qr_data': number,
                        'logos': detections
                    }
                else:
                    self.logger.info(f"Not found number if(hei<1800):") 
                    imagero180 = cv2.rotate(image, cv2.ROTATE_180)
                    number_image = imagero180[y1:y2, x1:x2]
                    number = self.ocr_number_cv2(number_image)
                    if number:
                        self.logger.info(f"Found number if(hei<1800) 180: {number}")
                        self.save_processed_image(imagero180, filename, number=number)   
                        return {
                            'filename': filename,
                            'qr_data': number,
                            'logos': detections
                        }
            elif(hei>1800):     #scan แนวตั้ง  หรือ ตั้งแบบกลับอีกด้าน
                imagero90 = cv2.rotate(image, cv2.ROTATE_90_CLOCKWISE)
                number_image = imagero90[y1:y2, x1:x2]
                number = self.ocr_number_cv2(number_image)
                if number:
                    self.logger.info(f"Found number if(hei>1800) 90 Clockwise: {number}")
                    self.save_processed_image(imagero90, filename, number=number)
                    return {
                        'filename': filename,
                        'qr_data': number,
                        'logos': detections
                    }
                else:
                    self.logger.info(f"Not found number if(hei>1800):")
                    imagero90 = cv2.rotate(image, cv2.ROTATE_90_COUNTERCLOCKWISE)
                    number_image = imagero90[y1:y2, x1:x2]
                    number = self.ocr_number_cv2(number_image)
                    if number:
                        self.logger.info(f"Found number if(hei>1800) 90 Counterclockwise: {number}")
                        self.save_processed_image(imagero90, filename, number=number)
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
        self.logger.info("****** Starting folder processing ******")
        try:
            ftp = self.connect_ftp()
            results = []
            
            # Get list of files
            files = ftp.nlst()
            image_files = [f for f in files if f.lower().endswith(('.jpg', '.jpeg', '.png'))]
            
            total_files = len(image_files)
            self.logger.info(f"Found {total_files} image files to process")
            
            for idx, filename in enumerate(image_files, 1):
                self.log_same_line(f"Processing {idx}/{total_files}: {filename}... ", level='info')
                
                # Download image to memory
                image = self.download_image_to_memory(ftp, filename)
                if image is None:
                    self.logger.info("Failed to download")
                    continue
                
                # Process image
                result = self.process_image(image, filename)
                results.append(result)
                self.logger.info("Done")
            
            ftp.quit()
            self.logger.info("****** Folder processing completed ******")
            return results
        except Exception as e:
            self.logger.error(f"Error processing folder: {str(e)}")
            return []
    
    def find_number_location(self, detections):
        """Find the location of the number template in detections"""
        for detection in detections:
            if detection['type'] == 'number':
                return detection['position']
        return None
    
    def extract_number_region(self, image, position):
        """Extract and resize the number region"""
        try:
            # Convert position points to numpy array
            pts = np.array(position, dtype=np.int32)
            
            # Get bounding rectangle
            x, y, w, h = cv2.boundingRect(pts)
            
            # Extract region
            number_region = image[y:y+h, x:x+w]
            
            # Resize to standard size
            return cv2.resize(number_region, (340, 62))
        except Exception as e:
            self.logger.error(f"Error extracting number region: {str(e)}")
            return None
    
    def ocr_number_cv2(self, image):
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