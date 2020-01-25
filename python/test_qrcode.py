import cv2
import pytesseract
import os
import pyodbc
from datetime import datetime
from ftplib import FTP
import qrcode
from pyzbar import pyzbar
import argparse
from scipy import ndimage

now = datetime.now()
startdate = now.strftime("%Y/%m/%d, %H:%M:%S")
print('now '+startdate)
img = cv2.imread('D:\\images2019_1\\problem\\5051999-22364_1-1000002107.jpg',cv2.IMREAD_COLOR)
#decodedObjects = decode(im)

decodedObjects = pyzbar.decode(img)
for obj in decodedObjects:
	print('Type : ', obj.type)
	print('Data : ', obj.data,'\n')

for decodedObject in decodedObjects: 
	points = decodedObject.polygon
 
    # If the points do not form a quad, find convex hull
if len(points) > 4 : 
	hull = cv2.convexHull(np.array([point for point in points], dtype=np.float32))
	hull = list(map(tuple, np.squeeze(hull)))
else : 
	hull = points;
     
    # Number of points in the convex hull
n = len(hull)
 
    # Draw the convext hull
for j in range(0,n):
	cv2.line(img, hull[j], hull[ (j+1) % n], (255,0,0), 3)
 
  # Display results 
scale_percent = 80 # percent of original size
width = int(img.shape[1] * scale_percent / 100)
height = int(img.shape[0] * scale_percent / 100)
dim = (width, height)
resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
cv2.imshow("Results", resized);
cv2.waitKey(0); 

#cv2.imshow('img',img)
#cv2.waitKey(0)