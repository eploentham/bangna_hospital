import cv2
import os
import json
from datetime import datetime
import logging
from src.config import HOSPITAL_CONFIGS, SAMPLE_PARAMS
import numpy as np

logger = logging.getLogger(__name__)

class LogoSampleManager:
    def __init__(self, hospital_id='bangna5'):
        config = HOSPITAL_CONFIGS[hospital_id]
        self.hospital_id = hospital_id
        self.hospital_name = config['name']
        self.sample_dirs = config['sample_dirs']
        self.templates = config['templates']
        self.database_path = config['database_path']
        self.create_directories()
        
    def create_directories(self):
        """สร้างโฟลเดอร์ทั้งหมดที่จำเป็น"""
        for dir_path in self.sample_dirs.values():
            os.makedirs(dir_path, exist_ok=True)
            print(f"Created directory: {dir_path}")

    def prepare_base_samples(self, logo_path):
        """เตรียมตัวอย่างโลโก้พื้นฐาน"""
        try:
            base_samples = {
                'original': cv2.imread(logo_path),
                'grayscale': cv2.imread(logo_path, 0),
                'binary': cv2.threshold(
                    cv2.imread(logo_path, 0), 
                    128, 255, 
                    cv2.THRESH_BINARY
                )[1]
            }
            
            # บันทึกตัวอย่างพื้นฐาน
            for sample_type, img in base_samples.items():
                output_path = os.path.join(
                    self.sample_dirs['original'], 
                    f'logo_{sample_type}.jpg'
                )
                cv2.imwrite(output_path, img)
                print(f"Saved {sample_type} sample to: {output_path}")
                
            return base_samples
            
        except Exception as e:
            print(f"Error preparing base samples: {str(e)}")
            return None

    def create_rotated_samples(self, logo_img):
        """สร้างตัวอย่างโลโก้ในมุมต่างๆ"""
        try:
            params = SAMPLE_PARAMS['rotation']
            for angle in range(params['min_angle'], 
                             params['max_angle'] + 1, 
                             params['step']):
                height, width = logo_img.shape[:2]
                center = (width/2, height/2)
                rotation_matrix = cv2.getRotationMatrix2D(center, angle, 1.0)
                rotated = cv2.warpAffine(logo_img, rotation_matrix, (width, height))
                
                output_path = os.path.join(
                    self.sample_dirs['rotated'], 
                    f'logo_rotated_{angle}.jpg'
                )
                cv2.imwrite(output_path, rotated)
                logger.info(f"Saved rotated sample ({angle}°) to: {output_path}")
                
        except Exception as e:
            logger.error(f"Error creating rotated samples: {str(e)}")

    def create_brightness_samples(self, logo_img):
        """สร้างตัวอย่างโลโก้ที่มีความสว่างต่างกัน"""
        try:
            params = SAMPLE_PARAMS['brightness']
            for beta in range(params['min_beta'], 
                            params['max_beta'] + 1, 
                            params['step']):
                adjusted = cv2.convertScaleAbs(logo_img, alpha=1, beta=beta)
                
                output_path = os.path.join(
                    self.sample_dirs['brightness'], 
                    f'logo_brightness_{beta}.jpg'
                )
                cv2.imwrite(output_path, adjusted)
                logger.info(f"Saved brightness sample ({beta}) to: {output_path}")
                
        except Exception as e:
            logger.error(f"Error creating brightness samples: {str(e)}")

    def create_scaled_samples(self, logo_img):
        """สร้างตัวอย่างโลโก้ที่มีขนาดต่างกัน"""
        try:
            params = SAMPLE_PARAMS['scale']
            for scale in range(params['min_scale'], 
                             params['max_scale'] + 1, 
                             params['step']):
                scale_factor = scale / 100.0
                width = int(logo_img.shape[1] * scale_factor)
                height = int(logo_img.shape[0] * scale_factor)
                scaled = cv2.resize(logo_img, (width, height))
                
                output_path = os.path.join(
                    self.sample_dirs['scaled'], 
                    f'logo_scaled_{scale}.jpg'
                )
                cv2.imwrite(output_path, scaled)
                logger.info(f"Saved scaled sample ({scale}%) to: {output_path}")
                
        except Exception as e:
            logger.error(f"Error creating scaled samples: {str(e)}")

    def create_blurred_samples(self, logo_img):
        """สร้างตัวอย่างโลโก้ที่มีการเบลอ"""
        try:
            params = SAMPLE_PARAMS['blur']
            for kernel_size in range(params['min_kernel'], 
                                  params['max_kernel'] + 1, 
                                  params['step']):
                # สร้าง kernel size เป็นเลขคี่
                ksize = kernel_size * 2 + 1
                blurred = cv2.GaussianBlur(logo_img, (ksize, ksize), 0)
                
                output_path = os.path.join(
                    self.sample_dirs['blurred'], 
                    f'logo_blurred_{ksize}.jpg'
                )
                cv2.imwrite(output_path, blurred)
                logger.info(f"Saved blurred sample (kernel={ksize}) to: {output_path}")
                
        except Exception as e:
            logger.error(f"Error creating blurred samples: {str(e)}")

    def create_noise_samples(self, logo_img):
        """สร้างตัวอย่างโลโก้ที่มีสัญญาณรบกวน"""
        try:
            params = SAMPLE_PARAMS['noise']
            for sigma in range(params['min_sigma'], 
                            params['max_sigma'] + 1, 
                            params['step']):
                # สร้างสัญญาณรบกวนแบบ Gaussian
                noise = np.random.normal(0, sigma, logo_img.shape).astype(np.uint8)
                noisy = cv2.add(logo_img, noise)
                
                output_path = os.path.join(
                    self.sample_dirs['noise'], 
                    f'logo_noise_{sigma}.jpg'
                )
                cv2.imwrite(output_path, noisy)
                logger.info(f"Saved noise sample (sigma={sigma}) to: {output_path}")
                
        except Exception as e:
            logger.error(f"Error creating noise samples: {str(e)}")

    def create_contrast_samples(self, logo_img):
        """สร้างตัวอย่างโลโก้ที่มีความคมชัดต่างกัน"""
        try:
            params = SAMPLE_PARAMS['contrast']
            for alpha in np.arange(params['min_alpha'], 
                                params['max_alpha'] + params['step'], 
                                params['step']):
                # ปรับความคมชัด
                adjusted = cv2.convertScaleAbs(logo_img, alpha=alpha, beta=0)
                
                output_path = os.path.join(
                    self.sample_dirs['contrast'], 
                    f'logo_contrast_{alpha:.1f}.jpg'
                )
                cv2.imwrite(output_path, adjusted)
                logger.info(f"Saved contrast sample (alpha={alpha:.1f}) to: {output_path}")
                
        except Exception as e:
            logger.error(f"Error creating contrast samples: {str(e)}")

    def create_perspective_samples(self, logo_img):
        """สร้างตัวอย่างโลโก้ที่มีมุมมองต่างกัน"""
        try:
            params = SAMPLE_PARAMS['perspective']
            height, width = logo_img.shape[:2]
            
            for angle in range(params['min_angle'], 
                            params['max_angle'] + 1, 
                            params['step']):
                # คำนวณจุดมุมสำหรับการแปลง perspective
                pts1 = np.float32([[0, 0], [width, 0], [0, height], [width, height]])
                pts2 = np.float32([
                    [0, 0],
                    [width, 0],
                    [width * np.sin(np.radians(angle)), height],
                    [width, height]
                ])
                
                # สร้างเมทริกซ์การแปลง
                M = cv2.getPerspectiveTransform(pts1, pts2)
                perspective = cv2.warpPerspective(logo_img, M, (width, height))
                
                output_path = os.path.join(
                    self.sample_dirs['perspective'], 
                    f'logo_perspective_{angle}.jpg'
                )
                cv2.imwrite(output_path, perspective)
                logger.info(f"Saved perspective sample ({angle}°) to: {output_path}")
                
        except Exception as e:
            logger.error(f"Error creating perspective samples: {str(e)}")

    def create_number_samples(self, logo_img):
        """สร้างตัวอย่างสำหรับการตรวจจับตัวเลข"""
        try:
            # แปลงเป็นภาพขาวดำ
            gray = cv2.cvtColor(logo_img, cv2.COLOR_BGR2GRAY)
            
            # ทำ threshold เพื่อแยกตัวเลขออกจากพื้นหลัง
            _, binary = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
            
            # หา contours
            contours, _ = cv2.findContours(binary, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
            
            params = SAMPLE_PARAMS['number_detection']
            
            # กรอง contours ตามขนาด
            valid_contours = []
            for cnt in contours:
                area = cv2.contourArea(cnt)
                if params['min_contour_area'] <= area <= params['max_contour_area']:
                    valid_contours.append(cnt)
            
            # วาด contours ที่ผ่านการกรอง
            result = logo_img.copy()
            cv2.drawContours(result, valid_contours, -1, (0, 255, 0), 2)
            
            # บันทึกผลลัพธ์
            output_path = os.path.join(
                self.sample_dirs['numbers'],
                'number_detection.jpg'
            )
            cv2.imwrite(output_path, result)
            logger.info(f"Saved number detection result to: {output_path}")
            
            # สร้างตัวอย่างแต่ละตัวเลข
            for i, cnt in enumerate(valid_contours):
                x, y, w, h = cv2.boundingRect(cnt)
                number = binary[y:y+h, x:x+w]
                
                # บันทึกตัวเลขแต่ละตัว
                number_path = os.path.join(
                    self.sample_dirs['numbers'],
                    f'number_{i+1}.jpg'
                )
                cv2.imwrite(number_path, number)
                logger.info(f"Saved individual number {i+1} to: {number_path}")
                
        except Exception as e:
            logger.error(f"Error creating number samples: {str(e)}")

    def create_database(self):
        """สร้างฐานข้อมูลของตัวอย่างทั้งหมด"""
        try:
            database = {
                'metadata': {
                    'creation_date': datetime.now().strftime("%Y-%m-%d"),
                    'version': '1.0',
                    'hospital_name': self.hospital_name
                },
                'samples': []
            }

            # เพิ่มตัวอย่างทั้งหมดลงในฐานข้อมูล
            for root, _, files in os.walk('samples/'):
                for file in files:
                    if file.endswith('.jpg'):
                        sample_path = os.path.join(root, file)
                        sample_type = root.split('/')[-1]
                        
                        img = cv2.imread(sample_path)
                        sift = cv2.SIFT_create()
                        keypoints, descriptors = sift.detectAndCompute(img, None)
                        
                        # แปลง keypoints เป็น list ที่สามารถ serialize ได้
                        keypoints_list = []
                        for kp in keypoints:
                            keypoints_list.append({
                                'pt': kp.pt,
                                'size': kp.size,
                                'angle': kp.angle,
                                'response': kp.response,
                                'octave': kp.octave,
                                'class_id': kp.class_id
                            })
                        
                        database['samples'].append({
                            'filename': file,
                            'type': sample_type,
                            'path': sample_path,
                            'keypoints': keypoints_list,
                            'descriptors': descriptors.tolist() if descriptors is not None else None,
                            'shape': img.shape
                        })

            # บันทึกฐานข้อมูล
            with open(self.database_path, 'w') as f:
                json.dump(database, f)
            logger.info(f"Created logo database: {self.database_path}")
            
        except Exception as e:
            logger.error(f"Error creating database: {str(e)}")

    def prepare_all_samples(self):
        """เตรียมตัวอย่างโลโก้ทั้งหมด"""
        try:
            # เตรียมตัวอย่างพื้นฐานจากทุก template
            for template_name, template_path in self.templates.items():
                logger.info(f"Processing template: {template_name}")
                original_logo = self.prepare_base_samples(template_path)
                
                if original_logo is not None:
                    if 'number_' in template_name:  # เปลี่ยนเงื่อนไขให้รองรับทุก template ที่ขึ้นต้นด้วย number_
                        # ถ้าเป็น template ตัวเลข ให้ใช้เมธอดสำหรับตรวจจับตัวเลข
                        self.create_number_samples(original_logo['original'])
                    else:
                        # สร้างตัวอย่างในรูปแบบต่างๆ สำหรับโลโก้
                        self.create_rotated_samples(original_logo['original'])
                        self.create_brightness_samples(original_logo['original'])
                        self.create_scaled_samples(original_logo['original'])
                        self.create_blurred_samples(original_logo['original'])
                        self.create_noise_samples(original_logo['original'])
                        self.create_contrast_samples(original_logo['original'])
                        self.create_perspective_samples(original_logo['original'])
                    logger.info(f"Successfully prepared all samples for {template_name}")
                else:
                    logger.error(f"Failed to prepare base samples for {template_name}")
                    
        except Exception as e:
            logger.error(f"Error preparing all samples: {str(e)}")

def main():
    # สร้าง instance ของ LogoSampleManager
    manager = LogoSampleManager()
    
    # เตรียมตัวอย่างทั้งหมด
    manager.prepare_all_samples()

if __name__ == "__main__":
    main()
