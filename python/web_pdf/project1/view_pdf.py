from flask import Flask, request, render_template
from flask_bootstrap import Bootstrap
from datetime import datetime
from ftplib import FTP
#import pyodbc
import pymssql

app = Flask(__name__, static_folder='app/static')
Bootstrap(app)
app.config['BOOTSTRAP_SERVE_LOCAL'] = True 

@app.route("/")
def hello():
    return "Hello, World!"

@app.route("/view_pdf", methods=['GET'])
def view_pdf():
    #ftp = FTP('172.1.1.5')
    ftp = FTP('localhost')
    ftp.login("ftpoutlab", "ftpoutlab")
    hn1=""
    
    txthn = request.args.get("txthn", None)
    if 'hn' in request.args:
        hn1 = request.args.get("hn")
    if 'txthn' in request.args and 'hn' not in request.args:        
        hn1 = txthn
    print("hn1 "+hn1)
    docscanid = "0"
    if request.args.get("doc_scan_id"):
        docscanid = request.args.get("doc_scan_id")
    print("docscanid "+docscanid)
    serverName="172.1.1.1"
    userDB="sa"
    passDB=""
    dataDB="bn1_outlab"
    #conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
    conn = pymssql.connect("172.1.1.1","sa","","bn1_outlab")
    cur = conn.cursor()
    sql = "Select doc_scan_id, visit_date, hn, vn, row_no, host_ftp, image_path, patient_fullname, date_req, req_id From doc_scan where hn = '"+hn1+"' and status_record = '2' and active = '1' Order By doc_scan_id"
    cur.execute(sql)
    result = cur.fetchall()
    rowcnt =cur.rowcount
    timestamp=""
    #data=hn
    if(len(docscanid)<=0):
        return render_template('view_pdf.html',data1=result, hn=hn1, rowcnt=rowcnt)
    sql="Select * From doc_scan where doc_scan_id = '"+docscanid+"'  Order By doc_scan_id"
    cur.execute(sql)
    result1 = cur.fetchall()
    for res in result1:
        #now = datetime.now()
        #startdate = now.strftime("%Y-%m-%d, %H:%M:%S")
        #timestamp = datetime.timestamp(datetime.now())
        timestamp = str(datetime.timestamp(datetime.now())).replace(".", "_")
        ftpfilename = '//'+res[22]+'//'+res[4]
        #filename = 'c:\\temp\\'+timestamp+'.pdf'
        #filename = "d:\\source\\bangna\\bangna_hospital\\python\\web_pdf\\project1\\app\static\\temp\\"+timestamp+".pdf"
        filename = "//root//view_pdf//view_pdf_bn1//app//static//temp//"+timestamp+".pdf"
        try:
            #ftp.sendcmd('TYPE I')pdf
            #sizeftpfile = ftp.size(ftpfilename)
            #if sizeftpfile <= 0 :
            #	continue
            ftp.retrbinary("RETR " + ftpfilename ,open(filename, 'wb').write)
        except ftplib.all_errors as e:
            print('FTP error:', e)
            continue

    return render_template('view_pdf.html',data1=result, hn=hn1, rowcnt=timestamp)

if __name__ == "__main__":
    #app.run(debug=True)
    app.run(host='0.0.0.0')
