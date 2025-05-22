from .samples_logo import LogoSampleManager
from .config import HOSPITAL_CONFIGS, SAMPLE_PARAMS
from .logo_detector import LogoDetector

__version__ = '1.0.0'
__author__ = 'BANGNA5 HOSPITAL'
__email__ = 'admin@bangna5.com'

# กำหนดค่าพื้นฐานที่ใช้ร่วมกัน
DEFAULT_SETTINGS = {
    'hospital_name': 'BANGNA5 GENERAL HOSPITAL',
    'sample_dirs': {
        'original': 'samples/original/',
        'rotated': 'samples/rotated/',
        'brightness': 'samples/brightness/',
        'scaled': 'samples/scaled/'
    },
    'template_path': 'templates/logo_original.jpg',
    'database_path': 'logo_database.json'
}

# กำหนดค่า parameters สำหรับการสร้างตัวอย่าง
SAMPLE_PARAMS = {
    'rotation': {
        'min_angle': -15,
        'max_angle': 15,
        'step': 5
    },
    'brightness': {
        'min_beta': -30,
        'max_beta': 30,
        'step': 10
    },
    'scale': {
        'min_scale': 80,
        'max_scale': 120,
        'step': 10
    }
}

# กำหนดค่า parameters สำหรับการตรวจจับ
DETECTION_PARAMS = {
    'template_matching': {
        'threshold': 0.8,
        'method': 'cv2.TM_CCOEFF_NORMED'
    },
    'feature_detection': {
        'min_matches': 10,
        'ratio_thresh': 0.7
    }
}

def get_version():
    """Return the current version of the package"""
    return __version__

def get_sample_manager():
    """Create and return a new LogoSampleManager instance with default settings"""
    return LogoSampleManager(
        sample_dirs=DEFAULT_SETTINGS['sample_dirs'],
        template_path=DEFAULT_SETTINGS['template_path']
    )

def get_logo_detector():
    """Create and return a new LogoDetector instance with default settings"""
    return LogoDetector(
        template_path=DEFAULT_SETTINGS['template_path'],
        detection_params=DETECTION_PARAMS
    )

# Initialize logging
import logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)

logger = logging.getLogger(__name__)
