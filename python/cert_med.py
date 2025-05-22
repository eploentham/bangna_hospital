import cv2
import pytesseract
import os
import pyodbc
from datetime import datetime
#from ftplib import FTP
import ftplib
#from pyzbar import pyzbar
from pyzbar.pyzbar import decode
import zxing
#import pymssql
import pyqrcode
import shutil
import time
from io import BytesIO
import numpy as np
import easyocr
import json
#now = datetime.now()
print(datetime.now().strftime("%y-%m-%d %H:%M:%S") +" new easyocr.Reader")
serverName="localhost"
userDB="sa"
passDB="Ekartc2c5"
dataDB="bn5_scan"

files = []
g_ftpfilename = "'"
path_cert_med = "c:\\temp_cert_med\\"
path_cert_med_err = "c:\\temp_cert_med_error\\"
folder_ftp = "images2019_1"
filenametemNO = "tem-no-thai.jpg"
filenametemCerrStf1 = "tem-staff1.jpg"
filenametemCerrStf11 = "tem-staff11.jpg"
filenametemCerrStf2 = "tem-staff2.jpg"
filenametemCerrStf22 = "tem-staff22.jpg"
doc_group_id = "1100000007"
doc_group_sub_id = "1200000030"
colorBorder = (0,255,255)
decodeby=""
readerEasyOCR = easyocr.Reader(['en','th'])
print(datetime.now().strftime("%y-%m-%d %H:%M:%S") +" end easyocr.Reader")
if(os.path.isfile(filenametemNO)):
    temNO = cv2.imread(filenametemNO)
    #weiTemNO,heiTemNO, _ = temNO.shape
    heiTemNO,weiTemNO, _ = temNO.shape  # ต้องแก้ เพราะ openimage ด้วย opencv ทำให้ shape จะเป็น hei กับ wei ถ้า openimage ด้วย PIL shape จะเป็น wei,hei
    temCerrStf1 = cv2.imread(filenametemCerrStf1)
    temCerrStf11 = cv2.imread(filenametemCerrStf11)
    temCerrStf2 = cv2.imread(filenametemCerrStf2)
    temCerrStf22 = cv2.imread(filenametemCerrStf22)
    heiTemCerrStf1,weiTemCerrStf1, _ = temNO.shape
    heiTemCerrStf11,weiTemCerrStf11, _ = temNO.shape
    heiTemCerrStf2,weiTemCerrStf2, _ = temNO.shape
    heiTemCerrStf22,weiTemCerrStf22, _ = temNO.shape
#cvversion=cv2.__version__
def CropSave(imgSou):
    wei,hei, _ = imgSou.shape
    res = cv2.matchTemplate(imgSou, temNO, cv2.TM_CCOEFF)
    min_val, max_val, min_loc, max_loc = cv2.minMaxLoc(res)
    top_letf = max_loc
    y=top_letf[1]
    x=top_letf[0]+weiTemNO+30
    h=heiTemNO
    w=weiTemNO+160
    crop = imgSou[y:y+h, x:x+w]
    #img3 = cv2.cvtColor(img[top_letf[1]:bottom_right[1], top_letf[0]:bottom_right[1]+heiTemNO])
    now = datetime.now()
    startdate = now.strftime("%Y-%m-%d, %H:%M:%S")
    timestamp = datetime.timestamp(now)
    timestamp = str(timestamp).replace(".", "_")
    cv2.imwrite(path_cert_med+timestamp+".jpg",crop)
def getCertIdByResult(result):
    resu11 = ""
    resu12 = ""
    resu21 = ""
    resu22 = ""
    txt1 = ""
    txt2 = ""
    if(len(result)==1):
        resu11 = result[0][1]
        resu12 = result[0][2]
        txt1 = clearTxt(resu11)
        if(txt1.isnumeric()==True):
            return txt1,resu12
        else:
            return "",0
    elif(len(result)==0):
        return "",0
    elif(len(result)==2):
        resu11 = result[0][1]
        resu12 = result[0][2]
        resu21 = result[1][1]
        resu22 = result[1][2]
        txt1 = clearTxt(resu11)
        txt2 = clearTxt(resu21)
        if(txt1.isnumeric()==True) and (txt2.isnumeric()==False):
            return txt1,resu12
        elif(txt1.isnumeric()==False) and (txt2.isnumeric()==True):
            return txt2,resu22
        elif(txt1.isnumeric()==True) and (txt2.isnumeric()==True) and (len(str(txt1))==7):
            return txt1,resu12
        elif(txt1.isnumeric()==True) and (txt2.isnumeric()==True) and (len(str(txt2))==7):
            return txt2,resu22
        else:
            return "",0
    elif(len(result)==3):
        resu11 = result[0][1]
        resu12 = result[0][2]
        resu21 = result[1][1]
        resu22 = result[1][2]
        resu31 = result[2][1]
        resu32 = result[2][2]
        txt1 = clearTxt(resu11)
        txt2 = clearTxt(resu21)
        txt3 = clearTxt(resu31)
        if(txt1.isnumeric()==True) and (txt2.isnumeric()==False) and (txt3.isnumeric()==False):
            return txt1,resu12
        elif(txt1.isnumeric()==False) and (txt2.isnumeric()==True) and (txt3.isnumeric()==False):
            return txt2,resu22
        elif(txt1.isnumeric()==True) and (txt2.isnumeric()==True) and (len(str(txt1))==7) and (txt3.isnumeric()==False):
            return txt1,resu12
        elif(txt1.isnumeric()==True) and (txt2.isnumeric()==True) and (len(str(txt2))==7) and (txt3.isnumeric()==False):
            return txt2,resu22
        elif(txt1.isnumeric()==False) and (txt2.isnumeric()==False) and (txt3.isnumeric()==False):
            return "",0
        elif(txt1.isnumeric()==True) and (txt2.isnumeric()==False) and (txt3.isnumeric()==True) and (len(txt1)==7) and (len(txt3) !=7):
            return txt1,resu12
        elif(txt1.isnumeric()==False) and (txt2.isnumeric()==True) and (txt3.isnumeric()==True) and (len(txt2)==7) and (len(txt3) !=7):
            return txt2,resu22
        else:
            return "",0
    elif(len(result)==4):
        return "",0
    elif(len(result)==5):
        return "",0
    else:
        return "",0
    
