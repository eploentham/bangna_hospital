import requests
import hmac
import hashlib
import sched, time
import pyodbc
import json
from datetime import datetime
import os,shutil
from os import path
from glob import glob

import sys

def requestAPIAuthen():
    nonce = 1
    customer_id = '3610400273411'
    API_SECRET = '$jwt@moph#'
    api_key = 'Geng5588@'
    message = '{} {} {}'.format(nonce, customer_id, api_key)
    signature = hmac.new(bytes(API_SECRET , 'utf-8'), msg = bytes(message , 'utf-8'), digestmod = hashlib.sha256).hexdigest().upper()
    print(signature)
    URL = "https://cvp1.moph.go.th/token"
    PARAMS = {'Action':'get_moph_access_token','user':'3610400273411','password_hash':signature,'hospital_code':'24036'}
    respAPIAuthen = requests.get(url = URL, params = PARAMS)
    print(respAPIAuthen)
def testrequests():
    URL = "http://maps.googleapis.com/maps/api/geocode/json"
  
    # location given here
    location = "delhi technological university"
    
    # defining a params dict for the parameters to be sent to the API
    PARAMS = {'address':location}
    
    # sending get request and saving the response as response object
    r = requests.get(url = URL, params = PARAMS)
    
    # extracting data in json format
    data = r.json()
    
    
    # extracting latitude, longitude and formatted address 
    # of the first matching location
    latitude = data['results'][0]['geometry']['location']['lat']
    longitude = data['results'][0]['geometry']['location']['lng']
    formatted_address = data['results'][0]['formatted_address']
    
    # printing the output
    print("Latitude:%s\nLongitude:%s\nFormatted Address:%s"
        %(latitude, longitude,formatted_address))
    
try:
    sql=""
    requestAPIAuthen()

except pyodbc.Error as ex:
    sql=""
    print(ex)
finally:
    sql=""