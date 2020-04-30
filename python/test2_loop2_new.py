import cv2
import pytesseract
import os
import pyodbc
from datetime import datetime
from ftplib import FTP
#import ftplib
#import FTP
#from pyzbar import pyzbar
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
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='030' :
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
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='081' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='082' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='083' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='084' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='085' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='086' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='087' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='088' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='089' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='090' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='091' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='092' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='093' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='094' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='095' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='096' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='097' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='098' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='099' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='100' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='101' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='102' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='103' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='104' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='105' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='106' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='107' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='108' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='109' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='110' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='111' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='112' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='113' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='114' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='115' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='116' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='117' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='118' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='119' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='120' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='121' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='122' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='123' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='124' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='125' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='126' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='127' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='128' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='129' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='130' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='131' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='132' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='133' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='134' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='135' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='136' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='137' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='138' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='139' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='140' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='141' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='142' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='143' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='144' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='145' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='146' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='147' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='148' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='149' :
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
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='171' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='172' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='173' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='174' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='175' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='176' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='177' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='178' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='179' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='180' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='181' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='182' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='183' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='184' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='185' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='186' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='187' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='188' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='189' :
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
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='220' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='221' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='222' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='223' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='224' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='225' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='226' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='227' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='228' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='229' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='230' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='231' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='232' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='233' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='234' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='235' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='236' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='237' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='238' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='239' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='240' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='241' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='242' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='243' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='244' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='245' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='246' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='247' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='248' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='249' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='250' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='251' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='252' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='253' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='254' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='255' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='256' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='257' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='258' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='259' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='260' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='261' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='262' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='263' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='264' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='265' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='266' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='267' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='268' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='269' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='270' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='271' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='272' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='273' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='274' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='275' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='276' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='277' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='278' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='279' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='280' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='281' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='282' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='283' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='284' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='285' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='286' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='287' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='288' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='289' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='290' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='291' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='292' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='293' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='294' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='295' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='296' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='297' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='298' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='299' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='300' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='301' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='302' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='303' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='304' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='305' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='306' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='307' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='308' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='309' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='310' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='311' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='312' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='313' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='314' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='315' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='316' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='317' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='318' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='319' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='320' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='321' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='322' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='323' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='324' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='325' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='326' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='327' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='328' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='329' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='330' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='331' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='332' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='333' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='334' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='335' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='336' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='337' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='338' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='339' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='340' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='341' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='342' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='343' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='344' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='345' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='346' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='347' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='348' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='349' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='350' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='351' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='352' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='353' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='354' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='355' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='356' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='357' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='358' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='359' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='360' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='361' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='362' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='363' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='364' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='365' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='366' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='367' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='368' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='369' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='370' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='371' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='372' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='373' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='374' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='375' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='376' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='377' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='378' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='379' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='380' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='381' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='382' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='383' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='384' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='385' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='386' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='387' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='388' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='389' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='390' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='391' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='392' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='393' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='394' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='395' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='396' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='397' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='398' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='399' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='400' :
			chk1 = True

		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='401' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='402' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='403' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='404' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='405' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='406' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='407' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='408' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='409' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='410' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='411' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='412' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='413' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='414' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='415' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='416' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='417' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='418' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='419' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='420' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='421' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='422' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='423' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='424' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='425' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='426' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='427' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='428' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='429' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='430' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='431' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='432' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='433' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='434' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='435' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='436' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='437' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='438' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='439' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='440' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='441' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='442' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='443' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='444' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='445' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='446' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='447' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='448' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='449' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='450' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='451' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='452' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='453' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='454' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='455' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='456' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='457' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='458' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='459' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='460' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='461' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='462' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='463' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='464' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='465' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='466' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='467' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='468' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='469' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='470' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='471' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='472' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='473' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='474' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='475' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='476' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='477' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='478' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='479' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='480' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='481' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='482' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='483' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='484' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='485' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='486' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='487' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='488' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='489' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='490' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='491' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='492' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='493' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='494' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='495' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='496' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='497' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='498' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='499' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='500' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='501' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='502' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='503' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='504' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='505' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='506' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='507' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='508' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='509' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='510' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='511' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='512' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='513' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='514' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='515' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='516' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='517' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='518' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='519' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='520' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='521' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='522' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='523' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='524' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='525' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='526' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='527' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='528' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='529' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='530' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='531' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='532' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='533' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='534' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='535' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='536' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='537' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='538' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='539' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='540' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='541' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='542' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='543' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='544' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='545' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='546' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='547' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='548' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='549' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='550' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='551' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='552' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='553' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='554' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='555' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='556' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='557' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='558' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='559' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='560' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='561' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='562' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='563' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='564' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='565' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='566' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='567' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='568' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='569' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='570' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='571' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='572' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='573' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='574' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='575' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='576' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='577' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='578' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='579' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='580' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='581' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='582' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='583' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='584' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='585' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='586' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='587' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='588' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='589' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='590' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='591' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='592' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='593' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='594' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='595' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='596' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='597' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='598' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='599' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='600' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='601' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='602' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='603' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='604' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='605' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='606' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='607' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='608' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='609' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='610' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='611' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='612' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='613' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='614' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='615' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='616' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='617' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='618' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='619' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='620' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='621' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='622' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='623' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='624' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='625' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='626' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='627' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='628' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='629' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='630' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='631' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='632' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='633' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='634' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='635' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='636' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='637' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='638' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='639' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='640' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='641' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='642' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='643' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='644' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='645' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='646' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='647' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='648' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='649' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='650' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='651' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='652' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='653' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='654' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='655' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='656' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='657' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='658' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='659' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='660' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='661' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='662' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='663' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='664' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='665' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='666' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='667' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='668' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='669' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='670' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='671' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='672' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='673' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='674' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='675' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='676' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='677' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='678' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='679' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='680' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='681' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='682' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='683' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='684' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='685' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='686' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='687' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='688' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='689' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='690' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='691' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='692' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='693' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='694' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='695' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='696' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='697' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='698' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='699' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='700' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='701' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='702' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='703' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='704' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='705' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='706' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='707' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='708' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='709' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='710' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='711' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='712' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='713' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='714' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='715' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='716' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='717' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='718' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='719' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='720' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='721' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='722' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='723' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='724' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='725' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='726' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='727' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='728' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='729' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='730' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='731' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='732' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='733' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='734' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='735' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='736' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='737' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='738' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='739' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='740' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='741' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='742' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='743' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='744' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='745' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='746' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='747' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='748' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='749' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='750' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='751' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='752' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='753' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='754' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='755' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='756' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='757' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='758' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='759' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='760' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='761' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='762' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='763' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='764' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='765' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='766' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='767' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='768' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='769' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='770' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='771' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='772' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='773' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='774' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='775' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='776' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='777' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='778' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='779' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='780' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='781' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='782' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='783' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='784' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='785' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='786' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='787' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='788' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='789' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='790' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='791' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='792' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='793' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='794' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='795' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='796' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='797' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='798' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='799' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='800' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='801' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='802' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='803' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='804' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='805' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='806' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='807' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='808' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='809' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='810' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='811' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='812' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='813' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='814' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='815' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='816' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='817' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='818' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='819' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='820' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='821' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='822' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='823' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='824' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='825' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='826' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='827' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='828' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='829' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='830' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='831' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='832' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='833' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='834' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='835' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='836' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='837' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='838' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='839' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='840' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='841' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='842' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='843' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='844' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='845' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='846' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='847' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='848' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='849' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='850' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='851' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='852' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='853' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='854' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='855' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='856' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='857' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='858' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='859' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='860' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='861' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='862' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='863' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='864' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='865' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='866' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='867' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='868' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='869' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='870' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='871' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='872' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='873' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='874' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='875' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='876' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='877' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='878' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='879' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='880' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='881' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='882' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='883' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='884' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='885' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='886' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='887' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='888' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='889' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='890' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='891' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='892' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='893' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='894' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='895' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='896' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='897' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='898' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='899' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='900' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='901' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='902' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='903' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='904' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='905' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='906' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='907' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='908' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='909' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='910' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='911' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='912' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='913' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='914' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='915' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='916' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='917' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='918' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='919' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='920' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='921' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='922' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='923' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='924' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='925' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='926' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='927' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='928' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='929' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='930' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='931' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='932' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='933' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='934' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='935' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='936' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='937' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='938' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='939' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='940' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='941' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='942' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='943' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='944' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='945' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='946' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='947' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='948' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='949' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='950' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='951' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='952' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='953' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='954' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='955' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='956' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='957' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='958' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='959' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='960' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='961' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='962' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='963' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='964' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='965' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='966' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='967' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='968' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='969' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='970' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='971' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='972' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='973' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='974' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='975' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='976' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='977' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='978' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='979' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='980' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='981' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='982' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='983' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='984' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='985' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='986' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='987' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='988' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='989' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='990' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='991' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='992' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='993' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='994' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='995' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='996' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='997' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='998' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='999' :
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
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='100' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='101' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='102' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='103' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='104' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='105' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='106' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='107' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='108' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='109' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='110' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='111' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='112' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='113' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='114' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='115' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='116' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='117' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='118' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='119' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='120' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='121' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='122' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='123' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='124' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='125' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='126' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='127' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='128' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='129' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='130' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='131' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='132' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='133' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='134' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='135' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='136' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='137' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='138' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='139' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='140' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='141' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='142' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='143' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='144' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='145' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='146' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='147' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='148' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='149' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='150' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='151' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='152' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='153' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='154' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='155' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='156' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='157' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='158' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='159' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='160' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='161' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='162' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='163' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='164' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='165' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='166' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='167' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='168' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='169' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='170' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='171' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='172' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='173' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='174' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='175' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='176' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='177' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='178' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='179' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='180' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='181' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='182' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='183' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='184' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='185' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='186' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='187' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='188' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='189' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='190' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='191' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='192' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='193' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='194' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='195' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='196' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='197' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='198' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='199' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='200' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='201' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='202' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='203' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='204' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='205' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='206' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='207' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='208' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='209' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='210' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='211' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='212' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='213' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='214' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='215' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='216' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='217' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='218' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='219' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='220' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='221' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='222' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='223' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='224' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='225' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='226' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='227' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='228' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='229' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='230' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='231' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='232' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='233' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='234' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='235' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='236' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='237' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='238' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='239' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='240' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='241' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='242' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='243' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='244' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='245' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='246' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='247' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='248' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='249' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='250' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='251' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='252' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='253' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='254' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='255' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='256' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='257' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='258' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='259' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='260' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='261' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='262' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='263' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='264' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='265' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='266' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='267' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='268' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='269' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='270' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='271' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='272' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='273' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='274' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='275' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='276' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='277' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='278' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='279' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='280' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='281' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='282' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='283' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='284' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='285' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='286' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='287' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='288' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='289' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='290' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='291' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='292' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='293' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='294' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='295' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='296' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='297' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='298' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='299' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='300' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='301' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='302' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='303' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='304' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='305' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='306' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='307' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='308' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='309' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='310' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='311' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='312' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='313' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='314' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='315' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='316' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='317' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='318' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='319' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='320' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='321' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='322' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='323' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='324' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='325' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='326' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='327' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='328' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='329' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='330' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='331' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='332' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='333' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='334' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='335' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='336' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='337' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='338' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='339' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='340' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='341' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='342' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='343' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='344' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='345' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='346' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='347' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='348' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='349' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='350' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='351' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='352' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='353' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='354' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='355' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='356' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='357' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='358' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='359' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='360' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='361' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='362' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='363' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='364' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='365' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='366' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='367' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='368' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='369' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='370' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='371' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='372' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='373' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='374' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='375' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='376' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='377' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='378' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='379' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='380' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='381' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='382' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='383' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='384' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='385' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='386' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='387' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='388' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='389' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='390' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='391' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='392' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='393' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='394' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='395' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='396' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='397' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='398' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='399' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='400' :
			chk1 = True

		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='401' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='402' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='403' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='404' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='405' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='406' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='407' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='408' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='409' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='410' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='411' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='412' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='413' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='414' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='415' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='416' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='417' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='418' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='419' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='420' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='421' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='422' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='423' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='424' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='425' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='426' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='427' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='428' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='429' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='430' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='431' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='432' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='433' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='434' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='435' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='436' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='437' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='438' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='439' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='440' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='441' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='442' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='443' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='444' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='445' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='446' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='447' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='448' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='449' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='450' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='451' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='452' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='453' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='454' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='455' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='456' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='457' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='458' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='459' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='460' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='461' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='462' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='463' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='464' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='465' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='466' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='467' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='468' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='469' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='470' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='471' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='472' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='473' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='474' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='475' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='476' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='477' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='478' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='479' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='480' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='481' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='482' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='483' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='484' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='485' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='486' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='487' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='488' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='489' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='490' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='491' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='492' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='493' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='494' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='495' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='496' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='497' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='498' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='499' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='500' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='501' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='502' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='503' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='504' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='505' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='506' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='507' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='508' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='509' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='510' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='511' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='512' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='513' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='514' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='515' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='516' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='517' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='518' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='519' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='520' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='521' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='522' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='523' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='524' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='525' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='526' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='527' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='528' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='529' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='530' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='531' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='532' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='533' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='534' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='535' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='536' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='537' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='538' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='539' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='540' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='541' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='542' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='543' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='544' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='545' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='546' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='547' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='548' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='549' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='550' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='551' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='552' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='553' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='554' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='555' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='556' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='557' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='558' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='559' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='560' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='561' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='562' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='563' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='564' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='565' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='566' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='567' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='568' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='569' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='570' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='571' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='572' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='573' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='574' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='575' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='576' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='577' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='578' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='579' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='580' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='581' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='582' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='583' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='584' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='585' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='586' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='587' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='588' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='589' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='590' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='591' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='592' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='593' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='594' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='595' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='596' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='597' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='598' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='599' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='600' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='601' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='602' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='603' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='604' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='605' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='606' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='607' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='608' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='609' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='610' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='611' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='612' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='613' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='614' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='615' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='616' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='617' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='618' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='619' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='620' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='621' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='622' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='623' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='624' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='625' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='626' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='627' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='628' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='629' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='630' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='631' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='632' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='633' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='634' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='635' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='636' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='637' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='638' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='639' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='640' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='641' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='642' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='643' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='644' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='645' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='646' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='647' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='648' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='649' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='650' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='651' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='652' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='653' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='654' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='655' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='656' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='657' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='658' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='659' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='660' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='661' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='662' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='663' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='664' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='665' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='666' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='667' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='668' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='669' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='670' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='671' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='672' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='673' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='674' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='675' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='676' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='677' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='678' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='679' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='680' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='681' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='682' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='683' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='684' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='685' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='686' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='687' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='688' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='689' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='690' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='691' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='692' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='693' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='694' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='695' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='696' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='697' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='698' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='699' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='700' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='701' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='702' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='703' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='704' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='705' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='706' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='707' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='708' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='709' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='710' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='711' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='712' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='713' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='714' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='715' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='716' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='717' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='718' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='719' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='720' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='721' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='722' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='723' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='724' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='725' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='726' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='727' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='728' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='729' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='730' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='731' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='732' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='733' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='734' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='735' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='736' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='737' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='738' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='739' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='740' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='741' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='742' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='743' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='744' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='745' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='746' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='747' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='748' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='749' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='750' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='751' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='752' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='753' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='754' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='755' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='756' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='757' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='758' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='759' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='760' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='761' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='762' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='763' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='764' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='765' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='766' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='767' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='768' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='769' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='770' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='771' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='772' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='773' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='774' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='775' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='776' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='777' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='778' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='779' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='780' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='781' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='782' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='783' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='784' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='785' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='786' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='787' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='788' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='789' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='790' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='791' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='792' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='793' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='794' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='795' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='796' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='797' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='798' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='799' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='800' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='801' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='802' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='803' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='804' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='805' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='806' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='807' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='808' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='809' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='810' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='811' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='812' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='813' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='814' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='815' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='816' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='817' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='818' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='819' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='820' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='821' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='822' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='823' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='824' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='825' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='826' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='827' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='828' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='829' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='830' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='831' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='832' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='833' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='834' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='835' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='836' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='837' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='838' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='839' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='840' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='841' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='842' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='843' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='844' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='845' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='846' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='847' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='848' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='849' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='850' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='851' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='852' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='853' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='854' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='855' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='856' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='857' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='858' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='859' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='860' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='861' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='862' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='863' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='864' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='865' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='866' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='867' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='868' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='869' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='870' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='871' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='872' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='873' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='874' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='875' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='876' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='877' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='878' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='879' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='880' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='881' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='882' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='883' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='884' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='885' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='886' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='887' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='888' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='889' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='890' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='891' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='892' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='893' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='894' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='895' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='896' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='897' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='898' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='899' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='900' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='901' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='902' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='903' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='904' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='905' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='906' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='907' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='908' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='909' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='910' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='911' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='912' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='913' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='914' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='915' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='916' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='917' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='918' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='919' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='920' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='921' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='922' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='923' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='924' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='925' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='926' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='927' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='928' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='929' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='930' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='931' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='932' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='933' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='934' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='935' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='936' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='937' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='938' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='939' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='940' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='941' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='942' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='943' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='944' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='945' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='946' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='947' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='948' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='949' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='950' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='951' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='952' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='953' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='954' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='955' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='956' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='957' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='958' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='959' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='960' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='961' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='962' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='963' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='964' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='965' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='966' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='967' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='968' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='969' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='970' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='971' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='972' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='973' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='974' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='975' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='976' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='977' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='978' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='979' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='980' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='981' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='982' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='983' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='984' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='985' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='986' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='987' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='988' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='989' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='990' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='991' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='992' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='993' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='994' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='995' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='996' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='997' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='998' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='999' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='001' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='002' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='003' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='004' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='005' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='006' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='007' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='008' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='009' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='010' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='011' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='U11' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='012' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='013' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='014' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='015' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='016' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='017' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='018' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='019' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='020' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='021' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='022' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='023' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='024' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='025' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='026' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='027' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='028' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='029' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='030' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='031' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='032' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='033' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='034' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='035' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='036' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='037' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='038' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='039' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='040' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='041' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='042' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='043' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='044' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='045' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='046' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='047' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='048' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='049' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='050' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='051' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='052' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='053' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='054' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='055' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='056' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='057' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='058' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='059' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='060' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='061' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='062' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='063' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='064' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='065' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='066' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='067' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='068' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='069' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='070' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='071' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='072' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='073' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='074' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='075' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='076' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='077' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='078' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='079' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='080' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='081' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='082' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='083' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='084' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='085' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='086' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='087' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='088' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='089' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='090' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='091' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='092' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='093' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='094' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='095' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='096' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='097' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='098' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='099' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='100' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='101' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='102' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='103' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='104' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='105' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='106' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='107' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='108' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='109' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='110' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='111' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='112' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='113' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='114' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='115' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='116' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='117' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='118' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='119' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='120' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='121' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='122' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='123' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='124' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='125' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='126' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='127' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='128' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='129' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='130' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='131' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='132' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='133' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='134' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='135' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='136' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='137' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='138' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='139' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='140' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='141' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='142' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='143' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='144' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='145' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='146' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='147' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='148' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='149' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='150' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='151' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='152' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='153' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='154' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='155' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='156' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='157' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='158' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='159' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='160' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='161' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='162' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='163' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='164' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='165' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='166' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='167' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='168' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='169' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='170' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='171' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='172' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='173' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='174' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='175' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='176' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='177' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='178' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='179' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='180' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='181' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='182' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='183' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='184' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='185' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='186' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='187' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='188' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='189' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='190' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='191' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='192' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='193' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='194' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='195' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='196' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='197' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='198' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='199' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='200' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='201' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='202' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='203' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='204' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='205' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='206' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='207' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='208' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='209' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='210' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='211' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='212' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='213' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='214' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='215' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='216' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='217' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='218' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='219' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='220' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='221' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='222' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='223' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='224' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='225' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='226' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='227' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='228' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='229' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='230' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='231' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='232' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='233' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='234' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='235' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='236' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='237' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='238' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='239' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='240' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='241' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='242' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='243' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='244' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='245' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='246' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='247' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='248' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='249' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='250' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='251' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='252' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='253' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='254' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='255' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='256' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='257' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='258' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='259' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='260' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='261' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='262' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='263' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='264' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='265' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='266' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='267' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='268' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='269' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='270' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='271' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='272' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='273' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='274' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='275' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='276' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='277' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='278' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='279' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='280' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='281' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='282' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='283' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='284' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='285' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='286' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='287' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='288' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='289' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='290' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='291' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='292' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='293' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='294' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='295' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='296' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='297' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='298' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='299' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='300' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='301' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='302' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='303' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='304' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='305' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='306' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='307' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='308' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='309' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='310' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='311' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='312' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='313' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='314' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='315' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='316' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='317' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='318' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='319' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='320' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='321' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='322' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='323' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='324' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='325' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='326' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='327' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='328' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='329' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='330' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='331' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='332' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='333' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='334' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='335' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='336' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='337' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='338' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='339' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='340' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='341' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='342' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='343' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='344' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='345' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='346' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='347' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='348' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='349' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='350' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='351' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='352' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='353' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='354' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='355' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='356' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='357' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='358' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='359' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='360' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='361' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='362' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='363' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='364' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='365' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='366' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='367' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='368' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='369' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='370' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='371' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='372' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='373' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='374' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='375' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='376' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='377' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='378' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='379' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='380' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='381' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='382' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='383' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='384' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='385' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='386' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='387' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='388' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='389' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='390' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='391' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='392' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='393' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='394' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='395' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='396' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='397' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='398' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='399' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='400' :
			chk1 = True

		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='401' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='402' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='403' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='404' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='405' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='406' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='407' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='408' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='409' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='410' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='411' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='412' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='413' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='414' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='415' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='416' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='417' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='418' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='419' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='420' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='421' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='422' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='423' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='424' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='425' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='426' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='427' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='428' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='429' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='430' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='431' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='432' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='433' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='434' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='435' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='436' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='437' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='438' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='439' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='440' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='441' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='442' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='443' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='444' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='445' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='446' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='447' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='448' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='449' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='450' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='451' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='452' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='453' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='454' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='455' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='456' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='457' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='458' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='459' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='460' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='461' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='462' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='463' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='464' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='465' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='466' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='467' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='468' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='469' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='470' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='471' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='472' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='473' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='474' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='475' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='476' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='477' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='478' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='479' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='480' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='481' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='482' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='483' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='484' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='485' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='486' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='487' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='488' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='489' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='490' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='491' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='492' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='493' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='494' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='495' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='496' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='497' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='498' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='499' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='500' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='501' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='502' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='503' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='504' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='505' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='506' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='507' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='508' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='509' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='510' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='511' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='512' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='513' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='514' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='515' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='516' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='517' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='518' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='519' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='520' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='521' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='522' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='523' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='524' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='525' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='526' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='527' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='528' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='529' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='530' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='531' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='532' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='533' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='534' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='535' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='536' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='537' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='538' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='539' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='540' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='541' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='542' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='543' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='544' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='545' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='546' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='547' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='548' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='549' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='550' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='551' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='552' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='553' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='554' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='555' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='556' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='557' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='558' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='559' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='560' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='561' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='562' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='563' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='564' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='565' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='566' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='567' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='568' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='569' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='570' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='571' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='572' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='573' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='574' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='575' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='576' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='577' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='578' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='579' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='580' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='581' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='582' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='583' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='584' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='585' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='586' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='587' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='588' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='589' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='590' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='591' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='592' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='593' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='594' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='595' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='596' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='597' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='598' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='599' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='600' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='601' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='602' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='603' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='604' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='605' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='606' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='607' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='608' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='609' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='610' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='611' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='612' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='613' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='614' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='615' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='616' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='617' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='618' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='619' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='620' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='621' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='622' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='623' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='624' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='625' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='626' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='627' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='628' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='629' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='630' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='631' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='632' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='633' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='634' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='635' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='636' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='637' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='638' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='639' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='640' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='641' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='642' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='643' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='644' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='645' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='646' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='647' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='648' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='649' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='650' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='651' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='652' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='653' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='654' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='655' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='656' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='657' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='658' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='659' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='660' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='661' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='662' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='663' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='664' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='665' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='666' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='667' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='668' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='669' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='670' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='671' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='672' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='673' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='674' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='675' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='676' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='677' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='678' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='679' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='680' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='681' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='682' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='683' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='684' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='685' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='686' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='687' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='688' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='689' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='690' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='691' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='692' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='693' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='694' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='695' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='696' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='697' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='698' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='699' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='700' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='701' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='702' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='703' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='704' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='705' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='706' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='707' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='708' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='709' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='710' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='711' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='712' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='713' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='714' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='715' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='716' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='717' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='718' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='719' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='720' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='721' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='722' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='723' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='724' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='725' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='726' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='727' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='728' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='729' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='730' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='731' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='732' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='733' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='734' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='735' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='736' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='737' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='738' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='739' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='740' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='741' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='742' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='743' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='744' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='745' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='746' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='747' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='748' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='749' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='750' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='751' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='752' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='753' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='754' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='755' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='756' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='757' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='758' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='759' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='760' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='761' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='762' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='763' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='764' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='765' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='766' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='767' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='768' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='769' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='770' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='771' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='772' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='773' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='774' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='775' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='776' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='777' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='778' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='779' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='780' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='781' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='782' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='783' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='784' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='785' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='786' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='787' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='788' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='789' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='790' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='791' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='792' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='793' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='794' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='795' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='796' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='797' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='798' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='799' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='800' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='801' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='802' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='803' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='804' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='805' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='806' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='807' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='808' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='809' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='810' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='811' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='812' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='813' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='814' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='815' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='816' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='817' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='818' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='819' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='820' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='821' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='822' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='823' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='824' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='825' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='826' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='827' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='828' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='829' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='830' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='831' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='832' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='833' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='834' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='835' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='836' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='837' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='838' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='839' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='840' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='841' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='842' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='843' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='844' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='845' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='846' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='847' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='848' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='849' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='850' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='851' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='852' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='853' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='854' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='855' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='856' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='857' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='858' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='859' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='860' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='861' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='862' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='863' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='864' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='865' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='866' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='867' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='868' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='869' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='870' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='871' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='872' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='873' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='874' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='875' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='876' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='877' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='878' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='879' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='880' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='881' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='882' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='883' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='884' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='885' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='886' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='887' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='888' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='889' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='890' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='891' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='892' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='893' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='894' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='895' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='896' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='897' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='898' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='899' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='900' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='901' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='902' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='903' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='904' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='905' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='906' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='907' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='908' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='909' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='910' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='911' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='912' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='913' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='914' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='915' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='916' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='917' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='918' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='919' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='920' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='921' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='922' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='923' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='924' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='925' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='926' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='927' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='928' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='929' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='930' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='931' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='932' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='933' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='934' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='935' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='936' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='937' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='938' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='939' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='940' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='941' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='942' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='943' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='944' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='945' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='946' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='947' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='948' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='949' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='950' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='951' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='952' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='953' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='954' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='955' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='956' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='957' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='958' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='959' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='960' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='961' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='962' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='963' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='964' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='965' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='966' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='967' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='968' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='969' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='970' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='971' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='972' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='973' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='974' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='975' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='976' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='977' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='978' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='979' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='980' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='981' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='982' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='983' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='984' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='985' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='986' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='987' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='988' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='989' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='990' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='991' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='992' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='993' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='994' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='995' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='996' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='997' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='998' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='999' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='001' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='002' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='003' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='004' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='005' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='006' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='007' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='008' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='009' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='010' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='011' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='U11' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='012' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='013' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='014' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='015' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='016' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='017' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='018' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='019' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='020' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='021' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='022' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='023' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='024' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='025' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='026' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='027' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='028' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='029' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='030' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='031' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='032' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='033' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='034' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='035' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='036' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='037' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='038' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='039' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='040' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='041' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='042' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='043' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='044' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='045' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='046' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='047' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='048' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='049' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='050' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='051' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='052' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='053' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='054' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='055' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='056' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='057' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='058' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='059' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='060' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='061' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='062' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='063' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='064' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='065' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='066' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='067' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='068' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='069' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='070' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='071' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='072' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='073' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='074' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='075' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='076' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='077' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='078' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='079' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='080' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='081' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='082' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='083' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='084' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='085' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='086' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='087' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='088' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='089' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='090' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='091' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='092' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='093' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='094' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='095' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='096' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='097' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='098' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='099' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='100' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='101' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='102' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='103' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='104' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='105' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='106' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='107' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='108' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='109' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='110' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='111' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='112' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='113' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='114' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='115' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='116' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='117' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='118' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='119' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='120' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='121' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='122' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='123' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='124' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='125' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='126' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='127' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='128' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='129' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='130' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='131' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='132' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='133' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='134' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='135' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='136' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='137' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='138' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='139' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='140' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='141' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='142' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='143' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='144' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='145' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='146' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='147' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='148' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='149' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='150' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='151' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='152' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='153' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='154' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='155' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='156' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='157' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='158' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='159' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='160' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='161' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='162' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='163' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='164' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='165' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='166' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='167' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='168' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='169' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='170' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='171' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='172' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='173' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='174' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='175' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='176' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='177' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='178' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='179' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='180' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='181' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='182' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='183' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='184' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='185' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='186' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='187' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='188' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='189' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='190' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='191' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='192' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='193' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='194' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='195' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='196' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='197' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='198' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='199' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='200' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='201' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='202' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='203' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='204' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='205' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='206' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='207' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='208' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='209' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='210' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='211' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='212' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='213' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='214' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='215' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='216' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='217' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='218' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='219' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='220' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='221' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='222' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='223' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='224' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='225' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='226' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='227' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='228' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='229' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='230' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='231' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='232' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='233' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='234' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='235' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='236' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='237' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='238' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='239' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='240' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='241' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='242' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='243' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='244' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='245' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='246' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='247' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='248' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='249' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='250' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='251' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='252' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='253' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='254' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='255' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='256' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='257' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='258' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='259' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='260' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='261' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='262' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='263' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='264' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='265' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='266' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='267' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='268' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='269' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='270' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='271' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='272' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='273' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='274' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='275' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='276' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='277' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='278' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='279' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='280' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='281' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='282' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='283' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='284' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='285' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='286' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='287' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='288' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='289' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='290' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='291' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='292' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='293' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='294' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='295' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='296' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='297' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='298' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='299' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='300' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='301' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='302' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='303' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='304' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='305' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='306' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='307' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='308' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='309' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='310' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='311' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='312' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='313' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='314' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='315' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='316' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='317' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='318' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='319' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='320' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='321' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='322' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='323' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='324' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='325' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='326' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='327' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='328' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='329' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='330' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='331' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='332' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='333' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='334' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='335' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='336' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='337' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='338' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='339' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='340' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='341' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='342' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='343' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='344' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='345' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='346' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='347' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='348' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='349' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='350' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='351' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='352' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='353' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='354' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='355' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='356' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='357' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='358' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='359' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='360' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='361' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='362' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='363' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='364' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='365' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='366' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='367' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='368' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='369' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='370' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='371' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='372' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='373' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='374' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='375' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='376' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='377' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='378' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='379' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='380' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='381' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='382' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='383' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='384' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='385' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='386' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='387' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='388' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='389' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='390' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='391' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='392' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='393' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='394' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='395' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='396' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='397' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='398' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='399' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='400' :
			chk1 = True

		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='401' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='402' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='403' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='404' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='405' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='406' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='407' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='408' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='409' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='410' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='411' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='412' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='413' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='414' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='415' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='416' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='417' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='418' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='419' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='420' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='421' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='422' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='423' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='424' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='425' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='426' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='427' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='428' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='429' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='430' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='431' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='432' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='433' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='434' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='435' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='436' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='437' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='438' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='439' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='440' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='441' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='442' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='443' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='444' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='445' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='446' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='447' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='448' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='449' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='450' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='451' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='452' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='453' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='454' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='455' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='456' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='457' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='458' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='459' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='460' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='461' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='462' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='463' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='464' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='465' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='466' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='467' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='468' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='469' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='470' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='471' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='472' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='473' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='474' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='475' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='476' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='477' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='478' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='479' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='480' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='481' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='482' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='483' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='484' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='485' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='486' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='487' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='488' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='489' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='490' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='491' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='492' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='493' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='494' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='495' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='496' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='497' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='498' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='499' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='500' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='501' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='502' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='503' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='504' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='505' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='506' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='507' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='508' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='509' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='510' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='511' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='512' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='513' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='514' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='515' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='516' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='517' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='518' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='519' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='520' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='521' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='522' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='523' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='524' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='525' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='526' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='527' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='528' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='529' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='530' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='531' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='532' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='533' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='534' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='535' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='536' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='537' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='538' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='539' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='540' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='541' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='542' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='543' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='544' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='545' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='546' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='547' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='548' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='549' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='550' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='551' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='552' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='553' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='554' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='555' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='556' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='557' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='558' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='559' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='560' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='561' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='562' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='563' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='564' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='565' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='566' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='567' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='568' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='569' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='570' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='571' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='572' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='573' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='574' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='575' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='576' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='577' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='578' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='579' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='580' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='581' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='582' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='583' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='584' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='585' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='586' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='587' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='588' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='589' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='590' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='591' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='592' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='593' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='594' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='595' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='596' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='597' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='598' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='599' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='600' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='601' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='602' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='603' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='604' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='605' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='606' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='607' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='608' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='609' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='610' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='611' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='612' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='613' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='614' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='615' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='616' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='617' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='618' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='619' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='620' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='621' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='622' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='623' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='624' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='625' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='626' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='627' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='628' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='629' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='630' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='631' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='632' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='633' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='634' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='635' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='636' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='637' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='638' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='639' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='640' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='641' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='642' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='643' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='644' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='645' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='646' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='647' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='648' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='649' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='650' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='651' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='652' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='653' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='654' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='655' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='656' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='657' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='658' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='659' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='660' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='661' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='662' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='663' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='664' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='665' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='666' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='667' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='668' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='669' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='670' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='671' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='672' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='673' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='674' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='675' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='676' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='677' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='678' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='679' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='680' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='681' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='682' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='683' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='684' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='685' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='686' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='687' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='688' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='689' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='690' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='691' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='692' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='693' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='694' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='695' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='696' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='697' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='698' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='699' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='700' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='701' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='702' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='703' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='704' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='705' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='706' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='707' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='708' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='709' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='710' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='711' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='712' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='713' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='714' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='715' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='716' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='717' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='718' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='719' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='720' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='721' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='722' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='723' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='724' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='725' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='726' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='727' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='728' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='729' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='730' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='731' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='732' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='733' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='734' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='735' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='736' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='737' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='738' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='739' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='740' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='741' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='742' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='743' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='744' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='745' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='746' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='747' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='748' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='749' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='750' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='751' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='752' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='753' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='754' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='755' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='756' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='757' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='758' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='759' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='760' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='761' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='762' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='763' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='764' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='765' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='766' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='767' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='768' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='769' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='770' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='771' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='772' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='773' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='774' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='775' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='776' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='777' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='778' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='779' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='780' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='781' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='782' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='783' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='784' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='785' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='786' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='787' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='788' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='789' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='790' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='791' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='792' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='793' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='794' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='795' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='796' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='797' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='798' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='799' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='800' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='801' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='802' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='803' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='804' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='805' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='806' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='807' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='808' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='809' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='810' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='811' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='812' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='813' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='814' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='815' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='816' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='817' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='818' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='819' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='820' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='821' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='822' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='823' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='824' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='825' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='826' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='827' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='828' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='829' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='830' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='831' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='832' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='833' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='834' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='835' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='836' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='837' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='838' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='839' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='840' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='841' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='842' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='843' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='844' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='845' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='846' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='847' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='848' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='849' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='850' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='851' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='852' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='853' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='854' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='855' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='856' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='857' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='858' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='859' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='860' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='861' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='862' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='863' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='864' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='865' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='866' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='867' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='868' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='869' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='870' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='871' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='872' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='873' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='874' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='875' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='876' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='877' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='878' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='879' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='880' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='881' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='882' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='883' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='884' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='885' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='886' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='887' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='888' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='889' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='890' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='891' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='892' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='893' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='894' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='895' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='896' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='897' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='898' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='899' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='900' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='901' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='902' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='903' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='904' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='905' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='906' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='907' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='908' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='909' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='910' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='911' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='912' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='913' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='914' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='915' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='916' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='917' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='918' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='919' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='920' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='921' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='922' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='923' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='924' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='925' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='926' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='927' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='928' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='929' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='930' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='931' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='932' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='933' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='934' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='935' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='936' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='937' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='938' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='939' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='940' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='941' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='942' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='943' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='944' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='945' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='946' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='947' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='948' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='949' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='950' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='951' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='952' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='953' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='954' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='955' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='956' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='957' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='958' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='959' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='960' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='961' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='962' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='963' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='964' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='965' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='966' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='967' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='968' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='969' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='970' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='971' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='972' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='973' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='974' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='975' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='976' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='977' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='978' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='979' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='980' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='981' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='982' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='983' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='984' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='985' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='986' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='987' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='988' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='989' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='990' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='991' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='992' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='993' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='994' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='995' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='996' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='997' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='998' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='999' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='001' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='002' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='003' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='004' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='005' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='006' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='007' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='008' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='009' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='010' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='011' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='U11' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='012' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='013' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='014' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='015' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='016' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='017' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='018' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='019' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='020' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='021' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='022' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='023' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='024' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='025' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='026' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='027' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='028' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='029' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='030' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='031' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='032' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='033' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='034' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='035' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='036' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='037' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='038' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='039' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='040' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='041' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='042' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='043' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='044' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='045' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='046' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='047' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='048' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='049' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='050' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='051' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='052' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='053' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='054' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='055' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='056' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='057' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='058' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='059' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='060' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='061' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='062' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='063' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='064' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='065' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='066' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='067' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='068' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='069' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='070' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='071' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='072' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='073' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='074' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='075' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='076' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='077' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='078' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='079' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='080' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='081' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='082' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='083' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='084' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='085' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='086' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='087' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='088' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='089' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='090' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='091' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='092' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='093' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='094' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='095' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='096' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='097' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='098' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='099' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='100' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='101' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='102' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='103' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='104' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='105' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='106' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='107' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='108' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='109' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='110' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='111' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='112' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='113' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='114' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='115' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='116' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='117' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='118' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='119' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='120' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='121' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='122' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='123' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='124' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='125' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='126' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='127' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='128' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='129' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='130' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='131' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='132' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='133' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='134' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='135' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='136' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='137' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='138' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='139' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='140' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='141' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='142' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='143' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='144' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='145' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='146' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='147' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='148' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='149' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='150' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='151' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='152' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='153' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='154' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='155' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='156' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='157' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='158' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='159' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='160' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='161' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='162' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='163' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='164' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='165' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='166' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='167' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='168' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='169' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='170' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='171' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='172' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='173' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='174' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='175' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='176' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='177' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='178' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='179' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='180' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='181' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='182' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='183' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='184' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='185' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='186' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='187' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='188' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='189' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='190' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='191' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='192' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='193' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='194' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='195' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='196' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='197' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='198' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='199' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='200' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='201' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='202' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='203' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='204' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='205' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='206' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='207' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='208' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='209' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='210' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='211' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='212' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='213' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='214' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='215' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='216' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='217' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='218' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='219' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='220' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='221' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='222' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='223' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='224' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='225' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='226' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='227' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='228' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='229' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='230' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='231' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='232' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='233' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='234' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='235' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='236' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='237' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='238' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='239' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='240' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='241' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='242' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='243' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='244' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='245' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='246' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='247' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='248' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='249' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='250' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='251' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='252' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='253' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='254' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='255' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='256' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='257' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='258' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='259' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='260' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='261' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='262' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='263' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='264' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='265' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='266' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='267' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='268' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='269' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='270' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='271' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='272' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='273' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='274' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='275' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='276' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='277' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='278' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='279' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='280' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='281' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='282' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='283' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='284' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='285' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='286' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='287' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='288' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='289' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='290' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='291' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='292' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='293' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='294' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='295' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='296' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='297' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='298' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='299' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='300' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='301' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='302' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='303' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='304' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='305' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='306' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='307' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='308' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='309' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='310' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='311' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='312' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='313' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='314' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='315' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='316' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='317' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='318' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='319' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='320' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='321' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='322' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='323' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='324' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='325' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='326' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='327' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='328' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='329' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='330' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='331' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='332' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='333' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='334' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='335' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='336' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='337' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='338' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='339' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='340' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='341' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='342' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='343' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='344' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='345' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='346' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='347' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='348' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='349' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='350' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='351' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='352' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='353' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='354' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='355' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='356' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='357' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='358' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='359' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='360' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='361' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='362' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='363' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='364' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='365' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='366' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='367' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='368' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='369' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='370' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='371' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='372' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='373' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='374' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='375' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='376' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='377' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='378' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='379' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='380' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='381' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='382' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='383' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='384' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='385' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='386' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='387' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='388' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='389' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='390' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='391' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='392' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='393' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='394' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='395' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='396' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='397' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='398' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='399' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='400' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='401' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='402' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='403' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='404' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='405' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='406' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='407' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='408' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='409' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='410' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='411' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='412' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='413' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='414' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='415' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='416' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='417' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='418' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='419' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='420' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='421' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='422' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='423' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='424' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='425' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='426' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='427' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='428' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='429' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='430' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='431' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='432' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='433' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='434' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='435' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='436' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='437' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='438' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='439' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='440' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='441' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='442' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='443' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='444' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='445' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='446' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='447' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='448' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='449' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='450' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='451' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='452' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='453' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='454' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='455' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='456' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='457' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='458' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='459' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='460' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='461' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='462' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='463' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='464' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='465' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='466' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='467' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='468' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='469' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='470' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='471' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='472' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='473' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='474' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='475' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='476' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='477' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='478' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='479' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='480' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='481' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='482' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='483' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='484' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='485' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='486' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='487' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='488' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='489' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='490' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='491' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='492' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='493' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='494' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='495' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='496' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='497' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='498' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='499' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='500' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='501' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='502' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='503' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='504' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='505' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='506' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='507' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='508' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='509' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='510' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='511' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='512' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='513' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='514' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='515' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='516' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='517' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='518' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='519' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='520' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='521' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='522' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='523' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='524' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='525' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='526' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='527' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='528' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='529' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='530' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='531' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='532' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='533' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='534' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='535' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='536' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='537' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='538' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='539' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='540' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='541' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='542' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='543' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='544' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='545' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='546' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='547' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='548' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='549' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='550' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='551' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='552' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='553' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='554' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='555' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='556' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='557' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='558' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='559' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='560' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='561' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='562' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='563' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='564' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='565' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='566' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='567' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='568' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='569' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='570' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='571' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='572' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='573' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='574' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='575' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='576' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='577' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='578' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='579' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='580' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='581' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='582' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='583' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='584' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='585' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='586' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='587' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='588' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='589' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='590' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='591' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='592' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='593' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='594' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='595' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='596' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='597' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='598' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='599' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='600' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='601' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='602' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='603' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='604' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='605' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='606' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='607' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='608' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='609' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='610' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='611' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='612' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='613' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='614' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='615' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='616' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='617' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='618' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='619' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='620' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='621' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='622' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='623' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='624' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='625' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='626' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='627' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='628' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='629' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='630' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='631' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='632' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='633' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='634' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='635' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='636' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='637' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='638' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='639' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='640' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='641' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='642' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='643' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='644' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='645' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='646' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='647' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='648' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='649' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='650' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='651' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='652' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='653' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='654' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='655' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='656' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='657' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='658' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='659' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='660' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='661' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='662' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='663' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='664' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='665' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='666' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='667' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='668' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='669' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='670' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='671' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='672' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='673' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='674' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='675' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='676' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='677' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='678' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='679' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='680' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='681' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='682' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='683' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='684' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='685' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='686' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='687' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='688' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='689' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='690' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='691' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='692' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='693' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='694' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='695' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='696' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='697' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='698' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='699' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='700' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='701' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='702' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='703' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='704' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='705' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='706' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='707' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='708' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='709' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='710' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='711' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='712' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='713' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='714' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='715' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='716' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='717' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='718' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='719' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='720' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='721' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='722' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='723' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='724' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='725' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='726' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='727' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='728' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='729' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='730' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='731' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='732' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='733' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='734' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='735' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='736' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='737' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='738' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='739' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='740' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='741' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='742' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='743' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='744' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='745' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='746' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='747' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='748' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='749' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='750' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='751' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='752' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='753' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='754' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='755' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='756' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='757' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='758' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='759' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='760' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='761' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='762' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='763' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='764' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='765' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='766' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='767' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='768' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='769' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='770' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='771' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='772' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='773' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='774' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='775' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='776' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='777' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='778' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='779' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='780' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='781' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='782' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='783' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='784' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='785' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='786' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='787' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='788' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='789' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='790' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='791' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='792' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='793' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='794' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='795' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='796' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='797' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='798' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='799' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='800' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='801' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='802' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='803' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='804' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='805' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='806' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='807' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='808' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='809' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='810' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='811' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='812' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='813' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='814' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='815' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='816' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='817' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='818' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='819' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='820' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='821' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='822' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='823' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='824' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='825' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='826' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='827' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='828' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='829' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='830' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='831' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='832' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='833' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='834' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='835' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='836' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='837' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='838' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='839' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='840' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='841' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='842' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='843' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='844' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='845' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='846' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='847' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='848' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='849' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='850' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='851' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='852' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='853' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='854' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='855' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='856' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='857' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='858' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='859' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='860' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='861' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='862' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='863' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='864' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='865' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='866' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='867' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='868' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='869' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='870' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='871' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='872' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='873' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='874' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='875' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='876' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='877' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='878' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='879' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='880' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='881' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='882' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='883' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='884' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='885' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='886' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='887' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='888' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='889' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='890' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='891' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='892' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='893' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='894' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='895' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='896' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='897' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='898' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='899' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='900' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='901' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='902' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='903' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='904' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='905' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='906' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='907' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='908' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='909' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='910' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='911' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='912' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='913' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='914' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='915' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='916' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='917' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='918' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='919' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='920' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='921' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='922' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='923' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='924' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='925' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='926' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='927' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='928' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='929' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='930' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='931' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='932' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='933' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='934' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='935' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='936' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='937' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='938' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='939' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='940' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='941' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='942' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='943' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='944' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='945' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='946' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='947' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='948' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='949' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='950' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='951' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='952' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='953' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='954' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='955' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='956' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='957' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='958' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='959' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='960' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='961' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='962' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='963' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='964' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='965' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='966' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='967' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='968' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='969' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='970' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='971' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='972' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='973' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='974' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='975' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='976' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='977' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='978' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='979' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='980' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='981' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='982' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='983' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='984' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='985' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='986' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='987' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='988' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='989' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='990' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='991' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='992' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='993' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='994' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='995' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='996' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='997' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='998' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='LAB' and fm[2]=='999' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='001' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='002' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='003' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='004' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='005' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='006' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='007' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='008' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='009' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='010' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='011' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='U11' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='012' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='013' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='014' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='015' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='016' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='017' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='018' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='019' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='020' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='021' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='022' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='023' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='024' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='025' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='026' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='027' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='028' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='029' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='030' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='031' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='032' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='033' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='034' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='035' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='036' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='037' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='038' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='039' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='040' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='041' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='042' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='043' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='044' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='045' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='046' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='047' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='048' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='049' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='050' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='051' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='052' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='053' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='054' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='055' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='056' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='057' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='058' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='059' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='060' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='061' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='062' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='063' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='064' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='065' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='066' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='067' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='068' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='069' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='070' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='071' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='072' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='073' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='074' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='075' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='076' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='077' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='078' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='079' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='080' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='081' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='082' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='083' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='084' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='085' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='086' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='087' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='088' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='089' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='090' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='091' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='092' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='093' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='094' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='095' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='096' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='097' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='098' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='099' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='100' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='101' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='102' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='103' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='104' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='105' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='106' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='107' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='108' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='109' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='110' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='111' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='112' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='113' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='114' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='115' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='116' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='117' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='118' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='119' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='120' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='121' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='122' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='123' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='124' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='125' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='126' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='127' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='128' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='129' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='130' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='131' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='132' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='133' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='134' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='135' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='136' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='137' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='138' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='139' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='140' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='141' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='142' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='143' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='144' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='145' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='146' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='147' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='148' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='149' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='150' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='151' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='152' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='153' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='154' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='155' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='156' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='157' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='158' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='159' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='160' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='161' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='162' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='163' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='164' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='165' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='166' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='167' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='168' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='169' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='170' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='171' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='172' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='173' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='174' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='175' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='176' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='177' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='178' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='179' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='180' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='181' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='182' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='183' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='184' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='185' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='186' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='187' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='188' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='189' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='190' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='191' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='192' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='193' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='194' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='195' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='196' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='197' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='198' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='199' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='200' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='201' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='202' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='203' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='204' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='205' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='206' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='207' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='208' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='209' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='210' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='211' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='212' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='213' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='214' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='215' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='216' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='217' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='218' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='219' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='220' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='221' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='222' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='223' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='224' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='225' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='226' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='227' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='228' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='229' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='230' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='231' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='232' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='233' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='234' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='235' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='236' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='237' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='238' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='239' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='240' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='241' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='242' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='243' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='244' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='245' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='246' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='247' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='248' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='249' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='250' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='251' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='252' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='253' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='254' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='255' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='256' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='257' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='258' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='259' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='260' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='261' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='262' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='263' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='264' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='265' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='266' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='267' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='268' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='269' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='270' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='271' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='272' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='273' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='274' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='275' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='276' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='277' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='278' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='279' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='280' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='281' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='282' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='283' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='284' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='285' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='286' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='287' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='288' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='289' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='290' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='291' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='292' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='293' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='294' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='295' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='296' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='297' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='298' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='299' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='300' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='301' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='302' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='303' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='304' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='305' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='306' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='307' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='308' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='309' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='310' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='311' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='312' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='313' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='314' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='315' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='316' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='317' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='318' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='319' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='320' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='321' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='322' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='323' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='324' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='325' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='326' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='327' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='328' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='329' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='330' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='331' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='332' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='333' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='334' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='335' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='336' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='337' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='338' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='339' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='340' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='341' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='342' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='343' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='344' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='345' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='346' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='347' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='348' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='349' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='350' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='351' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='352' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='353' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='354' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='355' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='356' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='357' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='358' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='359' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='360' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='361' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='362' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='363' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='364' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='365' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='366' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='367' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='368' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='369' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='370' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='371' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='372' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='373' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='374' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='375' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='376' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='377' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='378' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='379' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='380' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='381' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='382' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='383' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='384' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='385' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='386' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='387' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='388' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='389' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='390' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='391' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='392' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='393' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='394' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='395' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='396' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='397' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='398' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='399' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='400' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='401' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='402' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='403' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='404' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='405' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='406' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='407' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='408' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='409' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='410' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='411' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='412' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='413' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='414' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='415' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='416' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='417' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='418' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='419' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='420' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='421' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='422' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='423' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='424' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='425' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='426' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='427' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='428' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='429' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='430' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='431' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='432' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='433' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='434' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='435' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='436' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='437' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='438' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='439' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='440' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='441' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='442' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='443' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='444' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='445' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='446' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='447' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='448' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='449' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='450' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='451' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='452' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='453' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='454' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='455' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='456' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='457' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='458' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='459' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='460' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='461' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='462' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='463' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='464' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='465' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='466' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='467' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='468' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='469' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='470' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='471' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='472' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='473' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='474' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='475' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='476' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='477' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='478' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='479' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='480' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='481' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='482' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='483' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='484' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='485' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='486' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='487' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='488' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='489' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='490' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='491' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='492' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='493' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='494' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='495' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='496' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='497' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='498' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='499' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='500' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='501' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='502' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='503' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='504' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='505' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='506' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='507' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='508' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='509' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='510' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='511' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='512' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='513' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='514' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='515' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='516' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='517' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='518' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='519' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='520' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='521' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='522' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='523' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='524' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='525' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='526' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='527' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='528' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='529' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='530' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='531' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='532' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='533' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='534' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='535' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='536' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='537' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='538' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='539' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='540' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='541' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='542' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='543' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='544' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='545' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='546' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='547' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='548' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='549' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='550' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='551' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='552' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='553' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='554' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='555' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='556' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='557' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='558' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='559' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='560' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='561' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='562' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='563' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='564' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='565' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='566' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='567' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='568' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='569' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='570' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='571' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='572' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='573' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='574' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='575' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='576' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='577' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='578' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='579' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='580' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='581' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='582' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='583' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='584' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='585' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='586' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='587' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='588' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='589' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='590' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='591' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='592' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='593' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='594' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='595' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='596' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='597' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='598' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='599' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='600' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='601' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='602' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='603' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='604' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='605' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='606' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='607' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='608' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='609' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='610' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='611' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='612' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='613' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='614' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='615' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='616' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='617' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='618' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='619' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='620' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='621' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='622' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='623' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='624' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='625' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='626' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='627' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='628' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='629' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='630' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='631' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='632' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='633' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='634' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='635' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='636' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='637' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='638' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='639' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='640' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='641' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='642' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='643' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='644' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='645' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='646' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='647' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='648' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='649' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='650' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='651' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='652' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='653' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='654' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='655' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='656' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='657' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='658' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='659' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='660' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='661' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='662' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='663' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='664' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='665' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='666' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='667' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='668' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='669' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='670' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='671' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='672' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='673' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='674' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='675' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='676' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='677' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='678' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='679' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='680' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='681' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='682' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='683' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='684' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='685' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='686' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='687' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='688' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='689' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='690' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='691' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='692' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='693' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='694' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='695' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='696' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='697' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='698' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='699' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='700' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='701' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='702' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='703' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='704' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='705' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='706' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='707' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='708' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='709' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='710' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='711' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='712' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='713' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='714' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='715' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='716' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='717' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='718' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='719' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='720' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='721' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='722' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='723' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='724' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='725' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='726' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='727' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='728' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='729' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='730' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='731' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='732' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='733' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='734' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='735' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='736' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='737' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='738' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='739' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='740' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='741' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='742' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='743' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='744' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='745' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='746' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='747' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='748' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='749' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='750' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='751' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='752' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='753' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='754' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='755' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='756' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='757' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='758' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='759' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='760' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='761' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='762' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='763' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='764' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='765' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='766' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='767' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='768' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='769' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='770' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='771' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='772' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='773' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='774' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='775' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='776' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='777' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='778' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='779' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='780' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='781' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='782' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='783' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='784' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='785' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='786' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='787' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='788' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='789' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='790' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='791' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='792' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='793' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='794' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='795' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='796' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='797' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='798' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='799' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='800' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='801' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='802' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='803' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='804' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='805' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='806' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='807' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='808' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='809' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='810' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='811' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='812' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='813' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='814' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='815' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='816' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='817' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='818' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='819' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='820' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='821' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='822' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='823' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='824' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='825' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='826' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='827' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='828' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='829' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='830' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='831' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='832' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='833' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='834' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='835' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='836' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='837' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='838' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='839' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='840' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='841' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='842' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='843' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='844' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='845' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='846' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='847' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='848' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='849' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='850' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='851' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='852' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='853' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='854' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='855' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='856' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='857' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='858' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='859' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='860' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='861' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='862' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='863' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='864' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='865' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='866' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='867' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='868' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='869' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='870' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='871' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='872' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='873' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='874' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='875' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='876' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='877' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='878' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='879' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='880' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='881' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='882' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='883' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='884' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='885' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='886' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='887' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='888' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='889' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='890' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='891' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='892' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='893' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='894' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='895' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='896' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='897' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='898' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='899' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='900' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='901' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='902' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='903' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='904' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='905' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='906' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='907' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='908' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='909' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='910' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='911' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='912' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='913' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='914' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='915' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='916' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='917' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='918' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='919' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='920' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='921' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='922' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='923' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='924' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='925' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='926' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='927' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='928' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='929' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='930' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='931' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='932' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='933' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='934' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='935' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='936' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='937' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='938' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='939' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='940' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='941' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='942' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='943' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='944' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='945' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='946' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='947' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='948' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='949' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='950' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='951' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='952' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='953' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='954' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='955' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='956' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='957' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='958' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='959' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='960' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='961' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='962' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='963' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='964' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='965' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='966' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='967' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='968' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='969' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='970' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='971' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='972' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='973' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='974' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='975' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='976' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='977' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='978' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='979' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='980' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='981' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='982' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='983' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='984' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='985' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='986' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='987' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='988' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='989' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='990' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='991' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='992' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='993' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='994' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='995' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='996' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='997' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='998' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='AMS' and fm[2]=='999' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='001' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='002' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='003' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='004' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='005' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='006' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='007' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='008' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='009' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='010' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='011' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='U11' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='012' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='013' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='014' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='015' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='016' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='017' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='018' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='019' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='020' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='021' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='022' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='023' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='024' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='025' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='026' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='027' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='028' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='029' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='030' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='031' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='032' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='033' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='034' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='035' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='036' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='037' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='038' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='039' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='040' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='041' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='042' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='043' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='044' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='045' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='046' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='047' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='048' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='049' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='050' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='051' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='052' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='053' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='054' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='055' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='056' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='057' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='058' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='059' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='060' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='061' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='062' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='063' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='064' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='065' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='066' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='067' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='068' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='069' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='070' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='071' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='072' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='073' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='074' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='075' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='076' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='077' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='078' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='079' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='080' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='081' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='082' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='083' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='084' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='085' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='086' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='087' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='088' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='089' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='090' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='091' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='092' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='093' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='094' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='095' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='096' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='097' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='098' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='099' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='100' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='101' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='102' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='103' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='104' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='105' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='106' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='107' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='108' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='109' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='110' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='111' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='112' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='113' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='114' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='115' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='116' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='117' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='118' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='119' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='120' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='121' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='122' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='123' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='124' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='125' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='126' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='127' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='128' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='129' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='130' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='131' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='132' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='133' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='134' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='135' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='136' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='137' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='138' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='139' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='140' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='141' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='142' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='143' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='144' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='145' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='146' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='147' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='148' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='149' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='150' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='151' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='152' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='153' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='154' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='155' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='156' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='157' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='158' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='159' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='160' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='161' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='162' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='163' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='164' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='165' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='166' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='167' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='168' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='169' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='170' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='171' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='172' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='173' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='174' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='175' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='176' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='177' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='178' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='179' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='180' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='181' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='182' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='183' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='184' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='185' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='186' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='187' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='188' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='189' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='190' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='191' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='192' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='193' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='194' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='195' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='196' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='197' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='198' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='199' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='200' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='201' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='202' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='203' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='204' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='205' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='206' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='207' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='208' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='209' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='210' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='211' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='212' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='213' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='214' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='215' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='216' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='217' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='218' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='219' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='220' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='221' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='222' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='223' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='224' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='225' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='226' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='227' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='228' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='229' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='230' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='231' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='232' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='233' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='234' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='235' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='236' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='237' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='238' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='239' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='240' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='241' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='242' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='243' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='244' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='245' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='246' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='247' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='248' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='249' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='250' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='251' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='252' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='253' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='254' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='255' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='256' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='257' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='258' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='259' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='260' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='261' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='262' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='263' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='264' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='265' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='266' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='267' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='268' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='269' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='270' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='271' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='272' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='273' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='274' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='275' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='276' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='277' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='278' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='279' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='280' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='281' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='282' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='283' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='284' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='285' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='286' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='287' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='288' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='289' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='290' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='291' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='292' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='293' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='294' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='295' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='296' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='297' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='298' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='299' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='300' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='301' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='302' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='303' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='304' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='305' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='306' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='307' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='308' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='309' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='310' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='311' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='312' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='313' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='314' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='315' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='316' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='317' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='318' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='319' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='320' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='321' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='322' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='323' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='324' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='325' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='326' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='327' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='328' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='329' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='330' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='331' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='332' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='333' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='334' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='335' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='336' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='337' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='338' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='339' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='340' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='341' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='342' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='343' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='344' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='345' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='346' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='347' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='348' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='349' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='350' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='351' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='352' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='353' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='354' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='355' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='356' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='357' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='358' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='359' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='360' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='361' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='362' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='363' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='364' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='365' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='366' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='367' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='368' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='369' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='370' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='371' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='372' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='373' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='374' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='375' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='376' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='377' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='378' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='379' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='380' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='381' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='382' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='383' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='384' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='385' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='386' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='387' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='388' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='389' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='390' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='391' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='392' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='393' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='394' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='395' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='396' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='397' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='398' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='399' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='400' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='401' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='402' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='403' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='404' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='405' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='406' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='407' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='408' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='409' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='410' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='411' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='412' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='413' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='414' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='415' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='416' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='417' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='418' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='419' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='420' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='421' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='422' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='423' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='424' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='425' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='426' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='427' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='428' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='429' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='430' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='431' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='432' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='433' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='434' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='435' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='436' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='437' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='438' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='439' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='440' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='441' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='442' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='443' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='444' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='445' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='446' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='447' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='448' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='449' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='450' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='451' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='452' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='453' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='454' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='455' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='456' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='457' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='458' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='459' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='460' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='461' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='462' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='463' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='464' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='465' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='466' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='467' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='468' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='469' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='470' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='471' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='472' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='473' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='474' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='475' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='476' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='477' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='478' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='479' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='480' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='481' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='482' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='483' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='484' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='485' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='486' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='487' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='488' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='489' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='490' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='491' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='492' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='493' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='494' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='495' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='496' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='497' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='498' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='499' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='500' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='501' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='502' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='503' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='504' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='505' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='506' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='507' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='508' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='509' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='510' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='511' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='512' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='513' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='514' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='515' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='516' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='517' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='518' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='519' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='520' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='521' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='522' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='523' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='524' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='525' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='526' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='527' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='528' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='529' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='530' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='531' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='532' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='533' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='534' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='535' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='536' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='537' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='538' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='539' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='540' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='541' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='542' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='543' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='544' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='545' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='546' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='547' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='548' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='549' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='550' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='551' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='552' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='553' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='554' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='555' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='556' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='557' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='558' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='559' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='560' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='561' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='562' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='563' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='564' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='565' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='566' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='567' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='568' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='569' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='570' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='571' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='572' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='573' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='574' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='575' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='576' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='577' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='578' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='579' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='580' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='581' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='582' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='583' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='584' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='585' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='586' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='587' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='588' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='589' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='590' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='591' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='592' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='593' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='594' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='595' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='596' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='597' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='598' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='599' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='600' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='601' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='602' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='603' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='604' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='605' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='606' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='607' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='608' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='609' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='610' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='611' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='612' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='613' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='614' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='615' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='616' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='617' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='618' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='619' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='620' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='621' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='622' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='623' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='624' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='625' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='626' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='627' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='628' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='629' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='630' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='631' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='632' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='633' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='634' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='635' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='636' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='637' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='638' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='639' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='640' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='641' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='642' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='643' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='644' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='645' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='646' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='647' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='648' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='649' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='650' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='651' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='652' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='653' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='654' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='655' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='656' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='657' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='658' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='659' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='660' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='661' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='662' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='663' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='664' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='665' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='666' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='667' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='668' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='669' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='670' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='671' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='672' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='673' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='674' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='675' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='676' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='677' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='678' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='679' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='680' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='681' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='682' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='683' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='684' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='685' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='686' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='687' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='688' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='689' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='690' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='691' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='692' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='693' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='694' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='695' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='696' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='697' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='698' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='699' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='700' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='701' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='702' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='703' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='704' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='705' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='706' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='707' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='708' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='709' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='710' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='711' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='712' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='713' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='714' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='715' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='716' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='717' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='718' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='719' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='720' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='721' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='722' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='723' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='724' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='725' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='726' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='727' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='728' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='729' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='730' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='731' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='732' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='733' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='734' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='735' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='736' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='737' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='738' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='739' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='740' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='741' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='742' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='743' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='744' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='745' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='746' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='747' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='748' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='749' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='750' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='751' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='752' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='753' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='754' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='755' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='756' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='757' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='758' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='759' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='760' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='761' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='762' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='763' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='764' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='765' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='766' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='767' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='768' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='769' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='770' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='771' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='772' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='773' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='774' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='775' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='776' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='777' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='778' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='779' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='780' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='781' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='782' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='783' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='784' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='785' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='786' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='787' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='788' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='789' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='790' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='791' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='792' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='793' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='794' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='795' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='796' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='797' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='798' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='799' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='800' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='801' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='802' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='803' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='804' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='805' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='806' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='807' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='808' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='809' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='810' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='811' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='812' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='813' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='814' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='815' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='816' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='817' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='818' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='819' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='820' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='821' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='822' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='823' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='824' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='825' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='826' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='827' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='828' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='829' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='830' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='831' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='832' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='833' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='834' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='835' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='836' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='837' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='838' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='839' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='840' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='841' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='842' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='843' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='844' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='845' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='846' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='847' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='848' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='849' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='850' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='851' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='852' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='853' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='854' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='855' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='856' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='857' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='858' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='859' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='860' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='861' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='862' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='863' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='864' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='865' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='866' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='867' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='868' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='869' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='870' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='871' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='872' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='873' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='874' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='875' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='876' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='877' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='878' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='879' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='880' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='881' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='882' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='883' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='884' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='885' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='886' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='887' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='888' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='889' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='890' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='891' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='892' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='893' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='894' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='895' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='896' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='897' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='898' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='899' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='900' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='901' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='902' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='903' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='904' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='905' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='906' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='907' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='908' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='909' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='910' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='911' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='912' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='913' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='914' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='915' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='916' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='917' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='918' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='919' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='920' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='921' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='922' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='923' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='924' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='925' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='926' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='927' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='928' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='929' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='930' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='931' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='932' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='933' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='934' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='935' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='936' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='937' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='938' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='939' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='940' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='941' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='942' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='943' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='944' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='945' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='946' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='947' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='948' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='949' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='950' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='951' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='952' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='953' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='954' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='955' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='956' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='957' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='958' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='959' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='960' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='961' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='962' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='963' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='964' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='965' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='966' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='967' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='968' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='969' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='970' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='971' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='972' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='973' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='974' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='975' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='976' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='977' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='978' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='979' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='980' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='981' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='982' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='983' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='984' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='985' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='986' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='987' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='988' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='989' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='990' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='991' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='992' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='993' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='994' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='995' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='996' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='997' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='998' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='XRT' and fm[2]=='999' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='001' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='002' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='003' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='004' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='005' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='006' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='007' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='008' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='009' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='010' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='011' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='U11' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='012' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='013' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='014' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='015' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='016' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='017' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='018' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='019' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='020' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='021' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='022' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='023' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='024' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='025' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='026' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='027' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='028' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='029' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='030' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='031' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='032' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='033' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='034' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='035' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='036' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='037' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='038' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='039' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='040' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='041' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='042' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='043' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='044' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='045' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='046' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='047' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='048' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='049' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='050' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='051' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='052' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='053' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='054' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='055' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='056' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='057' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='058' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='059' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='060' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='061' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='062' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='063' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='064' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='065' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='066' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='067' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='068' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='069' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='070' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='071' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='072' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='073' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='074' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='075' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='076' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='077' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='078' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='079' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='080' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='081' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='082' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='083' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='084' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='085' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='086' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='087' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='088' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='089' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='090' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='091' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='092' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='093' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='094' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='095' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='096' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='097' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='098' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='099' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='100' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='101' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='102' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='103' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='104' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='105' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='106' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='107' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='108' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='109' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='110' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='111' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='112' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='113' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='114' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='115' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='116' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='117' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='118' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='119' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='120' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='121' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='122' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='123' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='124' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='125' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='126' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='127' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='128' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='129' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='130' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='131' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='132' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='133' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='134' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='135' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='136' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='137' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='138' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='139' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='140' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='141' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='142' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='143' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='144' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='145' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='146' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='147' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='148' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='149' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='150' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='151' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='152' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='153' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='154' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='155' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='156' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='157' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='158' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='159' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='160' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='161' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='162' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='163' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='164' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='165' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='166' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='167' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='168' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='169' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='170' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='171' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='172' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='173' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='174' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='175' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='176' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='177' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='178' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='179' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='180' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='181' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='182' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='183' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='184' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='185' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='186' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='187' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='188' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='189' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='190' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='191' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='192' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='193' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='194' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='195' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='196' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='197' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='198' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='199' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='200' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='201' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='202' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='203' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='204' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='205' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='206' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='207' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='208' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='209' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='210' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='211' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='212' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='213' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='214' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='215' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='216' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='217' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='218' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='219' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='220' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='221' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='222' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='223' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='224' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='225' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='226' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='227' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='228' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='229' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='230' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='231' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='232' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='233' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='234' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='235' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='236' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='237' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='238' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='239' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='240' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='241' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='242' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='243' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='244' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='245' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='246' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='247' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='248' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='249' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='250' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='251' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='252' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='253' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='254' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='255' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='256' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='257' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='258' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='259' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='260' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='261' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='262' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='263' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='264' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='265' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='266' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='267' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='268' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='269' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='270' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='271' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='272' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='273' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='274' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='275' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='276' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='277' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='278' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='279' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='280' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='281' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='282' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='283' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='284' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='285' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='286' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='287' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='288' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='289' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='290' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='291' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='292' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='293' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='294' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='295' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='296' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='297' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='298' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='299' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='300' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='301' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='302' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='303' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='304' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='305' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='306' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='307' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='308' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='309' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='310' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='311' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='312' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='313' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='314' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='315' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='316' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='317' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='318' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='319' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='320' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='321' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='322' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='323' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='324' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='325' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='326' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='327' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='328' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='329' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='330' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='331' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='332' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='333' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='334' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='335' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='336' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='337' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='338' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='339' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='340' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='341' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='342' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='343' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='344' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='345' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='346' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='347' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='348' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='349' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='350' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='351' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='352' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='353' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='354' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='355' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='356' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='357' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='358' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='359' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='360' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='361' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='362' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='363' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='364' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='365' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='366' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='367' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='368' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='369' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='370' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='371' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='372' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='373' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='374' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='375' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='376' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='377' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='378' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='379' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='380' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='381' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='382' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='383' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='384' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='385' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='386' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='387' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='388' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='389' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='390' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='391' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='392' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='393' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='394' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='395' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='396' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='397' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='398' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='399' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='400' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='401' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='402' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='403' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='404' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='405' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='406' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='407' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='408' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='409' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='410' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='411' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='412' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='413' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='414' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='415' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='416' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='417' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='418' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='419' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='420' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='421' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='422' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='423' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='424' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='425' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='426' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='427' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='428' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='429' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='430' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='431' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='432' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='433' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='434' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='435' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='436' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='437' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='438' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='439' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='440' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='441' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='442' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='443' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='444' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='445' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='446' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='447' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='448' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='449' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='450' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='451' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='452' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='453' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='454' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='455' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='456' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='457' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='458' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='459' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='460' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='461' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='462' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='463' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='464' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='465' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='466' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='467' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='468' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='469' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='470' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='471' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='472' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='473' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='474' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='475' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='476' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='477' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='478' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='479' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='480' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='481' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='482' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='483' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='484' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='485' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='486' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='487' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='488' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='489' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='490' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='491' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='492' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='493' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='494' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='495' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='496' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='497' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='498' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='499' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='500' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='501' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='502' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='503' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='504' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='505' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='506' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='507' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='508' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='509' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='510' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='511' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='512' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='513' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='514' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='515' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='516' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='517' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='518' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='519' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='520' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='521' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='522' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='523' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='524' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='525' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='526' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='527' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='528' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='529' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='530' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='531' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='532' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='533' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='534' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='535' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='536' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='537' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='538' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='539' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='540' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='541' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='542' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='543' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='544' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='545' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='546' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='547' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='548' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='549' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='550' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='551' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='552' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='553' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='554' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='555' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='556' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='557' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='558' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='559' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='560' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='561' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='562' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='563' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='564' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='565' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='566' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='567' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='568' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='569' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='570' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='571' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='572' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='573' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='574' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='575' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='576' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='577' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='578' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='579' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='580' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='581' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='582' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='583' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='584' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='585' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='586' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='587' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='588' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='589' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='590' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='591' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='592' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='593' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='594' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='595' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='596' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='597' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='598' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='599' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='600' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='601' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='602' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='603' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='604' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='605' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='606' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='607' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='608' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='609' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='610' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='611' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='612' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='613' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='614' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='615' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='616' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='617' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='618' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='619' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='620' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='621' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='622' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='623' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='624' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='625' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='626' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='627' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='628' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='629' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='630' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='631' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='632' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='633' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='634' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='635' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='636' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='637' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='638' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='639' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='640' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='641' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='642' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='643' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='644' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='645' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='646' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='647' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='648' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='649' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='650' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='651' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='652' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='653' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='654' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='655' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='656' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='657' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='658' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='659' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='660' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='661' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='662' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='663' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='664' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='665' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='666' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='667' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='668' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='669' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='670' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='671' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='672' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='673' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='674' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='675' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='676' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='677' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='678' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='679' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='680' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='681' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='682' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='683' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='684' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='685' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='686' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='687' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='688' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='689' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='690' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='691' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='692' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='693' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='694' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='695' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='696' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='697' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='698' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='699' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='700' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='701' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='702' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='703' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='704' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='705' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='706' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='707' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='708' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='709' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='710' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='711' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='712' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='713' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='714' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='715' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='716' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='717' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='718' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='719' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='720' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='721' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='722' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='723' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='724' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='725' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='726' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='727' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='728' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='729' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='730' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='731' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='732' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='733' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='734' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='735' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='736' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='737' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='738' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='739' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='740' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='741' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='742' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='743' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='744' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='745' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='746' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='747' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='748' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='749' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='750' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='751' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='752' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='753' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='754' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='755' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='756' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='757' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='758' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='759' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='760' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='761' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='762' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='763' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='764' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='765' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='766' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='767' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='768' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='769' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='770' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='771' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='772' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='773' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='774' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='775' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='776' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='777' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='778' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='779' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='780' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='781' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='782' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='783' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='784' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='785' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='786' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='787' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='788' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='789' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='790' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='791' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='792' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='793' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='794' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='795' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='796' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='797' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='798' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='799' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='800' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='801' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='802' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='803' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='804' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='805' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='806' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='807' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='808' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='809' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='810' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='811' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='812' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='813' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='814' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='815' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='816' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='817' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='818' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='819' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='820' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='821' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='822' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='823' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='824' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='825' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='826' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='827' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='828' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='829' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='830' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='831' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='832' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='833' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='834' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='835' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='836' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='837' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='838' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='839' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='840' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='841' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='842' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='843' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='844' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='845' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='846' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='847' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='848' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='849' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='850' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='851' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='852' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='853' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='854' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='855' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='856' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='857' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='858' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='859' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='860' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='861' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='862' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='863' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='864' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='865' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='866' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='867' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='868' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='869' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='870' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='871' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='872' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='873' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='874' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='875' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='876' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='877' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='878' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='879' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='880' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='881' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='882' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='883' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='884' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='885' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='886' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='887' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='888' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='889' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='890' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='891' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='892' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='893' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='894' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='895' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='896' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='897' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='898' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='899' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='900' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='901' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='902' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='903' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='904' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='905' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='906' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='907' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='908' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='909' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='910' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='911' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='912' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='913' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='914' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='915' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='916' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='917' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='918' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='919' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='920' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='921' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='922' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='923' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='924' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='925' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='926' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='927' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='928' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='929' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='930' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='931' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='932' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='933' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='934' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='935' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='936' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='937' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='938' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='939' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='940' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='941' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='942' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='943' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='944' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='945' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='946' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='947' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='948' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='949' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='950' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='951' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='952' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='953' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='954' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='955' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='956' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='957' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='958' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='959' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='960' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='961' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='962' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='963' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='964' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='965' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='966' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='967' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='968' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='969' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='970' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='971' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='972' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='973' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='974' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='975' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='976' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='977' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='978' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='979' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='980' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='981' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='982' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='983' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='984' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='985' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='986' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='987' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='988' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='989' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='990' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='991' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='992' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='993' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='994' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='995' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='996' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='997' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='998' :
			chk1 = True
		elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='PHA' and fm[2]=='999' :
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
sql = "Select top (3000) * From doc_scan where doc_scan_id >= 1000000000 and doc_scan_id <= 1000030000 and status_ml = '1' and active = '1' and status_record = '1' Order By doc_scan_id"
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
	#print('res[22] '+res[22]+' res[4] '+res[4]+" start date "+startdate)
	#print('res[22] '+res[22]+' res[4] '+res[4]+" start date "+startdate)
	#print(ftpfilename)
	#ftp.put_passive(True)
	filename = 'c:\\temp\\'+timestamp+'.jpg'
	sizeftpfile = 0
	print('res[4] '+res[4])
	listres = res[4].split('//')
	fileexit=''
	if len(listres) > 0:
		#print('listres '+listres[0])
		#print(listres[1])
		#print('listres[2] '+listres[0])
		ftp.cwd('//'+res[22]+'//'+listres[0])
		nitems = ftp.nlst()
		for item in nitems:
			#print('item '+str(item)+' listres[2] '+listres[2])
			#print('listres[2] '+listres[2])
			if item == listres[1]:
				fileexit='OK'
				print('found OK')
				break
	#print('nitems '+str(nitems))
	if fileexit != 'OK':
		continue
	#print(ftpfilename)
	#ftp.put_passive(True)
	#filename = 'c:\\temp\\'+timestamp+'.jpg'
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
			#decodedObjects = pyzbar.decode(img)
			#for obj in decodedObjects:
			#	qrcode = qrcode + obj.type
				#print('Type : ', obj.type)
				#print('Data : ', obj.data,'\n')
			#if qrcode == 'QRCODE':
			#	sql = "Update doc_scan Set ml_fm = 'FM-LAB-999', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' ,width = '" +str(width) +"', height = '"+ str(height) + "', rgb_1 = '"+str(color1)+"' Where doc_scan_id = '"+str(res[0])+"'"
			#	cur.execute(sql)
			#	conn.commit()
			#	chk=True
			#	print('CHKKKKKKKKKKKKKKKKKKKKK')
			#	print('qrcode '+qrcode)
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


