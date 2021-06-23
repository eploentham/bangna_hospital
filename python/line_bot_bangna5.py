import pyodbc
from ftplib import FTP
from datetime import datetime
import time
import cv2
from pdf2image import convert_from_path, convert_from_bytes
from PIL import Image, ImageDraw, ImageFont
import os
import sys
import json
from flask import Flask, request, abort

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
handler = WebhookHandler('2d9a359eea72d3255015291b54549207')
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
            time.sleep(1)
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
            time.sleep(1)
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
            #if text[0:2] == 'ขอ':
            if txt3[0] == 'ขอ':
                #txt2 = text[2:9]
                txt2 = txt3[1]
                print('txt2 '+txt2)
                #line_bot_api.reply_message(event.reply_token,TextSendMessage('test'))
                re = '';
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
            elif txt3[0] == 'ส่ง':
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
            elif txt3[0] == 'send':
                print('send')
                txt31 = txt3[1]
                txt32 = ""
                txt33 = ""
                doc = ""
                emailsend1=""
                emailsend2=""
                reqdate=""
                statusdocORdate=""
                sql=""
                print('txt31 '+txt31)
                if txt31.strip().lower() == 'email':
                    txt32 = txt3[2]
                    print('txt32 '+txt32)
                    if txt32.strip().lower() == 'lab':
                        txt33 = txt3[3]
                        print('txt33 '+txt33)
                        if txt33.strip().lower() == 'bn1':
                            print('txt31 '+txt31)
                            emailsend1=""
                            emailsend2=""
                        elif txt33.strip().lower() == 'bn2':
                            print('txt33 '+txt33)
                            emailsend1=""
                            emailsend2=""
                            doc = txt3[4]
                            if(len(doc)!=10):
                                line_bot_api.reply_message(event.reply_token,TextSendMessage("เลขที่ ไม่ถูกต้อง"))
                            statusdocORdate="doc"
                        else:
                            reqdate=""
                            try:
                                reqdate = datetime.strptime(txt33, '%Y-%m-%d').date()
                                statusdocORdate="date"
                            except ValueError as e:
                                reqdate="error date time"
                if(txt33!="bn1" and txt33!="bn2"):
                    line_bot_api.reply_message(event.reply_token,TextSendMessage("สาขา ไม่ถูกต้อง bn1, bn2"))
                if(statusdocORdate=="doc"):
                    sql="Select pttt01.mnc_hn_no, pttt01.mnc_hn_yr, pttt01.mnc_date, pttt01.mnc_pre_no  "
                    sql+="from patient_t01 pttt01 "
                    sql+="inner join patient_m01 pttm01 on pttm01.MNC_HN_NO =pttt01.MNC_HN_NO and pttm01.mnc_hn_yr = pttm01.mnc_hn_yr "
                    sql+="left join LAB_T01 labt01 ON pttt01.MNC_PRE_NO = labt01.MNC_PRE_NO AND pttt01.MNC_DATE = labt01.MNC_DATE "
                    sql+="left join LAB_T02 labt02 ON labt01.MNC_REQ_NO = labt02.MNC_REQ_NO AND labt01.MNC_REQ_DAT = labt02.MNC_REQ_DAT "
                    sql+="left join LAB_T05 labt05 ON labt02.MNC_REQ_NO = labt05.MNC_REQ_NO AND labt02.MNC_REQ_DAT = labt05.MNC_REQ_DAT AND labt02.MNC_LB_CD = labt05.MNC_LB_CD  "
                    sql+="left join LAB_M01 labm01 ON labt02.MNC_LB_CD = labm01.MNC_LB_CD "
                    sql+="left join LAB_M04 labm04 ON labt05.MNC_LB_RES_CD = labm04.MNC_LB_RES_CD and labt05.MNC_LB_CD = labm04.MNC_LB_CD "
                    sql+="left join userlog_m01 usr_result on usr_result.MNC_USR_NAME = labt02.mnc_usr_result " 
                    sql+="left join userlog_m01 usr_report on usr_report.MNC_USR_NAME = labt02.mnc_usr_result_report "
                    sql+="left join userlog_m01 usr_approve on usr_approve.MNC_USR_NAME = labt02.mnc_usr_result_approve " 
                    sql+="left join patient_m26 on patient_m26.MNC_DOT_CD = labt01.mnc_dot_cd "
                    sql+=" inner join patient_m02 on patient_m26.MNC_DOT_PFIX =patient_m02.MNC_PFIX_CD "
                    sql+="Where pttt01.patient_smartcard_id = '"+doc+"' "
                    sql+="Order By labt05.mnc_req_dat,labm01.MNC_LB_CD, labm04.MNC_LB_RES_CD "
                    print('sql '+sql)
                    conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
                    cur = conn.cursor()
                    cur.execute(sql)
                    resp = cur.fetchall()
                    hn=""
                    for row in resp:
                        hn=row[0]
                        print("hn "+str(hn))
                    if(hn==""):
                        print('no data ')
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