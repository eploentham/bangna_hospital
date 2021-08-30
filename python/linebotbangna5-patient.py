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
import mysql.connector

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
#line bot bangna1  ไว้ test
line_bot_api = LineBotApi('D2ww3Xka/bEqcnvYisDd8ng30Hs3wUb5FQ/BXM3Kz90Lh/4kTXxHInDAJA2VI9lh1RHZ8zDaXBFlz15jz722t4mcx0qZqa1zW4h9Finr+jBTy4D59R7zyfkZAzaJXxDoNfGMTP7kYVACCfCwMtY+ggdB04t89/1O/w1cDnyilFU=')
handler = WebhookHandler('41927b9bd90b80a83f270b996e454fa3')#Channel secret
json_line=""


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
        txt=""
        pid=""
        json_line = json.dumps(request.get_json())
        decoded = json.loads(json_line)
        eventtype = decoded["events"][0]['type']
        messagetype = decoded["events"][0]['message']['type']
        if(messagetype == "text"):
            text = decoded["events"][0]['message']['text']
        userid = decoded["events"][0]['source']['userId']
        sourcetype = decoded["events"][0]['source']['type']
        #sourceuser = decoded["events"][0]['source']['userId']
        print('text '+text)
        if(eventtype == "join"):
            cnx = mysql.connector.connect(
            host="127.0.0.1",
            port=3306,
            user="root",
            password="Ekartc2c5",
            database='bangna5_covid')
            #conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
            #conn = pyodbc.connect("DRIVER={MySQL ODBC 3.51 Driver}; SERVER=localhost; PORT=3306;DATABASE=bangna5_covid; UID=root; PASSWORD=Ekartc2c5;")
            cur = cnx.cursor()
            #cur = conn.cursor()
            #params = (text, userid,json_line,eventtype,sourcetype,userid,messagetype, id1)
            sql="insert into t_line_bot_log set "
            sql+=" date_create = now()"
            sql+=",user_id = '"+userid+"'"
            sql+=",events_type = '"+eventtype+"'"
            sql+=",source_type = '"+sourcetype+"'"
            sql+=",source_user = '"+userid+"'"
            sql+=",messagetype = '"+messagetype+"'"
            sql+=",message = '"+json_line+"'"
            #cur.execute("{call insert_t_line_bot_log1(?, ?, ?, ?, ?, ?, ?, ?)}", params)
            cur.execute(sql)
            cur.execute(sql)
            cnx.commit()
            cur.close()
            cnx.close()
        elif (eventtype == "message" and (sourcetype == "user")):
            #conn = pyodbc.connect("DRIVER={MySQL ODBC 3.51 Driver}; SERVER=localhost; PORT=3306;DATABASE=bangna5_covid; UID=root; PASSWORD=Ekartc2c5;")
            #cur = conn.cursor()
            cnx = mysql.connector.connect(
            host="127.0.0.1",
            port=3306,
            user="root",
            password="Ekartc2c5",
            database='bangna5_covid')
            sql="insert into t_line_bot_log set "
            sql+=" date_create = now()"
            sql+=",user_id = '"+userid+"'"
            sql+=",events_type = '"+eventtype+"'"
            sql+=",source_type = '"+sourcetype+"'"
            sql+=",source_user = '"+userid+"'"
            sql+=",message_type = '"+messagetype+"'"
            sql+=",message = '"+json_line+"'"
            sql+=",text = '"+text+"'"
            #print(sql)
            #sql="Insert into t_line_bot_log (date_create,user_id,events_type,source_type,source_user,messagetype,message)"
            #params = (text, userid,json_line,eventtype,sourcetype,userid,messagetype, id1)
            profile = line_bot_api.get_profile(userid)
            print(profile.status_message)
            profile1=""
            if profile.status_message is None:
                profile1=""
            else:
                profile1=profile.status_message
            cur = cnx.cursor()
            txt = text.split()
            if(len(txt)==2) and (txt[0].strip().lower()=="covid"):
                
                #print(profile.display_name)
                #print(profile.user_id)
                #print(profile.picture_url)
                
                pid=txt[1]
                sql="insert into t_line_bot_message set "
                sql+=" date_create = now()"
                sql+=", status_reply = '0'"
                sql+=", message = '"+text+"'"
                sql+=", txt1 = '"+txt[0]+"'"
                sql+=", txt2 = '"+txt[1]+"'"
                sql+=", txt3 = ''"
                sql+=", display_name = '"+profile.display_name+"'"
                sql+=", picture_url = '"+profile.picture_url+"'"
                sql+=", status_message = '"+profile1+"'"
                sql+=", status = '2'"
                #params = (text, userid,json_line,eventtype,sourcetype,userid,messagetype, id1)
                #cur.execute("{call insert_t_line_bot_log1(?, ?, ?, ?, ?, ?, ?, ?)}", params)
                cur.execute(sql)
                cnx.commit()
                cur.close()
                cnx.close()
                line_bot_api.push_message(userid,TextSendMessage("สวัสดี คุณ "+profile.display_name +" คำขอผลลงทะเบียน "+txt[1]+" เรียบร้อยค่ะ ผลออก จะแจ้งให้ทราบค่ะ"))
            else:
                sql="insert into t_line_bot_message set "
                sql+=" date_create = now()"
                sql+=", status_reply = '0'"
                sql+=", message = '"+text+"'"
                sql+=", txt1 = '"+text+"'"
                sql+=", txt2 = ''"
                sql+=", txt3 = ''"
                sql+=", display_name = '"+profile.display_name+"'"
                sql+=", picture_url = '"+profile.picture_url+"'"
                sql+=", status_message = '"+profile1+"'"
                sql+=", status = '1'"
                cur.execute(sql)
                cnx.commit()
                cur.close()
                cnx.close()
                #line_bot_api.push_message(userid,TextSendMessage(text))

        handler.handle(body, signature)
    except InvalidSignatureError:
        print("Invalid signature. Please check your channel access token/channel secret.")
        abort(400)

    return 'OK'
def sendOutLab(hn, userid):
    id1=''

@handler.add(MessageEvent, message=TextMessage)
def handle_message(event):
    text = event.message.text
    txt1 = ''
    txt2 = ''
    sql = ""
    statusdocORdate=""
    if(len(text)>2):
        txt1 = text.strip()
        txtsplit = txt1.split(' ')
        if txtsplit[0].strip().lower() == 'covid':
            sendOutLab(txtsplit[1], event.source.user_id)

    elif (statusdocORdate=="date"):
        txt1=""
    else:
        line_bot_api.reply_message(event.reply_token,TextSendMessage(text=event.message.text))

if __name__ == "__main__":
    app.run(port=80)