import pyodbc
from ftplib import FTP
from datetime import datetime
import time
import cv2
from pdf2image import convert_from_path, convert_from_bytes
from PIL import Image, ImageDraw, ImageFont
import os,shutil
import sys
import json
from flask import Flask, request, abort
import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
from email.mime.base import MIMEBase
from email import encoders
from bahttext import bahttext
from fpdf import FPDF
import qrcode

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

app = Flask(__name__)

line_bot_api = LineBotApi('CWWqAvxq9ruJYJTQWPagh8BwRIVj3W7yq9KHzqftSws6im1ajcg8GqUSWQgh85MJp2Lo4g/T3XztgIwWGNJGWv9y6aLAgUTsg76Ry+SMlVuN24Yq8K8S1lst23qUoeiP8HQQZ5lLPLw+zOWj4s/TZQdB04t89/1O/w1cDnyilFU=')
handler = WebhookHandler('2d9a359eea72d3255015291b54549207')#Channel secret
json_line=""

def sendOutLab(hn, userid):
    sql = "Select doc_scan_id, host_ftp, image_path,patient_fullname, folder_ftp from doc_scan Where hn = '"+hn+"' and active = '1' and status_record = '2' "
    conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
    cur = conn.cursor()
    cur.execute(sql)
    myresult = cur.fetchall()
    print('sql '+sql)
    for res in myresult:
        pttname = res[3]
        hostftp = res[1]
        imagepath = res[2]
        ftp = FTP('172.25.10.3')
        ftp.login("imagescan", "imagescan")
        ftpfilename = '//'+res[4]+'//'+res[2]  
        print('ftpfilename '+ftpfilename)   
        #try:
        now = datetime.now()
        startdate = now.strftime("%Y-%m-%d, %H:%M:%S")
        timestamp = datetime.timestamp(now)
        timestamp = str(timestamp).replace(".", "_")
        filename_pdf = 'c:\\temp_line\\'+timestamp+'.pdf'
        filename_jpg = 'c:\\temp_line\\'+timestamp
        ftp.retrbinary("RETR " + ftpfilename ,open(filename_pdf, 'wb').write)
        time.sleep(1)
        #images = convert_from_path(filename_pdf)
        i = 1
        print('filename_pdf ', filename_pdf)
        session = FTP('bangna.co.th','bangna','cy!C51x3')
        images = convert_from_bytes(open(filename_pdf, 'rb').read())
        text_message1 = TextSendMessage(text='ผล Out Lab HN '+hn+' '+pttname)
        line_bot_api.push_message(userid, text_message1)

        for image in images:
            #page.save(filename_jpg+'_'+i+'jpg', 'JPEG')
            filename_jpg1 = filename_jpg +'_'+ str(i) + '.jpg'
            filename_jpg1_re = filename_jpg +'_'+ str(i) + '_re.jpg'
            timestamp1 = timestamp +'_'+ str(i) + '.jpg'
            timestamp1_re = timestamp +'_'+ str(i) + '_re.jpg'
            url_org = 'https://bangna.co.th/line_bot/'+timestamp1
            urlprev = 'https://bangna.co.th/line_bot/'+timestamp1_re

            image.save(filename_jpg1, 'JPEG')
            print('filename_jpg1 '+filename_jpg1)
            time.sleep(0.5)
            img = cv2.imread(filename_jpg1, cv2.IMREAD_UNCHANGED)
            scale_percent = 20 # percent of original size
            width = int(img.shape[1] * scale_percent / 100)
            height = int(img.shape[0] * scale_percent / 100)
            dim = (width, height)
            # resize image
            resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
            cv2.imwrite(filename_jpg1_re, resized)
            print('filename_jpg1 '+filename_jpg1)
            if i==1:
                session.cwd('httpdocs/line_bot')
            file = open(filename_jpg1,'rb')                  # file to send
            session.storbinary('STOR '+timestamp1, file)     # send the file
            file = open(filename_jpg1_re,'rb')                  # file to send
            session.storbinary('STOR '+timestamp1_re, file)     # send the file

            file.close()                                    # close file and FTP
            time.sleep(0.5)
            #line_bot_api.reply_message(event.reply_token,TextSendMessage('ผล Out Lab HN '+hn+' '+pttname))
            print('line_bot_api '+ str(i))
            image_message = ImageSendMessage(original_content_url=url_org, preview_image_url=urlprev)
            #line_bot_api.push_message(get_sourceid(event), image_message)
            line_bot_api.push_message(userid, image_message)

            #if isinstance(event.source, SourceUser):
            #line_bot_api.reply_message(event.reply_token,TextSendMessage('HN '+hn+' '+ftpfilename))
            i = i+1
            #ftp = FTP('bangna.co.th')
            #ftp.login("bangna", "cy!C51x3")
        #session.quit()
        
        #except ftplib.all_errors as e:
        #    print('FTP error:', e)
        #    continue
    cur.close()
    conn.close()
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
def sendLabCovidPDFhn(hn1, user_id):
    print("sendLabCovidPDFhn hn " +str(hn1))
    print("sendLabCovidPDFhn user_id " +str(user_id))
    sqlother=""
    sqlother1=""
    #sqlwhere="Where pttm01.MNC_ID_NO = '"+str(pid)+"'  and pttt01.mnc_date = (Select max(mnc_date) from patient_t01 inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO = patient_t01.MNC_HN_NO and pttm01.mnc_hn_yr = pttm01.mnc_hn_yr  where pttm01.MNC_ID_NO = '"+str(pid)+"')   "
    #sqlwhere=" Where pttt01.mnc_hn_no = '"+hn1+"'  and pttt01.mnc_date = (Select max(mnc_date) from patient_t01 where mnc_hn_no = '"+hn1+"')   "
    sqlwhere=" Where pttt01.mnc_hn_no = '"+hn1+"'  and pttt01.mnc_date = (Select max(patient_t01.mnc_date) "
    sqlwhere+="from patient_t01 "
    sqlwhere+="left join LAB_T01 labt011 ON patient_t01.MNC_PRE_NO = labt011.MNC_PRE_NO AND patient_t01.MNC_DATE = labt011.MNC_DATE and patient_t01.MNC_hn_NO = labt011.MNC_hn_NO and patient_t01.MNC_hn_yr = labt011.MNC_hn_yr "
    sqlwhere+="left join LAB_T02 labt021 ON labt011.MNC_REQ_NO = labt021.MNC_REQ_NO AND labt011.MNC_REQ_DAT = labt021.MNC_REQ_DAT "
    sqlwhere+="where patient_t01.mnc_hn_no = '"+hn1+"' and labt021.MNC_LB_CD in ('SE184','SE629'))   "
    sql=""
    sql="SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC, labt05.MNC_RES_VALUE, labt05.MNC_STS, labt05.MNC_RES, labt05.MNC_LB_RES, labt05.MNC_RES_UNT  "
    sql+=",convert(VARCHAR(20),labt05.mnc_req_dat,23) as mnc_req_dat,isnull(pm02.MNC_PFIX_DSC_e,'')+' '+pttm01.MNC_FNAME_T+' '+ pttm01.MNC_LNAME_T as mnc_patname "
    sql+=",usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check,labt01.MNC_REQ_NO,convert(VARCHAR(20),pttm01.mnc_bday,23) as dob "
    sql+=", convert(VARCHAR(20),labt02.mnc_result_dat,107) as mnc_result_dat, labt02.mnc_result_tim  "
    sql+=",patient_m02.MNC_PFIX_DSC_e + ' ' + patient_m26.MNC_DOT_FNAME_e  + ' ' + patient_m26.MNC_DOT_LNAME_e as mnc_doctor_full, pttm01.mnc_id_no,convert(VARCHAR(20),labt05.mnc_req_dat,107) as mnc_req_dat1, DATEDIFF(hour,MNC_BDAY,GETDATE())/8766 AS AgeYearsIntTrunc "
    sql+=",labt02.mnc_usr_result,labt02.mnc_usr_result_report,labt02.mnc_usr_result_approve,pttm01.MNC_HN_NO "
    sql+=""
    sql+="from patient_t01 pttt01 "
    sql+="inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO =pttt01.MNC_HN_NO and pttm01.mnc_hn_yr = pttt01.mnc_hn_yr "
    sql+="left join patient_m02 pm02 on pttm01.MNC_DOT_PFIX =pm02.MNC_PFIX_CD "
    sql+="left join LAB_T01 labt01 ON pttt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pttt01.MNC_DATE = labt01.MNC_DATE and pttt01.MNC_hn_NO = labt01.MNC_hn_NO and pttt01.MNC_hn_yr = labt01.MNC_hn_yr "
    sql+="left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT "
    #sql+="left join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
    sql+="inner join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
    sql+="left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD "
    sql+="left join LAB_M04 labm04 ON labt05.MNC_LB_RES_CD = labm04.MNC_LB_RES_CD and labt05.MNC_LB_CD = labm04.MNC_LB_CD "
    sql+="left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " 
    sql+="left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report "
    sql+="left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " 
    sql+="left join patient_m26 on patient_m26.MNC_DOT_CD = labt01.mnc_dot_cd "
    sql+="inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD "
    sql+=sqlother
    sql+=sqlwhere
    sql+="  and labt02.MNC_LB_CD in ('SE184','SE629') "
    #sql+="and b_patient_smartcard.date_order = (select max(b_patient_smartcard.date_order) from bn5_scan.dbo.b_patient_smartcard where b_patient_smartcard.hn = '"+hn+"'  )"
    sql+=sqlother1
    print(sql)
    hn=""
    preno=""
    vsdate=""
    hnyear=""
    now = datetime.now()
    timestamp = datetime.timestamp(now)
    timestamp = str(timestamp).replace(".", "_")
    folderPID = "D:\\temp_line\\"
    tick = hn+"_"+str(datetime_tick())
    print("tick "+str(tick))
    createFolder(folderPID,tick)
    #deleteFileinFolder(folder)
    rowcnt=0
    rowResultCnt=0
    rowcount=1
    reqdate1=""
    restime=""
    labname=""
    pttname=""
    reqno=""
    doctor=""
    dob=""
    pid=""
    resdate=""
    reporter=""
    resulter=""
    approver=""
    reqdate=""
    age1=""
    user1=""
    user2=""
    user3=""
    filename=""
    mnc_hn_no=""
    qrcoderesult=""
    #print("sendLabCovidPDFpid 1111")
    conn1 = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
    cur1 = conn1.cursor()
    cur1.execute(sql)
    resp1 = cur1.fetchall()
    #print("sendLabCovidPDFpid 2222")
    #print("cur1.rowcount "+str(cur1.rowcount))
    pdf = FPDF('L', 'mm', 'A5')
    pdf.add_font('Sarabun', '', 'd:\\lab-result\\Sarabun\\Sarabun-Regular.ttf', uni=True)
    pdf.set_font('Sarabun')
    pdf.add_page()
    y = 63
    col1=10
    col2=85
    col3=150
    pdf.set_font_size(10)
    for row1 in resp1:
        if row1[1] == None:
            print("hn no result"+str(mnc_hn_no))
            line_bot_api.push_message(user_id,TextSendMessage(text="HN "+str(mnc_hn_no)+"นี้ ยังไม่มี ผล "))
            continue
        if (row1[4] != None) and (rowcount==1) :
            qrcoderesult=row1[2]
        rowcount = rowcount+1
        mnc_hn_no= row1[23]
        #print("mnc_hn_no "+str(mnc_hn_no))
        filename = folderPID+timestamp+"_"+str(mnc_hn_no)+'_lab.jpg'
        #hn=""
        labcode = row1[0]
        labname = row1[1]
        reqdate = str(row1[7])
        reporter=row1[9]
        resulter=row1[10]
        approver=row1[11]
        reqno = str(row1[12])
        dob = row1[13]
        resdate = str(row1[14])
        restime = "0"+str(row1[15])
        doctor = row1[16]
        pid = row1[17]
        age1 = row1[19]
        user1 = row1[20]
        user2 = row1[21]
        user3 = row1[22]
        reqdate1 = row1[18]
        
        if row1[4] != None:
            labval = row1[2]
            interpret = row1[3]
            labres = row1[4]
            normal = row1[5]
            unit = row1[6]
            pttname = row1[8]
            reqdate = row1[7]
        else:
            labval=''
            interpret=''
            labres=''
            normal=''
            unit=''
            pttname=''
            reqdate=''
        pdf.text(col1+5,y, labres)      #draw result lab
        pdf.text(col2+10,y, labval)      #draw result lab
        pdf.text(col3,y, unit)      #draw result lab
        y = y+10
        #print("sendLabCovidPDFpid 2222 rowcount "+str(rowcount))
    
    #pdf.set_font('Arial', '', 14)
    #pdf.cell(40, 10, "เอกภพ  เพลินธรรม", 0)
    if(len(restime)>=5):
        restime = restime[len(restime)-4:len(restime)]
    #pdf.text(40, 10, "โรงพยาบาล บางนา5", 0)
    #print("sendLabCovidPDFpid 3333")
    pdf.set_font_size(12)
    pdf.text(col1,10, "โรงพยาบาล บางนา5")
    pdf.text(col1,15, "Bangna5 General Hospital ")
    pdf.text(col1,20, "ใบรายงานผล Result LAB")
    pdf.set_font_size(8)
    pdf.text(col2,10, "Patient Name "+pttname)
    pdf.text(col2,15, "HN "+str(mnc_hn_no))
    pdf.text(col2,20, "เลขที่/Req no "+str(reqno)+"."+reqdate)
    pdf.text(col2,25, "แพทย์/Doctor "+str(doctor))

    pdf.text(col3,10, "วันเกิด/DOB : "+dob+" [age "+str(age1)+"]")
    pdf.text(col3,15, "ID/Passport : "+str(pid))
    pdf.text(col3,20, "วันที่ส่งตรวจ/Request Date : "+reqdate1)
    pdf.text(col3,25, "วันที่ออกผล/Result Date : "+resdate+" "+restime)
    pdf.set_font_size(10)
    pdf.text(col1+5,45, "GROUP : IMMUNO + TUMOR")
    pdf.text(col1+5,51, labname)
    pdf.set_font_size(8)

    pdf.text(col1,30, ".......................................................................................................................................................................................................................................................................")
    pdf.text(col1,38, ".......................................................................................................................................................................................................................................................................")
    pdf.text(col1+20,35, "รายการ/DESCRIPTION")
    pdf.text(col2,35, "ผลตรวจ/RESULT")
    pdf.text(col3,35, "แปลงผล/Interpret")

    pdf.text(col2-5,65, "................................................................................................")
    pdf.text(col2-5,75, "................................................................................................")
    pdf.text(col2-5,85, "................................................................................................")
    pdf.text(col2-5,95, "................................................................................................")
    pdf.text(col2-5,105, "...............................................................................................")

    pdf.text(col1-3,130, "......................................................................................................................................................................................................................................................................................")
    pdf.text(col1-3,138, "......................................................................................................................................................................................................................................................................................")
    pdf.text(col1,135, "ผู้บันทึก/recorder : "+reporter+"["+user1+"]")
    pdf.text(col2-10,135, "ผู้รายงาน/reporter : "+resulter+"["+user2+"]")
    pdf.text(col3-5,135, "ผู้ตรวจสอบ/approver : "+approver+"["+user3+"]")
    pdf.text(col1+5,145, "FM-LAB-096 (แก้ไขครั้งที่ 00-17/07/55) ")

    pdf.text(col3,145, "send date "+datetime.today().strftime("%d/%m/%Y %H:%M"))
    #print("sendLabCovidPDFpid 4444")
    #im = Image.open("D:\\lab-result\\LOGO-BW-tran.png") 
    #pdf.image("D:\\lab-result\\LOGO-BW-tran.png",col3-20,40,20,20)
    pdf.image("D:\\lab-result\\LOGO-BW-tran-400.png",col3-20,40,20,20)
    #imgQrcode = qrcode.make("http://result.bangna.co.th?hn"+str(mnc_hn_no)+"&req_no="+str(reqno)+"&req_date="+reqdate)
    #print("sendLabCovidPDFpid 4444 1111")
    imgQrcode = qrcode.make(pttname+" "+qrcoderesult+" "+str(reqno)+"."+reqdate+" "+str(mnc_hn_no)+" "+str(pid))
    type(imgQrcode)  # qrcode.image.pil.PilImage
    imgQrcode.save(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+".png")
    #print("sendLabCovidPDFpid 4444 2222")
    pdf.image(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+".png",col3+10,100,25,25)
    #print("sendLabCovidPDFpid 5555")
    pdf.output(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.pdf', 'F')
    time.sleep(0.2)

    images = convert_from_bytes(open(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.pdf', 'rb').read())
    for image in images:
        image.save(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.jpg', 'JPEG')
        #rint('filename_jpg1 '+filename_jpg1)
        #time.sleep(1)
    now = datetime.now()
    print("sendLabCovidPDFpid 6666")
    timestamp = datetime.timestamp(now)
    timestamp1 = str(timestamp)+'_lab.jpg'
    timestamp1_re = str(timestamp)+'_lab_re.jpg'
    url_org = 'https://bangna.co.th/line_bot/'+timestamp1
    urlprev = 'https://bangna.co.th/line_bot/'+timestamp1_re

    img = cv2.imread(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.jpg', cv2.IMREAD_UNCHANGED)
    scale_percent = 20 # percent of original size
    width = int(img.shape[1] * scale_percent / 100)
    height = int(img.shape[0] * scale_percent / 100)
    dim = (width, height)
    # resize image
    resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
    filename_re = folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'_re.jpg'
    cv2.imwrite(filename_re, resized)

    session = FTP('bangna.co.th','bangna','cy!C51x3')
    session.cwd('httpdocs/line_bot')
    file = open(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.jpg','rb')                  # file to send
    session.storbinary('STOR '+timestamp1, file)     # send the file
    file = open(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'_re.jpg','rb')                  # file to send
    session.storbinary('STOR '+timestamp1_re, file)     # send the file
    image_message = ImageSendMessage(original_content_url=url_org, preview_image_url=urlprev)
    line_bot_api.push_message(user_id, image_message)

def sendLabATKPDFpid(pid, user_id):
    print("sendLabATKPDFpid pid " +str(pid))
    print("sendLabATKPDFpid user_id " +str(user_id))

    #sqlwhere="Where pttm01.MNC_ID_NO = '"+str(pid)+"'  and pttt01.mnc_date = (Select max(mnc_date) from patient_t01 inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO = patient_t01.MNC_HN_NO and pttm01.mnc_hn_yr = pttm01.mnc_hn_yr  where pttm01.MNC_ID_NO = '"+str(pid)+"')   "
    sqlwhere=" Where pttm01.MNC_ID_NO = '"+str(pid)+"'  and pttt01.mnc_date = (Select max(patient_t01.mnc_date) "
    sqlwhere+="from patient_t01 "
    sqlwhere+="Inner Join patient_m01 pm01 on pm01.mnc_hn_no = patient_t01.mnc_hn_no and pm01.mnc_hn_yr = patient_t01.mnc_hn_yr "
    sqlwhere+="left join LAB_T01 labt011 ON patient_t01.MNC_PRE_NO = labt011.MNC_PRE_NO AND patient_t01.MNC_DATE = labt011.MNC_DATE and patient_t01.MNC_hn_NO = labt011.MNC_hn_NO and patient_t01.MNC_hn_yr = labt011.MNC_hn_yr "
    sqlwhere+="left join LAB_T02 labt021 ON labt011.MNC_REQ_NO = labt021.MNC_REQ_NO AND labt011.MNC_REQ_DAT = labt021.MNC_REQ_DAT "
    sqlwhere+="where pm01.MNC_ID_NO = '"+str(pid)+"' and labt021.MNC_LB_CD in ('SE184','SE629'))   "
    sql=""
    sql="SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC, labt05.MNC_RES_VALUE, labt05.MNC_STS, labt05.MNC_RES, labt05.MNC_LB_RES, labt05.MNC_RES_UNT  "
    sql+=",convert(VARCHAR(20),labt05.mnc_req_dat,23) as mnc_req_dat, isnull(pm02.MNC_PFIX_DSC_e,'')+' '+pttm01.MNC_FNAME_T+' '+ pttm01.MNC_LNAME_T as mnc_patname "
    sql+=",usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check,labt01.MNC_REQ_NO,convert(VARCHAR(20),pttm01.mnc_bday,23) as dob "
    sql+=", convert(VARCHAR(20),labt02.mnc_result_dat,107) as mnc_result_dat, labt02.mnc_result_tim  "
    sql+=",patient_m02.MNC_PFIX_DSC_e + ' ' + patient_m26.MNC_DOT_FNAME_e  + ' ' + patient_m26.MNC_DOT_LNAME_e as mnc_doctor_full, pttm01.mnc_id_no,convert(VARCHAR(20),labt05.mnc_req_dat,107) as mnc_req_dat1, DATEDIFF(hour,MNC_BDAY,GETDATE())/8766 AS AgeYearsIntTrunc "
    sql+=",labt02.mnc_usr_result,labt02.mnc_usr_result_report,labt02.mnc_usr_result_approve,pttm01.MNC_HN_NO "
    sql+=" "
    sql+="from patient_t01 pttt01 "
    sql+="inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO =pttt01.MNC_HN_NO and pttm01.mnc_hn_yr = pttt01.mnc_hn_yr "
    sql+="left join patient_m02 pm02 on pttm01.MNC_DOT_PFIX = pm02.MNC_PFIX_CD "
    sql+="left join LAB_T01 labt01 ON pttt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pttt01.MNC_DATE = labt01.MNC_DATE and pttt01.MNC_hn_NO = labt01.MNC_hn_NO and pttt01.MNC_hn_yr = labt01.MNC_hn_yr "
    sql+="left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT "
    #sql+="left join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
    sql+="inner join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
    sql+="left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD "
    sql+="left join LAB_M04 labm04 ON labt05.MNC_LB_RES_CD = labm04.MNC_LB_RES_CD and labt05.MNC_LB_CD = labm04.MNC_LB_CD "
    sql+="left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " 
    sql+="left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report "
    sql+="left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " 
    sql+="left join patient_m26 on patient_m26.MNC_DOT_CD = labt01.mnc_dot_cd "
    sql+="inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD "
    #sql+=sqlother
    sql+=sqlwhere
    sql+="  and labt02.MNC_LB_CD = 'SE640' "
    #sql+="and b_patient_smartcard.date_order = (select max(b_patient_smartcard.date_order) from bn5_scan.dbo.b_patient_smartcard where b_patient_smartcard.hn = '"+hn+"'  )"
    #sql+=sqlother1
    sql+="Order By labt05.mnc_req_dat,labm01.MNC_LB_CD, labm04.MNC_LB_RES_CD "

    hn=""
    preno=""
    vsdate=""
    hnyear=""
    now = datetime.now()
    timestamp = datetime.timestamp(now)
    timestamp = str(timestamp).replace(".", "_")
    folderPID = "D:\\temp_line_atk\\"
    tick = pid+"_"+str(datetime_tick())
    print("tick "+str(tick))
    createFolder(folderPID,tick)
    #deleteFileinFolder(folder)
    rowcnt=0
    rowResultCnt=0
    rowcount=1
    reqdate1=""
    restime=""
    labname=""
    pttname=""
    reqno=""
    doctor=""
    dob=""
    pid=""
    resdate=""
    reporter=""
    resulter=""
    approver=""
    reqdate=""
    age1=""
    user1=""
    user2=""
    user3=""
    filename=""
    mnc_hn_no=""
    qrcoderesult=""
    #print("sendLabCovidPDFpid 1111")
    conn1 = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
    cur1 = conn1.cursor()
    cur1.execute(sql)
    resp1 = cur1.fetchall()
    #print("sendLabCovidPDFpid 2222")
    #print("cur1.rowcount "+str(cur1.rowcount))
    pdf = FPDF('L', 'mm', 'A5')
    pdf.add_font('TH Sarabun New', '', 'd:\\lab-result\\Sarabun\\THSarabunNew.ttf', uni=True)
    pdf.set_font('TH Sarabun New')
    pdf.add_page()
    y = 63
    col1=10
    col2=85
    col3=150
    pdf.set_font_size(10)
    for row1 in resp1:
        if row1[1] == None:
            print("hn no result"+str(mnc_hn_no))
            line_bot_api.push_message(user_id,TextSendMessage(text="HN "+str(mnc_hn_no)+"นี้ ยังไม่มี ผล "))
            continue
        if (row1[4] != None) and (rowcount==1) :
            qrcoderesult=row1[2]
        rowcount = rowcount+1
        mnc_hn_no= row1[23]
        #print("mnc_hn_no "+str(mnc_hn_no))
        filename = folderPID+timestamp+"_"+str(mnc_hn_no)+'_lab.jpg'
        #hn=""
        labcode = row1[0]
        labname = row1[1]
        reqdate = str(row1[7])
        reporter=row1[9]
        resulter=row1[10]
        approver=row1[11]
        reqno = str(row1[12])
        dob = row1[13]
        resdate = str(row1[14])
        restime = "0"+str(row1[15])
        doctor = row1[16]
        pid = row1[17]
        age1 = row1[19]
        user1 = row1[20]
        user2 = row1[21]
        user3 = row1[22]
        reqdate1 = row1[18]
        
        if row1[4] != None:
            labval = row1[2]
            interpret = row1[3]
            labres = row1[4]
            normal = row1[5]
            unit = row1[6]
            pttname = row1[8]
            reqdate = row1[7]
        else:
            labval=''
            interpret=''
            labres=''
            normal=''
            unit=''
            pttname=''
            reqdate=''
        pdf.text(col1+5,y, labres)      #draw result lab
        pdf.text(col2+10,y, labval)      #draw result lab
        pdf.text(col3,y, unit)      #draw result lab
        y = y+10
        #print("sendLabCovidPDFpid 2222 rowcount "+str(rowcount))
    
    #pdf.set_font('Arial', '', 14)
    #pdf.cell(40, 10, "เอกภพ  เพลินธรรม", 0)
    if(len(restime)>=5):
        restime = restime[len(restime)-4:len(restime)]
        restime = restime[0:2]+":"+restime[2:4]
    #pdf.text(40, 10, "โรงพยาบาล บางนา5", 0)
    #print("sendLabCovidPDFpid 3333")
    pdf.set_font_size(12)
    pdf.text(col1,10, "โรงพยาบาล บางนา5")
    pdf.text(col1,15, "Bangna5 General Hospital ")
    pdf.text(col1,20, "ใบรายงานผล Result LAB")
    pdf.set_font_size(8)
    pdf.text(col2,10, "Patient Name "+pttname)
    pdf.text(col2,15, "HN "+str(mnc_hn_no))
    pdf.text(col2,20, "เลขที่/Req no "+str(reqno)+"."+reqdate)
    pdf.text(col2,25, "แพทย์/Doctor "+str(doctor))

    pdf.text(col3,10, "วันเกิด/DOB : "+dob+" [age "+str(age1)+"]")
    pdf.text(col3,15, "ID/Passport : "+str(pid))
    pdf.text(col3,20, "วันที่ส่งตรวจ/Request Date : "+reqdate1)
    pdf.text(col3,25, "วันที่ออกผล/Result Date : "+resdate+" "+restime)
    pdf.set_font_size(10)
    pdf.text(col1+5,45, "GROUP : IMMUNO + TUMOR")
    pdf.text(col1+5,51, labname)
    pdf.set_font_size(8)

    pdf.text(col1,30, ".......................................................................................................................................................................................................................................................................")
    pdf.text(col1,38, ".......................................................................................................................................................................................................................................................................")
    pdf.text(col1+20,35, "รายการ/DESCRIPTION")
    pdf.text(col2,35, "ผลตรวจ/RESULT")
    pdf.text(col3,35, "แปลงผล/Interpret")

    pdf.text(col2-5,65, "................................................................................................")
    pdf.text(col2-5,75, "................................................................................................")
    pdf.text(col2-5,85, "................................................................................................")
    pdf.text(col2-5,95, "................................................................................................")
    pdf.text(col2-5,105, "...............................................................................................")

    pdf.text(col1-3,130, "......................................................................................................................................................................................................................................................................................")
    pdf.text(col1-3,138, "......................................................................................................................................................................................................................................................................................")
    pdf.set_font_size(7)
    pdf.text(col1,135, "ผู้บันทึก/recorder : "+reporter+"["+user1+"]")
    pdf.text(col2-10,135, "ผู้รายงาน/reporter : "+resulter+"["+user2+"]")
    pdf.text(col3-5,135, "ผู้ตรวจสอบ/approver : "+approver+"["+user3+"]")
    pdf.text(col1+5,145, "FM-LAB-096 (แก้ไขครั้งที่ 00-17/07/55) ")

    pdf.text(col3,145, "send date "+datetime.today().strftime("%d/%m/%Y %H:%M"))
    pdf.set_font_size(8)
    #print("sendLabCovidPDFpid 4444 0000")
    #im = Image.open("D:\\lab-result\\LOGO-BW-tran.png") 
    #pdf.image("D:\\lab-result\\LOGO-BW-tran.png",col3-20,40,20,20)     # ช้ามาก
    pdf.image("D:\\lab-result\\LOGO-BW-tran-400.png",col3-20,40,20,20)
    #imgQrcode = qrcode.make("http://result.bangna.co.th?hn"+str(mnc_hn_no)+"&req_no="+str(reqno)+"&req_date="+reqdate)
    #print("sendLabCovidPDFpid 4444 1111")
    imgQrcode = qrcode.make(pttname+" "+qrcoderesult+" "+str(reqno)+"."+reqdate+" "+str(mnc_hn_no)+" "+str(pid))
    type(imgQrcode)  # qrcode.image.pil.PilImage
    imgQrcode.save(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+".png")
    #print("sendLabCovidPDFpid 4444 2222")
    pdf.image(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+".png",col3+10,100,25,25)
    #print("sendLabCovidPDFpid 5555")
    pdf.output(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.pdf', 'F')
    time.sleep(0.2)

    images = convert_from_bytes(open(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.pdf', 'rb').read())
    for image in images:
        image.save(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.jpg', 'JPEG')
        #rint('filename_jpg1 '+filename_jpg1)
        #time.sleep(1)
    now = datetime.now()
    print("sendLabCovidPDFpid 6666")
    timestamp = datetime.timestamp(now)
    timestamp1 = str(timestamp)+'_lab.jpg'
    timestamp1_re = str(timestamp)+'_lab_re.jpg'
    url_org = 'https://bangna.co.th/line_bot/'+timestamp1
    urlprev = 'https://bangna.co.th/line_bot/'+timestamp1_re

    img = cv2.imread(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.jpg', cv2.IMREAD_UNCHANGED)
    scale_percent = 20 # percent of original size
    width = int(img.shape[1] * scale_percent / 100)
    height = int(img.shape[0] * scale_percent / 100)
    dim = (width, height)
    # resize image
    resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
    filename_re = folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'_re.jpg'
    cv2.imwrite(filename_re, resized)

    session = FTP('bangna.co.th','bangna','cy!C51x3')
    session.cwd('httpdocs/line_bot')
    file = open(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.jpg','rb')                  # file to send
    session.storbinary('STOR '+timestamp1, file)     # send the file
    file = open(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'_re.jpg','rb')                  # file to send
    session.storbinary('STOR '+timestamp1_re, file)     # send the file
    image_message = ImageSendMessage(original_content_url=url_org, preview_image_url=urlprev)
    line_bot_api.push_message(user_id, image_message)

def sendLabCovidPDFpid(pid, user_id):
    print("sendLabCovidPDFpid pid " +str(pid))
    print("sendLabCovidPDFpid user_id " +str(user_id))

    #sqlwhere="Where pttm01.MNC_ID_NO = '"+str(pid)+"'  and pttt01.mnc_date = (Select max(mnc_date) from patient_t01 inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO = patient_t01.MNC_HN_NO and pttm01.mnc_hn_yr = pttm01.mnc_hn_yr  where pttm01.MNC_ID_NO = '"+str(pid)+"')   "
    sqlwhere=" Where pttm01.MNC_ID_NO = '"+str(pid)+"'  and pttt01.mnc_date = (Select max(patient_t01.mnc_date) "
    sqlwhere+="from patient_t01 "
    sqlwhere+="Inner Join patient_m01 pm01 on pm01.mnc_hn_no = patient_t01.mnc_hn_no and pm01.mnc_hn_yr = patient_t01.mnc_hn_yr "
    sqlwhere+="left join LAB_T01 labt011 ON patient_t01.MNC_PRE_NO = labt011.MNC_PRE_NO AND patient_t01.MNC_DATE = labt011.MNC_DATE and patient_t01.MNC_hn_NO = labt011.MNC_hn_NO and patient_t01.MNC_hn_yr = labt011.MNC_hn_yr "
    sqlwhere+="left join LAB_T02 labt021 ON labt011.MNC_REQ_NO = labt021.MNC_REQ_NO AND labt011.MNC_REQ_DAT = labt021.MNC_REQ_DAT "
    sqlwhere+="where pm01.MNC_ID_NO = '"+str(pid)+"' and labt021.MNC_LB_CD in ('SE184','SE629'))   "
    sql=""
    sql="SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC, labt05.MNC_RES_VALUE, labt05.MNC_STS, labt05.MNC_RES, labt05.MNC_LB_RES, labt05.MNC_RES_UNT  "
    sql+=",convert(VARCHAR(20),labt05.mnc_req_dat,23) as mnc_req_dat,isnull(pm02.MNC_PFIX_DSC_e,'')+' '+pttm01.MNC_FNAME_T+' '+ pttm01.MNC_LNAME_T as mnc_patname "
    sql+=",usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check,labt01.MNC_REQ_NO,convert(VARCHAR(20),pttm01.mnc_bday,23) as dob "
    sql+=", convert(VARCHAR(20),labt02.mnc_result_dat,107) as mnc_result_dat, labt02.mnc_result_tim  "
    sql+=",patient_m02.MNC_PFIX_DSC_e + ' ' + patient_m26.MNC_DOT_FNAME_e  + ' ' + patient_m26.MNC_DOT_LNAME_e as mnc_doctor_full, pttm01.mnc_id_no,convert(VARCHAR(20),labt05.mnc_req_dat,107) as mnc_req_dat1, DATEDIFF(hour,MNC_BDAY,GETDATE())/8766 AS AgeYearsIntTrunc "
    sql+=",labt02.mnc_usr_result,labt02.mnc_usr_result_report,labt02.mnc_usr_result_approve,pttm01.MNC_HN_NO "
    sql+=""
    sql+="from patient_t01 pttt01 "
    sql+="inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO =pttt01.MNC_HN_NO and pttm01.mnc_hn_yr = pttt01.mnc_hn_yr "
    sql+="left join patient_m02 pm02 on pttm01.MNC_DOT_PFIX =pm02.MNC_PFIX_CD "
    sql+="left join LAB_T01 labt01 ON pttt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pttt01.MNC_DATE = labt01.MNC_DATE and pttt01.MNC_hn_NO = labt01.MNC_hn_NO and pttt01.MNC_hn_yr = labt01.MNC_hn_yr "
    sql+="left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT "
    #sql+="left join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
    sql+="inner join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
    sql+="left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD "
    sql+="left join LAB_M04 labm04 ON labt05.MNC_LB_RES_CD = labm04.MNC_LB_RES_CD and labt05.MNC_LB_CD = labm04.MNC_LB_CD "
    sql+="left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " 
    sql+="left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report "
    sql+="left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " 
    sql+="left join patient_m26 on patient_m26.MNC_DOT_CD = labt01.mnc_dot_cd "
    sql+="inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD "
    #sql+=sqlother
    sql+=sqlwhere
    sql+="  and labt02.MNC_LB_CD in ('SE184','SE629') "
    #sql+="and b_patient_smartcard.date_order = (select max(b_patient_smartcard.date_order) from bn5_scan.dbo.b_patient_smartcard where b_patient_smartcard.hn = '"+hn+"'  )"
    #sql+=sqlother1
    sql+="Order By labt05.mnc_req_dat,labm01.MNC_LB_CD, labm04.MNC_LB_RES_CD "

    hn=""
    preno=""
    vsdate=""
    hnyear=""
    now = datetime.now()
    timestamp = datetime.timestamp(now)
    timestamp = str(timestamp).replace(".", "_")
    folderPID = "D:\\temp_line\\"
    tick = pid+"_"+str(datetime_tick())
    print("tick "+str(tick))
    createFolder(folderPID,tick)
    #deleteFileinFolder(folder)
    rowcnt=0
    rowResultCnt=0
    rowcount=1
    reqdate1=""
    restime=""
    labname=""
    pttname=""
    reqno=""
    doctor=""
    dob=""
    pid=""
    resdate=""
    reporter=""
    resulter=""
    approver=""
    reqdate=""
    age1=""
    user1=""
    user2=""
    user3=""
    filename=""
    mnc_hn_no=""
    qrcoderesult=""
    #print("sendLabCovidPDFpid 1111")
    conn1 = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
    cur1 = conn1.cursor()
    cur1.execute(sql)
    resp1 = cur1.fetchall()
    #print("sendLabCovidPDFpid 2222")
    #print("cur1.rowcount "+str(cur1.rowcount))
    pdf = FPDF('L', 'mm', 'A5')
    pdf.add_font('Sarabun', '', 'd:\\lab-result\\Sarabun\\Sarabun-Regular.ttf', uni=True)
    pdf.set_font('Sarabun')
    pdf.add_page()
    y = 63
    col1=10
    col2=85
    col3=150
    pdf.set_font_size(10)
    for row1 in resp1:
        if row1[1] == None:
            print("hn no result"+str(mnc_hn_no))
            line_bot_api.push_message(user_id,TextSendMessage(text="HN "+str(mnc_hn_no)+"นี้ ยังไม่มี ผล "))
            continue
        if (row1[4] != None) and (rowcount==1) :
            qrcoderesult=row1[2]
        rowcount = rowcount+1
        mnc_hn_no= row1[23]
        #print("mnc_hn_no "+str(mnc_hn_no))
        filename = folderPID+timestamp+"_"+str(mnc_hn_no)+'_lab.jpg'
        #hn=""
        labcode = row1[0]
        labname = row1[1]
        reqdate = str(row1[7])
        reporter=row1[9]
        resulter=row1[10]
        approver=row1[11]
        reqno = str(row1[12])
        dob = row1[13]
        resdate = str(row1[14])
        restime = "0"+str(row1[15])
        doctor = row1[16]
        pid = row1[17]
        age1 = row1[19]
        user1 = row1[20]
        user2 = row1[21]
        user3 = row1[22]
        reqdate1 = row1[18]
        
        if row1[4] != None:
            labval = row1[2]
            interpret = row1[3]
            labres = row1[4]
            normal = row1[5]
            unit = row1[6]
            pttname = row1[8]
            reqdate = row1[7]
        else:
            labval=''
            interpret=''
            labres=''
            normal=''
            unit=''
            pttname=''
            reqdate=''
        pdf.text(col1+5,y, labres)      #draw result lab
        pdf.text(col2+10,y, labval)      #draw result lab
        pdf.text(col3,y, unit)      #draw result lab
        y = y+10
        #print("sendLabCovidPDFpid 2222 rowcount "+str(rowcount))
    
    #pdf.set_font('Arial', '', 14)
    #pdf.cell(40, 10, "เอกภพ  เพลินธรรม", 0)
    if(len(restime)>=5):
        restime = restime[len(restime)-4:len(restime)]
        restime = restime[0:2]+":"+restime[2:4]
    #pdf.text(40, 10, "โรงพยาบาล บางนา5", 0)
    #print("sendLabCovidPDFpid 3333")
    pdf.set_font_size(12)
    pdf.text(col1,10, "โรงพยาบาล บางนา5")
    pdf.text(col1,15, "Bangna5 General Hospital ")
    pdf.text(col1,20, "ใบรายงานผล Result LAB")
    pdf.set_font_size(8)
    pdf.text(col2,10, "Patient Name "+pttname)
    pdf.text(col2,15, "HN "+str(mnc_hn_no))
    pdf.text(col2,20, "เลขที่/Req no "+str(reqno)+"."+reqdate)
    pdf.text(col2,25, "แพทย์/Doctor "+str(doctor))

    pdf.text(col3,10, "วันเกิด/DOB : "+dob+" [age "+str(age1)+"]")
    pdf.text(col3,15, "ID/Passport : "+str(pid))
    pdf.text(col3,20, "วันที่ส่งตรวจ/Request Date : "+reqdate1)
    pdf.text(col3,25, "วันที่ออกผล/Result Date : "+resdate+" "+restime)
    pdf.set_font_size(10)
    pdf.text(col1+5,45, "GROUP : IMMUNO + TUMOR")
    pdf.text(col1+5,51, labname)
    pdf.set_font_size(8)

    pdf.text(col1,30, ".......................................................................................................................................................................................................................................................................")
    pdf.text(col1,38, ".......................................................................................................................................................................................................................................................................")
    pdf.text(col1+20,35, "รายการ/DESCRIPTION")
    pdf.text(col2,35, "ผลตรวจ/RESULT")
    pdf.text(col3,35, "แปลงผล/Interpret")

    pdf.text(col2-5,65, "................................................................................................")
    pdf.text(col2-5,75, "................................................................................................")
    pdf.text(col2-5,85, "................................................................................................")
    pdf.text(col2-5,95, "................................................................................................")
    pdf.text(col2-5,105, "...............................................................................................")

    pdf.text(col1-3,130, "......................................................................................................................................................................................................................................................................................")
    pdf.text(col1-3,138, "......................................................................................................................................................................................................................................................................................")
    pdf.set_font_size(7)
    pdf.text(col1,135, "ผู้บันทึก/recorder : "+reporter+"["+user1+"]")
    pdf.text(col2-10,135, "ผู้รายงาน/reporter : "+resulter+"["+user2+"]")
    pdf.text(col3-5,135, "ผู้ตรวจสอบ/approver : "+approver+"["+user3+"]")
    pdf.text(col1+5,145, "FM-LAB-096 (แก้ไขครั้งที่ 00-17/07/55) ")

    pdf.text(col3,145, "send date "+datetime.today().strftime("%d/%m/%Y %H:%M"))
    pdf.set_font_size(8)
    #print("sendLabCovidPDFpid 4444 0000")
    #im = Image.open("D:\\lab-result\\LOGO-BW-tran.png") 
    #pdf.image("D:\\lab-result\\LOGO-BW-tran.png",col3-20,40,20,20)     # ช้ามาก
    pdf.image("D:\\lab-result\\LOGO-BW-tran-400.png",col3-20,40,20,20)
    #imgQrcode = qrcode.make("http://result.bangna.co.th?hn"+str(mnc_hn_no)+"&req_no="+str(reqno)+"&req_date="+reqdate)
    #print("sendLabCovidPDFpid 4444 1111")
    imgQrcode = qrcode.make(pttname+" "+qrcoderesult+" "+str(reqno)+"."+reqdate+" "+str(mnc_hn_no)+" "+str(pid))
    type(imgQrcode)  # qrcode.image.pil.PilImage
    imgQrcode.save(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+".png")
    #print("sendLabCovidPDFpid 4444 2222")
    pdf.image(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+".png",col3+10,100,25,25)
    #print("sendLabCovidPDFpid 5555")
    pdf.output(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.pdf', 'F')
    time.sleep(0.2)

    images = convert_from_bytes(open(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.pdf', 'rb').read())
    for image in images:
        image.save(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.jpg', 'JPEG')
        #rint('filename_jpg1 '+filename_jpg1)
        #time.sleep(1)
    now = datetime.now()
    print("sendLabCovidPDFpid 6666")
    timestamp = datetime.timestamp(now)
    timestamp1 = str(timestamp)+'_lab.jpg'
    timestamp1_re = str(timestamp)+'_lab_re.jpg'
    url_org = 'https://bangna.co.th/line_bot/'+timestamp1
    urlprev = 'https://bangna.co.th/line_bot/'+timestamp1_re

    img = cv2.imread(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.jpg', cv2.IMREAD_UNCHANGED)
    scale_percent = 20 # percent of original size
    width = int(img.shape[1] * scale_percent / 100)
    height = int(img.shape[0] * scale_percent / 100)
    dim = (width, height)
    # resize image
    resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
    filename_re = folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'_re.jpg'
    cv2.imwrite(filename_re, resized)

    session = FTP('bangna.co.th','bangna','cy!C51x3')
    session.cwd('httpdocs/line_bot')
    file = open(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'.jpg','rb')                  # file to send
    session.storbinary('STOR '+timestamp1, file)     # send the file
    file = open(folderPID+str(tick)+"\\"+str(mnc_hn_no)+"_"+str(reqno)+"_"+str(reqdate)+'_re.jpg','rb')                  # file to send
    session.storbinary('STOR '+timestamp1_re, file)     # send the file
    image_message = ImageSendMessage(original_content_url=url_org, preview_image_url=urlprev)
    line_bot_api.push_message(user_id, image_message)


def sendLabPID(pid, user_id):
    print("sendLabPID " + str(pid))
    sqlwhere="Where pttm01.MNC_ID_NO = '"+pid+"'  and pttt01.mnc_date = (Select max(mnc_date) from patient_t01 inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO = patient_t01.MNC_HN_NO and pttm01.mnc_hn_yr = pttm01.mnc_hn_yr  where pttm01.MNC_ID_NO = '"+pid+"')   "
    
    col2 = 260
    col3 = 600
    col4 = 800
    col5 = 1100
    col6 = 1200
    y = 295
    fnt = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 42)
    fnt36 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 36)
    fnt40 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 40)
    fnt45 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 45)
    fntB1 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 52)
    fntS30 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 30)
    fntS32 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 32)
    fntS28 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 28)
    fntS24 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 24)
    fntS20 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 20)
    
    sql=""
    sql="SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC, labt05.MNC_RES_VALUE, labt05.MNC_STS, labt05.MNC_RES, labt05.MNC_LB_RES, labt05.MNC_RES_UNT  "
    sql+=",convert(VARCHAR(20),labt05.mnc_req_dat,23) as mnc_req_dat, isnull(pm02.MNC_PFIX_DSC_e,'')+' '+pttm01.MNC_FNAME_T+' '+ pttm01.MNC_LNAME_T as mnc_patname "
    sql+=",usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check,labt01.MNC_REQ_NO,convert(VARCHAR(20),pttm01.mnc_bday,23) as dob "
    sql+=", convert(VARCHAR(20),labt02.mnc_result_dat,107) as mnc_result_dat, labt02.mnc_result_tim  "
    sql+=",patient_m02.MNC_PFIX_DSC_e + ' ' + patient_m26.MNC_DOT_FNAME_e  + ' ' + patient_m26.MNC_DOT_LNAME_e as mnc_doctor_full, pttm01.mnc_id_no,convert(VARCHAR(20),labt05.mnc_req_dat,107) as mnc_req_dat1, DATEDIFF(hour,MNC_BDAY,GETDATE())/8766 AS AgeYearsIntTrunc "
    sql+=",labt02.mnc_usr_result,labt02.mnc_usr_result_report,labt02.mnc_usr_result_approve,pttm01.MNC_HN_NO "
    sql+=""
    sql+="from patient_t01 pttt01 "
    sql+="inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO =pttt01.MNC_HN_NO and pttm01.mnc_hn_yr = pttm01.mnc_hn_yr "
    sql+="left join patient_m02 pm02 on pttm01.MNC_DOT_PFIX =pm02.MNC_PFIX_CD "
    sql+="left join LAB_T01 labt01 ON pttt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pttt01.MNC_DATE = labt01.MNC_DATE and pttt01.MNC_hn_NO = labt01.MNC_hn_NO and pttt01.MNC_hn_yr = labt01.MNC_hn_yr "
    sql+="left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT "
    #sql+="left join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
    sql+="inner join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
    sql+="left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD "
    sql+="left join LAB_M04 labm04 ON labt05.MNC_LB_RES_CD = labm04.MNC_LB_RES_CD and labt05.MNC_LB_CD = labm04.MNC_LB_CD "
    sql+="left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " 
    sql+="left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report "
    sql+="left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " 
    sql+="left join patient_m26 on patient_m26.MNC_DOT_CD = labt01.mnc_dot_cd "
    sql+="inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD "
    #sql+=sqlother
    sql+=sqlwhere
    sql+="  and labt02.MNC_LB_CD in ('SE184','SE629') "
    #sql+="and b_patient_smartcard.date_order = (select max(b_patient_smartcard.date_order) from bn5_scan.dbo.b_patient_smartcard where b_patient_smartcard.hn = '"+hn+"'  )"
    #sql+=sqlother1
    sql+="Order By labt05.mnc_req_dat,labm01.MNC_LB_CD, labm04.MNC_LB_RES_CD "
    
    #print(sql)
    hn=""
    preno=""
    vsdate=""
    hnyear=""
    now = datetime.now()
    timestamp = datetime.timestamp(now)
    timestamp = str(timestamp).replace(".", "_")
    folder = "D:\\temp_line\\"
    tick = str(datetime_tick())
    print("tick "+str(tick))
    createFolder(folder,tick)
    deleteFileinFolder(folder)
    # for filename in os.listdir(folder):
    #     file_path = os.path.join(folder, filename)
    #     try:
    #         if os.path.isfile(file_path) or os.path.islink(file_path):
    #             os.unlink(file_path)
    #         elif os.path.isdir(file_path):
    #             shutil.rmtree(file_path)
    #     except Exception as e:
    #         print('Failed to delete %s. Reason: %s' % (file_path, e))
    rowcnt=0
    rowResultCnt=0
    rowcount=1
    reqdate1=""
    restime=""
    labname=""
    pttname=""
    reqno=""
    doctor=""
    dob=""
    pid=""
    resdate=""
    reporter=""
    resulter=""
    approver=""
    reqdate=""
    age1=""
    user1=""
    user2=""
    user3=""
    filename=""
    mnc_hn_no=""
    image = Image.new(mode = "RGB", size = (1600,800), color = "white")
    draw = ImageDraw.Draw(image)
    conn1 = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
    cur1 = conn1.cursor()
    cur1.execute(sql)
    resp1 = cur1.fetchall()
    #print("cur1.rowcount "+str(cur1.rowcount))
    for row1 in resp1:
        if row1[1] == None:
            print("hn no result"+str(mnc_hn_no))
            line_bot_api.push_message(user_id,TextSendMessage(text="HN "+str(mnc_hn_no)+"นี้ ยังไม่มี ผล "))
            continue
        rowcount = rowcount+1
        mnc_hn_no= row1[23]
        print("mnc_hn_no "+str(mnc_hn_no))
        filename = folder+timestamp+"_"+str(mnc_hn_no)+'_lab.jpg'
        #hn=""
        labcode = row1[0]
        labname = row1[1]
        reqdate = row1[7]
        reporter=row1[9]
        resulter=row1[10]
        approver=row1[11]
        reqno = str(row1[12])
        dob = row1[13]
        resdate = row1[14]
        restime = "0"+str(row1[15])
        doctor = row1[16]
        pid = row1[17]
        age1 = row1[19]
        user1 = row1[20]
        user2 = row1[21]
        user3 = row1[22]
        reqdate1 = row1[18]
        
        if row1[4] != None:
            labval = row1[2]
            interpret = row1[3]
            labres = row1[4]
            normal = row1[5]
            unit = row1[6]
            pttname = row1[8]
            reqdate = row1[7]
        else:
            labval=''
            interpret=''
            labres=''
            normal=''
            unit=''
            pttname=''
            reqdate=''
        draw.text((col2-50,y), labres, font=fntS28, fill=(0,0,0))
        draw.text((col4-20,y), labval, font=fntS28, fill=(0,0,0))
        draw.text((col6,y), unit, font=fntS24, fill=(0,0,0))
        y = y+30
        #row = row+1
    if(len(restime)>=5):
            restime = restime[len(restime)-4:len(restime)]
    draw.text((40,235), "GROUP : IMMUNO + TUMOR", font=fntS28, fill=(0,0,0))
    draw.text((40,265), labname, font=fntS28, fill=(0,0,0))

    draw.text((20,20), "โรงพยาบาล บางนา5", font=fnt45, fill=(0,0,0))
    draw.text((20,60), "Bangna5 General Hospital ", font=fnt45, fill=(0,0,0))
    draw.text((20,100), "ใบรายงานผล Result LAB", font=fnt45, fill=(0,0,0))

    draw.text((520,20), "Patient Name "+pttname, font=fnt, fill=(0,0,0))
    draw.text((520,60), "HN "+str(mnc_hn_no), font=fnt, fill=(0,0,0))
    draw.text((520,100), "เลขที่/Req no "+str(reqno)+"."+reqdate, font=fntS30, fill=(0,0,0))
    draw.text((520,135), "แพทย์/Doctor "+str(doctor), font=fntS30, fill=(0,0,0))

    draw.text((1250,20), "วันเกิด/DOB : "+dob+" [age "+str(age1)+"]", font=fntS24, fill=(0,0,0))
    draw.text((1250,60), "ID/Passport : "+str(pid), font=fntS24, fill=(0,0,0))
    draw.text((1250,100), "วันที่ส่งตรวจ/Request Date : "+reqdate1, font=fntS24, fill=(0,0,0))
    draw.text((1250,135), "วันที่ออกผล/Result Date : "+resdate+" "+restime, font=fntS24, fill=(0,0,0))

    draw.text((25,153), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((25,190), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((300,170), "รายการ/DESCRIPTION", font=fntS32, fill=(0,0,0))
    draw.text((800,170), "ผลตรวจ/RESULT", font=fntS32, fill=(0,0,0))

    draw.text((550,305), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((550,335), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((550,365), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((550,395), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((550,425), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))

    draw.text((25,680), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((25,720), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((45,700), "ผู้บันทึก/recorder : "+reporter+"["+user1+"]", font=fntS28, fill=(0,0,0))
    draw.text((600,700), "ผู้รายงาน/reporter : "+resulter+"["+user2+"]", font=fntS28, fill=(0,0,0))
    draw.text((1150,700), "ผู้ตรวจสอบ/approver : "+approver+"["+user3+"]", font=fntS28, fill=(0,0,0))
    draw.text((35,740), "FM-LAB-096 (แก้ไขครั้งที่ 00-17/07/55) ", font=fntS20, fill=(0,0,0))

    draw.text((1400,740), "send date "+datetime.today().strftime("%d/%m/%Y"), font=fntS20, fill=(0,0,0))
    if(filename!=""):
        image.save(filename)
        rowResultCnt=rowResultCnt+1
        time.sleep(0.05)
    rowcnt = rowcnt+1
    print('send line_bot_api ')
    filename_re = 'D:\\temp_line\\'+timestamp+'_lab_re.jpg'
    timestamp1 = timestamp+'_lab.jpg'
    timestamp1_re = timestamp+'_lab_re.jpg'
    url_org = 'https://bangna.co.th/line_bot/'+timestamp1
    urlprev = 'https://bangna.co.th/line_bot/'+timestamp1_re


    time.sleep(0.2)
    print("filename "+filename)
    if(filename!=""):
        img = cv2.imread(filename, cv2.IMREAD_UNCHANGED)
        scale_percent = 20 # percent of original size
        width = int(img.shape[1] * scale_percent / 100)
        height = int(img.shape[0] * scale_percent / 100)
        dim = (width, height)
        # resize image
        resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
        cv2.imwrite(filename_re, resized)
        print('filename_re '+filename_re)
        session = FTP('bangna.co.th','bangna','cy!C51x3')
        session.cwd('httpdocs/line_bot')
        file = open(filename,'rb')                  # file to send
        session.storbinary('STOR '+timestamp1, file)     # send the file
        file = open(filename_re,'rb')                  # file to send
        session.storbinary('STOR '+timestamp1_re, file)     # send the file

        image_message = ImageSendMessage(original_content_url=url_org, preview_image_url=urlprev)
        #line_bot_api.push_message(get_sourceid(event), image_message)
        line_bot_api.push_message(user_id, image_message)
    else:
        line_bot_api.push_message(user_id,TextSendMessage(text=hn+"ไม่ได้ ผล "+mnc_hn_no))

    cur1.close()
    conn1.close()

def sendLabDoc(doc, user_id):
    print("sendLabDoc " + str(doc))
    col2 = 260
    col3 = 600
    col4 = 800
    col5 = 1100
    col6 = 1200
    y = 295
    fnt = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 42)
    fnt36 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 36)
    fnt40 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 40)
    fnt45 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 45)
    fntB1 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 52)
    fntS30 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 30)
    fntS32 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 32)
    fntS28 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 28)
    fntS24 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 24)
    fntS20 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 20)
    sqlwhere=""
    #sqlwhere+="Where b_patient_smartcard.doc = '"+doc+"' and labt02.MNC_LB_CD in ('SE184','SE629') and pttt01.MNC_DATE >= DATEADD(day,-3, GETDATE()) "
    # แก้เป็น 7 วันเพราะ ต้องการดูข้อมูลย้อนหลัง
    sqlwhere+="Where b_patient_smartcard.doc = '"+doc+"' and labt02.MNC_LB_CD in ('SE184','SE629') and pttt01.MNC_DATE >= DATEADD(day,-7, GETDATE()) "
    
    sql="Select pttt01.mnc_hn_no, pttt01.mnc_hn_yr, convert(VARCHAR(20),pttt01.mnc_date,23) as mnc_date, pttt01.mnc_pre_no  "
    sql+="from patient_t01 pttt01 "
    sql+="inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO = pttt01.MNC_HN_NO and pttm01.mnc_hn_yr = pttt01.mnc_hn_yr "
    #ที่สั่ง lab covid
    sql+="left join LAB_T01 labt01 ON pttt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pttt01.MNC_DATE = labt01.MNC_DATE and labt01.mnc_hn_no = pttt01.mnc_hn_no "
    sql+="left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT and labt01.mnc_req_yr = labt02.mnc_req_yr "
    sql+="inner join bn5_scan.dbo.b_patient_smartcard on pttm01.patient_smartcard_id = b_patient_smartcard.patient_smartcard_id "
    sql+=sqlwhere
    sql+="Order By b_patient_smartcard.patient_smartcard_id "
    #print(sql)
    conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
    cur = conn.cursor()
    cur.execute(sql)
    resp = cur.fetchall()
    hn=""
    preno=""
    vsdate=""
    hnyear=""
    now = datetime.now()
    timestamp = datetime.timestamp(now)
    timestamp = str(timestamp).replace(".", "_")
    folder = "c:\\temp_line\\"
    folderdocb2 = "c:\\temp_line_doc_b2\\"
    folderdocb1 = "c:\\temp_line_doc_b1\\"
    if (doc[0:1]=="1"):
        folder = folderdocb1
    elif (doc[0:1]=="2"):
        folder = folderdocb2
    deleteFileinFolder(folder)
    rowcnt=0
    for row in resp:
        rowcnt = rowcnt+1
    if (rowcnt>50) and (rowcnt<=100) :
        line_bot_api.push_message(user_id,TextSendMessage(text="จำนวนข้อมูล "+str(rowcnt)+" นานหน่อยครับ "))
    elif (rowcnt>100) and (rowcnt<=200):
        line_bot_api.push_message(user_id,TextSendMessage(text="จำนวนข้อมูล "+str(rowcnt)+" นานแน่ๆ "))
    elif (rowcnt<50):
        line_bot_api.push_message(user_id,TextSendMessage(text="จำนวนข้อมูล "+str(rowcnt)+" รอซักครู่ "))
    rowcnt=0
    rowResultCnt=0
    #print("sendLabDoc for row in resp: sql " +sql )
    for row in resp:
        rowcnt=rowcnt+1
        y = 295
        hn=str(row[0])
        preno=str(row[3])
        vsdate=str(row[2])
        hnyear=str(row[1])
        print("hn "+str(hn))
        sql=""
        sql="SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC, labt05.MNC_RES_VALUE, labt05.MNC_STS, labt05.MNC_RES, labt05.MNC_LB_RES, labt05.MNC_RES_UNT  "
        sql+=",convert(VARCHAR(20),labt05.mnc_req_dat,23) as mnc_req_dat, isnull(labt01.mnc_patname,'') as mnc_patname "
        sql+=",usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check,labt01.MNC_REQ_NO,convert(VARCHAR(20),pttm01.mnc_bday,23) as dob "
        sql+=", convert(VARCHAR(20),labt02.mnc_result_dat,107) as mnc_result_dat, labt02.mnc_result_tim  "
        sql+=",patient_m02.MNC_PFIX_DSC_e + ' ' + patient_m26.MNC_DOT_FNAME_e  + ' ' + patient_m26.MNC_DOT_LNAME_e as mnc_doctor_full, pttm01.mnc_id_no,convert(VARCHAR(20),labt05.mnc_req_dat,107) as mnc_req_dat1, DATEDIFF(hour,MNC_BDAY,GETDATE())/8766 AS AgeYearsIntTrunc "
        sql+=",labt02.mnc_usr_result,labt02.mnc_usr_result_report,labt02.mnc_usr_result_approve "
        sql+=""
        sql+="from patient_t01 pttt01 "
        sql+="inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO =pttt01.MNC_HN_NO and pttm01.mnc_hn_yr = pttm01.mnc_hn_yr "
        sql+="left join LAB_T01 labt01 ON pttt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pttt01.MNC_DATE = labt01.MNC_DATE and pttt01.MNC_hn_NO = labt01.MNC_hn_NO and pttt01.MNC_hn_yr = labt01.MNC_hn_yr "
        sql+="left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT "
        #sql+="left join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
        sql+="inner join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
        sql+="left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD "
        sql+="left join LAB_M04 labm04 ON labt05.MNC_LB_RES_CD = labm04.MNC_LB_RES_CD and labt05.MNC_LB_CD = labm04.MNC_LB_CD "
        sql+="left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " 
        sql+="left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report "
        sql+="left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " 
        sql+="left join patient_m26 on patient_m26.MNC_DOT_CD = labt01.mnc_dot_cd "
        sql+="inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD "
        #sql+="inner join bn5_scan.dbo.b_patient_smartcard on pttt01.patient_smartcard_id = b_patient_smartcard.patient_smartcard_id "
        sql+="Where pttt01.mnc_hn_no = '"+hn+"' and pttt01.mnc_hn_yr = '"+hnyear+"' and pttt01.mnc_date = '"+vsdate+"' and pttt01.mnc_pre_no = '"+preno+"'  "
        sql+="Order By labt05.mnc_req_dat,labm01.MNC_LB_CD, labm04.MNC_LB_RES_CD "
        #print("sql "+sql)
        rowcount=1
        reqdate1=""
        restime=""
        labname=""
        pttname=""
        reqno=""
        doctor=""
        dob=""
        pid=""
        resdate=""
        reporter=""
        resulter=""
        approver=""
        reqdate=""
        age1=""
        user1=""
        user2=""
        user3=""
        filename=""
        image = Image.new(mode = "RGB", size = (1600,800), color = "white")
        draw = ImageDraw.Draw(image)

        conn1 = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
        cur1 = conn1.cursor()
        cur1.execute(sql)
        resp1 = cur1.fetchall()
        #print("cur1.rowcount "+str(cur1.rowcount))
        for row1 in resp1:
            if row1[1] == None:
                line_bot_api.push_message(user_id,TextSendMessage(text="HN นี้ ยังไม่ได้ผล "))
                continue
            rowcount = rowcount+1
            filename = folder+str(rowcnt)+"_"+doc+"_"+timestamp+"_"+hn+'_lab.jpg'
            labcode = row1[0]
            labname = row1[1]
            reqdate = row1[7]
            reporter=row1[9]
            resulter=row1[10]
            approver=row1[11]
            reqno = str(row1[12])
            dob = row1[13]
            resdate = row1[14]
            restime = "0"+str(row1[15])
            doctor = row1[16]
            pid = row1[17]
            age1 = row1[19]
            user1 = row1[20]
            user2 = row1[21]
            user3 = row1[22]
            reqdate1 = row1[18]
            
            if row1[4] != None:
                labval = row1[2]
                interpret = row1[3]
                labres = row1[4]
                normal = row1[5]
                unit = row1[6]
                pttname = row1[8]
                reqdate = row1[7]
            else:
                labval=''
                interpret=''
                labres=''
                normal=''
                unit=''
                pttname=''
                reqdate=''
            draw.text((col2-50,y), labres, font=fntS28, fill=(0,0,0))
            draw.text((col4-20,y), labval, font=fntS28, fill=(0,0,0))
            draw.text((col6,y), unit, font=fntS24, fill=(0,0,0))
            y = y+30
            #row = row+1
        if(len(restime)>=5):
            restime = restime[len(restime)-4:len(restime)]
        draw.text((40,235), "GROUP : IMMUNO + TUMOR", font=fntS28, fill=(0,0,0))
        draw.text((40,265), labname, font=fntS28, fill=(0,0,0))

        draw.text((20,20), "โรงพยาบาล บางนา5", font=fnt45, fill=(0,0,0))
        draw.text((20,60), "Bangna5 General Hospital ", font=fnt45, fill=(0,0,0))
        draw.text((20,100), "ใบรายงานผล Result LAB", font=fnt45, fill=(0,0,0))

        draw.text((520,20), "Patient Name "+pttname, font=fnt, fill=(0,0,0))
        draw.text((520,60), "HN "+hn, font=fnt, fill=(0,0,0))
        draw.text((520,100), "เลขที่/Req no "+str(reqno)+"."+reqdate, font=fntS30, fill=(0,0,0))
        draw.text((520,135), "แพทย์/Doctor "+str(doctor), font=fntS30, fill=(0,0,0))

        draw.text((1250,20), "วันเกิด/DOB : "+dob+" [age "+str(age1)+"]", font=fntS24, fill=(0,0,0))
        draw.text((1250,60), "ID/Passport : "+str(pid), font=fntS24, fill=(0,0,0))
        draw.text((1250,100), "วันที่ส่งตรวจ/Request Date : "+reqdate1, font=fntS24, fill=(0,0,0))
        draw.text((1250,135), "วันที่ออกผล/Result Date : "+resdate+" "+restime, font=fntS24, fill=(0,0,0))

        draw.text((25,153), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
        draw.text((25,190), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
        draw.text((300,170), "รายการ/DESCRIPTION", font=fntS32, fill=(0,0,0))
        draw.text((800,170), "ผลตรวจ/RESULT", font=fntS32, fill=(0,0,0))

        draw.text((550,305), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
        draw.text((550,335), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
        draw.text((550,365), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
        draw.text((550,395), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
        draw.text((550,425), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))

        draw.text((25,680), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
        draw.text((25,720), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
        draw.text((45,700), "ผู้บันทึก/recorder : "+reporter+"["+user1+"]", font=fntS28, fill=(0,0,0))
        draw.text((600,700), "ผู้รายงาน/reporter : "+resulter+"["+user2+"]", font=fntS28, fill=(0,0,0))
        draw.text((1150,700), "ผู้ตรวจสอบ/approver : "+approver+"["+user3+"]", font=fntS28, fill=(0,0,0))
        draw.text((35,740), "FM-LAB-096 (แก้ไขครั้งที่ 00-17/07/55) ", font=fntS20, fill=(0,0,0))

        draw.text((1400,740), "send date "+datetime.today().strftime("%d/%m/%Y"), font=fntS20, fill=(0,0,0))
        if(filename!=""):
            image.save(filename)
            print(filename)
            rowResultCnt=rowResultCnt+1
            time.sleep(0.05)
        #rowcnt = rowcnt+1
        if (rowcnt==50):
            line_bot_api.push_message(user_id,TextSendMessage(text="ทำได้ "+str(rowcnt)+" รายการ "))
        elif (rowcnt==100):
            line_bot_api.push_message(user_id,TextSendMessage(text="ทำได้ "+str(rowcnt)+" รายการ "))
        cur1.close()
        conn1.close()
        if(labname==""):
            line_bot_api.push_message(user_id,TextSendMessage(text="hn ไม่มีผล  "+str(hn)))
            continue
        print(filename)
        if(hn==""):
            print('no data ')
        img = cv2.imread(filename, cv2.IMREAD_UNCHANGED)
        scale_percent = 20 # percent of original size
        width = int(img.shape[1] * scale_percent / 100)
        height = int(img.shape[0] * scale_percent / 100)
        dim = (width, height)
        # resize image
        #filename_re = folder+timestamp+'_lab_re.jpg'
        #now = datetime.now()
        #timestamp = datetime.timestamp(now)
        #timestamp = str(timestamp).replace(".", "_")
        #filename = folder+timestamp+'_lab.jpg'
        #filename_re = folder+timestamp+'_lab_re.jpg'
        #timestamp1 = timestamp+'_lab.jpg'
        #timestamp1_re = timestamp+'_lab_re.jpg'
        filename_re = folder+str(rowcnt)+"_"+doc+"_"+timestamp+"_"+hn+'_lab_re.jpg'
        resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
        print('filename_re '+filename_re)
        cv2.imwrite(filename_re, resized)
        timestamp1 = str(rowcnt)+"_"+doc+"_"+timestamp+"_"+hn+'_lab.jpg'
        timestamp1_re = str(rowcnt)+"_"+doc+"_"+timestamp+"_"+hn+'_lab_re.jpg'
        session = FTP('bangna.co.th','bangna','cy!C51x3')
        session.cwd('httpdocs/line_bot')
        print('filename '+filename)
        file = open(filename,'rb')                  # file to send
        session.storbinary('STOR '+timestamp1, file)     # send the file
        file = open(filename_re,'rb')                  # file to send
        session.storbinary('STOR '+timestamp1_re, file)     # send the file
        
        url_org = 'https://bangna.co.th/line_bot/'+timestamp1
        urlprev = 'https://bangna.co.th/line_bot/'+timestamp1_re
        time.sleep(0.05)
        image_message = ImageSendMessage(original_content_url=url_org, preview_image_url=urlprev)
        line_bot_api.push_message(user_id, image_message)
    cur.close()
    conn.close()
    text_message = TextSendMessage(text="send ข้อมูล เรียบร้อย จำนวน "+str(rowcnt)+" รายการ ได้ภาพผล จำนวน "+str(rowResultCnt)+" ภาพ ")
    line_bot_api.push_message(user_id, text_message)
    filecnt=0
    now = datetime.now()
    timestamp = datetime.timestamp(now)
    timestamp = str(timestamp).replace(".", "_")
    #filename = folder+timestamp+'_lab.jpg'
    #filename_re = folder+timestamp+'_lab_re.jpg'
    # for filename1 in os.listdir(folder):
    #     filecnt=filecnt+1
    # for filename in os.listdir(folder):
    #     if(filename!=""):
    #         img = cv2.imread(filename, cv2.IMREAD_UNCHANGED)
    #         scale_percent = 20 # percent of original size
    #         width = int(img.shape[1] * scale_percent / 100)
    #         height = int(img.shape[0] * scale_percent / 100)
    #         dim = (width, height)
    #         # resize image
    #         filename_re = 'c:\\temp_line\\'+timestamp+'_lab_re.jpg'
    #         resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
    #         cv2.imwrite(filename_re, resized)
    #         print('filename_re '+filename_re)
    #         session = FTP('bangna.co.th','bangna','cy!C51x3')
    #         session.cwd('httpdocs/line_bot')
    #         file = open(filename,'rb')                  # file to send
    #         session.storbinary('STOR '+timestamp1, file)     # send the file
    #         file = open(filename_re,'rb')                  # file to send
    #         session.storbinary('STOR '+timestamp1_re, file)     # send the file
    #         timestamp1 = timestamp+'_lab.jpg'
    #         timestamp1_re = timestamp+'_lab_re.jpg'
    #         url_org = 'https://bangna.co.th/line_bot/'+timestamp1
    #         urlprev = 'https://bangna.co.th/line_bot/'+timestamp1_re
    #         time.sleep(0.05)
    #         image_message = ImageSendMessage(original_content_url=url_org, preview_image_url=urlprev)
    #         #line_bot_api.push_message(get_sourceid(event), image_message)
    #         line_bot_api.push_message(user_id, image_message)
    #     #else:
    #         #line_bot_api.push_message(user_id,TextSendMessage(text=hn+"ไม่ได้ ผล "+mnc_hn_no))
def datetime_tick():
    #ticks = 52707330000
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
        print("creaFolder "+path)
        #os.mkdir("c:\\temp_line\\test\\")
        os.mkdir(path)
        
    except OSError as error: 
        print(error)
def sendLab(hn, reqdate2, userid):
    sql9=''
    sql10=''
    #1,2,3,4,5,6,7
    sql1 = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES, LAB_T05.MNC_LB_RES, LAB_T05.MNC_RES_UNT "
    #8,9
    sql2 = ",convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat, lab_t01.mnc_patname "
    #10,11,12,13,14
    sql21 = ",usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check,LAB_T01.MNC_REQ_NO,convert(VARCHAR(20),m01.mnc_bday,23) as dob "
    #15,16
    sql22 = ", convert(VARCHAR(20),LAB_T02.mnc_result_dat,107) as mnc_result_dat, LAB_T02.mnc_result_tim  "
    #17,18,19,20
    sql23=",patient_m02.MNC_PFIX_DSC_e + ' ' + patient_m26.MNC_DOT_FNAME_e  + ' ' + patient_m26.MNC_DOT_LNAME_e as mnc_doctor_full, m01.mnc_id_no,convert(VARCHAR(20),lab_t05.mnc_req_dat,107) as mnc_req_dat1, DATEDIFF(hour,MNC_BDAY,GETDATE())/8766 AS AgeYearsIntTrunc "
    sql3 = "FROM     PATIENT_m01 m01 "
    sql31 = " inner join patient_t01 t01 on m01.MNC_HN_NO =t01.MNC_HN_NO " 
    sql4 = "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE "
    sql41 = "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT "
    sql5 = "left join LAB_T05 ON LAB_T02.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T02.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT AND LAB_T02.MNC_LB_CD = LAB_T05.MNC_LB_CD  "
    sql6 = "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD "
    sql61 = "left join LAB_M04 ON LAB_T05.MNC_LB_RES_CD = LAB_M04.MNC_LB_RES_CD and LAB_T05.MNC_LB_CD = LAB_M04.MNC_LB_CD "
    sql62 = "left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = LAB_T02.mnc_usr_result " 
    sql63 = "left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = LAB_T02.mnc_usr_result_report "
    sql64 = "left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = LAB_T02.mnc_usr_result_approve " 
    sql65 = "left join patient_m26 on patient_m26.MNC_DOT_CD = LAB_T01.mnc_dot_cd "
    sql66=" inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD "
    sql7 = "where t01.MNC_DATE BETWEEN '" + reqdate2 + "' AND '" + reqdate2 + "' " 
    sql8 = "and t01.mnc_hn_no = '" + hn + "' " 
    #sql9 = "and t01.mnc_vn_no = '" + vn + "'  " +
    #sql10 = "and t01.mnc_Pre_no = '" + preNo + "'  " +
    sql11 = "Order By lab_t05.mnc_req_dat,LAB_M01.MNC_LB_CD, lab_m04.MNC_LB_RES_CD"
    now = datetime.now()
    timestamp = datetime.timestamp(now)
    timestamp = str(timestamp).replace(".", "_")
    filename = 'c:\\temp_line\\'+timestamp+'_lab.jpg'
    filename_re = 'c:\\temp_line\\'+timestamp+'_lab_re.jpg'
    timestamp1 = timestamp+'_lab.jpg'
    timestamp1_re = timestamp+'_lab_re.jpg'
    url_org = 'https://bangna.co.th/line_bot/'+timestamp1
    urlprev = 'https://bangna.co.th/line_bot/'+timestamp1_re
    fnt = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 42)
    fnt36 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 36)
    fnt40 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 40)
    fnt45 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 45)
    fntB1 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 52)
    fntS30 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 30)
    fntS32 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 32)
    fntS28 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 28)
    fntS24 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 24)
    fntS20 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 20)
    
    sql = sql1 + sql2+sql21+sql22+sql23 + sql3+sql31 + sql4+ sql41 + sql5 + sql6 + sql61+sql62+sql63+sql64+sql65+sql66 + sql7 + sql8 + sql9 + sql10 + sql11
    print('sql ', sql)
    conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
    cur = conn.cursor()
    cur.execute(sql)
    myresult = cur.fetchall()
    y = int(60)
    x = int(10)
    row = int(1)
    col1 = 1
    #col2 = 200
    col2 = 260
    #col3 = 500
    col3 = 600
    #col4 = 700
    col4 = 800
    #col5 = 1000
    col5 = 1100
    col6 = 1200
    statusHE001 = ''
    statusMS001 = ''
    image = Image.new(mode = "RGB", size = (1600,800), color = "white")
    draw = ImageDraw.Draw(image)
    for reshe in myresult:
        if reshe[0] == 'HE001':
            statusHE001 = '1'
            labcode = reshe[0]
            labname = reshe[1]
            labval = reshe[2]
            labres = reshe[4]
            interpret = reshe[3]
            normal = reshe[5]
            unit = reshe[6]
            pttname = reshe[8]
            reqdate = reshe[7]
            print('MNC_LB_CD ', labcode)
            print('y ', y)
            if labcode == 'HE001': labname = 'CBC'
            draw.text((x,y+30), labname, font=fntS28, fill=(0,0,0))
            draw.text((x+col2,y+30), labres, font=fnt36, fill=(0,0,0))
            draw.text((x+col3,y+30), labval, font=fnt36, fill=(0,0,0))
            draw.text((x+col4,y+30), interpret, font=fnt36, fill=(0,0,0))
            draw.text((x+col5,y+30), normal, font=fntS24, fill=(0,0,0))  # column ค่ามาตรฐาน
            draw.text((x+col6,y+30), unit, font=fntS24, fill=(0,0,0))      # column unit name
            y = y+30
            row = row+1
    if statusHE001 == '1':
        draw.text((10,20), "โรงพยาบาล บางนา5", font=fnt45, fill=(0,0,0))
        draw.text((300,20), "Result LAB CBC ", font=fnt45, fill=(0,0,0))
        draw.text((520,20), "patient name "+pttname+" hn "+hn+" request date "+reqdate, font=fnt, fill=(0,0,0))
        draw.text((1300,740), "send date "+datetime.today().strftime("%d/%m/%Y") , font=fntS20, fill=(0,0,0))
        image.save(filename)
        time.sleep(1)
        img = cv2.imread(filename, cv2.IMREAD_UNCHANGED)
        scale_percent = 20 # percent of original size
        width = int(img.shape[1] * scale_percent / 100)
        height = int(img.shape[0] * scale_percent / 100)
        dim = (width, height)
        # resize image
        resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
        cv2.imwrite(filename_re, resized)
        print('filename_re '+filename_re)
        session = FTP('bangna.co.th','bangna','cy!C51x3')
        session.cwd('httpdocs/line_bot')
        file = open(filename,'rb')                  # file to send
        session.storbinary('STOR '+timestamp1, file)     # send the file
        file = open(filename_re,'rb')                  # file to send
        session.storbinary('STOR '+timestamp1_re, file)     # send the file
        image_message = ImageSendMessage(original_content_url=url_org, preview_image_url=urlprev)
        #line_bot_api.push_message(get_sourceid(event), image_message)
        line_bot_api.push_message(userid, image_message)
    image = Image.new(mode = "RGB", size = (1600,800), color = "white")
    draw = ImageDraw.Draw(image)
    now = datetime.now()
    timestamp = datetime.timestamp(now)
    timestamp = str(timestamp).replace(".", "_")
    filename = 'c:\\temp_line\\'+timestamp+'_lab.jpg'
    filename_re = 'c:\\temp_line\\'+timestamp+'_lab_re.jpg'
    timestamp1 = timestamp+'_lab.jpg'
    timestamp1_re = timestamp+'_lab_re.jpg'
    url_org = 'https://bangna.co.th/line_bot/'+timestamp1
    urlprev = 'https://bangna.co.th/line_bot/'+timestamp1_re
    y = 60
    for reshe in myresult:
        if reshe[0] == 'MS001':
            statusMS001 = '1'
            labcode = reshe[0]
            labname = reshe[1]
            labval = reshe[2]
            labres = reshe[4]
            interpret = reshe[3]
            normal = reshe[5]
            unit = reshe[6]
            pttname = reshe[8]
            reqdate = reshe[7]
            print('MNC_LB_CD ', labcode)
            print('y ', y)
            if labcode == 'MS001': labname = 'UA'
            draw.text((x,y+30), labname, font=fntS28, fill=(0,0,0))
            draw.text((x+col2,y+30), labres, font=fnt36, fill=(0,0,0))
            draw.text((x+col3,y+30), labval, font=fnt36, fill=(0,0,0))
            draw.text((x+col4,y+30), interpret, font=fnt36, fill=(0,0,0))
            draw.text((x+col5,y+30), normal, font=fntS24, fill=(0,0,0))
            draw.text((x+col6,y+30), unit, font=fntS24, fill=(0,0,0))
            y = y+30
            row = row+1
    if statusMS001 == '1':
        draw.text((10,20), "โรงพยาบาล บางนา5", font=fnt45, fill=(0,0,0))
        draw.text((300,20), "Result LAB UA ", font=fnt45, fill=(0,0,0))
        draw.text((520,20), "patient name "+pttname+" hn "+hn+" request date "+reqdate, font=fnt, fill=(0,0,0))
        draw.text((1300,740), "send date "+datetime.today().strftime("%d/%m/%Y"), font=fntS20, fill=(0,0,0))
        image.save(filename)
        time.sleep(1)
        img = cv2.imread(filename, cv2.IMREAD_UNCHANGED)
        scale_percent = 20 # percent of original size
        width = int(img.shape[1] * scale_percent / 100)
        height = int(img.shape[0] * scale_percent / 100)
        dim = (width, height)
        # resize image
        resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
        cv2.imwrite(filename_re, resized)
        print('filename_re '+filename_re)
        session = FTP('bangna.co.th','bangna','cy!C51x3')
        session.cwd('httpdocs/line_bot')
        file = open(filename,'rb')                  # file to send
        session.storbinary('STOR '+timestamp1, file)     # send the file
        file = open(filename_re,'rb')                  # file to send
        session.storbinary('STOR '+timestamp1_re, file)     # send the file
        image_message = ImageSendMessage(original_content_url=url_org, preview_image_url=urlprev)
        #line_bot_api.push_message(get_sourceid(event), image_message)
        line_bot_api.push_message(userid, image_message)

    #y = 60
    y = 160
    image = Image.new(mode = "RGB", size = (1600,800), color = "white")
    draw = ImageDraw.Draw(image)
    now = datetime.now()
    timestamp = datetime.timestamp(now)
    timestamp = str(timestamp).replace(".", "_")
    filename = 'c:\\temp_line\\'+timestamp+'_lab.jpg'
    filename_re = 'c:\\temp_line\\'+timestamp+'_lab_re.jpg'
    timestamp1 = timestamp+'_lab.jpg'
    timestamp1_re = timestamp+'_lab_re.jpg'
    url_org = 'https://bangna.co.th/line_bot/'+timestamp1
    urlprev = 'https://bangna.co.th/line_bot/'+timestamp1_re
    for res in myresult:
        if res[1] == None:
            continue
        if res[0] == 'HE001':
            continue
        if res[0] == 'MS001':
            continue
        if res[0] == 'SE629':
            continue
        labcode = res[0]
        labname = res[1]
        
        if res[4] != None:
            labval = res[2]
            interpret = res[3]
            labres = res[4]
            normal = res[5]
            unit = res[6]
            pttname = res[8]
            reqdate = res[7]
        else:
            labval=''
            interpret=''
            labres=''
            normal=''
            unit=''
            pttname=''
            reqdate=''
        
        print('MNC_LB_CD ', labcode, 'name', labname)
        print('y ', y)
        if labcode == 'HE001': labname = 'CBC'
        if labcode == 'MS001': labname = 'UA'
        if labcode == 'CH010': labname = 'liver function...'
        if labcode == 'SE005': labname = 'anti hiv screen'
        draw.text((x,y+30), labname, font=fntS28, fill=(0,0,0))
        draw.text((x+col2,y+30), labres, font=fnt36, fill=(0,0,0))
        draw.text((x+col3,y+30), labval, font=fnt36, fill=(0,0,0))
        draw.text((x+col4,y+30), interpret, font=fnt36, fill=(0,0,0))
        draw.text((x+col5,y+30), normal, font=fntS24, fill=(0,0,0))
        draw.text((x+col6,y+30), unit, font=fntS24, fill=(0,0,0))
        y = y+30
        row = row+1
        #x = x + 10
    y = 295
    i=0
    labname1=""
    reporter=""
    resulter=""
    approver=""
    reqno=""
    dob=""
    resdate=""
    restime=""
    reqdate=""
    doctor=""
    pid=""
    age1=""
    reqdate1=""
    for res in myresult:
        if res[1] == None:
            continue
        if res[0] == 'SE629':
            labcode = res[0]
            labname = res[1]
            reqdate = res[7]
            reporter=res[9]
            resulter=res[10]
            approver=res[11]
            reqno = str(res[12])
            dob = res[13]
            resdate = res[14]
            restime = "0"+str(res[15])
            doctor = res[16]
            pid = res[17]
            age1 = res[19]
            reqdate1 = res[18]
            if res[4] != None:
                labval = res[2]
                interpret = res[3]
                labres = res[4]
                normal = res[5]
                unit = res[6]
                pttname = res[8]
                reqdate = res[7]
            else:
                labval=''
                interpret=''
                labres=''
                normal=''
                unit=''
                pttname=''
                reqdate=''
            
            draw.text((col2-50,y), labres, font=fntS28, fill=(0,0,0))
            draw.text((col4-20,y), labval, font=fntS28, fill=(0,0,0))
            #draw.text((x+col4,y), interpret, font=fntS28, fill=(0,0,0))
            #draw.text((col5,y), normal, font=fntS28, fill=(0,0,0))
            draw.text((col6,y), unit, font=fntS24, fill=(0,0,0))
            y = y+30
            row = row+1
            i = i+1
    if(len(restime)>=5):
        restime = restime[len(restime)-4:len(restime)]
    draw.text((40,235), "GROUP : IMMUNO + TUMOR", font=fntS28, fill=(0,0,0))
    draw.text((40,265), labname, font=fntS28, fill=(0,0,0))

    draw.text((20,20), "โรงพยาบาล บางนา5/Bangna5 General Hospital", font=fnt45, fill=(0,0,0))
    draw.text((20,60), "ใบรายงานผล Result LAB ", font=fnt45, fill=(0,0,0))

    draw.text((520,20), "Patient Name "+pttname, font=fnt, fill=(0,0,0))
    draw.text((520,60), "HN "+hn, font=fnt, fill=(0,0,0))
    draw.text((520,100), "เลขที่/Req no "+str(reqno)+"."+reqdate, font=fntS30, fill=(0,0,0))
    draw.text((520,135), "แพทย์/Doctor "+str(doctor), font=fntS30, fill=(0,0,0))

    draw.text((1320,20), "วันเกิด/DOB : "+dob+" [age "+str(age1)+"]", font=fntS24, fill=(0,0,0))
    draw.text((1320,60), "ID/Passport : "+str(pid), font=fntS24, fill=(0,0,0))
    draw.text((1320,100), "วันที่ส่งตรวจ/Request Date : "+reqdate1, font=fntS24, fill=(0,0,0))
    draw.text((1320,135), "วันที่ออกผล/Result Date : "+resdate+" "+restime, font=fntS24, fill=(0,0,0))

    draw.text((25,153), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((25,190), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((300,170), "รายการ/DESCRIPTION", font=fntS32, fill=(0,0,0))
    draw.text((800,170), "ผลตรวจ/RESULT", font=fntS32, fill=(0,0,0))

    draw.text((550,305), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((550,335), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((550,365), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((550,395), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((550,425), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))

    draw.text((25,680), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((25,720), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
    draw.text((45,700), "ผู้บันทึก/recorder : "+reporter, font=fntS28, fill=(0,0,0))
    draw.text((600,700), "ผู้รายงาน/reporter : "+resulter, font=fntS28, fill=(0,0,0))
    draw.text((1200,700), "ผู้ตรวจสอบ/approver : "+approver, font=fntS28, fill=(0,0,0))

    draw.text((1400,740), "send date "+datetime.today().strftime("%d/%m/%Y"), font=fntS20, fill=(0,0,0))
    image.save(filename)
    time.sleep(1)
    img = cv2.imread(filename, cv2.IMREAD_UNCHANGED)
    scale_percent = 20 # percent of original size
    width = int(img.shape[1] * scale_percent / 100)
    height = int(img.shape[0] * scale_percent / 100)
    dim = (width, height)
    # resize image
    resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
    cv2.imwrite(filename_re, resized)
    print('filename_re '+filename_re)
    session = FTP('bangna.co.th','bangna','cy!C51x3')
    session.cwd('httpdocs/line_bot')
    file = open(filename,'rb')                  # file to send
    session.storbinary('STOR '+timestamp1, file)     # send the file
    file = open(filename_re,'rb')                  # file to send
    session.storbinary('STOR '+timestamp1_re, file)     # send the file

    file.close()                                    # close file and FTP
    time.sleep(1)
    #line_bot_api.reply_message(event.reply_token,TextSendMessage('ผล Out Lab HN '+hn+' '+pttname))
    print('send line_bot_api ')
    image_message = ImageSendMessage(original_content_url=url_org, preview_image_url=urlprev)
    #line_bot_api.push_message(get_sourceid(event), image_message)
    line_bot_api.push_message(userid, image_message)

    cur.close()
    conn.close()
    print('send line_bot_api completed ')
#json_line=""
@app.route("/callback", methods=['POST'])
def callback():
    # get X-Line-Signature header value
    signature = request.headers['X-Line-Signature']

    # get request body as text
    body = request.get_data(as_text=True)
    app.logger.info("Request body: " + body)
    json_line=""
    # handle webhook body
    try:
        #json_line = request.get_json()
        id1=''
        text = ''
        json_line = json.dumps(request.get_json())
        decoded = json.loads(json_line)
        eventtype = decoded["events"][0]['type']
        messagetype = decoded["events"][0]['message']['type']
        if(messagetype == "test"):
            text = decoded["events"][0]['message']['text']
        userid = decoded["events"][0]['source']['userId']
        sourcetype = decoded["events"][0]['source']['type']
        #sourceuser = decoded["events"][0]['source']['userId']
        print('text '+text)
        if(eventtype == "join"):
            conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
            cur = conn.cursor()
            params = (text, userid,json_line,eventtype,sourcetype,userid,messagetype, id1)
            cur.execute("{call insert_t_line_bot_log1(?, ?, ?, ?, ?, ?, ?, ?)}", params)
            cur.commit()
        elif (eventtype == "message" and (sourcetype == "user")):
            conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
            cur = conn.cursor()
            params = (text, userid,json_line,eventtype,sourcetype,userid,messagetype, id1)
            cur.execute("{call insert_t_line_bot_log1(?, ?, ?, ?, ?, ?, ?, ?)}", params)
            cur.commit()
        
        handler.handle(body, signature)
    except InvalidSignatureError:
        print("Invalid signature. Please check your channel access token/channel secret.")
        abort(400)

    return 'OK'

@handler.add(MessageEvent, message=TextMessage)
def handle_message(event):
    #print('event.message '+event.message) 
    text = event.message.text
    txt1 = ''
    txt2 = ''
    sql = ""
    #id1=''
    #conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
    #cur = conn.cursor()
    #params = (text, event.source.user_id,json_line, id1)
    #cur.execute("{call insert_t_line_bot_log(?, ?, ?, ?)}", params)
    #cur.commit()

    if(len(text)>2):
        txt1 = text[0:2]
    if(len(text)>11):
        txt1 = text[0:11]
    if text.lower() == 'profile':
        #line_bot_api.reply_message(event.reply_token, TextSendMessage('Ekapop Ploentham'))
        if isinstance(event.source, SourceUser):
            profile = line_bot_api.get_profile(event.source.user_id)
            line_bot_api.reply_message(event.reply_token, TextSendMessage(text='Display name: ' + profile.display_name +' '+ event.source.user_id))
    else:
        if txt1.lower() == 'profile add':
            txt2 = text[11:]
            txt2 = txt2.strip()
            print('id '+txt2)
            id=''
            id1=''
            re=''
            #conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
            conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
            cur = conn.cursor()
            sql = "select stf.mnc_usr_name, stf.mnc_usr_pw, stf.mnc_usr_full From userlog_m01 Where mnc_usr_name = '" + txt2 + "'"
            #curSel = conn.cursor()
            #myresultSel = curSel.fetchall()
            #for res in myresultSel:
            params = (event.source.user_id, txt2, id1)
            cur.execute("{call insert_b_line_bot(?, ?, ?)}", params)
            #recs = re.fetchall()
            cur.commit()
            
            sql = "Select staff_fullname From b_line_bot Where staff_id = '"+txt2 + "'"
            cur1 = conn.cursor()
            cur1.execute(sql)
            myresult1 = cur1.fetchall()
            for res1 in myresult1:
                re = res1[0] + " "
                #print('re '+re)

            cur1.close()
            #conn1.close()
            cur.close()
            conn.close()
            #print('ans '+ans)
            line_bot_api.reply_message(event.reply_token,TextSendMessage('สวัสดี '+re+' add Line success'))
        else:
            txt3 = text.split()
            print('txt3 '+txt3[0])
            #if text[0:2] == 'ขอ':
            if txt3[0].strip() == 'ขอ':
                #txt2 = text[2:9]
                txt2 = txt3[1]
                print('txt2 '+txt2)
                #line_bot_api.reply_message(event.reply_token,TextSendMessage('test'))
                re = ''
                if txt2.strip().lower() =='outlab':
                    hn=''
                    #txt3 = text[9:13]
                    if len(text)>16:
                        hn = text[9:17].strip()
                    print('hn outlab '+hn)
                    sendOutLab(hn, event.source.user_id)
                    #line_bot_api.reply_message(event.reply_token,TextSendMessage('HN '+hn+' '+ftpfilename))
                elif txt2.strip().lower() == 'lab':
                    hn=''
                    reqdate=''
                    if len(txt3)>1:
                        hn = txt3[2].strip()
                    print('hn lab ', hn)
                    if len(txt3)>2:
                        reqdate = txt3[3]
                    print('reqdate ', reqdate)
                    reqdate1 = reqdate.split('/')
                    year = int(reqdate1[2])
                    if year >2500:
                        year = year - 543
                    reqdate2 = str(year)+'-'+reqdate1[1]+'-'+reqdate1[0]
                    print('reqdate2 ', reqdate2)
                    sendLab(hn,reqdate2, event.source.user_id)
                elif txt2.strip().lower() == 'xray':
                    txt=""
                else:
                    hn=''
                    reqdate=''
                    if len(text)>11:
                        hn = text[5:12]
                    print('hn ', hn)
                    if len(text)>24:
                        reqdate = text[13:19]
                    print('reqdate ', reqdate)

                    line_bot_api.reply_message(event.reply_token,TextSendMessage('aaaaa'))
            elif txt3[0].strip() == 'ส่ง':
                txt2 = txt3[1]
                if txt2.strip().lower() == 'lab':
                    aa=''
                    hn=''
                    reqdate=''
                    dtrcode=''
                    dtruserid=''
                    if len(txt3)>1:
                        hn = txt3[2].strip()
                    print('hn lab ', hn)
                    if len(txt3)>2:
                        reqdate = txt3[3]
                    print('reqdate ', reqdate)
                    reqdate1 = reqdate.split('/')
                    year = int(reqdate1[2])
                    if year >2500:
                        year = year - 543
                    reqdate2 = str(year)+'-'+reqdate1[1]+'-'+reqdate1[0]
                    print('reqdate2 ', reqdate2)
                    if len(txt3)>3:
                        dtrcode = txt3[4]
                    print('dtrcode ', dtrcode)
                    sql = "Select user_id From b_line_bot Where staff_id = '"+dtrcode+"'"
                    conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
                    cur = conn.cursor()
                    cur.execute(sql)
                    myresult3 = cur.fetchall()
                    for res3 in myresult3:
                        dtruserid=res3[0]
                    if len(dtruserid) > 0:
                        print('user id ',dtruserid)
                        sendLab(hn,reqdate2, event.source.user_id)
                        sendLab(hn,reqdate2, dtruserid)
                    else:
                        print('user id not found ')
                else:
                    aa=''
            elif txt3[0].strip().lower() == 'atk':
                txt312=""
                len1=0
                for ttt in text.split():
                    len1=len1+1
                if((len1>1) and (len(txt3[0].strip())>0) and len(txt3[0].strip())<10 ):
                    txt311 = txt3[1].strip()
                    #txt31 = txt3[1].strip()
                elif((len1>1) and (len(txt3[0].strip())==10)):
                    txt311 = txt3[1].strip()

                if (len(txt311)==13):
                    sendLabCovidPDFpid(txt3[1].strip(),event.source.user_id)
                elif(txt311[0:1]=="5" and len(txt311) < 13) :
                    sendLabCovidPDFhn(txt3[1].strip(),event.source.user_id)
            elif txt3[0].strip().lower() == 'covid':
                txt311=""
                len1=0
                for ttt in text.split():
                    len1=len1+1
                if((len1>1) and (len(txt3[0].strip())>0) and len(txt3[0].strip())<10 ):
                    txt311 = txt3[1].strip()
                    #txt31 = txt3[1].strip()
                elif((len1>1) and (len(txt3[0].strip())==10)):
                    txt311 = txt3[1].strip()

                if (len(txt311)==13):
                    sendLabCovidPDFpid(txt3[1].strip(),event.source.user_id)
                elif(txt311[0:1]=="5" and len(txt311) < 13) :
                    sendLabCovidPDFhn(txt3[1].strip(),event.source.user_id)
            elif txt3[0].strip().lower() == 'labcovid':
                len1=0
                for ttt in text.split():
                    len1=len1+1
                print('len txt3 '+str(len1))
                print('txt3[0] '+str(len(txt3[0].strip())))
                txt31=""
                if((len1>1) and (len(txt3[0].strip())>0) and len(txt3[0].strip())<10 ):
                    txt31 = txt3[1].strip()
                    #txt31 = txt3[1].strip()
                elif((len1>1) and (len(txt3[0].strip())==10)):
                    txt31 = txt3[1].strip()
                print('labcovid txt31[0:1] '+txt31[0:1])
                print('labcovid hn '+str(txt31))
                hn = str(txt31)
                sqlwhere=""
                sqlother=""
                sqlother1=""
                sql=""
                statusSelect=""

                if(txt31[0:1]=="5" and len(txt31) < 13) :
                    print('hn bangna5')
                    statusSelect="labcovid"
                    sqlwhere="Where pttt01.mnc_hn_no = '"+hn+"'  and pttt01.mnc_date = (Select max(mnc_date) from patient_t01 where mnc_hn_no = '"+hn+"')   "
                    sqlother1=" "
                elif (len(txt31)==13):
                    print('len(txt31)==13 '+str(txt31))
                    statusSelect="labcovid_pid"
                    sendLabPID(txt31,event.source.user_id)
                    return
                elif (len(txt31)==10):
                    print('len(txt31)==10 '+str(txt31))
                    statusSelect="labcovid_doc"
                    sendLabDoc(txt31,event.source.user_id)
                    return
                elif (txt31[0:1]=="1"):
                    print('hn bangna1')
                    statusSelect="labcovid"
                    sqlother="inner join bn5_scan.dbo.b_patient_smartcard on pttt01.mnc_hn_no = b_patient_smartcard.mnc_hn_no "
                    sqlwhere=sqlwhere+"Where b_patient_smartcard.hn = '"+hn+"' "
                    sqlwhere=sqlwhere+"and pttt01.mnc_date = (Select max(patient_t01.mnc_date) "
                    sqlwhere=sqlwhere+"from patient_t01 "
                    sqlwhere=sqlwhere+"inner join bn5_scan.dbo.b_patient_smartcard on patient_t01.mnc_hn_no = b_patient_smartcard.mnc_hn_no "
                    sqlwhere=sqlwhere+"inner join lab_t01 labt01 on labt01.MNC_HN_NO = patient_t01.mnc_hn_no and labt01.MNC_HN_yr = patient_t01.mnc_hn_yr and labt01.MNC_PRE_NO = patient_t01.MNC_PRE_NO and labt01.MNC_DATE = patient_t01.MNC_DATE "
                    sqlwhere=sqlwhere+"inner join lab_t02 labt02 on labt01.MNC_REQ_YR = labt02.MNC_REQ_YR and labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT and labt01.MNC_REQ_NO = labt02.MNC_REQ_NO "
                    sqlwhere=sqlwhere+"where b_patient_smartcard.hn = '"+hn+"')   "
                    sqlother1=" and b_patient_smartcard.date_order = (select max(b_patient_smartcard.date_order) from bn5_scan.dbo.b_patient_smartcard where b_patient_smartcard.hn = '"+hn+"'  )"
                elif (txt31[0:1]=="2"):
                    print('hn bangna2')
                    statusSelect="labcovid"
                    sqlother="inner join bn5_scan.dbo.b_patient_smartcard on pttt01.mnc_hn_no = b_patient_smartcard.mnc_hn_no "
                    #sqlwhere="Where b_patient_smartcard.hn = '"+hn+"'  and pttt01.mnc_date = (Select max(patient_t01.mnc_date) from patient_t01 inner join bn5_scan.dbo.b_patient_smartcard on patient_t01.mnc_hn_no = b_patient_smartcard.mnc_hn_no where b_patient_smartcard.hn = '"+hn+"')   "
                    sqlwhere=sqlwhere+"Where b_patient_smartcard.hn = '"+hn+"' "
                    sqlwhere=sqlwhere+"and pttt01.mnc_date = (Select max(patient_t01.mnc_date) "
                    sqlwhere=sqlwhere+"from patient_t01 "
                    sqlwhere=sqlwhere+"inner join bn5_scan.dbo.b_patient_smartcard on patient_t01.mnc_hn_no = b_patient_smartcard.mnc_hn_no "
                    sqlwhere=sqlwhere+"inner join lab_t01 labt01 on labt01.MNC_HN_NO = patient_t01.mnc_hn_no and labt01.MNC_HN_yr = patient_t01.mnc_hn_yr and labt01.MNC_PRE_NO = patient_t01.MNC_PRE_NO and labt01.MNC_DATE = patient_t01.MNC_DATE "
                    sqlwhere=sqlwhere+"inner join lab_t02 labt02 on labt01.MNC_REQ_YR = labt02.MNC_REQ_YR and labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT and labt01.MNC_REQ_NO = labt02.MNC_REQ_NO "
                    sqlwhere=sqlwhere+"where b_patient_smartcard.hn = '"+hn+"')   "
                    sqlother1=" and b_patient_smartcard.date_order = (select max(b_patient_smartcard.date_order) from bn5_scan.dbo.b_patient_smartcard where b_patient_smartcard.hn = '"+hn+"'  )"
                elif (len(txt31)==0):
                    print('len(txt31)==0')
                    statusSelect="labcovidcount"
                
                # elif (len(txt31)==10):
                #     print('len(txt31)==10')
                #     statusSelect="labcovid"
                #     sqlother="inner join bn5_scan.dbo.b_patient_smartcard on pttt01.mnc_hn_no = b_patient_smartcard.mnc_hn_no "
                #     sqlwhere="Where b_patient_smartcard.doc = '"+txt31+"'     "
                col2 = 260
                col3 = 600
                col4 = 800
                col5 = 1100
                col6 = 1200
                y = 295
                fnt = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 42)
                fnt36 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 36)
                fnt40 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 40)
                fnt45 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 45)
                fntB1 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 52)
                fntS30 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 30)
                fntS32 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 32)
                fntS28 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 28)
                fntS24 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 24)
                fntS20 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 20)
                if(statusSelect=="labcovid"):
                    sql=""
                    sql="SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC, labt05.MNC_RES_VALUE, labt05.MNC_STS, labt05.MNC_RES, labt05.MNC_LB_RES, labt05.MNC_RES_UNT  "
                    sql+=",convert(VARCHAR(20),labt05.mnc_req_dat,23) as mnc_req_dat,isnull(pm02.MNC_PFIX_DSC_e,'')+' '+pttm01.MNC_FNAME_T+' '+ pttm01.MNC_LNAME_T as mnc_patname,  "
                    sql+=",usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check,labt01.MNC_REQ_NO,convert(VARCHAR(20),pttm01.mnc_bday,23) as dob "
                    sql+=", convert(VARCHAR(20),labt02.mnc_result_dat,107) as mnc_result_dat, labt02.mnc_result_tim  "
                    sql+=",patient_m02.MNC_PFIX_DSC_e + ' ' + patient_m26.MNC_DOT_FNAME_e  + ' ' + patient_m26.MNC_DOT_LNAME_e as mnc_doctor_full, pttm01.mnc_id_no,convert(VARCHAR(20),labt05.mnc_req_dat,107) as mnc_req_dat1, DATEDIFF(hour,MNC_BDAY,GETDATE())/8766 AS AgeYearsIntTrunc "
                    sql+=",labt02.mnc_usr_result,labt02.mnc_usr_result_report,labt02.mnc_usr_result_approve,pttm01.MNC_HN_NO "
                    sql+=""
                    sql+="from patient_t01 pttt01 "
                    sql+="inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO =pttt01.MNC_HN_NO and pttm01.mnc_hn_yr = pttm01.mnc_hn_yr "
                    sql+="left join patient_m02 pm02 on pttm01.MNC_DOT_PFIX =pm02.MNC_PFIX_CD "
                    sql+="left join LAB_T01 labt01 ON pttt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pttt01.MNC_DATE = labt01.MNC_DATE and pttt01.MNC_hn_NO = labt01.MNC_hn_NO and pttt01.MNC_hn_yr = labt01.MNC_hn_yr "
                    sql+="left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT "
                    #sql+="left join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
                    sql+="inner join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
                    sql+="left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD "
                    sql+="left join LAB_M04 labm04 ON labt05.MNC_LB_RES_CD = labm04.MNC_LB_RES_CD and labt05.MNC_LB_CD = labm04.MNC_LB_CD "
                    sql+="left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " 
                    sql+="left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report "
                    sql+="left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " 
                    sql+="left join patient_m26 on patient_m26.MNC_DOT_CD = labt01.mnc_dot_cd "
                    sql+="inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD "
                    sql+=sqlother
                    sql+=sqlwhere
                    sql+="  and labt02.MNC_LB_CD in ('SE184','SE629') "
                    #sql+="and b_patient_smartcard.date_order = (select max(b_patient_smartcard.date_order) from bn5_scan.dbo.b_patient_smartcard where b_patient_smartcard.hn = '"+hn+"'  )"
                    sql+=sqlother1
                    sql+="Order By labt05.mnc_req_dat,labm01.MNC_LB_CD, labm04.MNC_LB_RES_CD "
                elif(statusSelect=="labcovidcount"):
                    print("labcovidcount")
                    sql="Select count(doc) as cnt, doc from bn5_scan.dbo.b_patient_smartcard where date_order =convert(VARCHAR(20), DATEADD(day,-1, GETDATE()) ,23) group by doc "
                    conn1 = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
                    cur1 = conn1.cursor()
                    cur1.execute(sql)
                    resp1 = cur1.fetchall()
                    for row1 in resp1:
                        print("labcovidcount "+str(row1[1]))
                        line_bot_api.push_message(event.source.user_id,TextSendMessage(text="เลขที่เอกสาร "+str(row1[1])+" จำนวน "+str(row1[0])))
                    return
                print(sql)
                #hn=""
                preno=""
                vsdate=""
                hnyear=""
                now = datetime.now()
                timestamp = datetime.timestamp(now)
                timestamp = str(timestamp).replace(".", "_")
                folder = "c:\\temp_line\\"
                tick = str(datetime_tick())
                print("tick "+str(tick))
                createFolder(folder,tick)
                deleteFileinFolder(folder)
                # for filename in os.listdir(folder):
                #     file_path = os.path.join(folder, filename)
                #     try:
                #         if os.path.isfile(file_path) or os.path.islink(file_path):
                #             os.unlink(file_path)
                #         elif os.path.isdir(file_path):
                #             shutil.rmtree(file_path)
                #     except Exception as e:
                #         print('Failed to delete %s. Reason: %s' % (file_path, e))
                rowcnt=0
                rowResultCnt=0
                rowcount=1
                reqdate1=""
                restime=""
                labname=""
                pttname=""
                reqno=""
                doctor=""
                dob=""
                pid=""
                resdate=""
                reporter=""
                resulter=""
                approver=""
                reqdate=""
                age1=""
                user1=""
                user2=""
                user3=""
                filename=""
                mnc_hn_no=""
                image = Image.new(mode = "RGB", size = (1600,800), color = "white")
                draw = ImageDraw.Draw(image)
                conn1 = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
                cur1 = conn1.cursor()
                cur1.execute(sql)
                resp1 = cur1.fetchall()
                #print("cur1.rowcount "+str(cur1.rowcount))
                for row1 in resp1:
                    if row1[1] == None:
                        print("hn no result"+str(hn))
                        line_bot_api.push_message(event.source.user_id,TextSendMessage(text="HN "+str(hn)+"นี้ ยังไม่มี ผล "))
                        continue
                    rowcount = rowcount+1
                    filename = folder+timestamp+"_"+hn+'_lab.jpg'
                    labcode = row1[0]
                    labname = row1[1]
                    reqdate = row1[7]
                    reporter=row1[9]
                    resulter=row1[10]
                    approver=row1[11]
                    reqno = str(row1[12])
                    dob = row1[13]
                    resdate = row1[14]
                    restime = "0"+str(row1[15])
                    doctor = row1[16]
                    pid = row1[17]
                    age1 = row1[19]
                    user1 = row1[20]
                    user2 = row1[21]
                    user3 = row1[22]
                    reqdate1 = row1[18]
                    mnc_hn_no= row1[23]
                    if row1[4] != None:
                        labval = row1[2]
                        interpret = row1[3]
                        labres = row1[4]
                        normal = row1[5]
                        unit = row1[6]
                        pttname = row1[8]
                        reqdate = row1[7]
                    else:
                        labval=''
                        interpret=''
                        labres=''
                        normal=''
                        unit=''
                        pttname=''
                        reqdate=''
                    draw.text((col2-50,y), labres, font=fntS28, fill=(0,0,0))
                    draw.text((col4-20,y), labval, font=fntS28, fill=(0,0,0))
                    draw.text((col6,y), unit, font=fntS24, fill=(0,0,0))
                    y = y+30
                    #row = row+1
                if(len(restime)>=5):
                        restime = restime[len(restime)-4:len(restime)]
                draw.text((40,235), "GROUP : IMMUNO + TUMOR", font=fntS28, fill=(0,0,0))
                draw.text((40,265), labname, font=fntS28, fill=(0,0,0))

                draw.text((20,20), "โรงพยาบาล บางนา5", font=fnt45, fill=(0,0,0))
                draw.text((20,60), "Bangna5 General Hospital ", font=fnt45, fill=(0,0,0))
                draw.text((20,100), "ใบรายงานผล Result LAB", font=fnt45, fill=(0,0,0))

                draw.text((520,20), "Patient Name "+pttname, font=fnt, fill=(0,0,0))
                draw.text((520,60), "HN "+hn, font=fnt, fill=(0,0,0))
                draw.text((520,100), "เลขที่/Req no "+str(reqno)+"."+reqdate, font=fntS30, fill=(0,0,0))
                draw.text((520,135), "แพทย์/Doctor "+str(doctor), font=fntS30, fill=(0,0,0))

                draw.text((1250,20), "วันเกิด/DOB : "+dob+" [age "+str(age1)+"]", font=fntS24, fill=(0,0,0))
                draw.text((1250,60), "ID/Passport : "+str(pid), font=fntS24, fill=(0,0,0))
                draw.text((1250,100), "วันที่ส่งตรวจ/Request Date : "+reqdate1, font=fntS24, fill=(0,0,0))
                draw.text((1250,135), "วันที่ออกผล/Result Date : "+resdate+" "+restime, font=fntS24, fill=(0,0,0))

                draw.text((25,153), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                draw.text((25,190), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                draw.text((300,170), "รายการ/DESCRIPTION", font=fntS32, fill=(0,0,0))
                draw.text((800,170), "ผลตรวจ/RESULT", font=fntS32, fill=(0,0,0))

                draw.text((550,305), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                draw.text((550,335), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                draw.text((550,365), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                draw.text((550,395), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                draw.text((550,425), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))

                draw.text((25,680), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                draw.text((25,720), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                draw.text((45,700), "ผู้บันทึก/recorder : "+reporter+"["+user1+"]", font=fntS28, fill=(0,0,0))
                draw.text((600,700), "ผู้รายงาน/reporter : "+resulter+"["+user2+"]", font=fntS28, fill=(0,0,0))
                draw.text((1150,700), "ผู้ตรวจสอบ/approver : "+approver+"["+user3+"]", font=fntS28, fill=(0,0,0))
                draw.text((35,740), "FM-LAB-096 (แก้ไขครั้งที่ 00-17/07/55) ", font=fntS20, fill=(0,0,0))

                draw.text((1400,740), "send date "+datetime.today().strftime("%d/%m/%Y"), font=fntS20, fill=(0,0,0))
                if(filename!=""):
                    image.save(filename)
                    rowResultCnt=rowResultCnt+1
                    time.sleep(0.05)
                rowcnt = rowcnt+1
                print('send line_bot_api ')
                filename_re = 'c:\\temp_line\\'+timestamp+'_lab_re.jpg'
                timestamp1 = timestamp+'_lab.jpg'
                timestamp1_re = timestamp+'_lab_re.jpg'
                url_org = 'https://bangna.co.th/line_bot/'+timestamp1
                urlprev = 'https://bangna.co.th/line_bot/'+timestamp1_re


                time.sleep(0.2)
                print("filename "+filename)
                if(filename!=""):
                    img = cv2.imread(filename, cv2.IMREAD_UNCHANGED)
                    scale_percent = 20 # percent of original size
                    width = int(img.shape[1] * scale_percent / 100)
                    height = int(img.shape[0] * scale_percent / 100)
                    dim = (width, height)
                    # resize image
                    resized = cv2.resize(img, dim, interpolation = cv2.INTER_AREA)
                    cv2.imwrite(filename_re, resized)
                    print('filename_re '+filename_re)
                    session = FTP('bangna.co.th','bangna','cy!C51x3')
                    session.cwd('httpdocs/line_bot')
                    file = open(filename,'rb')                  # file to send
                    session.storbinary('STOR '+timestamp1, file)     # send the file
                    file = open(filename_re,'rb')                  # file to send
                    session.storbinary('STOR '+timestamp1_re, file)     # send the file

                    image_message = ImageSendMessage(original_content_url=url_org, preview_image_url=urlprev)
                    #line_bot_api.push_message(get_sourceid(event), image_message)
                    line_bot_api.push_message(event.source.user_id, image_message)
                else:
                    line_bot_api.push_message(event.source.user_id,TextSendMessage(text=hn+"ไม่ได้ ผล "+mnc_hn_no))

                cur1.close()
                conn1.close()

            elif txt3[0].strip().lower() == 'send':
                print('send')
                #text_message = TextSendMessage(text="ส่ง email เรียบร้อย")
                #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="ส่ง email เรียบร้อย"))
                txt31 = txt3[1]
                txt32 = ""
                txt33 = ""
                doc = ""
                emailsend1=""
                emailsend2=""
                reqdate2=""
                statusdocORdate=""
                sql=""
                print('txt31 '+txt31)
                if txt31.strip().lower() == 'email':
                    txt32 = txt3[2]
                    emailsend1=""
                    emailsend2=""
                    print('txt32 '+txt32)
                    if txt32.strip().lower() == 'lab':
                        txt33 = txt3[3].strip()
                        print('txt33 '+txt33)
                        
                        #doc = txt3[4]
                        if(len(txt33)==10):
                            statusdocORdate="doc"
                            doc = txt33
                            #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="เลขที่ ไม่ถูกต้อง"))
                        else:
                            reqdate2=""
                            try:
                                reqdate2 = datetime.strptime(txt33.strip(), '%Y-%m-%d').date()
                                statusdocORdate="date"
                            except ValueError as e:
                                reqdate2="error date time"
                    elif txt32.strip().lower() == 'sm1':
                        txt33 = txt3[3].strip()
                        print('txt33 '+txt33)
                        if(len(txt33)==10):
                            statusdocORdate="doc"
                            doc = txt33

                if(txt32.strip().lower() == 'lab') and ((statusdocORdate=="doc") or (statusdocORdate=="date")):
                    sqlwhere=""
                    #receiver_address = 'bangna5lab@hotmail.com'
                    receiver_address = 'bangna5lab@hotmail.com'
                    # receiver_address1 = 'bangna5lab@hotmail.com'
                    # receiver_address2 = 'bangna5lab@hotmail.com'
                    if(statusdocORdate=="doc") :
                        #limit monthly
                        #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="okครับ ให้ส่ง email ตามเลขที่ "+doc+" ส่งไป "+receiver_address))#limit monthly
                        sqlwhere=""
                        sqlwhere+="Where b_patient_smartcard.doc = '"+doc+"' and labt02.MNC_LB_CD in ('SE184','SE629') and pttt01.MNC_DATE >= DATEADD(day,-3, GETDATE()) "
                    else:
                        #limit monthly
                        #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="okครับ ให้ส่ง email ตามวันที่ "+reqdate2+" ส่งไป "+receiver_address))#limit monthly
                        sqlwhere="Where b_patient_smartcard.date_order = '"+reqdate2+"'  and labt02.MNC_LB_CD in ('SE184','SE629') and pttt01.MNC_DATE >= DATEADD(day,-3, GETDATE()) "
                    col2 = 260
                    col3 = 600
                    col4 = 800
                    col5 = 1100
                    col6 = 1200
                    fnt = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 42)
                    fnt36 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 36)
                    fnt40 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 40)
                    fnt45 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 45)
                    fntB1 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 52)
                    fntS30 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 30)
                    fntS32 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 32)
                    fntS28 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 28)
                    fntS24 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 24)
                    fntS20 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 20)

                    sql="Select pttt01.mnc_hn_no, pttt01.mnc_hn_yr, convert(VARCHAR(20),pttt01.mnc_date,23) as mnc_date, pttt01.mnc_pre_no  "
                    sql+="from patient_t01 pttt01 "
                    sql+="inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO = pttt01.MNC_HN_NO and pttm01.mnc_hn_yr = pttt01.mnc_hn_yr "
                    #ที่สั่ง lab covid
                    sql+="left join LAB_T01 labt01 ON pttt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pttt01.MNC_DATE = labt01.MNC_DATE and labt01.mnc_hn_no = pttt01.mnc_hn_no "
                    sql+="left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT and labt01.mnc_req_yr = labt02.mnc_req_yr "
                    #sql+="left join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
                    #sql+="left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD "
                    #sql+="left join LAB_M04 labm04 ON labt05.MNC_LB_RES_CD = labm04.MNC_LB_RES_CD and labt05.MNC_LB_CD = labm04.MNC_LB_CD "
                    #sql+="left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " 
                    #sql+="left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report "
                    #sql+="left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " 
                    #sql+="left join patient_m26 on patient_m26.MNC_DOT_CD = labt01.mnc_dot_cd "
                    #sql+="inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD "
                    #sql+="inner join bn5_scan.dbo.b_patient_smartcard on pttt01.patient_smartcard_id = b_patient_smartcard.patient_smartcard_id "
                    sql+="inner join bn5_scan.dbo.b_patient_smartcard on pttm01.patient_smartcard_id = b_patient_smartcard.patient_smartcard_id "
                    sql+=sqlwhere
                    #sql+="Order By labt05.mnc_req_dat,labm01.MNC_LB_CD, labm04.MNC_LB_RES_CD "
                    sql+="Order By b_patient_smartcard.patient_smartcard_id "
                    #print('sql '+sql)
                    conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
                    cur = conn.cursor()
                    cur.execute(sql)
                    resp = cur.fetchall()
                    hn=""
                    preno=""
                    vsdate=""
                    hnyear=""
                    now = datetime.now()
                    timestamp = datetime.timestamp(now)
                    timestamp = str(timestamp).replace(".", "_")
                    
                    folder = "c:\\temp_line\\"
                    folderemailb1="c:\\temp_line_email_lab_b1\\"
                    folderemailb2="c:\\temp_line_email_lab_b2\\"
                    if txt31.strip().lower() == 'email':
                        if(txt32.strip().lower() == 'lab') and (statusdocORdate=="doc"):
                            if (doc[0:1]=="1"):
                                folder = folderemailb1
                            elif (doc[0:1]=="2"):
                                folder = folderemailb2

                    deleteFileinFolder(folder)
                    # for filename in os.listdir(folder):
                    #     file_path = os.path.join(folder, filename)
                    #     try:
                    #         if os.path.isfile(file_path) or os.path.islink(file_path):
                    #             os.unlink(file_path)
                    #         elif os.path.isdir(file_path):
                    #             shutil.rmtree(file_path)
                    #     except Exception as e:
                    #         print('Failed to delete %s. Reason: %s' % (file_path, e))
                    rowcnt=0
                    for row in resp:
                        rowcnt = rowcnt+1
                    if (rowcnt>50) and (rowcnt<=100) :
                        print("50")
                        #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="จำนวนข้อมูล "+str(rowcnt)+" นานหน่อยครับ "))#limit monthly
                    elif (rowcnt>100) and (rowcnt<=200):
                        print("100")
                        #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="จำนวนข้อมูล "+str(rowcnt)+" นานแน่ๆ "))#limit monthly
                    elif (rowcnt<50):
                        print("<50")
                        #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="จำนวนข้อมูล "+str(rowcnt)+" รอซักครู่ "))#limit monthly
                    rowcnt=0
                    rowResultCnt=0
                    for row in resp:
                        y = 295
                        hn=str(row[0])
                        preno=str(row[3])
                        vsdate=str(row[2])
                        hnyear=str(row[1])
                        print("hn "+str(hn))
                        sql=""
                        sql="SELECT labt02.MNC_LB_CD, labm01.MNC_LB_DSC, labt05.MNC_RES_VALUE, labt05.MNC_STS, labt05.MNC_RES, labt05.MNC_LB_RES, labt05.MNC_RES_UNT  "
                        sql+=",convert(VARCHAR(20),labt05.mnc_req_dat,23) as mnc_req_dat, isnull(labt01.mnc_patname,'') as mnc_patname "
                        sql+=",usr_result.MNC_USR_FULL as user_lab,usr_report.MNC_USR_FULL as user_report,usr_approve.MNC_USR_FULL as user_check,labt01.MNC_REQ_NO,convert(VARCHAR(20),pttm01.mnc_bday,23) as dob "
                        sql+=", convert(VARCHAR(20),labt02.mnc_result_dat,107) as mnc_result_dat, labt02.mnc_result_tim  "
                        sql+=",patient_m02.MNC_PFIX_DSC_e + ' ' + patient_m26.MNC_DOT_FNAME_e  + ' ' + patient_m26.MNC_DOT_LNAME_e as mnc_doctor_full, pttm01.mnc_id_no,convert(VARCHAR(20),labt05.mnc_req_dat,107) as mnc_req_dat1, DATEDIFF(hour,MNC_BDAY,GETDATE())/8766 AS AgeYearsIntTrunc "
                        sql+=",labt02.mnc_usr_result,labt02.mnc_usr_result_report,labt02.mnc_usr_result_approve "
                        sql+=""
                        sql+="from patient_t01 pttt01 "
                        sql+="inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO =pttt01.MNC_HN_NO and pttm01.mnc_hn_yr = pttm01.mnc_hn_yr "
                        sql+="left join LAB_T01 labt01 ON pttt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pttt01.MNC_DATE = labt01.MNC_DATE and pttt01.MNC_hn_NO = labt01.MNC_hn_NO and pttt01.MNC_hn_yr = labt01.MNC_hn_yr "
                        sql+="left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT "
                        #sql+="left join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
                        sql+="inner join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
                        sql+="left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD "
                        sql+="left join LAB_M04 labm04 ON labt05.MNC_LB_RES_CD = labm04.MNC_LB_RES_CD and labt05.MNC_LB_CD = labm04.MNC_LB_CD "
                        sql+="left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " 
                        sql+="left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report "
                        sql+="left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " 
                        sql+="left join patient_m26 on patient_m26.MNC_DOT_CD = labt01.mnc_dot_cd "
                        sql+="inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD "
                        #sql+="inner join bn5_scan.dbo.b_patient_smartcard on pttt01.patient_smartcard_id = b_patient_smartcard.patient_smartcard_id "
                        sql+="Where pttt01.mnc_hn_no = '"+hn+"' and pttt01.mnc_hn_yr = '"+hnyear+"' and pttt01.mnc_date = '"+vsdate+"' and pttt01.mnc_pre_no = '"+preno+"'  "
                        sql+="  and labt02.MNC_LB_CD in ('SE184','SE629') "
                        sql+="Order By labt05.mnc_req_dat,labm01.MNC_LB_CD, labm04.MNC_LB_RES_CD "
                        #print("sql "+sql)
                        rowcount=1
                        reqdate1=""
                        restime=""
                        labname=""
                        pttname=""
                        reqno=""
                        doctor=""
                        dob=""
                        pid=""
                        resdate=""
                        reporter=""
                        resulter=""
                        approver=""
                        reqdate=""
                        age1=""
                        user1=""
                        user2=""
                        user3=""
                        filename=""
                        image = Image.new(mode = "RGB", size = (1600,800), color = "white")
                        draw = ImageDraw.Draw(image)

                        conn1 = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
                        cur1 = conn1.cursor()
                        cur1.execute(sql)
                        resp1 = cur1.fetchall()
                        #print("cur1.rowcount "+str(cur1.rowcount))
                        for row1 in resp1:
                            if row1[1] == None:
                                #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="HN นี้ ยังไม่ได้ผล "))#limit monthly
                                continue
                            rowcount = rowcount+1
                            filename = folder+doc+"_"+timestamp+"_"+hn+'_lab.jpg'
                            labcode = row1[0]
                            labname = row1[1]
                            reqdate = row1[7]
                            reporter=row1[9]
                            resulter=row1[10]
                            approver=row1[11]
                            reqno = str(row1[12])
                            dob = row1[13]
                            resdate = row1[14]
                            restime = "0"+str(row1[15])
                            doctor = row1[16]
                            pid = row1[17]
                            age1 = row1[19]
                            user1 = row1[20]
                            user2 = row1[21]
                            user3 = row1[22]
                            reqdate1 = row1[18]
                            
                            if row1[4] != None:
                                labval = row1[2]
                                interpret = row1[3]
                                labres = row1[4]
                                normal = row1[5]
                                unit = row1[6]
                                pttname = row1[8]
                                reqdate = row1[7]
                            else:
                                labval=''
                                interpret=''
                                labres=''
                                normal=''
                                unit=''
                                pttname=''
                                reqdate=''
                            draw.text((col2-50,y), labres, font=fntS28, fill=(0,0,0))
                            draw.text((col4-20,y), labval, font=fntS28, fill=(0,0,0))
                            draw.text((col6,y), unit, font=fntS24, fill=(0,0,0))
                            y = y+30
                            #row = row+1
                        if(len(restime)>=5):
                                restime = restime[len(restime)-4:len(restime)]
                        draw.text((40,235), "GROUP : IMMUNO + TUMOR", font=fntS28, fill=(0,0,0))
                        draw.text((40,265), labname, font=fntS28, fill=(0,0,0))

                        draw.text((20,20), "โรงพยาบาล บางนา5", font=fnt45, fill=(0,0,0))
                        draw.text((20,60), "Bangna5 General Hospital ", font=fnt45, fill=(0,0,0))
                        draw.text((20,100), "ใบรายงานผล Result LAB", font=fnt45, fill=(0,0,0))

                        draw.text((520,20), "Patient Name "+pttname, font=fnt, fill=(0,0,0))
                        draw.text((520,60), "HN "+hn, font=fnt, fill=(0,0,0))
                        draw.text((520,100), "เลขที่/Req no "+str(reqno)+"."+reqdate, font=fntS30, fill=(0,0,0))
                        draw.text((520,135), "แพทย์/Doctor "+str(doctor), font=fntS30, fill=(0,0,0))

                        draw.text((1250,20), "วันเกิด/DOB : "+dob+" [age "+str(age1)+"]", font=fntS24, fill=(0,0,0))
                        draw.text((1250,60), "ID/Passport : "+str(pid), font=fntS24, fill=(0,0,0))
                        draw.text((1250,100), "วันที่ส่งตรวจ/Request Date : "+reqdate1, font=fntS24, fill=(0,0,0))
                        draw.text((1250,135), "วันที่ออกผล/Result Date : "+resdate+" "+restime, font=fntS24, fill=(0,0,0))

                        draw.text((25,153), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                        draw.text((25,190), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                        draw.text((300,170), "รายการ/DESCRIPTION", font=fntS32, fill=(0,0,0))
                        draw.text((800,170), "ผลตรวจ/RESULT", font=fntS32, fill=(0,0,0))

                        draw.text((550,305), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                        draw.text((550,335), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                        draw.text((550,365), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                        draw.text((550,395), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                        draw.text((550,425), ".........................................................................................................................................................................................", font=fntS20, fill=(0,0,0))

                        draw.text((25,680), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                        draw.text((25,720), "............................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................................", font=fntS20, fill=(0,0,0))
                        draw.text((45,700), "ผู้บันทึก/recorder : "+reporter+"["+user1+"]", font=fntS28, fill=(0,0,0))
                        draw.text((600,700), "ผู้รายงาน/reporter : "+resulter+"["+user2+"]", font=fntS28, fill=(0,0,0))
                        draw.text((1150,700), "ผู้ตรวจสอบ/approver : "+approver+"["+user3+"]", font=fntS28, fill=(0,0,0))
                        draw.text((35,740), "FM-LAB-096 (แก้ไขครั้งที่ 00-17/07/55) ", font=fntS20, fill=(0,0,0))

                        draw.text((1400,740), "send date "+datetime.today().strftime("%d/%m/%Y"), font=fntS20, fill=(0,0,0))
                        if(filename!=""):
                            image.save(filename)
                            rowResultCnt=rowResultCnt+1
                            time.sleep(0.05)
                        rowcnt = rowcnt+1
                        if (rowcnt==50):
                            print("==50")
                            #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="ทำได้ "+str(rowcnt)+" รายการ "))#limit monthly
                        elif (rowcnt==100):
                            print("==100")
                            #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="ทำได้ "+str(rowcnt)+" รายการ "))#limit monthly
                        cur1.close()
                        conn1.close()
                    if(hn==""):
                        print('no data ')
                    cur.close()
                    conn.close()
                    #line_bot_api.reply_message(event.reply_token,TextSendMessage("เตรียมข้อมูล เรียบร้อย"))
                    
                    #text_message = TextSendMessage(text="เตรียมข้อมูล เรียบร้อย จำนวน "+str(rowcnt)+" รายการ ได้ภาพผล จำนวน "+str(rowResultCnt)+" ภาพ ")#limit monthly
                    #line_bot_api.push_message(event.source.user_id, text_message)#limit monthly
                    filecnt=0
                    for filename1 in os.listdir(folder):
                        filecnt=filecnt+1
                    mail_content = ""
                    #if(filecnt<=30):
                    mail_content = ""
                    sender_address = 'eploentham@gmail.com'
                    sender_pass = 'Gsdscitsigol1*'
                    #receiver_address = 'bangna5lab@hotmail.com'
                    #receiver_address = 'eploentham@gmail.com'
                    receiver_address = "bangna5.lab@gmail.com"
                    receiver_address1 = "bng1hos@gmail.com"#"nurbangna1@gmail.com"
                    receiver_address11 = "nurbangna1@gmail.com"
                    receiver_address2 = "supeera.bn2@gmail.com"#supat.icu2@gmail.com     Supeera.bn2@gmail.com

                    if(len(doc)>1):
                        if(doc[0:1]=="1"):
                            #receiver_address = 'bangna5.lab@gmail.com,'+receiver_address1
                            #receiver_address = "bangna5.lab@gmail.com, ".join(receiver_address1).join(receiver_address11)
                            receiver_address = receiver_address1#+","+receiver_address11
                        elif(doc[0:1]=="2"):
                            #receiver_address = 'bangna5.lab@gmail.com,'+receiver_address2
                            receiver_address = receiver_address2

                    message = MIMEMultipart()
                    message['From'] = sender_address
                    message['To'] = receiver_address
                    if(statusdocORdate=="doc") :
                        message['Subject'] = "รายงานผล LAB บางนา5 เลขที่ "+doc
                    else:
                        message['Subject'] = "รายงานผล LAB บางนา5 วันที่ "+reqdate2
                    filecnt1=0
                    filesend=0
                    filecnt2=0
                    for filename1 in os.listdir(folder):
                        #print("filename "+filename1)
                        try:
                            filecnt1=filecnt1+1
                            #print("filename "+filename1+" filecnt "+str(filecnt))
                            #if(filecnt1==1):
                            print("filecnt1 "+str(filecnt1))
                            # message = MIMEMultipart()
                            # message['From'] = sender_address
                            # message['To'] = receiver_address
                            #payload.set_payload()
                            message.attach(MIMEText(mail_content, 'plain'))
                            attach_file = open(folder+filename1, 'rb') # Open the file as binary mode
                            payload = MIMEBase('application', 'octate-stream')
                            payload.set_payload((attach_file).read())
                            encoders.encode_base64(payload) #encode the attachment
                            #add payload header with filename
                            #payload.add_header('Content-Decomposition', 'attachment', filename=filename1)
                            payload.add_header('Content-Disposition', 'attachment', filename=filename1)
                            message.attach(payload)
                            # elif(filecnt1<30):
                            #     message.attach(MIMEText(mail_content, 'plain'))
                            #     attach_file = open(folder+filename1, 'rb') # Open the file as binary mode
                            #     payload = MIMEBase('application', 'octate-stream')
                            #     payload.set_payload((attach_file).read())
                            #     encoders.encode_base64(payload) #encode the attachment
                            #     #add payload header with filename
                            #     #payload.add_header('Content-Decomposition', 'attachment', filename=filename1)
                            #     payload.add_header('Content-Disposition', 'attachment', filename=filename1)
                            #     message.attach(payload)
                            # elif(filecnt1==30):
                            #     filecnt2=filecnt2+1
                            #     filesend=filesend+1
                            #     message['From'] = sender_address
                            #     message['To'] = receiver_address
                            #     message['Subject'] = "รายงานผล LAB บางนา5  "+doc+"-"+str(filecnt2)
                            #     message.attach(MIMEText(mail_content, 'plain'))
                            #     attach_file = open(folder+filename1, 'rb') # Open the file as binary mode
                            #     #payload = MIMEBase('application', 'octate-stream')
                            #     payload.set_payload((attach_file).read())
                            #     encoders.encode_base64(payload) #encode the attachment
                            #     #add payload header with filename
                            #     #payload.add_header('Content-Decomposition', 'attachment', filename=filename1)
                            #     payload.add_header('Content-Disposition', 'attachment', filename=filename1)
                            #     message.attach(payload)

                            #     #Create SMTP session for sending the mail
                            #     session = smtplib.SMTP('smtp.gmail.com', 587) #use gmail with port
                            #     session.starttls() #enable security
                            #     session.login(sender_address, sender_pass) #login with mail_id and password
                            #     text = message.as_string()
                            #     session.sendmail(sender_address, receiver_address, text)
                            #     session.quit()
                            #     print("filename "+str(filecnt1))
                            #     filecnt1=0                           
                            #     #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="ส่ง email ไป "+receiver_address+" email "+str(filecnt2)+" เรียบร้อย ครับ"))#limit monthly
                            # elif(filecnt1==filecnt):
                            #     filecnt2=filecnt2+1
                            #     filesend=filesend+1
                            #     message['From'] = sender_address
                            #     message['To'] = receiver_address
                            #     message['Subject'] = "รายงานผล LAB บางนา5  "+doc+"-"+str(filecnt2)
                            #     message.attach(MIMEText(mail_content, 'plain'))
                            #     attach_file = open(folder+filename1, 'rb') # Open the file as binary mode
                            #     payload = MIMEBase('application', 'octate-stream')
                            #     payload.set_payload((attach_file).read())
                            #     encoders.encode_base64(payload) #encode the attachment
                            #     #add payload header with filename
                            #     #payload.add_header('Content-Decomposition', 'attachment', filename=filename1)
                            #     payload.add_header('Content-Disposition', 'attachment', filename=filename1)
                            #     message.attach(payload)
                            #     #Create SMTP session for sending the mail
                            #     session = smtplib.SMTP('smtp.gmail.com', 587) #use gmail with port
                            #     session.starttls() #enable security
                            #     session.login(sender_address, sender_pass) #login with mail_id and password
                            #     text = message.as_string()
                            #     session.sendmail(sender_address, receiver_address, text)
                            #     session.quit()
                            #     #line_bot_api.push_message(event.source.user_id,TextSendMessage(text="ส่ง email ไป "+receiver_address+" email "+str(filecnt2)+" เรียบร้อย ครับ"))#limit monthly
                            #     filecnt1=0
                            # else:
                            #     filecnt1=0

                        except Exception as e:
                            print(' %s' % (filename1, e))
                    print("start send email ")
                    session = smtplib.SMTP('smtp.gmail.com', 587) #use gmail with port
                    session.starttls() #enable security
                    session.login(sender_address, sender_pass) #login with mail_id and password
                    text = message.as_string()
                    session.sendmail(sender_address, receiver_address, text)
                    session.quit()
                    print('Mail Sent')


                    text_message = TextSendMessage(text="ส่ง email ไป "+receiver_address+" เรียบร้อย ครับ")
                    #line_bot_api.push_message(event.source.user_id,text_message)#limit monthly
                    #line_bot_api.reply_message(event.reply_token,TextSendMessage("ส่ง email เรียบร้อย"))
                elif (txt32.strip().lower() == 'sm1') and (statusdocORdate=="doc"):
                    print("sm1 "+statusdocORdate)
                    col2 = 260
                    col3 = 600
                    col4 = 800
                    col5 = 1100
                    col6 = 1200
                    fnt = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 42)
                    fnt36 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 36)
                    fnt40 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 40)
                    fnt45 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 45)
                    fntB1 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 52)
                    fntS30 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 30)
                    fntS32 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 32)
                    fntS28 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 28)
                    fntS24 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 24)
                    fntS20 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 20)
                    fntS18 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 18)
                    fntS16 = ImageFont.truetype('c:\\webview\\webview\\THSarabunNew.ttf', 16)
                    col2 = 260
                    col3 = 600
                    col4 = 800
                    col5 = 1100
                    col6 = 1200
                    sqlwhere=""
                    sql=""
                    receiver_address = 'bangna5cashier@gmail.com'
                    now = datetime.now()
                    timestamp = datetime.timestamp(now)
                    timestamp = str(timestamp).replace(".", "_")
                    folder = "c:\\temp_line_cashier\\"
                    folderemailb1="c:\\temp_line_email_sm1_b1\\"
                    folderemailb2="c:\\temp_line_email_sm1_b2\\"
                    if txt31.strip().lower() == 'email':
                        if(txt32.strip().lower() == 'sm1') and (statusdocORdate=="doc"):
                            if (doc[0:1]=="1"):
                                folder = folderemailb1
                            elif (doc[0:1]=="2"):
                                folder = folderemailb2
                    deleteFileinFolder(folder)
                    # for filename in os.listdir(folder):
                    #     file_path = os.path.join(folder, filename)
                    #     try:
                    #         if os.path.isfile(file_path) or os.path.islink(file_path):
                    #             os.unlink(file_path)
                    #         elif os.path.isdir(file_path):
                    #             shutil.rmtree(file_path)
                    #     except Exception as e:
                    #         print('Failed to delete %s. Reason: %s' % (file_path, e))
                    if(statusdocORdate=="doc") :

                        hn=""
                        preno=""
                        vsdate=""
                        vstime=""
                        hnyear=""
                        vn=""
                        line_bot_api.push_message(event.source.user_id,TextSendMessage(text="okครับ ให้ส่ง email ตามเลขที่ "+doc+" ส่งไป "+receiver_address))
                        sqlwhere=""
                        sqlwhere+="Where b_patient_smartcard.doc = '"+doc+"' and pttt01.MNC_DATE >= DATEADD(day,-3, GETDATE()) "
                        
                        sql="Select pttt01.mnc_hn_no, pttt01.mnc_hn_yr, convert(VARCHAR(20),pttt01.mnc_date,23) as mnc_date, pttt01.mnc_pre_no, pttt01.mnc_vn_no, pttt01.mnc_vn_seq  "
                        sql+=", pttt01.mnc_time "
                        sql+="from patient_t01 pttt01 "
                        sql+="inner join bng5_dbms_front.dbo.patient_m01 pttm01 on pttm01.MNC_HN_NO = pttt01.MNC_HN_NO and pttm01.mnc_hn_yr = pttt01.mnc_hn_yr "
                        sql+="inner join bn5_scan.dbo.b_patient_smartcard on pttm01.patient_smartcard_id = b_patient_smartcard.patient_smartcard_id "
                        sql+=sqlwhere
                        sql+="Order By b_patient_smartcard.patient_smartcard_id "
                        #print(sql)
                        conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
                        cur = conn.cursor()
                        cur.execute(sql)
                        resp = cur.fetchall()
                        rowcnt=0
                        rowResultCnt=0
                        desc=""
                        defcd=""
                        fullname=""
                        filename=""
                        for row in resp:
                            y = 295
                            rowcnt=rowcnt+1
                            rowResultCnt=0
                            hn=str(row[0])
                            preno=str(row[3])
                            vsdate=str(row[2])
                            hnyear=str(row[1])
                            vstime=str(row[6])
                            vn=str(row[4])+"/"+str(row[5])
                            fullname=""
                            diag=""
                            docno=""
                            desc=""
                            docyear=""
                            docdate=""
                            defcd=""
                            space=""
                            amt=0
                            amtbaht=""
                            total=0
                            #print("hn "+str(hn))
                            filename = folder+doc+"_"+timestamp+"_"+hn+'_cashier.jpg'
                            sql="Select tem01.MNC_DOC_CD, tem01.MNC_DOC_YR, tem01.MNC_DOC_NO,convert(VARCHAR(20), tem01.mnc_doc_dat ,23) as mnc_doc_dat, tem02.MNC_DEF_CD, tem02.MNC_DEF_DSC, tem02.MNC_ORD_BY "#6
                            sql+=", tem02.MNC_AMT, tem02.MNC_DEF_LEV, tem02.MNC_TOT_AMT, tem02.MNC_DIS_AMT, tem02.MNC_STS, tem02.MNC_QTY "#7    12
                            sql+=", tem01.mnc_hn_name, tem01.mnc_com_name, tem01.mnc_com_addr, tem01.mnc_age, tem01.mnc_sex "#13    17
                            sql+=", tem02.MNC_def_lev, tem01.MNC_DIA_DSC, tem01.MNC_sum_amt "#18   19
                            sql+="From temporary_m01 tem01 "
                            sql+="inner join temporary_m02 tem02 on tem01.MNC_DOC_CD = tem02.MNC_DOC_CD and tem01.MNC_DOC_YR = tem02.MNC_DOC_YR and tem01.MNC_DOC_NO = tem02.MNC_DOC_NO and tem01.MNC_DOC_DAT = tem02.MNC_DOC_DAT "
                            sql+="where tem01.mnc_hn_yr  = '"+ hnyear + "' and tem01.mnc_hn_no = '"+ hn + "' and tem01.mnc_vn_no = '"+vn+"' and tem01.mnc_doc_dat = '"+vsdate+"'"
                            sql+="Order By tem02.mnc_ord_by "
                            #print("sql "+sql)
                            conn1 = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
                            cur1 = conn1.cursor()
                            cur1.execute(sql)
                            resp1 = cur1.fetchall()
                            #image = Image.new(mode = "RGB", size = (1600,800), color = "white")
                            a4im = Image.new('RGB', (595, 842),(255, 255, 255)) # A4 at 72dpi # White
                            draw = ImageDraw.Draw(a4im)
                            
                            
                            for row1 in resp1:
                                rowResultCnt=rowResultCnt+1
                                space+="     "
                                desc=str(row1[5])
                                defcd=str(row1[4])
                                fullname=row1[13]
                                diag=row1[19].strip()                          
                                docyear=row1[1]
                                docno="PO"+str(docyear)+str(row1[2])
                                docdate=row1[3]
                                amt=row1[7]
                                total=row1[20]
                                amtbaht=row1[10]
                                #print("defcd "+defcd+" "+desc)
                                #reqdate2 = datetime.strptime(vsdate.strip(), '%d-%m-%Y').date()
                                if(rowResultCnt==1):
                                    draw.text((500,50), "Page 1 of 1 "+hn, font=fntS16, fill=(0,0,0))
                                    draw.text((30,60), "HN: "+hn, font=fntS20, fill=(0,0,0))
                                    draw.text((480,60), ""+docno, font=fntS20, fill=(0,0,0))
                                    draw.text((200,120), "ใบงบหน้าสรุปรายการค่ารักษาพยาบาล ", font=fntS28, fill=(0,0,0))
                                    
                                    #draw.text((500,120), ""+datetime.strptime(vsdate.strip(), '%Y-%m-%d'), font=fntS24, fill=(0,0,0))
                                    draw.text((500,90), ""+vsdate.strip(), font=fntS20, fill=(0,0,0))
                                    draw.text((40,145), ""+fullname, font=fntS24, fill=(0,0,0))
                                    draw.text((350,145), ""+diag, font=fntS20, fill=(0,0,0))
                                    #draw.text((450,170), ""+datetime.strptime(vsdate.strip(), '%Y-%m-%d')+"  "+vstime, font=fntS20, fill=(0,0,0))
                                    draw.text((450,175), ""+vsdate.strip()+"  "+vstime, font=fntS20, fill=(0,0,0))
                                    #draw.text((350,200), ""+datetime.strptime(vsdate.strip(), '%Y-%m-%d'), font=fntS20, fill=(0,0,0))
                                    draw.text((350,205), ""+vsdate.strip(), font=fntS20, fill=(0,0,0))
                                    draw.text((450,205), "ดังรายการต่อไปนี้", font=fntS20, fill=(0,0,0))
                                    draw.text((30,700), bahttext(total), font=fntS24, fill=(0,0,0))
                                    #draw.text((500,700), str(total), font=fntS24, fill=(0,0,0))
                                y = y+30
                                draw.text((60,y), space+defcd+"   "+desc, font=fntS20, fill=(0,0,0))
                                if(amt>1):
                                    draw.text((500,y), str(amt), font=fntS20, fill=(0,0,0))
                                
                                if(filename!=""):
                                    a4im.save(filename)
                                rowResultCnt=rowResultCnt+1
                                time.sleep(0.05)

                        text_message = TextSendMessage(text="เตรียมข้อมูล เรียบร้อย จำนวน "+str(rowcnt)+" รายการ ")
                        line_bot_api.push_message(event.source.user_id, text_message)

                        mail_content = ""
                        sender_address = 'eploentham@gmail.com'
                        sender_pass = 'Gsdscitsigol1*'
                        receiver_address = 'bangna5cashier@gmail.com'       # Bangna5cashier@gmail.com
                        message = MIMEMultipart()
                        message['From'] = sender_address
                        message['To'] = receiver_address
                        message['Subject'] = "ใบงบหน้าสรุปรายการค่ารักษาพยาบาล บางนา5 เลขที่ "+doc

                        for filename1 in os.listdir(folder):
                            print("filename "+filename1)
                            try:
                                message.attach(MIMEText(mail_content, 'plain'))
                                attach_file = open(folder+filename1, 'rb') # Open the file as binary mode
                                payload = MIMEBase('application', 'octate-stream')
                                payload.set_payload((attach_file).read())
                                encoders.encode_base64(payload) #encode the attachment
                                #add payload header with filename
                                #payload.add_header('Content-Decomposition', 'attachment', filename=filename1)
                                payload.add_header('Content-Disposition', 'attachment', filename=filename1)
                                message.attach(payload)
                            except Exception as e:
                                print(' %s' % (filename1, e))
                        
                        #Create SMTP session for sending the mail
                        session = smtplib.SMTP('smtp.gmail.com', 587) #use gmail with port
                        session.starttls() #enable security
                        session.login(sender_address, sender_pass) #login with mail_id and password
                        text = message.as_string()
                        session.sendmail(sender_address, receiver_address, text)
                        session.quit()
                        print('Mail Sent')
                        text_message = TextSendMessage(text="ส่ง email ไป "+receiver_address+" เรียบร้อย ครับ")
                        line_bot_api.push_message(event.source.user_id,text_message)
                elif (statusdocORdate=="date"):
                    sql="Select * from patient_t01 "
                    +"Where '"+doc+"'"
            else:
                line_bot_api.reply_message(event.reply_token,TextSendMessage(text=event.message.text))   
        #else:
        #    line_bot_api.reply_message(event.reply_token,TextSendMessage(text=event.message.text))
    #line_bot_api.reply_message(
    #    event.reply_token,
    #    TextSendMessage(text=event.message.text))
if __name__ == "__main__":
    app.run(port=80)