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
sql = "Select top (100) * From doc_scan where status_ml is null Order By doc_scan_id"
cur.execute(sql)
myresult = cur.fetchall()
w=230
h=50

ftp = FTP('172.25.10.3')
ftp.login("imagescan", "imagescan")

for res in myresult:
	now = datetime.now()
	startdate = now.strftime("%Y/%m/%d, %H:%M:%S")
	print(res[4])
	timestamp = datetime.timestamp(now)
	timestamp = str(timestamp).replace(".", "_")
	#img = cv2.imread('C:\\imagescan\\imagescan\\'+res[4],cv2.IMREAD_GRAYSCALE)
	#img = cv2.imread('//sharedfolders//'+res[22]+'//'+res[4],cv2.IMREAD_GRAYSCALE)
	img = cv2.imread('//sharedfolders//'+res[22]+'//'+res[4],cv2.IMREAD_COLOR)
	#imgrgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
	#cv2.imshow('img', img)
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
			#print('xx '+str(xx))
			#print('yy1 '+str(yy))
			crop_img = img[yy-int(h): yy, xx:xx+w]
			crop_img = cv2.cvtColor(crop_img, cv2.COLOR_BGR2GRAY)
			txt = pytesseract.image_to_string(crop_img,lang='eng')
			fm = txt.split('-')
			now1 = datetime.now()
			enddate = now1.strftime("%Y/%m/%d, %H:%M:%S")
			if len(fm)>=3 and fm[0]=='FM' and fm[1]=='REG' and fm[2]=='015' :
				print(fm+' res '+str(res[0]))
				chk=True
				try:
					sql = "Update doc_scan Set ml_fm = 'FM-REG-015', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
					",width = '" +width +"', height = '"+ height + "'"
					"Where doc_scan_id = '"+str(res[0])+"'"
					cur.execute(sql)
					conn.commit()
					break
				except:
					print('error')
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='001' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-001', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='002' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-002', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='003' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-003', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='007' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-007', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='008' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-008', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='011' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-011', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='013' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-013', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='014' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-014', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='016' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-016', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='060' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-060', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='NUR' and fm[2]=='123' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-NUR-123', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='001' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-MED-001', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='002' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-MED-002', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='MED' and fm[2]=='003' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-MED-003', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='005' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-ORD-005', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='014' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-ORD-014', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			elif len(fm)>=3 and fm[0]=='FM' and fm[1]=='ORD' and fm[2]=='035' :
				print(fm)
				chk=True
				sql = "Update doc_scan Set ml_fm = 'FM-ORD-035', ml_date_time_start = '"+startdate+"', ml_date_time_end = '"+enddate+"', status_ml= '1' " +
				"Where doc_scan_id = '"+str(res[0])+"'"
				cur.execute(sql)
				conn.commit()
				break
			#cv2.imshow('img', crop_img)
			#cv2.waitKey()


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