def getCertIdByEasyOCR(imgSou):
    try:
        print("getCertIdByEasyOCR")
        #wei,hei, _ = imgSou.shape
        hei,wei, _ = imgSou.shape   # ต้องแก้ เพราะ openimage ด้วย opencv ทำให้ shape จะเป็น hei กับ wei ถ้า openimage ด้วย PIL shape จะเป็น wei,hei
        if(hei>1500):       #รูปscan 90
            if(wei>=1500) and (wei<1600):
                return "0",0     #น่าจะเป็นใบยา staff note
            if(wei>=1900) and (wei<1980):
                return "0",0     #น่าจะเป็นใบยา staff note
            if(hei>3100):   #A4 แน่ๆ
                x1=2110
                y1=200
                x2=x1+weiTemNO +150
                y2=y1+heiTemNO+110
                result = readerEasyOCR.readtext(imgSou[y1:y2, x1:x2])
                txt, thou = getCertIdByResult(result)
                if(txt.isnumeric()==True):
                    return txt,thou
                else:
                    img_cw_180 = cv2.rotate(imgSou, cv2.ROTATE_180) #ลอง rotation180 ดู
                    x1=2100
                    y1=1850+180
                    x2=wei-10
                    y2=y1+heiTemNO+110
                    #imgccc = img_cw_180[y1:y2, x1:x2]
                    #cv2.imwrite("img_cw_180.jpg",img_cw_180)
                    #cv2.imwrite("ccc.jpg",img_cw_180[y1:y2, x1:x2])
                    result = readerEasyOCR.readtext(img_cw_180[y1:y2, x1:x2])
                    txt, thou = getCertIdByResult(result)
                    if(txt.isnumeric()==True):
                        return txt,thou
                    else:
                        img_cw_90WISE = cv2.rotate(img_cw_180, cv2.ROTATE_180)
                        result = readerEasyOCR.readtext(img_cw_180[y1:y2, x1:x2])
                        txt, thou = getCertIdByResult(result)
                        if(txt.isnumeric()==True):
                            return txt,thou
                        else:
                            return "",0
            else:
                x1=2100
                y1=200
                x2=x1+weiTemNO +170
                y2=y1+heiTemNO+110
                #hei,wei, _ = imgSou.shape   #wei = x
                #step first scan ถูกต้อง
                if((x2-wei)<200):
                    result = readerEasyOCR.readtext(imgSou[y1:y2, x1:x2])
                    txt,thou = getCertIdByResult(result)
                    if(txt.isnumeric()==True) and (len(txt)==7):
                        return txt,thou
                    else:
                        return "",0
                if(x2<=wei):
                    #imgccc = imgSou[y1:y2, x1:x2]
                    #cv2.imwrite("ccc.jpg",imgccc)
                    result = readerEasyOCR.readtext(imgSou[y1:y2, x1:x2])
                    txt,thou = getCertIdByResult(result)
                    if(txt.isnumeric()==True):
                        if(int(txt)<10000):
                            x1=2010
                            y1=200
                            x2=wei-10
                            y2=y1+heiTemNO+110
                            imgccc = imgSou[y1:y2, x1:x2]
                            #cv2.imwrite("ccc.jpg",imgccc)
                            result = readerEasyOCR.readtext(imgSou[y1:y2, x1:x2])
                            txt,thou = getCertIdByResult(result)
                        return txt,thou
                    else:
                        img_cw_180 = cv2.rotate(imgSou, cv2.ROTATE_180)
                        result = readerEasyOCR.readtext(img_cw_180[y1:y2, x1:x2])
                        txt, thou = getCertIdByResult(result)
                        if(txt.isnumeric()==True):
                            return txt,thou
                        else:
                            return "",0
            img_cw_90WISE = cv2.rotate(imgSou, cv2.ROTATE_90_CLOCKWISE)
            hei90,wei90, _ = img_cw_90WISE.shape
            x1=2120
            y1=200
            x2=wei90 - 10
            y2=y1+heiTemNO+110
            #imgccc = imgSou[y1:y2, x1:x2]
            #cv2.imwrite("img_cw_90WISE.jpg",img_cw_90WISE)
            #cv2.imwrite("ccc.jpg",img_cw_90WISE[y1:y2, x1:x2])
            result = readerEasyOCR.readtext(img_cw_90WISE[y1:y2, x1:x2])
            if(len(result)>0):
                txt, thou = getCertIdByResult(result)
                if(txt.isnumeric()==True) and (len(txt)==7):
                    return txt,thou
                else:
                    img_cw_180 = cv2.rotate(img_cw_90WISE, cv2.ROTATE_180)
                    #imgccc = img_cw_180[y1:y2, x1:x2]
                    #cv2.imwrite("img_cw_180.jpg",img_cw_180)
                    result = readerEasyOCR.readtext(img_cw_180[y1:y2, x1:x2])
                    txt, thou = getCertIdByResult(result)
                    if(txt.isnumeric()==True):
                        return txt,thou
                    else:
                        img_cw_180WISE = cv2.rotate(img_cw_180, cv2.ROTATE_180)
                        result = readerEasyOCR.readtext(img_cw_180WISE[y1:y2, x1:x2])
                        txt, thou = getCertIdByResult(result)
                        if(txt.isnumeric()==True):
                            return txt,thou
                        else:
                            return "",0
            else:
                img_cw_180 = cv2.rotate(img_cw_90WISE, cv2.ROTATE_180)
                #cv2.imwrite("aaa.jpg",img_cw_180)
                #img_cw_180_bbb = img_cw_180[y1:y2, x1:x2]
                #cv2.imwrite("bbb.jpg",img_cw_180_bbb)
                result = readerEasyOCR.readtext(img_cw_180[y1:y2, x1:x2])
                txt, thou = getCertIdByResult(result)
                if(txt.isnumeric()==True):
                    return txt,thou
                else:
                    img_cw_90WISE = cv2.rotate(img_cw_180, cv2.ROTATE_180)
                    result = readerEasyOCR.readtext(img_cw_90WISE[y1:y2, x1:x2])
                    txt, thou = getCertIdByResult(result)
                    if(txt.isnumeric()==True):
                        return txt,thou
                    else:
                        return "",0
        res = cv2.matchTemplate(imgSou, temNO, cv2.TM_CCOEFF)
        min_val, max_val, min_loc, max_loc = cv2.minMaxLoc(res)
        top_letf = max_loc
        bottom_right = (top_letf[0] + weiTemNO + 250,top_letf[1] + heiTemNO)
        #imgerr = cv2.rectangle(imgSou, top_letf, bottom_right,colorBorder,2)
        y=top_letf[1]
        #x=top_letf[0]+weiTemNO+30
        x=top_letf[0]+weiTemNO
        h=heiTemNO
        w=weiTemNO+160
        #result = readerEasyOCR.readtext(imgSou[y:y+h, x:x+w])
        #we have (x1,y1) as the top-left vertex and (x2,y2) as the bottom-right
        #roi = im[y1:y2, x1:x2]
        #imgaaa = imgSou[top_letf[1]:top_letf[1]+heiTemNO, x:x+weiTemNO+160]
        #cv2.imwrite("aaa.jpg",imgaaa)
        #cv2.imwrite("bbb.jpg",imgerr)
        result = readerEasyOCR.readtext(imgSou[top_letf[1]:top_letf[1]+heiTemNO, x:x+weiTemNO+160])
        txt, thou = getCertIdByResult(result)
        if(txt.isnumeric()==True):
            return txt,thou
        else:
            x1=2120
            y1=200
            x2=x1+weiTemNO +150
            y2=y1+heiTemNO+90
            if(x2<=wei):
                #imgaaa = imgSou[y1:y2, x1:x2]
                #cv2.imwrite("aaa.jpg",imgaaa)
                result = readerEasyOCR.readtext(imgSou[y1:y2, x1:x2])
                txt, thou = getCertIdByResult(result)
                if(txt.isnumeric()==True):
                    return txt,thou
                else:
                    return "",0
            else:
                return "",0
    except Exception as ex:
        print("getCertIdByEasyOCR ")
        print(ex)
        x1=2130
        y1=200
        x2=x1+weiTemNO +150
        y2=y1+heiTemNO+80
        #imgccc = imgSou[y1:y2, x1:x2]
        #cv2.imwrite("ccc.jpg",imgccc)
        #if(imgccc.size==0):
        #    return ""
        hei,wei, _ = imgSou.shape
        if(wei>x2) and (hei>300):
            result = readerEasyOCR.readtext(imgSou[y1:y2, x1:x2])
            if(len(result)==0):
                return "",0
            else:
                txt = str(result[0][1])
                txt = clearTxt(txt)
                if(txt.isnumeric()==True):
                    return txt,0
                else:
                    return "",0
        else:
            return "",0
