using bangna_hospital.control;
using bangna_hospital.object1;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public partial class FrmLisLink : Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEditBig, ffB;
        Color bg, fc, color;
        Label lbLoading;
        C1FlexGrid grfReq, grfItms, grfRes;
        int colReqDate = 0, colReqTime = 1, colReqNo = 2, colReqHn = 3, colReqPttName = 4, colReqDepName = 5, colReqPaidName = 6, colReqDtrName = 7, colReqVsDate=8, colReqPreno=9, colReqdepid=10, colReqsts=11;

        Boolean pageLoad = false;
        Timer time1;
        public FrmLisLink(BangnaControl bc)
        {
            this.bc = bc;
            InitializeComponent();
            initConfig();
        }
        private void initConfig()
        {
            pageLoad = true;
            time1 = new Timer();
            time1.Enabled = true;
            time1.Interval = bc.timerCheckLabOut*1000;
            time1.Tick += Time1_Tick;

            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 3, FontStyle.Bold);
            fEditBig = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 7, FontStyle.Regular);
            lbLoading = new Label();
            lbLoading.Font = new Font("AngsanaUPC", 24, FontStyle.Bold);
            lbLoading.BackColor = Color.WhiteSmoke;
            lbLoading.ForeColor = Color.Black;
            lbLoading.AutoSize = false;
            lbLoading.Size = new Size(300, 60);
            lbLoading.Location = new Point(50, 50);
            this.Controls.Add(lbLoading);

            initGrfRequest();
            initGrfItems();

            //var tim = new System.Threading.Timer()
            lb1.Text = "format date " + System.DateTime.Now.ToString()+" year "+System.DateTime.Now.Year.ToString();
            pageLoad = false;
        }

        private void Time1_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setLinkLIS();
            
            setGrfLabReq();
        }
        
        private void setLinkLIS()
        {
            time1.Enabled = false;
            showLbLoading();
            setLbLoading("Loading ...");
            DataTable dt = new DataTable();
            DataTable dtres = new DataTable();
            int i = 0;
            String err = "";
            dt = bc.bcDB.labT01DB.selectNoSendByStatusLis();
            try
            {
                String datetime1 = "";
                datetime1 = DateTime.Now.Year.ToString() + DateTime.Now.ToString("MMddHHmmss");
                rb1.Text = "request = "+dt.Rows.Count.ToString();
                //update master ก่อนส่ง request
                bc.bcDB.labreqDB.updateLabMaster();

                foreach (DataRow drow in dt.Rows)
                {
                    err = "00";
                    DataTable dtreq = new DataTable();
                    String reqdate = "", reqtime="", reqno="", docno="", linkLIS="", hn="", prefix="", firstname="", lastname="", dob="", sex="";
                    String opdtype = "", dept="", an="", vn="", pttType="", addate="", disc="", deptname="";
                    String newrpca = "", appectreqdatetime="", dtrcode="", userreceivedatetime="";
                    if (drow["status_lis"].ToString().Equals("0"))
                    {
                        hn = drow["MNC_HN_NO"].ToString();
                        prefix = drow["MNC_PFIX_DSC"].ToString();
                        firstname = drow["MNC_FNAME_T"].ToString();
                        lastname = drow["MNC_LNAME_T"].ToString();
                        dob = drow["MNC_BDAY"].ToString();
                        sex = drow["MNC_SEX"].ToString();
                        err = "01";
                        an = drow["MNC_AN_NO"].ToString();
                        opdtype = an.Equals("") ? "O" : "I";
                        if (an.Equals("0"))
                        {
                            opdtype = "O";
                            dept = drow["MNC_REQ_DEP"].ToString();
                            deptname = bc.bcDB.pm32DB.selectDeptOPD(dept);
                        }
                        else
                        {
                            opdtype = "I";
                            dept = drow["MNC_wd_no"].ToString();
                            deptname = bc.bcDB.pm32DB.selectDeptIPD(dept);
                        }
                        dtrcode = drow["MNC_ORD_DOT"].ToString()+"^"+ drow["MNC_USR_FULL"].ToString();
                        dept = dept + "^" + deptname;
                        err = "02";
                        an += "."+drow["MNC_AN_yr"].ToString();
                        vn = drow["MNC_VN_NO"].ToString()+"."+ drow["MNC_VN_SEQ"].ToString()+"."+ drow["MNC_VN_SUM"].ToString();

                        reqdate = drow["MNC_REQ_DAT"].ToString();
                        reqtime = "0000"+drow["MNC_REQ_TIM"].ToString();
                        reqtime = reqtime.Substring(reqtime.Length - 4);
                        reqtime = reqtime.Substring(0, 2) + ":" + reqtime.Substring(reqtime.Length - 2);
                        docno = "0000"+drow["MNC_REQ_NO"].ToString();
                        docno = docno.Substring(docno.Length - 4);
                        reqno = drow["MNC_REQ_DAT"].ToString().Replace("-","").Replace("-", "") + docno;
                        userreceivedatetime = drow["MNC_EMPC_CD"].ToString()+"^"+ drow["MNC_USR_FULL_usr"].ToString();
                        err = "03";
                        newrpca = "nw";
                        dtreq = bc.bcDB.labT02DB.selectItemNoSendByStatusLis(drow["MNC_REQ_DAT"].ToString(), drow["MNC_REQ_NO"].ToString());
                        LisLabRequest labreq = new LisLabRequest();
                        labreq.lab_request_data = "";
                        labreq.lab_request_datetime = reqdate+" "+reqtime;
                        labreq.lab_request_id = "";
                        labreq.lab_request_lon = reqno;
                        labreq.lab_request_msg_type = "1";
                        labreq.lab_request_receive = "";        // สรุปให้เป็น ค่าว่าง ไม่ต้องใส่ค่าอะไร โปรแกรม LIS จะดึงตามสถานะว่าง
                        labreq.lab_request_receive_data = "";
                        labreq.lab_request_receive_datetime = "";
                        labreq.hn = hn;
                        labreq.req_date = reqdate;
                        labreq.req_no = drow["MNC_REQ_NO"].ToString();
                        appectreqdatetime = reqdate + " " + reqtime;
                        err = "04";
                        linkLIS = labreq.genReq(datetime1, hn, prefix, firstname, lastname,"", dob, sex, opdtype, dept, an, vn, pttType, addate, disc, newrpca, reqno, appectreqdatetime, dtrcode, dept, dtreq, userreceivedatetime);
                        err = "05";
                        labreq.lab_request_data = linkLIS;
                        String chk = bc.bcDB.labreqDB.insert(labreq, "");
                        if (chk.Equals("1"))
                        {
                            int chk1 = 0, chk2=0;
                            if (dtreq.Rows.Count > 0)
                            {
                                foreach (DataRow dreq in dtreq.Rows)
                                {
                                    String re1 = bc.bcDB.labT02DB.updateStatusLinkLIS(dreq["mnc_req_yr"].ToString(), dreq["MNC_REQ_NO"].ToString(), dreq["MNC_REQ_DAT"].ToString(), dreq["MNC_LB_CD"].ToString());
                                    if (int.TryParse(re1, out chk1))
                                    {
                                        chk2 += chk1;
                                        if (chk2 == dtreq.Rows.Count)
                                        {
                                            String re2 = bc.bcDB.labT01DB.updateStatusLinkLIS(dreq["mnc_req_yr"].ToString(), dreq["MNC_REQ_NO"].ToString(), dreq["MNC_REQ_DAT"].ToString());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                String re1 = bc.bcDB.labT01DB.updateStatusLinkLIS(drow["mnc_req_yr"].ToString(), drow["MNC_REQ_NO"].ToString(), drow["MNC_REQ_DAT"].ToString());
                            }
                            
                        }
                        dtreq.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmLisLink setLinkLIS request  " + err +" "+ ex.Message);
            }
            finally
            {
                dt.Dispose();
                
                hideLbLoading();
            }
            dtres = bc.bcDB.labresDB.selectNoSendByStatusLis();
            try
            {
                new LogWriter("d", "FrmLisLink setLinkLIS result  " + dtres.Rows.Count.ToString());
                rb2.Text = "result = " + dtres.Rows.Count.ToString();
                foreach (DataRow drow in dtres.Rows)
                {
                    err = "00";
                    List<LinkLabOBX> lOBX;
                    lOBX = bc.bcDB.labresDB.labres.genResult(drow);
                    err = "01";
                    new LogWriter("e", "FrmLisLink setLinkLIS lOBX   " + lOBX.Count);
                    if (lOBX.Count > 0)
                    {
                        DataTable dtreq = new DataTable();
                        err = "02";
                        foreach (LinkLabOBX obx in lOBX)
                        {
                            //  1. check request
                            //  2. insert result
                            String reqno = "", reqdate = "", reqNo = "", reqyear = "";
                            reqno = obx.reqno;
                            err = "021";
                            reqdate = obx.reqdate.Substring(0, 8);
                            err = "022";
                            reqdate = reqdate.Substring(0, 4) + "-" + reqdate.Substring(4, 2) + "-" + reqdate.Substring(reqdate.Length - 2);
                            err = "023";
                            reqyear = obx.reqdate.Substring(0, 4);
                            err = "024";
                            reqNo = obx.reqno.Replace(obx.reqno.Substring(0, 8), "");
                            err = "03";
                            dtreq = bc.bcDB.labT02DB.selectLabReq("", reqdate, reqNo, obx.lab_code, obx.lab_sub_code);
                            err = "04";
                            if (dtreq.Rows.Count > 0)
                            {
                                String approver = "", reporter = "", time="";
                                int time1 = 0, chk=0;
                                String[] userapprove = obx.OBX_IdNum.Split('^');
                                if (userapprove.Length > 1)
                                {
                                    approver = userapprove[0];
                                    reporter = userapprove[1];
                                    //approver = approver.Length > 5 ? approver.Substring(0,5) : approver;
                                    //reporter = reporter.Length > 5 ? reporter.Substring(0,5) : reporter;
                                }
                                time = DateTime.Now.ToString("HHmm");
                                if(!int.TryParse(time, out time1))
                                {
                                    time1 = 0;
                                }
                                err = "05";
                                //bc.bcDB.labresDB.updateHnReqDate(obx.hn,reqdate, reqNo, drow["lab_result_id"].ToString());
                                LabT05 labt05 = new LabT05();
                                labt05.MNC_REQ_YR = (int.Parse(reqyear)+543).ToString();
                                labt05.MNC_REQ_DAT = reqdate;
                                labt05.MNC_REQ_NO = int.Parse(reqNo).ToString();
                                labt05.MNC_LB_CD = obx.lab_code;
                                //labt05.MNC_LB_RES_CD = obx.lab_sub_code;
                                labt05.MNC_LB_RES_CD = obx.running;         //   แก้เพราะ เปิดดู source code และ ข้อมูลใน table lab_t05.mnc_lb_res_cd เป็น running
                                labt05.MNC_RES = dtreq.Rows[0]["MNC_LB_RES"].ToString();      //name lab sub
                                labt05.MNC_RES_MAX = dtreq.Rows[0]["MNC_LB_RES_MAX"].ToString();
                                labt05.MNC_RES_MIN = dtreq.Rows[0]["MNC_LB_RES_MIN"].ToString();
                                //labt05.MNC_RES_UNT = obx.OBX_Units;
                                labt05.MNC_RES_UNT = dtreq.Rows[0]["MNC_RES_UNT"].ToString();
                                labt05.MNC_RES_VALUE = obx.OBX_Obsvalue;    // value result
                                err = "06";
                                labt05.MNC_STS = "";            // transalate
                                labt05.MNC_LB_USR = approver;
                                labt05.MNC_LAB_PRN = dtreq.Rows[0]["MNC_LAB_PRN"].ToString();        //  sequene
                                labt05.MNC_LB_ACT = reporter;
                                labt05.MNC_LB_RES = dtreq.Rows[0]["MNC_LB_RES"].ToString()+" [ " + dtreq.Rows[0]["MNC_LB_RES_MIN"].ToString()+" - " + dtreq.Rows[0]["MNC_LB_RES_MAX"].ToString()+" ]";             // standard
                                labt05.MNC_STAMP_DAT = DateTime.Now.Year+"-"+DateTime.Now.ToString("MM-dd");
                                labt05.MNC_STAMP_TIM = time1.ToString();
                                //labt05.MNC_LB_RES = "";
                                err = "07";
                                bc.bcDB.labT05DB.deleteLabT05(labt05.MNC_REQ_YR, labt05.MNC_REQ_NO, labt05.MNC_REQ_DAT, labt05.MNC_LB_CD, labt05.MNC_LB_RES_CD);
                                String re = bc.bcDB.labT05DB.insertLabT05(labt05, "");
                                if(int.TryParse(re, out chk))
                                {
                                    re = bc.bcDB.labT02DB.updateResultDateLinkLIS(labt05.MNC_REQ_YR,labt05.MNC_REQ_NO, labt05.MNC_REQ_DAT, labt05.MNC_LB_CD, obx.OBX_DateTimeObs, reporter, approver);
                                    re = bc.bcDB.labresDB.updateStatusReceive(obx.hn, reqdate, reqNo, drow["lab_result_id"].ToString());
                                }
                                else
                                {
                                    new LogWriter("e", "FrmLisLink setLinkLIS insertLabT05 ไม่มี error   " + reqdate + " " + reqNo + " " + obx.lab_code + " " + obx.lab_sub_code);
                                }
                            }
                            else
                            {
                                new LogWriter("e", "FrmLisLink setLinkLIS select ใน lab_t02 ไม่พบ   " + reqdate + " " + reqNo+" "+ obx.lab_code+" "+ obx.lab_sub_code);
                            }
                        }
                        dtreq.Dispose();
                    }
                }
            }
            catch(Exception ex1)
            {
                new LogWriter("e", "FrmLisLink setLinkLIS reqult  " + err + " " + ex1.Message);
            }
            finally
            {
                dtres.Dispose();
                time1.Enabled = true;
            }
        }
        private void setGrfLabReq()
        {
            showLbLoading();
            setLbLoading("Loading ...");
            String vsdate = "";
            vsdate = DateTime.Now.Year + DateTime.Now.ToString("-MM-dd");
            lbYear.Text = vsdate;
            DataTable dt = new DataTable();
            grfReq.Rows.Count = 1;
            int i = 0;
            dt = bc.bcDB.labT01DB.selectNoSendByStatusLis(vsdate);
            grfReq.Rows.Count = dt.Rows.Count + 1;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    grfReq[i, colReqDate] = row1["MNC_REQ_DAT"].ToString();
                    grfReq[i, colReqTime] = row1["MNC_REQ_TIM"].ToString();
                    grfReq[i, colReqNo] = row1["MNC_REQ_NO"].ToString();
                    grfReq[i, colReqHn] = row1["MNC_HN_NO"].ToString();
                    grfReq[i, colReqPttName] = row1["MNC_PFIX_DSC"].ToString()+" "+ row1["MNC_FNAME_T"].ToString()+" "+ row1["MNC_LNAME_T"].ToString();
                    //grfReq[i, colReqDepName] = row1["MNC_MD_DEP_DSC"].ToString();
                    grfReq[i, colReqPaidName] = row1["MNC_FN_TYP_CD"].ToString();
                    grfReq[i, colReqDtrName] = row1["MNC_USR_FULL"].ToString();
                    grfReq[i, colReqVsDate] = row1["mnc_date"].ToString();
                    grfReq[i, colReqPreno] = row1["MNC_PRE_NO"].ToString();
                    grfReq[i, colReqdepid] = row1["MNC_REQ_DEP"].ToString();
                    //grfReq[i, colReqdepid] = row1["MNC_REQ_DEP"].ToString();
                    grfReq[i, colReqsts] = row1["MNC_REQ_STS"].ToString();
                    if (row1["status_lis"].ToString().Equals("1"))
                    {
                        grfReq.Rows[i].StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                    }
                    else if (row1["status_lis"].ToString().Equals("2"))
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmLisLink setGrfLabReq grfReq " + ex.Message);
            }
            finally
            {
                dt.Dispose();
                hideLbLoading();
            }
        }
        private void initGrfRequest()
        {
            grfReq = new C1FlexGrid();
            grfReq.Font = fEdit;
            grfReq.Dock = System.Windows.Forms.DockStyle.Fill;
            grfReq.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfReq.Cols[colReqPreno].Visible = false;
            grfReq.Cols[colReqVsDate].Visible = false;
            grfReq.Rows.Count = 1;
            grfReq.Cols.Count = 12;
            grfReq.Cols[colReqDate].Caption = "reqdate";
            grfReq.Cols[colReqTime].Caption = "time";
            grfReq.Cols[colReqNo].Caption = "reqno";
            grfReq.Cols[colReqHn].Caption = "hn";
            grfReq.Cols[colReqPttName].Caption = "patient";
            grfReq.Cols[colReqDepName].Caption = "depname";
            grfReq.Cols[colReqPaidName].Caption = "paidname";
            grfReq.Cols[colReqDtrName].Caption = "doctor";
            grfReq.Cols[colReqVsDate].Caption = "vsdate";
            grfReq.Cols[colReqPreno].Caption = "preno";
            grfReq.Cols[colReqdepid].Caption = "dep id";
            grfReq.Cols[colReqsts].Caption = "Reqsts";
            grfReq.Cols[colReqDate].Width = 100;
            grfReq.Cols[colReqTime].Width = 90;
            grfReq.Cols[colReqNo].Width = 60;
            grfReq.Cols[colReqHn].Width = 90;
            grfReq.Cols[colReqPttName].Width = 260;
            grfReq.Cols[colReqDepName].Width = 120;
            grfReq.Cols[colReqPaidName].Width = 60;
            grfReq.Cols[colReqDtrName].Width = 260;
            grfReq.Cols[colReqVsDate].Width = 60;
            grfReq.Cols[colReqPreno].Width = 60;
            grfReq.Cols[colReqdepid].Width = 60;
            grfReq.Name = "grfReq";
            ContextMenu menuGwOrder = new ContextMenu();
            menuGwOrder.MenuItems.Add("ต้องการ Print ", new EventHandler(ContextMenu_xray_infinitt));
            menuGwOrder.MenuItems.Add("ต้องการ Export PDF ", new EventHandler(ContextMenu_xray_infinitt));
            grfReq.ContextMenu = menuGwOrder;
            pnRequest.Controls.Add(grfReq);
        }
        private void ContextMenu_xray_infinitt(object sender, System.EventArgs e)
        {
            if (grfReq == null) return;
            if (grfReq.Row <= 1) return;
            if (grfReq.Col <= 0) return;
            String address = "";            
        }
        private void initGrfItems()
        {
            grfItms = new C1FlexGrid();
            grfItms.Font = fEdit;
            grfItms.Dock = System.Windows.Forms.DockStyle.Fill;
            grfItms.Location = new System.Drawing.Point(0, 0);
            //grfOrder.Rows[0].Visible = false;
            //grfOrder.Cols[0].Visible = false;
            grfItms.Cols[colReqPreno].Visible = false;
            grfItms.Cols[colReqVsDate].Visible = false;
            grfItms.Rows.Count = 1;
            grfItms.Cols.Count = 11;
            grfItms.Cols[colReqDate].Caption = "ชื่อ";
            grfItms.Cols[colReqTime].Caption = "-";
            grfItms.Cols[colReqNo].Caption = "QTY";
            grfItms.Cols[colReqHn].Caption = "QTY";
            grfItms.Cols[colReqPttName].Caption = "QTY";
            grfItms.Cols[colReqDepName].Caption = "QTY";
            grfItms.Cols[colReqPaidName].Caption = "QTY";
            grfItms.Cols[colReqDtrName].Caption = "QTY";
            grfItms.Cols[colReqVsDate].Caption = "vsdate";
            grfItms.Cols[colReqPreno].Caption = "preno";
            grfItms.Cols[colReqdepid].Caption = "dep id";
            grfItms.Cols[colReqDate].Width = 100;
            grfItms.Cols[colReqTime].Width = 90;
            grfItms.Cols[colReqNo].Width = 60;
            grfItms.Cols[colReqHn].Width = 90;
            grfItms.Cols[colReqPttName].Width = 260;
            grfItms.Cols[colReqDepName].Width = 260;
            grfItms.Cols[colReqPaidName].Width = 260;
            grfItms.Cols[colReqDtrName].Width = 260;
            grfItms.Cols[colReqVsDate].Width = 60;
            grfItms.Cols[colReqPreno].Width = 60;
            grfItms.Cols[colReqdepid].Width = 60;
            grfItms.Name = "grfRes";
            ContextMenu menuGwOrder = new ContextMenu();
            menuGwOrder.MenuItems.Add("ต้องการ Print ", new EventHandler(ContextMenu_xray_infinitt));
            menuGwOrder.MenuItems.Add("ต้องการ Export PDF ", new EventHandler(ContextMenu_xray_infinitt));
            grfItms.ContextMenu = menuGwOrder;
            pnItems.Controls.Add(grfItms);
        }
        private void setLbLoading(String txt)
        {
            lbLoading.Text = txt;
            Application.DoEvents();
        }
        private void showLbLoading()
        {
            lbLoading.Show();
            lbLoading.BringToFront();
            Application.DoEvents();
        }
        private void hideLbLoading()
        {
            lbLoading.Hide();
            Application.DoEvents();
        }
        private void FrmLisLink_Load(object sender, EventArgs e)
        {
            pageLoad = true;
            this.Text = "Last Update 2023-04-10 insert lab_t05 update lab_master ";
            showLbLoading();
            setLbLoading("Loading ...");
            //rb1.Text = "format date "+System.DateTime.Now.ToString();

            hideLbLoading();
            pageLoad = false;
            lb3.Text = bc.iniC.hostDBMainHIS + " "+bc.iniC.nameDBMainHIS+" timer "+ bc.timerCheckLabOut;
        }
    }
}
