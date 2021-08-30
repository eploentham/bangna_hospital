import sched, time
import pyodbc
import mysql.connector
import json
from datetime import datetime
import os,shutil
from fpdf import FPDF
from PIL import Image, ImageDraw, ImageFont
from pdf2image import convert_from_path, convert_from_bytes

import qrcode

#pip install mysql-connector-python
#pip install pyodbc
#python -m pip install --upgrade pip
#pip install pillow
#pip install pdf2image
#pip install qrcode[pil]
#pip install line-bot-sdk

from linebot import (
    LineBotApi, WebhookHandler
)
from linebot.exceptions import (
    InvalidSignatureError
)
from linebot.models import (
    MessageEvent, TextMessage, TextSendMessage,ImageSendMessage,
    SourceUser,
    ImageMessage, VideoMessage, AudioMessage,
)
line_bot_api = LineBotApi('SGStcLj9x1ZJUXcPLqsUHswrtiBkZr3Q0DVZnv9W/gXkgiqiZYuiZbknq4GgNqCaxtrTYltAUCbGY55l/ExZl/Z0U7Z7knyJmub7JoHGJd46JK+0YzIf7o9ptInMpvbpRENsFRfIDb86wXq4dergagdB04t89/1O/w1cDnyilFU=')
handler = WebhookHandler('5cd72f8c8389f94d5c3838a5bcbc7996')#Channel secret
json_line=""


interval = 600
#interval = 6

s = sched.scheduler(time.time, time.sleep)

def scheduleNovel(sc): 
	#print("scheduleNovel Start")
	print("Start Scheduler in "+str(interval)+" second")
	# do your stuff
	selectNovelSchedule()

	#print("scheduleNovel End")
	print("End Scheduler in "+str(interval)+" second")
	s.enter(interval, 1, scheduleNovel, (sc,))
