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
import math
import numpy as np

now = datetime.now()
startdate = now.strftime("%Y/%m/%d, %H:%M:%S")
print('now '+startdate)
img = cv2.imread('D:\\images2019_1\\problem\\5051999-22364_1-1000002107.jpg',cv2.IMREAD_COLOR)
#decodedObjects = decode(im)

img_gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
img_edges = cv2.Canny(img_gray, 100, 100, apertureSize=3)
lines = cv2.HoughLinesP(img_edges, 1, math.pi / 180.0, 100, minLineLength=100, maxLineGap=5)

angles = []

for x1, y1, x2, y2 in lines[0]:
    cv2.line(img, (x1, y1), (x2, y2), (255, 0, 0), 3)
    angle = math.degrees(math.atan2(y2 - y1, x2 - x1))
    angles.append(angle)

median_angle = np.median(angles)
img_rotated = ndimage.rotate(img, median_angle)

#print "Angle is ".format(median_angle)
cv2.imshow('img_rotated', img_rotated)  

#cv2.imshow('img',img)
#cv2.waitKey(0)