import os
import sys

# Add src directory to Python path
sys.path.append(os.path.join(os.path.dirname(__file__), 'src'))

from src.ftp_certmed_detector import FTPCertmedDetector

def main():
    # FTP configuration
    ftp_host = "172.25.10.3"
    ftp_user = "u_cert_med"
    ftp_pass = "u_cert_med"
    ftp_folder = "cert_med"
    
    # Database path for logo detection
    db_path = os.path.join(os.path.dirname(__file__), "logo_database_bangna5.json")
    
    # Create detector instance
    detector = FTPCertmedDetector(
        ftp_host=ftp_host,
        ftp_user=ftp_user,
        ftp_pass=ftp_pass,
        ftp_folder=ftp_folder,
        db_path=db_path,
        min_matches=10,  # Add min_matches parameter
        match_ratio=0.7,  # Add match_ratio parameter
        debug=True  # Set to True to save debug images
    )
    
    # Process all images in FTP folder
    results = detector.process_folder()
    
    # Print results
    for result in results:
        print(f"File: {result['filename']}")
        print(f"QR Data: {result['qr_data']}")
        print(f"Logos: {result['logos']}")
        print("-" * 50)

if __name__ == "__main__":
    main() 