def selectNovelSchedule():
	folderPID = "D:\\novel-result\\"
	createFolder(folderPID,"")
	cnx = mysql.connector.connect(host="127.0.0.1", port=3306, user="root", password="Ekartc2c5", database='bangna5_covid')
	sql="Select novel_id, pid, passport, mobile From bangna5_covid.t_novel Where status_novel = '0' Order By novel_id"
	print(sql)
	curMy = cnx.cursor()
	curMy.execute(sql)
	records = curMy.fetchall()
	hn=""
	vsdate=""
	preno=""
	reqno=""
	reqdate=""
	reqyear=""
	patientname=""
	age=""
	sex=""
	dob=""
	mobile=""
	try:
		for rowMy in records:
			novel_id=rowMy[0]
			pid=rowMy[1]
			passport=rowMy[2]
			mobile=rowMy[3]
			sql = "Select lcresd.lab_covid_detected_id,lcresd.mnc_hn_no , isnull(smartcard.hn, '') as hn, convert(VARCHAR(20),pt01.mnc_date ,23 ) as mnc_date, isnull(pt01.mnc_pre_no,'') as mnc_pre_no "
			sql += ", isnull(labt021.mnc_req_no,'') as mnc_req_no,convert(VARCHAR(20),labt021.mnc_req_dat,23) as mnc_req_dat, labt021.mnc_req_yr, pm02.MNC_PFIX_DSC +' '+pm01.MNC_FNAME_T +' ' +pm01.MNC_LNAME_T as patient_fullname,  convert(VARCHAR(20),pm01.MNC_BDAY,23) AS MNC_BDAY "
			sql += ", isnull(CONVERT(int,ROUND(DATEDIFF(hour,pm01.MNC_BDAY,GETDATE())/8766.0,0)),'') AS AgeYearsIntRound, isnull(pm01.mnc_sex,'') as mnc_sex "
			sql += "FROM BNG5_DBMS_FRONT.dbo.t_lab_covid_detected lcresd  "
			sql += "inner join BNG5_DBMS_FRONT.dbo.patient_m01 pm01 on lcresd.mnc_hn_no = pm01.mnc_hn_no  "
			sql += "left join patient_m02 pm02 on pm01.MNC_PFIX_CDT = pm02.MNC_PFIX_CD "
			sql += "left join bn5_scan.dbo.b_patient_smartcard smartcard on pm01.patient_smartcard_id = smartcard.patient_smartcard_id  "
			sql += "Inner Join patient_t01 pt01 on pm01.mnc_hn_no = pt01.mnc_hn_no and pm01.mnc_hn_yr = pt01.mnc_hn_yr "
			sql += "left join LAB_T01 labt011 ON pt01.MNC_PRE_NO = labt011.MNC_PRE_NO AND pt01.MNC_DATE = labt011.MNC_DATE and pt01.MNC_hn_NO = labt011.MNC_hn_NO and pt01.MNC_hn_yr = labt011.MNC_hn_yr "
			sql += "left join LAB_T02 labt021 ON labt011.MNC_REQ_NO = labt021.MNC_REQ_NO AND labt011.MNC_REQ_DAT = labt021.MNC_REQ_DAT "
			sql += "where pm01.MNC_ID_NO = '"+str(pid)+"' and labt021.MNC_LB_CD in ('SE184','SE629')   "
			sql += "and pt01.mnc_date = (Select max(patient_t01.mnc_date) From  patient_t01 Inner Join patient_m01 pm01 on pm01.mnc_hn_no = patient_t01.mnc_hn_no and pm01.mnc_hn_yr = patient_t01.mnc_hn_yr "
			sql += "left join LAB_T01 labt011 ON patient_t01.MNC_PRE_NO = labt011.MNC_PRE_NO AND patient_t01.MNC_DATE = labt011.MNC_DATE and patient_t01.MNC_hn_NO = labt011.MNC_hn_NO and patient_t01.MNC_hn_yr = labt011.MNC_hn_yr "
			sql += "left join LAB_T02 labt021 ON labt011.MNC_REQ_NO = labt021.MNC_REQ_NO AND labt011.MNC_REQ_DAT = labt021.MNC_REQ_DAT where pm01.MNC_ID_NO = '"+str(pid)+"' and labt021.MNC_LB_CD in ('SE184','SE629')) "
			#print(sql)
			conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
			curMainHIS = conn.cursor()
			curMainHIS.execute(sql)
			recordsMainHIS = curMainHIS.fetchall()
			for rowMainHIS in recordsMainHIS:
				lcresd_id=rowMainHIS[0]
				mnc_hn_no=rowMainHIS[1]
				hn=rowMainHIS[2]
				vsdate=rowMainHIS[3]
				preno=rowMainHIS[4]
				reqno=rowMainHIS[5]
				reqdate=rowMainHIS[6]
				reqyear=rowMainHIS[7]
				patientname=rowMainHIS[8]
				dob=rowMainHIS[9]
				age=rowMainHIS[10]
				sex=rowMainHIS[11]
				#print("pid "+pid)
				sql="Update BNG5_DBMS_FRONT.dbo.t_lab_covid_detected Set status_novel = '1', pid ='"+str(pid)+"', passport ='"+str(passport)+"'"
				sql+= ", novel_id ='"+str(novel_id)+"', hn='"+str(hn)+"', mobile='"+str(mobile)+"' Where lab_covid_detected_id = '"+str(lcresd_id)+"' "
				#print(sql)
				curMainHISUpdate = conn.cursor()
				curMainHISUpdate.execute(sql)
				curMainHISUpdate.commit()
				curMainHISUpdate.close()

				sql = "Update bangna5_covid.t_novel Set status_novel = '1',mnc_hn_no ='"+str(mnc_hn_no)+"', hn='"+str(hn)+"'"
				sql += ", visit_date ='"+str(vsdate)+"', mnc_pre_no ='"+str(preno)+"', req_no ='"+str(reqno)+"', req_date ='"+str(reqdate)+"', req_year ='"+str(reqyear)+"' "
				sql += ", patient_fullname = '"+str(patientname)+"' ,patient_age='"+str(age)+"', sex ='"+str(sex)+"', dob ='"+str(dob)+"' "
				#sql += ", patient_fullname = '"+str(patientname)+"',patient_age='"+str(age)+"', sex ='"+sex+"', dob ='"+str(dob)+"' "
				sql +="Where novel_id = '"+str(novel_id)+"' "
				#print(sql)
				print("pid "+str(pid)+" novel_id "+str(novel_id))
				curUpdate = cnx.cursor()
				curUpdate.execute(sql)
				#curUpdate.commit()
				curUpdate.close()
				#print("Update status_novel = 1 "+str(novel_id))
				#printNovelPDF(novel_id)
	except mysql.connector.Error as e:
		print(e)
	finally:
		if cnx.is_connected():
			curMy.close()
			cnx.close()
	
