import cv2
import pytesseract
import os
import pyodbc
from datetime import datetime
from ftplib import FTP
#import pymssql

#	1 select data 
#	2 read image
# 	3 crop image
# 	4 pytesseract

serverName="localhost"
userDB="sa"
passDB="Ekartc2c5"
dataDB="bn5_scan"
#conn = pymssql.connect(serverName , userDB , passDB, dataDB)
#conn = pymssql.connect('Driver={SQL Server};Server='+serverName+';Database='+dataDB+';;UID='+userDB+';PWD='+passDB+';Trusted_Connection=yes;')
conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
cur = conn.cursor()
sql = "Select top (1000) * From doc_scan where status_ml is null Order By doc_scan_id"
cur.execute(sql)
myresult = cur.fetchall()
#print('ok1')
w=230
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
	ftp.retrbinary("RETR " + ftpfilename ,open(filename, 'wb').write)
	#img = cv2.imread('C:\\imagescan\\imagescan\\'+res[4],cv2.IMREAD_GRAYSCALE)
	#img = cv2.imread('//sharedfolders//'+res[22]+'//'+res[4],cv2.IMREAD_GRAYSCALE)
	#img = cv2.imread('//sharedfolders//'+res[22]+'//'+res[4],cv2.IMREAD_COLOR)
	img = cv2.imread(filename,cv2.IMREAD_COLOR)
	color = str(img[300, 300])
	#cv2.imshow('img', img)
	#cv2.waitKey()
	#imgrgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
	#print("res_4 "+res[4]+"res_22 "+res[22])
	height = img.shape[0]
	width = img.shape[1]
	xx = 0
	yy = height
	chk = False
	for x in range(0, 20):
		xx = xx + int(10)
		yy = height
		if chk==True:
			break
		for y in range(0, 20):
			yy = yy - int(10)
			print('xx '+str(xx))
			crop_img = img[yy-int(h): yy, xx:xx+w]
			crop_img = cv2.cvtColor(crop_img, cv2.COLOR_BGR2GRAY)
			print('yy '+str(yy))
			#cv2.imshow('img', crop_img)
			#cv2.waitKey()
			txt = pytesseract.image_to_string(crop_img,lang='eng')
			fm = txt.split('-')
			now1 = datetime.now()
			enddate = now1.strftime("%Y-%m-%d, %H:%M:%S")
			print(enddate)
			if len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='015' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				try:
					sql = "Update doc_scan Set ml_fm = 'FM-REG-015', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' ,width = '" +str(width) +"', height = '"+ str(height) + "', rgb1 = '"+str(color)+"' Where doc_scan_id = '"+str(res[0])+"'"
					cur.execute(sql)
					conn.commit()
					break
				except:
					print('error')
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='001' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-001', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='002' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-002', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='003' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-003', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='007' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-007', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='008' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-008', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='011' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-011', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='013' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-013', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='014' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-014', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='016' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-016', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='060' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-060', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='123' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-123', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='001' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-MED-001', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='002' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-MED-002', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='003' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-MED-003', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='005' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-ORD-005', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='014' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-ORD-014', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='035' :
				print(fm[0]+fm[1]+fm[2]+' endate '+enddate)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-ORD-035', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
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