def getCertIdByEasyOCR1(imgSou):
    try:
        print("getCertIdByEasyOCR1")
        #wei,hei, _ = imgSou.shape
        #cv2.imwrite("aaa.jpg",imgSou)
        hei,wei, _ = imgSou.shape   # ต้องแก้ เพราะ openimage ด้วย opencv ทำให้ shape จะเป็น hei กับ wei ถ้า openimage ด้วย PIL shape จะเป็น wei,hei
        if(hei>2000):
            if(hei>3000):   #A4 แน่ๆ
                x1=2120
                y1=200
                x2=x1+weiTemNO +150
                y2=y1+heiTemNO+110
                result = readerEasyOCR.readtext(imgSou[y1:y2, x1:x2])
                txt, thou = getCertIdByResult(result)
                if(txt.isnumeric()==True):
                    return txt,thou
            img_cw_90WISE = cv2.rotate(imgSou, cv2.ROTATE_90_CLOCKWISE)
            #cv2.imwrite("aaa.jpg",img_cw_90WISE)
            x1=2110
            y1=200
            x2=x1+weiTemNO +180
            y2=y1+heiTemNO+90
            result = readerEasyOCR.readtext(img_cw_90WISE[y1:y2, x1:x2])
            txt, thou = getCertIdByResult(result)
            if(txt.isnumeric()==True):
                return txt,thou
            else:
                img_cw_180 = cv2.rotate(img_cw_90WISE, cv2.ROTATE_180)
                result = readerEasyOCR.readtext(img_cw_180[y1:y2, x1:x2])
                txt, thou = getCertIdByResult(result)
                if(txt.isnumeric()==True):
                    return txt,thou
                else:
                    img_cw_180 = cv2.rotate(img_cw_90WISE, cv2.ROTATE_180)
                    result = readerEasyOCR.readtext(img_cw_180[y1:y2, x1:x2])
                    txt, thou = getCertIdByResult(result)
                    if(txt.isnumeric()==True):
                        return txt,thou
                    else:
                        img_cw_180 = cv2.rotate(img_cw_90WISE, cv2.ROTATE_180)
                        #cv2.imwrite("aaa.jpg",img_cw_180)
                        #img_cw_180_bbb = img_cw_180[y1:y2, x1:x2]
                        #cv2.imwrite("bbb.jpg",img_cw_180_bbb)
                        result = readerEasyOCR.readtext(img_cw_180[y1:y2, x1:x2])
                        txt, thou = getCertIdByResult(result)
                        if(txt.isnumeric()==True):
                            return txt,thou
                        else:
                            img_cw_90WISE = cv2.rotate(img_cw_180, cv2.ROTATE_180)
                            result = readerEasyOCR.readtext(img_cw_180[y1:y2, x1:x2])
                            txt, thou = getCertIdByResult(result)
                            if(txt.isnumeric()==True):
                                return txt,thou
        #ลองทำใหม่
        x1=2110
        y1=200
        x2=x1+weiTemNO +180
        y2=y1+heiTemNO+90
        #imgccc = imgSou[y1:y2, x1:x2]
        #cv2.imwrite("ccc.jpg",imgccc)
        result = readerEasyOCR.readtext(imgSou[y1:y2, x1:x2])
        txt, thou = getCertIdByResult(result)
        if(txt.isnumeric()==True):
            return txt,thou
        else:
            x1=2100
            y1=200
            x2=x1+weiTemNO +150
            y2=y1+heiTemNO+80
            #imgccc = imgSou[y1:y2, x1:x2]
            #cv2.imwrite("ccc.jpg",imgccc)
            result = readerEasyOCR.readtext(imgSou[y1:y2, x1:x2])
            resu1 = result[0][1]
            resu2 = result[0][2]
            if(float(resu2)<0.8):
                x1=2140
                result = readerEasyOCR.readtext(imgSou[y1:y2, x1:x2])
            txt = str(result[0][1])
            txt = clearTxt(txt)
            if(txt.isnumeric()==True):
                return txt,resu2
            else:
                return "",0
        
    except Exception as ex:
        print(" getCertIdByEasyOCR1 ")
        print(ex)
        hei,wei, _ = imgSou.shape   #wei = x
        if(wei<2000):   # เป็นรูปแนวตั้ง
            return "",0
        x1=2110
        y1=200
        x2=x1+weiTemNO +180
        y2=y1+heiTemNO+80
        imgccc = imgSou[y1:y2, x1:x2]
        #cv2.imwrite("ccc.jpg",imgccc)
        #if(imgccc.size==0):
        #    return "",0
        result = readerEasyOCR.readtext(imgccc)
        if(len(result)==0):
            return "",0
        else:
            txt = str(result[0][1])
            txt = clearTxt(txt)
            if(txt.isnumeric()==True):
                return txt, result[0][2]
            else:
                return "",0