def printNovelPDF(novid_id):
	print("printNovelPDF")
	folderPID = "D:\\novel-result\\"
	now = datetime.today().strftime("%Y-%m-%d_%H-%M")
	
	
	col1=10
	cnx = mysql.connector.connect(host="127.0.0.1", port=3306, user="root", password="Ekartc2c5", database='bangna5_covid')
	try:
		pdf = FPDF('P', 'mm', 'A4')
		pdf.add_font('TH Sarabun New', '', 'd:\\lab-result\\Sarabun\\THSarabunNew.ttf', uni=True)
		pdf.set_font('TH Sarabun New')
		pdf.add_page()
		sql="Select patient_fullname,patient_age,visit_date,nation,occupation,mobile,address,home_type,symptom,disease,status_vaccine_covid,pregnant,pregnant_week,mnc_hn_no,address  From bangna5_covid.t_novel Where novel_id = '"+str(novid_id)+"' "
		
		curMy = cnx.cursor()
		curMy.execute(sql)
		records = curMy.fetchall()
		for rowMy in records:
			patient_fullname=rowMy[0]
			patient_age=rowMy[1]
			visit_date=rowMy[2]
			nation=rowMy[3]
			occupation=rowMy[4]
			mobile=rowMy[5]
			address=rowMy[6]
			home_type=rowMy[7]
			symptom=rowMy[8]	#8
			disease=rowMy[9]
			status_vaccine_covid=rowMy[10]
			pregnant=rowMy[11]
			pregnant_week=rowMy[12]
			mnc_hn_no=rowMy[13]
			address=rowMy[14]
			#mnc_hn_no=rowMy[15]
			#pregnant=rowMy[6]
			#pregnant=rowMy[6]
			#pregnant=rowMy[6]
			#pregnant=rowMy[6]
			#pregnant=rowMy[6]
			#pdf.text(col3,145, "send date "+datetime.today().strftime("%d/%m/%Y %H:%M"))
			deleteFileinFolder(folderPID,str(mnc_hn_no)+"_"+str(now)+".pdf")
			pdf.set_font_size(12)
			pdf.text(col1,10, "แบบรายงานผู้ป่วยโรคติดเชื้อโคโรนา 2019 ฉบับย่อ")
			pdf.set_font_size(10)
			pdf.text(150,10, "Code")
			pdf.text(150,16, "Novelcorona 3")
			pdf.line(10, 14, 180, 14)
			pdf.text(col1,20, "ข้อมูลทั่วไป")
			pdf.text(140,25, "เลขบัตรประชาชน/passport .........................")
			pdf.text(col1,30, "ชื่อ-นามสกุล .......................................  เพศ      ชาย      หญิง  อายุ ..... ปี  สัญชาติ ............")
			pdf.text(col1,35, "ประเภท     PUI     ผู้สัมผัสใกล้ชิดผู้ติดเชื้อ     การค้นหา/สำรวจเชิงรุก     Sentinel surveilance    อื่นๆ ................")
			pdf.text(col1,40, "อาชีพ (ระบุลักษณะงาน เช่น บุคลากรทางการแพทย์ งานที่สัมผัสกับนักท่องเที่ยว/ขาวต่างชาติ) ...................................")
			pdf.text(col1,45, "เบอร์โทรศัพท์ ..........................  สถานที่ทำงาน/สถานศึกษา .............................")
			pdf.text(col1,50, "ที่อยู่ขณะป่วยในประเทศไทย ชื่อสถานที่ ...................................................")
			pdf.text(col1,55, "หมู่ ..........  ตำบล ............... อำเภอ .............. จังหวัด ...........................")
			pdf.text(col1,60, "ลักษณะที่พักอาสัย     บ้านเดี่ยว     ตึกแถว/ทาวน์เฮ้าส์     หอพัก/คอนโด/ห้องเช่า")
			pdf.text(col1,65, "                   พักห้องรวมกับคนจำนวนมาก เช่น แคมป์ก่อสร้าง หอผู้ป่วยใน รพ.     อื่นๆ ระบุ .......................")


			print(folderPID+"\\"+str(mnc_hn_no)+"_"+str(now)+".pdf")
			pdf.output(folderPID+"\\"+str(mnc_hn_no)+"_"+str(now)+".pdf", 'F')
			time.sleep(0.01)

	except mysql.connector.Error as e:
		print(e)
	finally:
		if cnx.is_connected():
			curMy.close()
			cnx.close()
def datetime_tick():
	#ticks = 52707330000`
	#converted_ticks = datetime.now() + datetime.timedelta(microseconds = ticks/10)

	t0 = datetime(1, 1, 1)
	now = datetime.utcnow()
	seconds = (now - t0).total_seconds()
	ticks = seconds #* 10**7

	return ticks
def createFolder(parent_dir, folder):
	try:
		print("creaFolder ")
		path = os.path.join(parent_dir, folder)
		if not os.path.exists(parent_dir):
			print("creaFolder "+parent_dir)
			#os.mkdir("c:\\temp_line\\test\\")
			os.mkdir(parent_dir)
		if not os.path.exists(path):
			print("creaFolder "+path)
			#os.mkdir("c:\\temp_line\\test\\")
			os.mkdir(path)

	except OSError as error: 
		print("createFolder "+str(error))
def deleteFileinFolder(folder):
	for filename in os.listdir(folder):
		file_path = os.path.join(folder, filename)
		try:
			if os.path.isfile(file_path) or os.path.islink(file_path):
				os.unlink(file_path)
			elif os.path.isdir(file_path):
				shutil.rmtree(file_path)
		except Exception as e:
			print('Failed to delete %s. Reason: %s' % (file_path, e))
def deleteFileinFolder(folder, filename):
	file_path = os.path.join(folder, filename)
	print(file_path)
	try:
		if os.path.isfile(file_path) or os.path.islink(file_path):
			os.unlink(file_path)
		elif os.path.isdir(file_path):
			shutil.rmtree(file_path)
	except Exception as e:
		print('Failed to delete %s. Reason: %s' % (file_path, e))

print("Start Scheduler in "+str(interval)+" second")
s.enter(interval, 1, scheduleNovel, (s,))
s.run()


