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
def chkPHA(txtFM):
	chk2 = False
	for index in range(0, 999):
		chk = "000"+str(index)

		chk1 = chk[len(chk)-3:]
		print('chk1 '+chk1)
		if txtFM == chk1:
			chk2 =True
			break
	return (chk2)

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

		if fm[1] == 'NUR' and chk1 = False
			if chkPHA(txtFM)
				chk1 = True
		elif fm[1] == 'ORD' and chk1 = False
			if chkPHA(txtFM)
				chk1 = True
		elif fm[1] == 'PHA' and chk1 = False
			if chkPHA(txtFM)
				chk1 = True
		elif fm[1] == 'XRT' and chk1 = False
			if chkPHA(txtFM)
				chk1 = True
		elif fm[1] == 'AMS' and chk1 = False
			if chkPHA(txtFM)
				chk1 = True
		elif fm[1] == 'LAB' and chk1 = False
			if chkPHA(txtFM)
				chk1 = True
		elif fm[1] == 'MED' and chk1 = False
			if chkPHA(txtFM)
				chk1 = True
		elif fm[1] == 'REG' and chk1 = False
			if chkPHA(txtFM)
				chk1 = True

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


