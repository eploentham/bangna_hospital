import cv2
import numpy as np
import json
import logging
from pathlib import Path

logger = logging.getLogger(__name__)

class LogoDetector:
    def __init__(self, database_path):
        """
        Initialize the logo detector with a database of logo samples.
        
        Args:
            database_path (str): Path to the logo database JSON file
        """
        self.database_path = database_path
        self.sift = cv2.SIFT_create()
        self.matcher = cv2.BFMatcher()
        self.database = self._load_database()
        
    def _load_database(self):
        """Load the logo database from JSON file"""
        try:
            with open(self.database_path, 'r') as f:
                return json.load(f)
        except Exception as e:
            logger.error(f"Error loading database: {str(e)}")
            return None
            
    def _convert_descriptors(self, descriptors_list):
        """Convert descriptors from list to numpy array"""
        if descriptors_list is None:
            return None
        return np.array(descriptors_list, dtype=np.float32)
            
    def _convert_keypoints(self, keypoints_list):
        """Convert keypoints from list to cv2.KeyPoint objects"""
        keypoints = []
        for kp_dict in keypoints_list:
            kp = cv2.KeyPoint()
            kp.pt = tuple(kp_dict['pt'])
            kp.size = kp_dict['size']
            kp.angle = kp_dict['angle']
            kp.response = kp_dict['response']
            kp.octave = kp_dict['octave']
            kp.class_id = kp_dict['class_id']
            keypoints.append(kp)
        return keypoints
            
    def detect(self, image, threshold=0.7):
        """
        Detect logos in the given image.
        
        Args:
            image: Input image (BGR format)
            threshold: Matching threshold (0-1)
            
        Returns:
            List of detected logos with their positions and confidence scores
        """
        if self.database is None:
            logger.error("Database not loaded")
            return []
            
        try:
            # Convert image to grayscale
            gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
            
            # Detect keypoints and compute descriptors
            kp, des = self.sift.detectAndCompute(gray, None)
            if des is None:
                logger.warning("No features detected in input image")
                return []
                
            detections = []
            
            # Compare with each sample in database
            for sample in self.database['samples']:
                sample_des = self._convert_descriptors(sample['descriptors'])
                if sample_des is None:
                    continue
                    
                # Convert keypoints
                sample_kp = self._convert_keypoints(sample['keypoints'])
                    
                # Match descriptors
                matches = self.matcher.knnMatch(des, sample_des, k=2)
                
                # Apply ratio test
                good_matches = []
                for m, n in matches:
                    if m.distance < threshold * n.distance:
                        good_matches.append(m)
                        
                # If enough good matches found
                if len(good_matches) > 10:
                    # Get matched keypoints
                    src_pts = np.float32([kp[m.queryIdx].pt for m in good_matches]).reshape(-1, 1, 2)
                    dst_pts = np.float32([sample_kp[m.trainIdx].pt for m in good_matches]).reshape(-1, 1, 2)
                    
                    # Find homography
                    M, mask = cv2.findHomography(src_pts, dst_pts, cv2.RANSAC, 5.0)
                    
                    if M is not None:
                        # Calculate confidence score
                        confidence = len(good_matches) / len(matches)
                        
                        # Get logo position
                        h, w = sample['shape'][:2]
                        pts = np.float32([[0, 0], [0, h-1], [w-1, h-1], [w-1, 0]]).reshape(-1, 1, 2)
                        dst = cv2.perspectiveTransform(pts, M)
                        
                        detections.append({
                            'filename': sample['filename'],
                            'type': sample['type'],
                            'confidence': confidence,
                            'position': dst.reshape(4, 2).tolist()
                        })
                        
            return detections
            
        except Exception as e:
            logger.error(f"Error detecting logos: {str(e)}")
            return []
            
    def draw_detections(self, image, detections):
        """
        Draw detected logos on the image.
        
        Args:
            image: Input image
            detections: List of detections from detect() method
            
        Returns:
            Image with detections drawn
        """
        result = image.copy()
        
        for detection in detections:
            # Draw bounding box
            pts = np.array(detection['position'], np.int32)
            cv2.polylines(result, [pts], True, (0, 255, 0), 2)
            
            # Add label
            label = f"{detection['filename']} ({detection['confidence']:.2f})"
            cv2.putText(result, label, (int(pts[0][0]), int(pts[0][1])-10),
                       cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2)
                       
        return result
