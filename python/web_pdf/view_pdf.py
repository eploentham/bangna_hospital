from flask import Flask, request, render_template
from flask_bootstrap import Bootstrap

app = Flask(__name__, static_folder='app/static')
Bootstrap(app)
app.config['BOOTSTRAP_SERVE_LOCAL'] = True 

@app.route("/")
def hello():
    return "Hello, World!"

@app.route("/view_pdf", methods=['GET'])
def view_pdf():
    hn = request.args.get("hn")
    serverName="172.25.10.5"
    userDB="sa"
    passDB=""
    dataDB="bn5_scan"
    conn = pyodbc.connect('Driver={SQL Server};Server='+serverName+';Database='dataDB';UID=sa;PWD=;Trusted_Connection=no;')
    cur = conn.cursor()
    sql = "Select * From doc_scan where hn = "+hn+" and status_record = '1' and active = '1' Order By doc_scan_id"
    cur.execute(sql)
    myresult = cur.fetchall()

    data=5555
    return render_template('view_pdf.html',data1=hn)

if __name__ == "__main__":
    app.run(debug=True)
