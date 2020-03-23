from flask import Flask, request, abort

from linebot import (
    LineBotApi, WebhookHandler
)
from linebot.exceptions import (
    InvalidSignatureError
)
from linebot.models import (
    MessageEvent, TextMessage, TextSendMessage,
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
    if text == 'profile':
        line_bot_api.reply_message(
        event.reply_token,
        TextSendMessage('Ekapop Ploentham'))
    elif txt1 == 'ขอ':
        txt2 = text[2:8]
        hn = txt2
        re = '';
        sql = "Select doc_scan_id, host_ftp, image_path,patient_fullname from doc_scan Where hn = '"+hn+"' and active = '1' and status_record = '2' "
        conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
        cur = conn.cursor()
        cur.execute(sql)
        myresult = cur.fetchall()
        for res in myresult:
            re += res[0] + " "
        
        line_bot_api.reply_message(event.reply_token,TextSendMessage(re))
    else:
        line_bot_api.reply_message(
        event.reply_token,
        TextSendMessage(text=event.message.text))
    #line_bot_api.reply_message(
    #    event.reply_token,
    #    TextSendMessage(text=event.message.text))
if __name__ == "__main__":
    app.run(port=80)