def getCertIdByCvMatchTemplateError(imgSou):
    txt = ""
    try:
        img2 = imgSou.copy()
        res = cv2.matchTemplate(imgSou, temNO, cv2.TM_CCOEFF)
        min_val, max_val, min_loc, max_loc = cv2.minMaxLoc(res)
        top_letf = max_loc
        bottom_right = (top_letf[0] + weiTemNO + 250,top_letf[1] + heiTemNO)
        #cv2.rectangle(img2, top_letf, bottom_right,colorBorder,2)
        txt = pytesseract.image_to_string(cv2.cvtColor(imgSou[top_letf[1]:top_letf[1]+weiTemNO, top_letf[0]+100:top_letf[0]+heiTemNO+220], cv2.COLOR_BGR2GRAY),lang='eng')
        if(txt.find(" ")!=-1):
            txt = txt.replace(" ", "")
        if(txt.find(" ")!=-1):
            txt = txt.replace(" ", "")
        if(txt.isnumeric()==False):
            txt = ""
            x1=2130
            y1=200
            x2=x1+weiTemNO +150
            y2=y1+heiTemNO+80
            txt = pytesseract.image_to_string(cv2.cvtColor(imgSou[top_letf[1]:top_letf[1]+weiTemNO, top_letf[0]+100:top_letf[0]+heiTemNO+220], cv2.COLOR_BGR2GRAY),lang='eng')
            #imgccc = img[y1:y2, x1:x2]
        #if(txt.find(".")!=-1):
        #print(".....txt.find(.)***** " +str(txt.find(".")))
        if(txt.find(".")==7):
            txt = txt[0:7]
        certi_id = int(txt)
    except Exception as ex:
        print("getCertIdByCvMatchTemplateError")
        print(ex)
        now = datetime.now()
        startdate = now.strftime("%Y-%m-%d, %H:%M:%S")
        timestamp = datetime.timestamp(now)
        timestamp = str(timestamp).replace(".", "_")
        filename = timestamp+".jpg"
        cv2.imwrite(path_cert_med+filename, img2)
    return txt
def getCertIdByCvMatchTemplate(imgSou):
    txt = ""
    err=""
    try:
        #img2 = imgSou.copy()
        res = cv2.matchTemplate(imgSou, temNO, cv2.TM_CCOEFF)
        min_val, max_val, min_loc, max_loc = cv2.minMaxLoc(res)
        top_letf = max_loc
        bottom_right = (top_letf[0] + weiTemNO + 250,top_letf[1] + heiTemNO)
        #cv2.rectangle(img2, top_letf, bottom_right,color,2)
        #imgCrop = imgSou[top_letf[1]:top_letf[1]+weiTemNO, top_letf[0]+100:top_letf[0]+heiTemNO+250]
        #cv2.imwrite("eee.jpg", imgCrop)
        #threshold = 0.8
        #loc = np.where( res >= threshold)
        #if(res<threshold):
        #    print("res<threshold")
        #txt = pytesseract.image_to_string(cv2.cvtColor(imgCrop, cv2.COLOR_BGR2GRAY),lang='eng')
        #txt = pytesseract.image_to_string(cv2.cvtColor(imgSou[top_letf[1]:top_letf[1]+weiTemNO, top_letf[0]+100:top_letf[0]+heiTemNO+250], cv2.COLOR_BGR2GRAY),lang='eng')
        err="01"
        txt = pytesseract.image_to_string(imgSou[top_letf[1]:top_letf[1]+weiTemNO, top_letf[0]+100:top_letf[0]+heiTemNO+250],lang='eng')
        #print("****getCertIdByCvMatchTemplate*****")
        #txt = str(txt)
        #print(type(txt))
        #print(len(txt))
        if(txt.find(" ")!=-1):
            txt = txt.replace(" ", "")
        if(txt.find(" ")!=-1):
            txt = txt.replace(" ", "")
        #if(txt.find(".")!=-1):
        #print(".....txt.find(.)***** " +str(txt.find(".")))
        err="011"
        if(txt.find(".")==7):
            txt = txt[0:7]
        txt = txt.replace("\n","")
        err="012"
        if(txt.isnumeric()==False):
            img1 = imgSou
            err="013"
            height, width = img1.shape[:2]
            if(height>width):
                #รูปภาพตั้ง
                rotated_90_clockwise = cv2.rotate(imgSou, cv2.ROTATE_90_CLOCKWISE)
                rotated_90_counterclockwise = cv2.rotate(imgSou, cv2.ROTATE_90_COUNTERCLOCKWISE)
                img1 = rotated_90_clockwise
            txt = ""
            x1=2100
            y1=200
            x2=x1+weiTemNO +180
            y2=y1+heiTemNO+80
            #imgccc = imgSou[y1:y2, x1:x2]
            err="02"
            if x1 >= 0 and y1 >= 0 and x2 <= width and y2 <= height:
                # ตัดรูปและแปลงเป็นข้อความ
                gray = cv2.cvtColor(img1[y1:y2, x1:x2], cv2.COLOR_BGR2GRAY)
                # ทำ threshold
                _, threshold = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
                txt = pytesseract.image_to_string(threshold, lang='eng')
                if(txt.isnumeric()==False):
                    img1 = rotated_90_counterclockwise
                    gray = cv2.cvtColor(img1[y1:y2, x1:x2], cv2.COLOR_BGR2GRAY)
                    _, threshold = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
                    txt = pytesseract.image_to_string(threshold, lang='eng')
            else:
                print("พิกัดที่กำหนดอยู่นอกขอบเขตของรูปภาพ x1 "+str(x1)+" y1 "+str(y1)+" x2 "+str(x2)+" y2 "+str(y2)+" width "+str(width)+" height "+str(height))
            chk = txt.split(' ')
            if(len(chk)>1):
                txt1 = chk[1]
                txt = txt1
            txt = clearTxt(txt)
        #print(type(txt))
        #print(txt)
        #print("****getCertIdByCvMatchTemplate*****")
        #decodeby="crop pytesseract"
    except Exception as ex:
        print("getCertIdByCvMatchTemplate "+err)
        print(ex)
    return txt
def clearTxt(text):
    txt1=text
    if(len(text)>0):
        txtsplit = text.split(' ') #ตรวจสอบว่า  เช่น 1 0023232
        if(len(txtsplit)>1):
            if(len(txtsplit[0]) + len(txtsplit[1]) == 7):
                txt1 = str(txtsplit[0])+str(txtsplit[1])
            elif(len(txtsplit)==3):
                txtsplit1 = txtsplit[0]
                txtsplit2 = txtsplit[1]
                txtsplit3 = txtsplit[2]
                if(txtsplit1.isnumeric() == True and txtsplit2.isnumeric() == False and txtsplit3.isnumeric() == False):
                    txt1 = txtsplit1
                elif(txtsplit1.isnumeric() == False and txtsplit2.isnumeric() == True and txtsplit3.isnumeric() == False):
                    txt1 = txtsplit2
                elif(txtsplit1.isnumeric() == False and txtsplit2.isnumeric() == False and txtsplit3.isnumeric() == True):
                    txt1 = txtsplit3
            else:
                txt1 = txtsplit[1]
        txt1 = txt1.replace("ที่","")
        txt1 = txt1.replace("ที","")
        txt1 = txt1.replace("ท","")
        txt1 = txt1.replace(" ", "")
        txt1 = txt1.replace("`", "")
        txt1 = txt1.replace("ก", "")
        txt1 = txt1.replace("่", "")
        txt1 = txt1.replace("ี", "")
        txt1 = txt1.replace(")", "")
        txt1 = txt1.replace("\n","")
        txt1 = txt1.replace("ว", "")
        txt1 = txt1.replace("/", "")
        txt1 = txt1.replace("\\", "")
        txt1 = txt1.replace("เ", "")
        txt1 = txt1.replace("|", "")
        txt1 = txt1.replace(".", "")
    return txt1
