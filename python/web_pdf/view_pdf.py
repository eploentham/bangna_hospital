from flask import Flask, request, render_template
from flask_bootstrap import Bootstrap
from datetime import datetime
from ftplib import FTP
import pyodbc

app = Flask(__name__, static_folder='app/static')
Bootstrap(app)
app.config['BOOTSTRAP_SERVE_LOCAL'] = True 

@app.route("/")
def hello():
    return "Hello, World!"

@app.route("/view_pdf", methods=['GET'])
def view_pdf():
    ftp = FTP('172.25.10.3')
    ftp.login("imagescan", "imagescan")
    hn1 = request.args.get("hn")
    docscanid = request.args.get("doc_scan_id")
    serverName="172.25.10.5"
    userDB="sa"
    passDB=""
    dataDB="bn5_scan"
    conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
    cur = conn.cursor()
    sql = "Select doc_scan_id, visit_date, hn, vn, row_no, host_ftp, image_path, patient_fullname From doc_scan where hn = '"+hn1+"' and status_record = '2' and active = '1' Order By doc_scan_id"
    cur.execute(sql)
    result = cur.fetchall()
    rowcnt =cur.rowcount
    #data=hn
    sql="Select * From doc_scan where doc_scan_id = '"+docscanid+"'  Order By doc_scan_id"
    cur.execute(sql)
    result1 = cur.fetchall()
    for res in result1:
        #now = datetime.now()
        #startdate = now.strftime("%Y-%m-%d, %H:%M:%S")
        #timestamp = datetime.timestamp(datetime.now())
        timestamp = str(datetime.timestamp(datetime.now())).replace(".", "_")
        ftpfilename = '//'+res[22]+'//'+res[4]
        filename = 'c:\\temp\\'+timestamp+'.pdf'
        try:
            #ftp.sendcmd('TYPE I')
            #sizeftpfile = ftp.size(ftpfilename)
            #if sizeftpfile <= 0 :
            #	continue
            ftp.retrbinary("RETR " + ftpfilename ,open(filename, 'wb').write)
        except ftplib.all_errors as e:
            print('FTP error:', e)
            continue

    return render_template('view_pdf.html',data1=result, hn=hn1, rowcnt=rowcnt)

if __name__ == "__main__":
    app.run(debug=True)
