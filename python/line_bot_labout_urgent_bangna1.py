import pyodbc
from ftplib import FTP
from datetime import datetime
import time
import cv2
from pdf2image import convert_from_path, convert_from_bytes
from PIL import Image, ImageDraw, ImageFont
import os
import sys

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

line_bot_api = LineBotApi('D2ww3Xka/bEqcnvYisDd8ng30Hs3wUb5FQ/BXM3Kz90Lh/4kTXxHInDAJA2VI9lh1RHZ8zDaXBFlz15jz722t4mcx0qZqa1zW4h9Finr+jBTy4D59R7zyfkZAzaJXxDoNfGMTP7kYVACCfCwMtY+ggdB04t89/1O/w1cDnyilFU=')
handler = WebhookHandler('41927b9bd90b80a83f270b996e454fa3')


def sendOutLab(docscanid):
    sql = "Select doc_scan_id, host_ftp, image_path,patient_fullname, folder_ftp, hn from doc_scan Where doc_scan_id = '"+docscanid+"'  "
    conn = pyodbc.connect('Driver={SQL Server};Server=172.1.1.1;Database=bn1_outlab;UID=sa;PWD=;Trusted_Connection=no;')
    cur = conn.cursor()
    cur.execute(sql)
    myresult = cur.fetchall()
    print('sql '+sql)
    userid="Uee2edfda076dc8e16f123810c993289f"
    for res in myresult:
        pttname = res[3]
        hostftp = res[1]
        imagepath = res[2]
        hn = res[5]
        ftp = FTP('172.1.1.5')
        ftp.login("ftpoutlab", "ftpoutlab")
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
        text_message1 = TextSendMessage(text='ผล Out Lab HN Urgent '+hn+' '+pttname)
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
    print('send line_bot_api completed ')

print('sys.argv[0]', sys.argv[0])
print('sys.argv[1]', sys.argv[1])

sendOutLab(sys.argv[1])
                    
    #line_bot_api.reply_message(event.reply_token,TextSendMessage(text=event.message.text))   