def saveImgError(img):
    print("saveImgError")
    #img2 = img.copy()
    #res = cv2.matchTemplate(img2, temNO, cv2.TM_CCOEFF)
    #min_val, max_val, min_loc, max_loc = cv2.minMaxLoc(res)
    #top_letf = max_loc
    #bottom_right = (top_letf[0] + weiTemNO + 250,top_letf[1] + heiTemNO)
    #img2 = cv2.rectangle(img2, top_letf, bottom_right,colorBorder,2)
    if not os.path.exists(path_cert_med_err):
        os.makedirs(path_cert_med_err)
    now = datetime.now()
    startdate = now.strftime("%Y-%m-%d, %H:%M:%S")
    #timestamp = datetime.timestamp(now)
    #timestamp = str(timestamp).replace(".", "_")
    filename = "tem-staff1_"+startdate+".jpg"
    if(cv2.imwrite(path_cert_med_err+"\\"+filename, img)):
        return True
    else:
        return False
def checkCertMedCorrect(imgSou):
    print("checkCertMedCorrect")
    try:
        resCerrStf1 = cv2.matchTemplate(imgSou, temCerrStf1, cv2.TM_CCOEFF)
        min_valStf, max_valStf, min_locStf, max_locStf = cv2.minMaxLoc(resCerrStf1)
        top_letfStf = max_locStf
        if(len(max_locStf)>0):
            if(saveImgError(imgSou)):
                return True
            else:
                return False
        else:
            return False
    except Exception as ex:
        print("checkCertMedCorrect ")
        print(ex)
