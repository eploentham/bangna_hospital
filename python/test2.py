import cv2
import pytesseract
import os
import pyodbc
from datetime import datetime
from ftplib import FTP
from pyzbar import pyzbar
#import pymssql

#	1 select data 
#	2 read image
# 	3 crop image
# 	4 pytesseract
def chkFM(txtFM):
	chk1 = False
	if len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='REG' and txtFM[2]=='015' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='001' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='002' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='003' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='007' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='008' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='011' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='013' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='014' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='016' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='060' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='123' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='NUR' and txtFM[2]=='136' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='MED' and txtFM[2]=='001' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='MED' and txtFM[2]=='002' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='MED' and txtFM[2]=='003' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='ORD' and txtFM[2]=='005' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='ORD' and txtFM[2]=='014' :
		chk1 = True
	elif len(txtFM)>=3 and txtFM[0]=='FM' and txtFM[1]=='ORD' and txtFM[2]=='035' :
		chk1 = True
	return (chk1)
def routeFM(XX, YY, IMG,HH,WW):
	chk1 = False
	txt1 = ''
	xxx = int(XX)
	#print('1111')
	for z in range(0, 20):
		xxx = int(xxx) + int(10)
		#print('xxx'+str(xxx)+' XX '+str(XX))
		#crop_img1 = IMG[YY-int(HH): YY, xxx:int(xxx)+int(WW)]
		#crop_img1 = cv2.cvtColor(crop_img1, cv2.COLOR_BGR2GRAY)
		#crop_img1 = cv2.cvtColor(IMG[YY-int(HH): YY, xxx:int(xxx)+int(WW)], cv2.COLOR_BGR2GRAY)
		#cv2.imshow('img', crop_img1)
		#cv2.waitKey()
		txt = pytesseract.image_to_string(cv2.cvtColor(IMG[YY-int(HH): YY, xxx:int(xxx)+int(WW)], cv2.COLOR_BGR2GRAY),lang='eng')
		fm = txt.split('-')
		#txt1 = txt
		if len(fm)==1:
			print('fm0['+fm[0]+']')
		if len(fm)==2:
			print('fm0['+fm[0]+'] fm1['+fm[1]+']')
		if len(fm)==3:
			print('fm0['+fm[0]+'] fm1['+fm[1]+']fm2 ['+fm[2]+']')
			txt = fm[0]+'-'+fm[1]+'-'+fm[2]
		if len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='015' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2][:3]=='015':
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='017' :
			chk1 = True
		
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='096' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='001' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='002' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='003' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='004' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='005' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='006' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='007' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='008' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='009' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='010' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='011' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='U11' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='012' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='013' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='014' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='015' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='016' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='017' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='018' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='019' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='020' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='021' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='022' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='023' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='024' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='025' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='026' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='027' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='028' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='029' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='305' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='031' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='032' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='033' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='034' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='035' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='036' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='037' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='038' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='039' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='040' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='041' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='042' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='043' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='044' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='045' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='046' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='047' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='048' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='049' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='050' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='051' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='052' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='053' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='054' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='055' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='056' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='057' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='058' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='059' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='060' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='061' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='062' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='063' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='064' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='065' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='066' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='067' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='068' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='069' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='070' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='071' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='072' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='073' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='074' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='075' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='076' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='077' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='078' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='079' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='080' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='122' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='123' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='136' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='150' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='151' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='152' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='153' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='154' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='155' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='156' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='157' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='158' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='159' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='160' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='161' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='162' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='163' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='164' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='165' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='166' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='167' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='168' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='169' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='170' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='190' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='191' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='192' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='193' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='194' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='195' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='196' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='197' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='198' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='199' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='200' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='201' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='202' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='203' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='204' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='205' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='206' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='207' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='208' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='209' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='210' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='211' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='212' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='213' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='214' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='215' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='216' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='217' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='218' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='219' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2].upper()=='06C' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2].upper()=='O6C' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2].upper()=='O60' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='039' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and len(fm[2])==4 :
			if fm[2][:3]=='123':
				chk1 = True
			elif fm[2][:3]=='011':
				chk1 = True
			elif fm[2][:3]=='039':
				chk1 = True
			elif fm[2][:3]=='050':
				chk1 = True
			elif fm[2][:3]=='007':
				chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='001' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='O01' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='002' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='003' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='001' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='002' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='003' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='004' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='005' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='006' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='007' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='008' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='009' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='010' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='011' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='012' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='013' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='014' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='015' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='016' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='017' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='018' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='019' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='020' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='021' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='022' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='023' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='024' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='025' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='026' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='027' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='028' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='029' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='030' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='035' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='036' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='037' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='038' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='039' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='040' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='041' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='042' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='043' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='044' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='045' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='046' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='047' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='048' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='049' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='050' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='051' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='052' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='053' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='054' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='055' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='056' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='057' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='058' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='059' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='000' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='060' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='061' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='062' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='063' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='064' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='065' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='066' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='067' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='068' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='069' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='070' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='071' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='072' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='073' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='074' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='075' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='076' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='077' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='078' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='079' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='080' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='081' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='082' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='083' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='084' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='085' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='086' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='087' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='088' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='089' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='090' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='091' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='092' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='093' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='094' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='095' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='096' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='097' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='098' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='099' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1].upper()=='AMS' and fm[2]=='014' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='042' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='001' :
			chk1 = True
		if(chk1):
			txt1 = fm[0]+'-'+fm[1]+'-'+fm[2]
			break
	return (chk1,txt1)




