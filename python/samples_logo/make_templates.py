from src.samples_logo import LogoSampleManager
import logging

# Setup logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

def make_templates():
    """สร้าง templates และฐานข้อมูลสำหรับการตรวจจับโลโก้และตัวเลข"""
    try:
        # สร้าง sample manager สำหรับ Bangna5
        bangna5_manager = LogoSampleManager('bangna5')
        
        # ประมวลผลทุก template
        bangna5_manager.prepare_all_samples()
        
        # สร้างฐานข้อมูล
        bangna5_manager.create_database()
        
        logger.info("Successfully created all templates and database")
        
    except Exception as e:
        logger.error(f"Error creating templates: {str(e)}")

if __name__ == "__main__":
    make_templates() 