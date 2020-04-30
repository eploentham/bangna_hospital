import pyodbc
from ftplib import FTP
from datetime import datetime
import time
import cv2
from pdf2image import convert_from_path, convert_from_bytes
from PIL import Image, ImageDraw, ImageFont
import os
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


@app.route("/callback", methods=['POST'])
def callback():
    # get X-Line-Signature header value
    signature = request.headers['X-Line-Signature']

    # get request body as text
    body = request.get_data(as_text=True)
    app.logger.info("Request body: " + body)

    # handle webhook body
    try:
        handler.handle(body, signature)
    except InvalidSignatureError:
        print("Invalid signature. Please check your channel access token/channel secret.")
        abort(400)

    return 'OK'


@handler.add(MessageEvent, message=TextMessage)
def handle_message(event):
    text = event.message.text
    txt1 = ''
    txt2 = ''
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
            #sql = "select stf.mnc_usr_name, stf.mnc_usr_pw, stf.mnc_usr_full From userlog_m01 Where mnc_usr_name = '" + txt2 + "'"
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
            line_bot_api.reply_message(event.reply_token,TextSendMessage('สวัสดดี '+re+' add Line success'))
        else:
            txt3 = text.split()
            #if text[0:2] == 'ขอ':
            if txt3[0] == 'ขอ':
                #txt2 = text[2:9]
                txt2 = txt3[1]
                print('id '+txt2)
                #line_bot_api.reply_message(event.reply_token,TextSendMessage('test'))
                re = '';
                if txt2.strip().lower() =='outlab':
                    hn=''
                    #txt3 = text[9:13]
                    if len(text)>16:
                        hn = text[9:17].strip()
                    print('hn outlab '+hn)
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
                        line_bot_api.push_message(event.source.user_id, text_message1)

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
                            line_bot_api.push_message(event.source.user_id, image_message)

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
                    sql9=''
                    sql10=''
                    sql1 = "SELECT LAB_T02.MNC_LB_CD, LAB_M01.MNC_LB_DSC, LAB_T05.MNC_RES_VALUE, LAB_T05.MNC_STS, LAB_T05.MNC_RES "
                    sql2 = ", LAB_T05.MNC_RES_UNT, LAB_T05.MNC_LB_RES,convert(VARCHAR(20),lab_t05.mnc_req_dat,23) as mnc_req_dat, lab_t05.mnc_res "
                    sql3 = "FROM     PATIENT_T01 t01 "
                    sql4 = "left join LAB_T01 ON t01.MNC_PRE_NO = LAB_T01.MNC_PRE_NO AND t01.MNC_DATE = LAB_T01.MNC_DATE "
                    sql41 = "left join LAB_T02 ON LAB_T01.MNC_REQ_NO = LAB_T02.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T02.MNC_REQ_DAT "
                    sql5 = "left join LAB_T05 ON LAB_T01.MNC_REQ_NO = LAB_T05.MNC_REQ_NO AND LAB_T01.MNC_REQ_DAT = LAB_T05.MNC_REQ_DAT "
                    sql6 = "left join LAB_M01 ON LAB_T02.MNC_LB_CD = LAB_M01.MNC_LB_CD "
                    sql7 = "where t01.MNC_DATE BETWEEN '" + reqdate2 + "' AND '" + reqdate2 + "' " 
                    sql8 = "and t01.mnc_hn_no = '" + hn + "' " 
                    #sql9 = "and t01.mnc_vn_no = '" + vn + "'  " +
                    #sql10 = "and t01.mnc_Pre_no = '" + preNo + "'  " +
                    sql11 = "Order By lab_t05.mnc_req_dat,LAB_M01.MNC_LB_CD, lab_t05.mnc_lab_prn"
                    now = datetime.now()
                    timestamp = datetime.timestamp(now)
                    timestamp = str(timestamp).replace(".", "_")
                    filename = 'c:\\temp_line\\'+timestamp+'_lab.jpg'
                    fnt = ImageFont.truetype('THSarabunNew.ttf', 18)
                    image = Image.new(mode = "RGB", size = (400,600), color = "white")
                    draw = ImageDraw.Draw(image)
                    sql = sql1 + sql2 + sql3 + sql4+ sql41 + sql5 + sql6 + sql7 + sql8 + sql9 + sql10 + sql11
                    print('sql ', sql)
                    conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bng5_dbms_front;UID=sa;PWD=;Trusted_Connection=no;')
                    cur = conn.cursor()
                    cur.execute(sql)
                    myresult = cur.fetchall()
                    y = int(1)
                    x = int(1)
                    col1 = 1
                    col2 = 200
                    col3 = 400
                    for res in myresult:
                        if res[1] == None:
                            continue
                        labname = res[1]
                        labval = res[2]
                        labres = res[3]
                        print('labname ', labname)
                        print('y ', y)
                        draw.text((x,y+30), labname, font=fnt, fill=(0,0,0))
                        draw.text((x+col2,y+30), labval, font=fnt, fill=(0,0,0))
                        draw.text((x+col3,y+30), labres, font=fnt, fill=(0,0,0))
                        y = y+20
                        #x = x + 10
                    
                    image.save(filename)

                    cur.close()
                    conn.close()
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
            else:
                line_bot_api.reply_message(event.reply_token,TextSendMessage(text=event.message.text))   
        #else:
        #    line_bot_api.reply_message(event.reply_token,TextSendMessage(text=event.message.text))
    #line_bot_api.reply_message(
    #    event.reply_token,
    #    TextSendMessage(text=event.message.text))
if __name__ == "__main__":
    app.run(port=80)