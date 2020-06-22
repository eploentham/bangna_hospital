from flask import Flask

app = Flask(__name__)

@app.route("/")
def hello():
    return "Hello, World!"

@app.route("/view_pdf")
def view_pdf():
    return render_template('view_pdf.html')

if __name__ == "__main__":
    app.run(debug=True)