def qrCodeMertMed():
    try:
        conn = pyodbc.connect('Driver={SQL Server};Server=172.25.10.5;Database=bn5_scan;UID=sa;PWD=;Trusted_Connection=no;')
        ftpDocScan = ftplib.FTP('172.25.10.3')
        ftpDocScan.login("imagescanupload", "imagescanupload")
        ftpDocScan.cwd(folder_ftp)
        
        ftp = ftplib.FTP('172.25.10.3')
        ftp.login("u_cert_med", "u_cert_med")
        ftp.cwd('cert_med')
        files = ftp.nlst()
    except ftplib.error_perm as resp:
        if str(resp) == "550 No files found":
            print("No files in this directory")
        else:
            raise
    detector = cv2.QRCodeDetector()
    #decodeby=""
    now = datetime.now()
    
    print(now.strftime("%y-%m-%d %H:%M:%S") +" found file "+str(len(files)))
    #print("cv2 version "+cv2.__version__)
    for ftpfilename in files:
        try:
            chkCerrCert = True
            print(ftpfilename)
            g_ftpfilename = ftpfilename
            #now = datetime.now()
            decodeby=""
            #timestamp = datetime.timestamp(now)
            #print(datetime.datetime.now(time_zone=th))
            #timestamp = str(timestamp).replace(".", "_")
            
            #filename = path_cert_med+timestamp+'.jpg'
            #ftp.retrbinary("RETR " + ftpfilename ,open(path_cert_med+ftpfilename, 'wb').write)
            #myfile=os.BytesIO()
            #ftp.retrbinary("RETR " + ftpfilename ,myfile.write)
            #myfile.seek(0)
            #ftp.retrbinary("RETR " + ftpfilename ,open(path_cert_med+ftpfilename, 'wb').write)
            #img = cv2.imread(path_cert_med+ftpfilename,cv2.IMREAD_COLOR)
            
            myfile=BytesIO()
            ftp.retrbinary("RETR " + ftpfilename ,myfile.write)
            myfile.seek(0)
            if(ftpfilename=="Thumbs.db"):
                ftp.delete(ftpfilename)
                continue
            if(ftpfilename.find(".pdf")>=0):
                ftp.delete(ftpfilename)
                continue
            img = np.asarray(bytearray(myfile.getvalue()), dtype="uint8")
            #step 1. qrcode จาก cv2 ก่อน
            img = cv2.imdecode(img, cv2.IMREAD_COLOR)
            imgheight, imgwidth, channels = img.shape
            #if(imgheight>3000): #A4
            #    x1=0
            #    y1=0
            #    x2=2486
             #   y2=1732
             #   crop_img = img[y1:y2, x1:x2]
            #    img = crop_img
            #imgGRAY = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY) # ทำการแปลงสี
            #txt = pytesseract.image_to_string(img,lang='eng+tha')
            #findtxt = txt.find('เล')
            #print(findtxt)
            #certmedid = txt[findtxt:findtxt+20]
            #print(certmedid)
            
            if(ftpfilename=="W5706.jpg"):
                bbbb = ""
                gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
                blurred = cv2.GaussianBlur(gray, (5, 5,), 0)
                edged = cv2.Canny(blurred, 75, 200)
                cnts = cv2.findContours(edged.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
                #cnts = imutils.grab_contours(cnts)
                cv2.imwrite("Edged.jpg", edged)
                #cv2.imshow("Edged", edged)
                #cv2.waitKey(0)
                print("************************************"+ftpfilename)
            certmedid=""
            extension = os.path.splitext(ftpfilename)[1][1:]
            if(extension=="db"):
                print("extension db")
                continue
            #if(checkCertMedCorrect(img)):
            #    ftp.delete(ftpfilename)
            #    continue
            try:
                #data, vertices_array, binary_qrcode = detector.detectAndDecode(cv2.cvtColor(img, cv2.COLOR_BGR2RGB))
                
                data, vertices_array, binary_qrcode = detector.detectAndDecode(img)
                if vertices_array is not None:
                    #print("*******************data********************")
                    #print(data)
                    #print("*******************data********************")
                    decodeby="cv2 " + cv2.__version__
                    certmedid = data[len(data)-7:len(data)]
                    #if(bool(re.search(r"\s", certmedid))):
                    if(certmedid.find(" ")!=-1):
                        certmedid = certmedid.replace(" ", "")
                    if(ftpfilename=="OPD14656.jpg"):
                        print(certmedid)
                        print(len(certmedid))
                    if(len(data)==0):
                        print('len=0')
                        certmedideasyocr, thou = getCertIdByEasyOCR(img)
                        if(thou < 0.5):
                            certmedid = getCertIdByCvMatchTemplate(img)
                        else:
                            certmedid = certmedideasyocr
                        print(certmedid)
                        decodeby="[cv -> tesseract getCertIdByCvMatchTemplate]"
                        if(len(certmedid)==0):
                            certmedid,_ = getCertIdByEasyOCR(img)
                            print(certmedid)
                            if(certmedid.isnumeric()):
                                decodeby="[cv -> tesseract getCertIdByEasyOCR]"
                        elif(len(certmedid)==8):
                            certmedid,_ = getCertIdByEasyOCR(img)
                        #if(len(certmedid)<=0):
                        #txt = pytesseract.image_to_string(img,lang='eng+tha')
                        #print("*******************data********************")
                        #print(txt)
                        #print("*******************data********************")
                        #findtxt = txt.find('00')
                        
                        #certmedid = txt[findtxt:findtxt+7]
                    elif(len(data)<=6):
                        certmedideasyocr, thou = getCertIdByEasyOCR(img)
                else:
                    print("There was some error")
                    certmedid = getCertIdByCvMatchTemplate(img)
                    if(len(certmedid)==0):
                        certmedid,_ = getCertIdByEasyOCR(img)
                        if(len(str(certmedid))==0):
                            txt = pytesseract.image_to_string(img,lang='eng+tha')
                            findtxt = txt.find('00')
                            #print("findtxt "+str(findtxt))
                            decodeby="tesseract"
                            certmedid = txt[findtxt:findtxt+7]
                            if(len(certmedid)==0):
                                output = decode(img)
                                decodeby="pyzbar.pyzbar"
                                for symbol in output:
                                    #print(symbol.data)
                                    new_string = str(symbol.data)
                                    new_string1 = new_string.replace("'", "")
                                    new_string2 = new_string.replace("'", "")
                                    #print(new_string2)
                    elif(len(certmedid)<=6):
                        certmedid,_ = getCertIdByEasyOCR(img)
                    else:
                        if(certmedid.isnumeric()==False):
                            certmedid,_ = getCertIdByEasyOCR(img)
                            decodeby="easyocr"
            except ValueError as err:
                 print("qrCodeMertMed ")
                 print(err)
                 certmedid = getCertIdByCvMatchTemplate(img)
            #print(decodeby)
            #print(str(certmedid) +" "+decodeby)
            certmedid = str(certmedid)
            if(certmedid.isnumeric()==True):
                if(int(certmedid)>9000000):
                    certmedid,_ = getCertIdByEasyOCR(img)
                elif(int(certmedid)>1000000):       #ตอนนี้ ค่ายังไม่ถึง 1000000 แน่นอน อาจเป็นการ ตีค่าผิด เพราะการ crop รูป อาจไม่สมบรูณ์
                    certmedid = "0"+certmedid[1:len(certmedid)]
                elif(len(certmedid)<=6):
                    certmedid,_ = getCertIdByEasyOCR(img)
            if(certmedid=="660007"):
                certmedid="0007555"
            if(len(certmedid)==4):
                certmedid = "555000"+str(certmedid)
            elif(len(certmedid)==5):
                certmedid = "55500"+str(certmedid)
            elif(len(certmedid)==6):
                certmedid = "5550"+str(certmedid)
            elif(len(certmedid)==1):
                certmedid = "555"+str(certmedid)
            else:
                certmedid = "555"+str(certmedid)
            try:
                certi_id = int(certmedid)
            except ValueError as err:
                print("error int(certmedid)0")
                #saveImgError(img)
                img_cw_180 = cv2.rotate(img, cv2.ROTATE_180)
                #imgccc = imgSou[y1:y2, x1:x2]
                cv2.imwrite("ccc.jpg",img_cw_180)
                certmedid = getCertIdByCvMatchTemplate(img_cw_180)
                if(len(certmedid)==0):
                    img_cw_90WISE = cv2.rotate(img_cw_180, cv2.ROTATE_90_CLOCKWISE)
                    cv2.imwrite("ddd.jpg",img_cw_90WISE)
                    certmedid = getCertIdByCvMatchTemplate(img_cw_90WISE)
                    if(certmedid.isnumeric()==False):
                        img_cw_90CLOCKWISE = cv2.rotate(img_cw_90WISE, cv2.ROTATE_90_COUNTERCLOCKWISE)
                        cv2.imwrite("eee.jpg",img_cw_90CLOCKWISE)
                        certmedid = getCertIdByCvMatchTemplate(img_cw_90CLOCKWISE)
                    if(len(certmedid)>0):
                        certmedid = "555"+str(certmedid)
            #    reader = zxing.BarCodeReader()
            #    barcode = reader.decode(img)
            #    for result in zxing.read_barcodes(img):
            #        print(result.text)
            #    print(barcode.raw)
            try:
                certi_id = int(certmedid)
            except ValueError as err:
                print("error int(certmedid)1")
                #if(ftpfilename=="img772116.jpg"):
                certmedid,_ = getCertIdByEasyOCR(img)        # ลองเปลี่ยนเป็น grayScale เพราะใน google ให้เป็น grayScale
                #certmedid = getCertIdByEasyOCR(imgGRAY)
                certmedid = "555"+str(certmedid)
                #saveImgError(img)
            try:
                certmedid = certmedid.replace("\n","")
                if(ftpfilename=="img772116.jpg"):
                    print("+++++++++++++++++++++++++++++++ "+certmedid)
                    saveImgError(img)
                certi_id = int(certmedid)
                if(certi_id==555):
                    img_cw_90WISE = cv2.rotate(img, cv2.ROTATE_90_CLOCKWISE)
                    certmedid = getCertIdByCvMatchTemplate(img_cw_90WISE)
                    if(certmedid.isnumeric()==False):
                        img_cw_90CLOCKWISE = cv2.rotate(img, cv2.ROTATE_90_COUNTERCLOCKWISE)
                        certmedid = getCertIdByCvMatchTemplate(img_cw_90CLOCKWISE)
                    if(certmedid.isnumeric()==False):
                        img_cw_180 = cv2.rotate(img, cv2.ROTATE_180)
                        certmedid = getCertIdByCvMatchTemplate(img_cw_180)
                        certmedid = clearTxt(certmedid)
                        if(certmedid.isnumeric()==False):
                            certmedid,_ = getCertIdByEasyOCR(img_cw_180)
                    if(certmedid==555):
                        chkCerrCert = False
                        #continue
                    certi_id = int(certmedid)
                    certmedid = "555"+str(certmedid)
                #print("certi_id "+str(certi_id))
                if(certi_id<=0):
                    print("if(certi_id<=0)")
                    chkCerrCert = False
                    #continue
                if(certi_id<=5550):
                    print("if(certi_id<=0)")
                    chkCerrCert = False
                if(str(certi_id) == "000000"):
                    print("str(certi_id)")
                    chkCerrCert = False
                    #continue
                if(chkCerrCert == False):
                    if(checkCertMedCorrect(img)):
                        ftp.delete(ftpfilename)
                    continue
                sql = "Select isnull(certi.hn,''),isnull(certi.pre_no,''),isnull(certi.status_ipd,''),isnull(certi.visit_date,''),isnull(certi.an,''),isnull(certi.doc_scan_id,''), isnull(dsc.vn,''),isnull(dsc.ml_fm,'') "
                sql = sql + "From t_medical_certificate certi left join doc_scan dsc on certi.doc_scan_id = dsc.doc_scan_id Where certi.certi_id = '"+str(certmedid)+"' "
                #print(sql)
                cur = conn.cursor()
                curUpdate = conn.cursor()
                curSelect = conn.cursor()
                cur.execute(sql)
                myresult = cur.fetchall()
                if(len(myresult)<=0):
                    if(checkCertMedCorrect(img)):
                        ftp.delete(ftpfilename)
                for res in myresult:
                    status_ipd = str(res[2])
                    hn = str(res[0])
                    pre_no = str(res[1])
                    visit_date = str(res[3])
                    an = str(res[4])
                    doc_scan_id = str(res[5])
                    vn = str(res[6])
                    ml_fm = str(res[7])
                    print("Found hn "+hn+" pre_no "+pre_no+" visit_date "+visit_date+" doc_scan_id "+doc_scan_id+" "+str(certmedid) +" "+decodeby)
                    #void doc_scan อันเก่า
                    sql = "update doc_scan set active = '3', status_conv1 = 'cert_med "+certmedid+"' where doc_scan_id = '"+doc_scan_id +"' and status_record = '4' "
                    curUpdate.execute(sql)
                    #insert ของใหม่
                    #folder_ftp = "images2019_1"
                    #doc_group_id = "1100000007"
                    #doc_group_sub_id = "1200000030"
                    image_path = ""
                    hostFTP = "ftp://172.25.10.3"
                    sql = "Insert into doc_scan (doc_group_id,row_no,host_ftp,image_path,hn, vn, an, pre_no, visit_date, an_date, active "
                    sql = sql+", remark,date_create, user_create, doc_group_sub_id,status_ipd,folder_ftp, status_ml, ml_fm, row_cnt, status_version, status_record,sort1) "
                    sql = sql+"Values('"+doc_group_id+"','1','"+hostFTP+"','"+image_path+"','"+hn+"','"+vn+"','','"+pre_no+"','"+visit_date+"','','1' "
                    sql = sql+",'JPG',convert(varchar,getdate(),23),'','"+doc_group_sub_id+"','O','"+folder_ftp+"','2','FM-MED-001','1','','4','1')"
                    curUpdate.execute(sql)
                    #select 
                    sql = "Select top 1 doc_scan_id From doc_scan Where hn = '"+hn+"' and pre_no = '"+pre_no +"' and visit_date = '"+visit_date+"' and active = '1'  Order By doc_scan_id desc"
                    curSelect.execute(sql)
                    myresultSelect = curSelect.fetchall()
                    for resSelect in myresultSelect:
                        doc_scan_id_new = str(resSelect[0])
                        #image_path = txtHn.Text.Replace("/", "-") + "//" + txtHn.Text.Replace("/", "-") + "-" + re + ext;
                        
                        ext = ftpfilename.split(".")[-1]
                        image_path = hn+"//"+hn+"-"+doc_scan_id_new+"."+ext
                        sql = "Update doc_scan set image_path = '" + image_path +"' Where doc_scan_id = '"+doc_scan_id_new +"' "
                        curUpdate.execute(sql)
                        
                        #fileUpload = open(path_cert_med+ftpfilename, "rb")myfile
                        #ftpDocScan.storbinary("STOR "+image_path, fileUpload)
                        ftpDocScan.storbinary("STOR "+image_path, myfile)
                        #fileUpload.close()
                        #os.remove(path_cert_med+ftpfilename)
                        ftp.delete(ftpfilename)
                        
                        sql = "Update t_medical_certificate set doc_scan_id = '"+doc_scan_id_new+"', status_scan_upload = '1' where certi_id = '"+certmedid+"'"
                        #print(sql)
                        curUpdate.execute(sql)
                        conn.commit()
                curUpdate.close()
                curSelect.close()
                cur.close()
            except ValueError as err:
                print("qrCodeMertMed ")
                print(err)
                if(checkCertMedCorrect(img)):
                    ftp.delete(ftpfilename)
            #print(txt)
        except ftplib.error_perm as resp:
            print(resp)
    ftp.quit()
    ftpDocScan.quit()
    conn.close()

def checkCertMed():
    while True:
        print('start Check Cert Med')
        #now = datetime.now()
        #print(now.strftime("%y-%m-%d %H-%M"))
        qrCodeMertMed()
        print('end Check Cert Med')
        time.sleep(120)

checkCertMed()
#for filename in os.listdir(path_cert_med):
#    print(filename)
    #image = cv2.imread(os.path.join(folder_path, file))
    #txt = pytesseract.image_to_string(cv2.cvtColor(IMG[YY-int(HH): YY, xxx:int(xxx)+int(WW)], cv2.COLOR_BGR2GRAY),lang='eng tha')

def prepare_logo_samples():
    base_samples = {
        'original': cv2.imread('logo_original.jpg'),
        'grayscale': cv2.imread('logo_original.jpg', 0),
        'binary': cv2.threshold(cv2.imread('logo_original.jpg', 0), 
                              128, 255, cv2.THRESH_BINARY)[1]
    }
    return base_samples

def detect_hospital_logo(image):
    # อ่านภาพ template ของโลโก้
    logo_template = cv2.imread('hospital_logo_template.jpg', 0)
    
    # แปลงภาพเป็น grayscale
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    
    # ใช้ Template Matching
    result = cv2.matchTemplate(gray, logo_template, cv2.TM_CCOEFF_NORMED)
    
    # กำหนดค่า threshold
    threshold = 0.8
    loc = np.where(result >= threshold)
    
    if len(loc[0]) > 0:
        return True
    return False

def detect_hospital_logo_features(image):
    # ใช้ SIFT หรือ ORB สำหรับ Feature Detection
    sift = cv2.SIFT_create()
    
    # อ่านภาพ template
    template = cv2.imread('hospital_logo_template.jpg', 0)
    kp1, des1 = sift.detectAndCompute(template, None)
    
    # แปลงภาพที่ต้องการตรวจสอบเป็น grayscale
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    kp2, des2 = sift.detectAndCompute(gray, None)
    
    # ใช้ FLANN matcher
    FLANN_INDEX_KDTREE = 1
    index_params = dict(algorithm=FLANN_INDEX_KDTREE, trees=5)
    search_params = dict(checks=50)
    flann = cv2.FlannBasedMatcher(index_params, search_params)
    
    matches = flann.knnMatch(des1, des2, k=2)
    
    # Apply ratio test
    good_matches = []
    for m, n in matches:
        if m.distance < 0.7 * n.distance:
            good_matches.append(m)
    
    # ถ้าอย่างน้อย 2 วิธีเจอโลโก้ ถือว่าเจอโลโก้
    if len(good_matches) > 10:
        return True
    return False

def detect_hospital_logo_region(image):
    # กำหนดพื้นที่ที่น่าจะมีโลโก้ (บริเวณมุมบนซ้าย)
    height, width = image.shape[:2]
    roi_x = int(width * 0.05)  # 5% จากขอบซ้าย
    roi_y = int(height * 0.05)  # 5% จากขอบบน
    roi_w = int(width * 0.2)    # กว้าง 20% ของความกว้างเอกสาร
    roi_h = int(height * 0.2)   # สูง 20% ของความสูงเอกสาร
    
    # ตัดเฉพาะส่วน ROI
    roi = image[roi_y:roi_y+roi_h, roi_x:roi_x+roi_w]
    
    # ทำ Feature Detection บน ROI
    return detect_hospital_logo_features(roi)

def enhanced_logo_detection(image):
    # ปรับปรุงคุณภาพภาพ
    # 1. ปรับความสว่าง/คอนทราสต์
    lab = cv2.cvtColor(image, cv2.COLOR_BGR2LAB)
    l, a, b = cv2.split(lab)
    clahe = cv2.createCLAHE(clipLimit=3.0, tileGridSize=(8,8))
    cl = clahe.apply(l)
    enhanced = cv2.merge((cl,a,b))
    enhanced = cv2.cvtColor(enhanced, cv2.COLOR_LAB2BGR)
    
    # 2. ลด Noise
    denoised = cv2.fastNlMeansDenoisingColored(enhanced)
    
    # 3. ตรวจจับโลโก้ด้วยหลายวิธี
    results = {
        'template_matching': detect_hospital_logo(denoised),
        'feature_detection': detect_hospital_logo_features(denoised),
        'region_based': detect_hospital_logo_region(denoised)
    }
    
    # ถ้าอย่างน้อย 2 วิธีเจอโลโก้ ถือว่าเจอ
    return sum(results.values()) >= 2

def create_rotated_samples(logo_img):
    rotated_samples = []
    # หมุนทีละ 5 องศา ในช่วง -15 ถึง +15 องศา
    for angle in range(-15, 16, 5):
        height, width = logo_img.shape[:2]
        center = (width/2, height/2)
        rotation_matrix = cv2.getRotationMatrix2D(center, angle, 1.0)
        rotated = cv2.warpAffine(logo_img, rotation_matrix, (width, height))
        rotated_samples.append({
            'angle': angle,
            'image': rotated
        })
    return rotated_samples

def create_brightness_samples(logo_img):
    brightness_samples = []
    # ปรับความสว่างในช่วง -30 ถึง +30
    for beta in range(-30, 31, 10):
        adjusted = cv2.convertScaleAbs(logo_img, alpha=1, beta=beta)
        brightness_samples.append({
            'brightness': beta,
            'image': adjusted
        })
    return brightness_samples

def create_scaled_samples(logo_img):
    scaled_samples = []
    # ปรับขนาดตั้งแต่ 80% ถึง 120%
    for scale in range(80, 121, 10):
        scale_factor = scale / 100.0
        width = int(logo_img.shape[1] * scale_factor)
        height = int(logo_img.shape[0] * scale_factor)
        scaled = cv2.resize(logo_img, (width, height))
        scaled_samples.append({
            'scale': scale,
            'image': scaled
        })
    return scaled_samples

def manage_logo_samples():
    # สร้างโฟลเดอร์สำหรับเก็บตัวอย่าง
    sample_dirs = {
        'original': 'samples/original/',
        'rotated': 'samples/rotated/',
        'brightness': 'samples/brightness/',
        'scaled': 'samples/scaled/'
    }
    
    for dir_path in sample_dirs.values():
        os.makedirs(dir_path, exist_ok=True)
    
    # อ่านโลโก้ต้นฉบับ
    original_logo = cv2.imread('logo_original.jpg')
    
    # สร้างและบันทึกตัวอย่างต่างๆ
    # 1. ตัวอย่างที่หมุน
    rotated = create_rotated_samples(original_logo)
    for sample in rotated:
        filename = f"rotated_{sample['angle']}.jpg"
        cv2.imwrite(os.path.join(sample_dirs['rotated'], filename), 
                   sample['image'])
    
    # 2. ตัวอย่างที่ปรับความสว่าง
    brightness = create_brightness_samples(original_logo)
    for sample in brightness:
        filename = f"brightness_{sample['brightness']}.jpg"
        cv2.imwrite(os.path.join(sample_dirs['brightness'], filename), 
                   sample['image'])
    
    # 3. ตัวอย่างที่ปรับขนาด
    scaled = create_scaled_samples(original_logo)
    for sample in scaled:
        filename = f"scaled_{sample['scale']}.jpg"
        cv2.imwrite(os.path.join(sample_dirs['scaled'], filename), 
                   sample['image'])

def create_logo_database():
    database = {
        'metadata': {
            'creation_date': datetime.now().strftime("%Y-%m-%d"),
            'version': '1.0',
            'hospital_name': 'BANGNA5 GENERAL HOSPITAL'
        },
        'samples': []
    }
    
    # เพิ่มตัวอย่างทั้งหมดลงในฐานข้อมูล
    for root, dirs, files in os.walk('samples/'):
        for file in files:
            if file.endswith('.jpg'):
                sample_path = os.path.join(root, file)
                sample_type = root.split('/')[-1]
                
                # อ่านภาพและคำนวณ features
                img = cv2.imread(sample_path)
                sift = cv2.SIFT_create()
                keypoints, descriptors = sift.detectAndCompute(img, None)
                
                database['samples'].append({
                    'filename': file,
                    'type': sample_type,
                    'path': sample_path,
                    'descriptors': descriptors.tolist() if descriptors is not None else None,
                    'shape': img.shape
                })
    
    # บันทึกฐานข้อมูล
    with open('logo_database.json', 'w') as f:
        json.dump(database, f)
