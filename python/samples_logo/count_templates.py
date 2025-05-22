import json

def count_templates():
    try:
        with open('logo_database_bangna5.json', 'r') as f:
            data = json.load(f)
            num_templates = len(data['samples'])
            print(f'Number of templates: {num_templates}')
    except Exception as e:
        print(f'Error: {str(e)}')

if __name__ == '__main__':
    count_templates() 