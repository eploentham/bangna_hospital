HOSPITAL_CONFIGS = {
    'bangna5': {
        'name': 'BANGNA5 GENERAL HOSPITAL',
        'sample_dirs': {
            'original': 'samples/bangna5/original/',
            'rotated': 'samples/bangna5/rotated/',
            'brightness': 'samples/bangna5/brightness/',
            'scaled': 'samples/bangna5/scaled/',
            'blurred': 'samples/bangna5/blurred/',
            'noise': 'samples/bangna5/noise/',
            'contrast': 'samples/bangna5/contrast/',
            'perspective': 'samples/bangna5/perspective/',
            'numbers': 'samples/bangna5/numbers/'
        },
        'templates': {
            'logo_1': 'templates/logo_bangna/logo_1.jpg',
            'logo_2': 'templates/logo_bangna/logo_2.jpg',
            'logo_3': 'templates/logo_bangna/logo_3.jpg',
            'logo_4': 'templates/logo_bangna/logo_4.jpg',
            'logo_5': 'templates/logo_bangna/logo_5.jpg',
            'logo_6': 'templates/logo_bangna/logo_6.jpg',
            'logo_7': 'templates/logo_bangna/logo_7.jpg',
            'logo_8': 'templates/logo_bangna/logo_8.jpg',
            'logo_9': 'templates/logo_bangna/logo_9.jpg',
            'logo_10': 'templates/logo_bangna/logo_10.jpg',
            'logo_11': 'templates/logo_bangna/logo_11.jpg',
            'logo_12': 'templates/logo_bangna/logo_12.jpg',
            'logo_13': 'templates/logo_bangna/logo_13.jpg',
            'logo_14': 'templates/logo_bangna/logo_14.jpg',
            'logo_15': 'templates/logo_bangna/logo_15.jpg',
            'logo_16': 'templates/logo_bangna/logo_16.jpg',
            'logo_17': 'templates/logo_bangna/logo_17.jpg',
            'logo_18': 'templates/logo_bangna/logo_18.jpg',
            'logo_19': 'templates/logo_bangna/logo_19.jpg',
            'logo_20': 'templates/logo_bangna/logo_20.jpg',
            'logo_21': 'templates/logo_bangna/logo_21.jpg',
            'logo_22': 'templates/logo_bangna/logo_22.jpg',
            'logo_23': 'templates/logo_bangna/logo_23.jpg',
            'logo_24': 'templates/logo_bangna/logo_24.jpg',
            'logo_25': 'templates/logo_bangna/logo_25.jpg',
            'number_1': 'templates/logo_bangna/number_1.jpg'
        },
        'database_path': 'logo_database_bangna5.json'
    }
}

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
    },
    'blur': {
        'min_kernel': 1,
        'max_kernel': 5,
        'step': 2
    },
    'noise': {
        'min_sigma': 0,
        'max_sigma': 20,
        'step': 5
    },
    'contrast': {
        'min_alpha': 0.5,
        'max_alpha': 1.5,
        'step': 0.2
    },
    'perspective': {
        'min_angle': -10,
        'max_angle': 10,
        'step': 5
    },
    'number_detection': {
        'threshold': 0.8,
        'min_contour_area': 100,
        'max_contour_area': 1000
    }
}