serverName="localhost"
userDB="sa"
passDB="Ekartc2c5"
dataDB="bn5_scan"
#conn = pymssql.connect(serverName , userDB , passDB, dataDB)
#conn = pymssql.connect('Driver={SQL Server};Server='+serverName+';Database='+dataDB+';;UID='+userDB+';PWD='+passDB+';Trusted_Connection=yes;')
conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
cur = conn.cursor()
sql = "Select top (1000) * From doc_scan where status_ml is null and doc_scan_id > 1000061000 Order By doc_scan_id"
cur.execute(sql)
myresult = cur.fetchall()
print('ok1')
w=240
h=50

ftp = FTP('172.25.10.3')
ftp.login("imagescan", "imagescan")

#success = ftp.connect()
#if (success != True):
	#print('ftp fail')

#ftp.cwd('/images2019_1/')
for res in myresult:
	now = datetime.now()
	startdate = now.strftime("%Y-%m-%d, %H:%M:%S")
	timestamp = datetime.timestamp(now)
	timestamp = str(timestamp).replace(".", "_")
	#unixtime = startdate.timetuple()
	#print(timestamp)

	#ftpfilename = '//sharedfolders//'+res[22]+'//'+res[4]
	ftpfilename = '//'+res[22]+'//'+res[4]
	print('res[22] '+res[22]+' res[4] '+res[4]+" start date "+startdate)
	#print(ftpfilename)
	#ftp.put_passive(True)
	filename = 'd:\\temp\\'+timestamp+'.jpg'
	try:
		ftp.retrbinary("RETR " + ftpfilename ,open(filename, 'wb').write)
	except ftplib.all_errors as e:
		print('FTP error:', e)
		continue

	#print('FTP datetime '+now.strftime("%Y-%m-%d, %H:%M:%S"))
	#img = cv2.imread('C:\\imagescan\\imagescan\\'+res[4],cv2.IMREAD_GRAYSCALE)
	#img = cv2.imread('//sharedfolders//'+res[22]+'//'+res[4],cv2.IMREAD_GRAYSCALE)
	#img = cv2.imread('//sharedfolders//'+res[22]+'//'+res[4],cv2.IMREAD_COLOR)
	img = cv2.imread(filename,cv2.IMREAD_COLOR)
	height = img.shape[0]
	width = img.shape[1]
	color1 = str(img[1, 1])
	color2 = str(img[1, 1])
	color3 = str(img[1, 1])
	color4 = str(img[1, 1])
	if(height>300 and width>300):
		color1 = str(img[300, 300])
	if(height>2000 and width>300):
		color2 = str(img[2000, 300])
	if(height>300 and width>3100):
		color3 = str(img[300, 3100])
	if(height>2000 and width>3100):
		color2000 = str(img[2000, 3100])
	#cv2.imshow('img', img)
	#cv2.waitKey()
	#imgrgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
	#
	
	xx = 0
	yy = height
	chk = False
	chkFM = False
	tractCnt = 0
	qrcode=''
	sql = "Update doc_scan Set status_ml = '1' Where doc_scan_id = '"+str(res[0])+"'"
	cur.execute(sql)
	conn.commit()
	#ในการหาค่าครั้งแรก ปรับตรงนี้ ให้เป็น 10 เพราะ ถ้าเจอ ก็จะเจอ ถ้าไม่เจอจะนาน เสียเวลา
	for x in range(0, 10):
		xx = xx + int(10)
		yy = height
		print ('chk '+str(chk))
		if xx == 50:
			qrcode=''
			decodedObjects = pyzbar.decode(img)
			for obj in decodedObjects:
				qrcode = qrcode + obj.type
				#print('Type : ', obj.type)
				#print('Data : ', obj.data,'\n')
			if qrcode == 'QRCODE':
				sql = "Update doc_scan Set ml_fm = 'FM-LAB-999', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' ,width = '" +str(width) +"', height = '"+ str(height) + "', rgb_1 = '"+str(color1)+"' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				chk=True
				print('CHKKKKKKKKKKKKKKKKKKKKK')
				print('qrcode '+qrcode)
		elif xx == 70:
			sql = "Update doc_scan Set  ml_xx= '07' Where doc_scan_id = '"+str(res[0])+"'"
			cur.execute(sql)
			conn.commit()
		if chk==True:
			break
		if tractCnt >= 10:
			break
		for y in range(0, 20):
			#print('chk '+str(chk))
			#if chk==True:
			#break
			yy = yy - int(10)
			#print('yy '+str(yy))
			#print('xx '+str(xx))
			#crop_img = img[yy-int(h): yy, xx:xx+w]
			#crop_img = cv2.cvtColor(crop_img, cv2.COLOR_BGR2GRAY)
			#crop_img = cv2.cvtColor(img[yy-int(h): yy, xx:xx+w], cv2.COLOR_BGR2GRAY)
			#print("For y ")
			print('xx[' + str(xx)+'] yy['+str(yy)+'] y['+ str(y)+'] res[22] '+res[22]+' res[4] '+res[4]+' '+filename)
			#cv2.imshow('img', crop_img)
			#cv2.waitKey()
			#txt = pytesseract.image_to_string(cv2.cvtColor(img[yy-int(h): yy, xx:xx+w], cv2.COLOR_BGR2GRAY),lang='eng')
			txt = pytesseract.image_to_string(img[yy-int(h): yy, xx:xx+w],lang='eng')
			fm = txt.split('-')
			now1 = datetime.now()
			enddate = now1.strftime("%Y-%m-%d, %H:%M:%S")
			#print(enddate+' '+fm)
			if len(fm)>=1 and (fm[0]=='FM'):
				tractCnt += 1
				if tractCnt >= 10:
					break
				print('tractCnt '+str(tractCnt))
				(chkTxt,txt1) = routeFM(xx,yy,img,h,w)
				if (chkTxt):
					print('chkTxt true')
					sql = "Update doc_scan Set ml_fm = '"+txt1.replace("'", "''")+"'"+", ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '2' "+",width = '" +str(width) +"', height = '"+ str(height) + "', rgb_1 = '"+str(color1)+"',ml_xx='"+str(xx)+"'"+" Where doc_scan_id = '"+str(res[0])+"'"
					cur.execute(sql)
					conn.commit()
					chk=True
					print('CHKKKKKKKKKKKKKKKKKKKKK')
					break

			
			#print('date start '+startdate+' end date '+enddate)
	#cv2.imshow('img', img)
	
			#cv2.imshow('img', crop_img)
			#cv2.waitKey()

ftp.close()


#img = cv2.imread('C:\\5131639-23578_2\\5131639-23578_2-1000087114.jpg',cv2.IMREAD_GRAYSCALE)
#cv2.imshow('img', img)
#cv2.waitKey(0)
#height = img.shape[0]
#width = img.shape[1]
#xx = 0
#yy = height
#print('height '+str(height))

#w=230
#h=50


