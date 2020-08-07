using bangna_hospital.control;
using bangna_hospital.object1;
using bangna_hospital.Properties;
using C1.C1Pdf;
using C1.Util.DX.Direct2D;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.C1SplitContainer;
using C1.Win.C1SuperTooltip;
using C1.Win.C1Themes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmOrOperativeNote:Form
    {
        BangnaControl bc;
        Font fEdit, fEditB, fEdit1, fEdit1B;

        Label lbTitle, lbDept, lbWard, lbDtrId, lbDtrName, lbPttid, lbPttName, lbDateOperative, lbTimeInsion, lbTimeFinish, lbTotalTime, lbSurgeon, lbSurgeon1, lbSurgeon2, lbSurgeon3, lbSurgeon4;
        Label lbSurgeonName1, lbSurgeonName2, lbSurgeonName3, lbSurgeonName4, lbAssistant, lbAssistant1, lbAssistant2, lbAssistant3, lbAssistant4, lbAssistantName1, lbAssistantName2, lbAssistantName3, lbAssistantName4;
        Label lbScrubNurse, lbScrubNurse1, lbScrubNurseName1, lbScrubNurse2, lbScrubNurseName2, lbScrubNurse3, lbScrubNurseName3, lbScrubNurse4, lbScrubNurseName4;
        Label lbCircuNurse, lbCircuNurse1, lbCircuNurseName1, lbCircuNurse2, lbCircuNurseName2, lbCircuNurse3, lbCircuNurseName3, lbCircuNurse4, lbCircuNurseName4;
        C1TextBox txtDept, txtWard, txtDtrId, txtDtrname, txtTimeInsion, txtTimeFinish, txtTotalTime, txtSurgeon1, txtSurgeon2, txtSurgeon3, txtSurgeon4, txtAssistant1, txtAssistant2, txtAssistant3, txtAssistant4;
        C1TextBox txtScrubNurse1, txtScrubNurse2, txtScrubNurse3, txtScrubNurse4;
        C1TextBox txtCircuNurse1, txtCircuNurse2, txtCircuNurse3, txtCircuNurse4;
        Label lbPerfusionist, lbPerfusionist1, lbPerfusionistName1, lbPerfusionist2, lbPerfusionistName2;
        C1TextBox txtPerfusionist1, txtPerfusionist2;
        Label lbAnesthetist, lbAnesthetist1, lbAnesthetistName1;
        C1TextBox txtAnesthetist1;
        Label lbAnesthetistAssist, lbAnesthetistAssist1, lbAnesthetistAssistName1, lbAnesthetistAssist2, lbAnesthetistAssistName2;
        C1TextBox txtAnesthetistAssist1, txtAnesthetistAssist2;
        Label lbTimeAnthe, lbTimeFinishAnthe, lbTotalTimeAnthe;
        C1TextBox txtTimeAnthe, txtTimeFinishAnthe, txtTotalTimeAnthe;
        C1SplitContainer sCOper, sCFinding;
        C1SplitterPanel scOperView, scOperAdd, scFinding, scPrecidures;
        C1FlexGrid grfView;

        Label lbAnesthetistTechni;
        C1TextBox txtAnesthetistTechni1;
        Label lbPreOperation;
        C1TextBox txtPreOperation;
        Label lbPostOperation;
        C1TextBox txtPostOperation;
        Label lbOperation1, lbOperation2, lbOperation3, lbOperation4;
        C1TextBox txtOperation1, txtOperation2, txtOperation3, txtOperation4;

        Label lbComplication, lbEstimateBloodlose, lbTissueBiopsy, lbSpecialSpecimen, lbEstimateBloodloseUnit;
        RadioButton chkComplicationNo, chkComplicationYes, chkTissueBiopsyNo, chkTissueBiopsyYes;
        C1TextBox txtComplication, txtEstimateBloodlose, txtTissueBiopsy, txtSpecialSpecimen;

        Label lbFinding;
        C1TextBox txtFinding;

        Label lbGrfHnSearch;
        C1TextBox txtGrfHnSearch;

        C1Button btnPttSearch, btnSave, btnPrint, btnNew;
        C1DateEdit txtDateOperative;
        RadioButton chkOPD, chkIPD;
        Panel pnOperative, pnProcidures, pnLeft, pnLeftTop, pnLeftBotton, pnComplication, pnTissue;
        C1ThemeController theme1;
        Patient ptt;

        ContextMenu menuAnesTech, menuOrDeptOpeNote, menuDept;
        OperativeNote operNote;

        C1SuperTooltip stt;
        C1SuperErrorProvider sep;
        C1.C1Pdf.C1PdfDocument _c1pdf;

        int rtfFinding = 0, rtfProcidures=0;
        String opernote_id = "";
        int colHn = 1, colName = 2, colAn = 3, colDateOper = 4, colDept = 5, colWard = 6, colAttending = 7, colID=8;

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Printer);
        public FrmOrOperativeNote(BangnaControl bc, String opernote_id)
        {
            this.bc = bc;
            this.opernote_id = opernote_id;
            initConfig();
        }
        private void initConfig()
        {
            fEdit = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Regular);
            fEditB = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize, FontStyle.Bold);
            fEdit1 = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Regular);
            fEdit1B = new Font(bc.iniC.grdViewFontName, bc.grdViewFontSize + 2, FontStyle.Bold);
            theme1 = new C1.Win.C1Themes.C1ThemeController();
            operNote = new OperativeNote();
            operNote = bc.bcDB.operNoteDB.setOperativeNote(operNote);
            stt = new C1SuperTooltip();
            sep = new C1SuperErrorProvider();
            _c1pdf = new C1.C1Pdf.C1PdfDocument();
            _c1pdf.DocumentInfo.Producer = "ComponentOne C1Pdf";
            _c1pdf.Security.AllowCopyContent = true;
            _c1pdf.Security.AllowEditAnnotations = true;
            _c1pdf.Security.AllowEditContent = true;
            _c1pdf.Security.AllowPrint = true;

            initCompoment();
            initGrfView();

            this.Load += FrmOrOperativeNote_Load;
            btnPttSearch.Click += BtnPttSearch_Click;
            txtDtrId.KeyUp += TxtDtrId_KeyUp;
            btnSave.Click += BtnSave_Click;
            btnPrint.Click += BtnPrint_Click;
            btnNew.Click += BtnNew_Click;

            //txtAnesthetistTechni1.KeyUp += TxtAnesthetistTechni1_KeyUp;
            txtSurgeon1.KeyUp += TxtSurgeon1_KeyUp;
            txtSurgeon2.KeyUp += TxtSurgeon2_KeyUp;
            //txtSurgeon3.KeyUp += TxtSurgeon3_KeyUp;
            //txtSurgeon4.KeyUp += TxtSurgeon4_KeyUp;
            txtAssistant1.KeyUp += TxtAssistant1_KeyUp;
            txtAssistant2.KeyUp += TxtAssistant2_KeyUp;
            //txtAssistant3.KeyUp += TxtAssistant3_KeyUp;
            //txtAssistant4.KeyUp += TxtAssistant4_KeyUp;
            txtScrubNurse1.KeyUp += TxtScrubNurse1_KeyUp;
            txtScrubNurse2.KeyUp += TxtScrubNurse2_KeyUp;
            //txtScrubNurse3.KeyUp += TxtScrubNurse3_KeyUp;
            //txtScrubNurse4.KeyUp += TxtScrubNurse4_KeyUp;
            txtCircuNurse1.KeyUp += TxtCircuNurse1_KeyUp;
            txtCircuNurse2.KeyUp += TxtCircuNurse2_KeyUp;
            //txtCircuNurse3.KeyUp += TxtCircuNurse3_KeyUp;
            //txtCircuNurse4.KeyUp += TxtCircuNurse4_KeyUp;
            txtPerfusionist1.KeyUp += TxtPerfusionist1_KeyUp;
            txtPerfusionist2.KeyUp += TxtPerfusionist2_KeyUp;
            txtAnesthetist1.KeyUp += TxtAnesthetist1_KeyUp1;
            txtAnesthetistAssist1.KeyUp += TxtAnesthetistAssist1_KeyUp;
            txtAnesthetistAssist2.KeyUp += TxtAnesthetistAssist2_KeyUp;
            txtGrfHnSearch.KeyUp += TxtGrfHnSearch_KeyUp;
            txtTimeInsion.KeyUp += TxtTimeInsion_KeyUp;
            txtTimeFinish.KeyUp += TxtTimeFinish_KeyUp;
            txtTotalTime.KeyUp += TxtTotalTime_KeyUp;
            txtTotalTimeAnthe.KeyUp += TxtTotalTimeAnthe_KeyUp;
            txtTimeAnthe.KeyUp += TxtTimeAnthe_KeyUp;
            txtTimeFinishAnthe.KeyUp += TxtTimeFinishAnthe_KeyUp;
            txtTimeAnthe.Enter += TxtTimeAnthe_Enter;
            txtPreOperation.KeyUp += TxtPreOperation_KeyUp;
            txtPostOperation.KeyUp += TxtPostOperation_KeyUp;
            txtOperation1.KeyUp += TxtOperation1_KeyUp;
            txtOperation2.KeyUp += TxtOperation2_KeyUp;
            txtOperation3.KeyUp += TxtOperation3_KeyUp;
            txtOperation4.KeyUp += TxtOperation4_KeyUp;

            txtDateOperative.Value = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
            setContextMenuAnesthesis();
            setContextMenuOrDepartment();
            setContextMenuDepartment();
            txtAnesthetistTechni1.ContextMenu = menuAnesTech;
            txtDept.ContextMenu = menuOrDeptOpeNote;
            txtWard.ContextMenu = menuDept;

            setControl();
        }

        private void TxtOperation4_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();

        }

        private void TxtOperation3_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperation4.SelectAll();
                txtOperation4.Focus();
            }
        }

        private void TxtOperation2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperation3.SelectAll();
                txtOperation3.Focus();
            }
        }

        private void TxtOperation1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperation2.SelectAll();
                txtOperation2.Focus();
            }
        }

        private void TxtPostOperation_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtOperation1.SelectAll();
                txtOperation1.Focus();
            }
        }

        private void TxtPreOperation_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPostOperation.SelectAll();
                txtPostOperation.Focus();
            }
        }

        private void TxtTimeAnthe_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtTimeAnthe.SelectAll();
        }

        private void TxtTimeFinishAnthe_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //TimeSpan chk = new TimeSpan();
                DateTime chk = new DateTime();
                String date = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
                if (DateTime.TryParse(date + " " + txtTimeFinishAnthe.Text.Trim(), out chk))
                {
                    DateTime chkstart = new DateTime();
                    DateTime chkend = new DateTime();
                    if (DateTime.TryParse(txtDateOperative.Text + " " + txtTimeAnthe.Text.Trim(), out chkstart))
                    {
                        if (DateTime.TryParse(txtDateOperative.Text + " " + txtTimeFinishAnthe.Text.Trim(), out chkend))
                        {
                            TimeSpan span = chkend.Subtract(chkstart);

                            txtTotalTimeAnthe.Value = span.TotalMinutes;
                        }
                    }
                    sep.Clear();
                    txtTotalTimeAnthe.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบtime");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtTimeAnthe_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //TimeSpan chk = new TimeSpan();
                DateTime chk = new DateTime();
                String date = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
                if (DateTime.TryParse(date + " " + txtTimeAnthe.Text.Trim(), out chk))
                {
                    sep.Clear();
                    txtTimeFinishAnthe.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบtime");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtTotalTimeAnthe_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtPreOperation.Focus();
            }
        }

        private void TxtTotalTime_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                txtSurgeon1.Focus();
            }
        }

        private void TxtTimeFinish_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //TimeSpan chk = new TimeSpan();
                DateTime chk = new DateTime();
                String date = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM-dd");
                if (DateTime.TryParse(date + " " + txtTimeFinish.Text.Trim(), out chk))
                {
                    DateTime chkstart = new DateTime();
                    DateTime chkend = new DateTime();
                    if (DateTime.TryParse(txtDateOperative.Text + " " + txtTimeInsion.Text.Trim(), out chkstart))
                    {
                        if (DateTime.TryParse(txtDateOperative.Text + " " + txtTimeFinish.Text.Trim(), out chkend))
                        {
                            TimeSpan span = chkend.Subtract(chkstart);
                            txtTotalTime.Value = span.TotalMinutes;
                        }
                    }
                    sep.Clear();
                    txtTotalTime.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบtime");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtTimeInsion_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                //TimeSpan chk = new TimeSpan();
                DateTime chk = new DateTime();
                String date = DateTime.Now.Year+"-"+ DateTime.Now.ToString("MM-dd");
                if(DateTime.TryParse(date+" "+txtTimeInsion.Text.Trim(), out chk))
                {
                    sep.Clear();
                    txtTimeFinish.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบtime");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            setControlNew();
            scOperAdd.SizeRatio = 100;
        }

        private void TxtGrfHnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                setGrHn();
                scOperAdd.SizeRatio = 65;
            }
            else
            {
                if (txtGrfHnSearch.Text.Length >= bc.txtSearchHnLenghtStart)
                {
                    setGrHn();
                    scOperAdd.SizeRatio = 65;
                }
            }
        }
        private void setGrHn()
        {
            //new Thread(() =>
            //{
            try
            {
                DataTable dt = new DataTable();
                String vn = "", preno = "", vsdate = "", an = "";

                Application.DoEvents();
                if (txtGrfHnSearch.Text.Length <= 0) return;
                dt = bc.bcDB.operNoteDB.selectByHn(txtGrfHnSearch.Text.Trim());
                //grfHn.Rows.Count = 1;
                //grfLab.Cols[colOrderId].Visible = false;
                grfView.Rows.Count = 1;
                grfView.Rows.Count = dt.Rows.Count + 1;
                grfView.Cols.Count = 9;
                grfView.Cols[colHn].Caption = "HN";
                grfView.Cols[colName].Caption = "Patient Name";
                grfView.Cols[colAn].Caption = "AN";
                grfView.Cols[colDateOper].Caption = "Date Operation";
                grfView.Cols[colDept].Caption = "Department";
                grfView.Cols[colWard].Caption = "Ward";
                grfView.Cols[colAttending].Caption = "Attending";

                grfView.Cols[colHn].Width = 90;
                grfView.Cols[colName].Width = 250;
                grfView.Cols[colAn].Width = 80;
                grfView.Cols[colDateOper].Width = 100;
                grfView.Cols[colDept].Width = 200;
                grfView.Cols[colWard].Width = 100;
                grfView.Cols[colAttending].Width = 200;

                int i = 0;
                decimal aaa = 0;
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    grfView[i, 0] = (i);
                    grfView[i, colID] = row1["operative_note_id"].ToString();
                    grfView[i, colHn] = row1["patient_hn"].ToString();
                    grfView[i, colName] = row1["patient_fullname"].ToString();
                    grfView[i, colAn] = row1["an"].ToString();
                    grfView[i, colDateOper] = row1["date_operation"].ToString();
                    grfView[i, colDept] = row1["dept_name"].ToString();
                    grfView[i, colWard] = row1["ward_name"].ToString();
                    grfView[i, colAttending] = row1["attending_stf_name"].ToString();

                    //row1[0] = (i - 2);
                }
                CellNoteManager mgr = new CellNoteManager(grfView);
                grfView.Cols[colID].Visible = false;
                grfView.Cols[colHn].AllowEditing = false;
                grfView.Cols[colName].AllowEditing = false;
                grfView.Cols[colAn].AllowEditing = false;
                grfView.Cols[colDateOper].AllowEditing = false;
                grfView.Cols[colDept].AllowEditing = false;
                grfView.Cols[colWard].AllowEditing = false;
                grfView.Cols[colAttending].AllowEditing = false;
            }
            catch (Exception ex)
            {
                new LogWriter("e", ex.Message);
            }

            //}).Start();
        }
        private void setControlNew()
        {
            ptt = new Patient();
            operNote = new OperativeNote();
            operNote = bc.bcDB.operNoteDB.setOperativeNote(operNote);

            txtDept.Value = "";
            txtWard.Value = "";
            lbDtrName.Text = "...";
            txtDtrId.Value = "";
            lbPttName.Text = "...";
            txtDateOperative.Value = "";
            txtTimeInsion.Value = "";
            txtTimeFinish.Value = "";
            txtTotalTime.Value = "";
            lbSurgeonName1.Text = "...";
            txtSurgeon1.Value = "";
            lbSurgeonName2.Text = "...";
            txtSurgeon2.Value = "";
            //lbSurgeonName3.Text = "...";
            //txtSurgeon3.Value = "";
            //lbSurgeonName4.Text = "...";
            //txtSurgeon4.Value = "";

            lbAssistantName1.Text = "...";
            txtAssistant1.Value = "";
            lbAssistantName2.Text = "...";
            txtAssistant2.Value = "";
            //lbAssistantName3.Text = "...";
            //txtAssistant3.Value = "";
            //lbAssistantName4.Text = "...";
            //txtAssistant4.Value = "";

            lbScrubNurseName1.Text = "...";
            txtScrubNurse1.Value = "";
            lbScrubNurseName2.Text = "...";
            txtScrubNurse2.Value = "";
            //lbScrubNurseName3.Text = "...";
            //txtScrubNurse3.Value = "";
            //lbScrubNurseName4.Text = "...";
            //txtScrubNurse4.Value = "";

            lbCircuNurseName1.Text = "...";
            txtCircuNurse1.Value = "";
            lbCircuNurseName2.Text = "...";
            txtCircuNurse2.Value = "";
            //lbCircuNurseName3.Text = "...";
            //txtCircuNurse3.Value = "";
            //lbCircuNurseName4.Text = "...";
            //txtCircuNurse4.Value = "";

            lbPerfusionistName1.Text = "...";
            txtPerfusionist1.Value = "";
            lbPerfusionistName2.Text = "...";
            txtPerfusionist2.Value = "";

            lbAnesthetistName1.Text = "...";
            txtAnesthetist1.Value = "";

            lbAnesthetistAssistName1.Text = "...";
            txtAnesthetistAssist1.Value = "";
            lbAnesthetistAssistName2.Text = "...";
            txtAnesthetistAssist2.Value = "";

            txtTimeAnthe.Value = "";
            txtTimeFinishAnthe.Value = "";
            txtTotalTimeAnthe.Value = "";

            txtAnesthetistTechni1.Value = "";
            txtPreOperation.Value = "";
            txtPostOperation.Value = "";
            txtOperation1.Value = "";
            txtOperation2.Value = "";
            txtOperation3.Value = "";
            txtOperation4.Value = "";

            txtComplication.Value = "";
            txtTissueBiopsy.Value = "";
            txtEstimateBloodlose.Value = "";
            txtSpecialSpecimen.Value = "";
            
            chkComplicationNo.Checked = false;
            chkComplicationYes.Checked = false;
            chkTissueBiopsyNo.Checked = false;
            chkTissueBiopsyYes.Checked = false;

            //txtFinding.Value = operNote.finding_1;
            setControlFrmDoctorDiag();
        }
        private void setControl()
        {
            if (opernote_id.Equals("")) return;
            operNote = bc.bcDB.operNoteDB.selectByPk(opernote_id);
            if (operNote.operative_note_id.Length == 0)
            {
                setControlNew();
                return;
            }

            ptt = bc.bcDB.pttDB.selectPatinet(operNote.patient_hn);
            bc.operative_note_precidures_1 = operNote.procidures_1;     // ต้องใส่ เพราะเวลา save รูปเมื่อแก้ไข จะได้ไหมหาย
            txtDept.Value = operNote.dept_name;
            txtWard.Value = operNote.ward_name;
            lbDtrName.Text = operNote.attending_stf_name;
            txtDtrId.Value = operNote.attending_stf_id;
            lbPttName.Text = ptt.Name + " Age " + ptt.AgeStringShort() + " An " + bc.sPtt.an;
            txtDateOperative.Value = operNote.date_operation;
            txtTimeInsion.Value = operNote.time_start;
            txtTimeFinish.Value = operNote.time_finish;
            txtTotalTime.Value = operNote.total_time;
            lbSurgeonName1.Text = operNote.surgeon_name_1;
            txtSurgeon1.Value = operNote.surgeon_id_1;
            lbSurgeonName2.Text = operNote.surgeon_name_2;
            txtSurgeon2.Value = operNote.surgeon_id_2;
            //lbSurgeonName3.Text = operNote.surgeon_name_3;
            //txtSurgeon3.Value = operNote.surgeon_id_3;
            //lbSurgeonName4.Text = operNote.surgeon_name_4;
            //txtSurgeon4.Value = operNote.surgeon_id_4;
            
            lbAssistantName1.Text = operNote.assistant_name_1;
            txtAssistant1.Value = operNote.assistant_id_1;
            lbAssistantName2.Text = operNote.assistant_name_2;
            txtAssistant2.Value = operNote.assistant_id_2;
            //lbAssistantName3.Text = operNote.assistant_name_3;
            //txtAssistant3.Value = operNote.assistant_id_3;
            //lbAssistantName4.Text = operNote.assistant_name_4;
            //txtAssistant4.Value = operNote.assistant_id_4;

            lbScrubNurseName1.Text = operNote.scrub_nurse_name_1;
            txtScrubNurse1.Value = operNote.scrub_nurse_id_1;
            lbScrubNurseName2.Text = operNote.scrub_nurse_name_2;
            txtScrubNurse2.Value = operNote.scrub_nurse_id_2;
            //lbScrubNurseName3.Text = operNote.scrub_nurse_name_3;
            //txtScrubNurse3.Value = operNote.scrub_nurse_id_3;
            //lbScrubNurseName4.Text = operNote.scrub_nurse_name_4;
            //txtScrubNurse4.Value = operNote.scrub_nurse_id_4;

            lbCircuNurseName1.Text = operNote.circulation_nurse_name_1;
            txtCircuNurse1.Value = operNote.circulation_nurse_id_1;
            lbCircuNurseName2.Text = operNote.circulation_nurse_name_2;
            txtCircuNurse2.Value = operNote.circulation_nurse_id_2;
            //lbCircuNurseName3.Text = operNote.circulation_nurse_name_3;
            //txtCircuNurse3.Value = operNote.circulation_nurse_id_3;
            //lbCircuNurseName4.Text = operNote.circulation_nurse_name_4;
            //txtCircuNurse4.Value = operNote.circulation_nurse_id_4;

            lbPerfusionistName1.Text = operNote.perfusionist_name_1;
            txtPerfusionist1.Value = operNote.perfusionist_id_1;
            lbPerfusionistName2.Text = operNote.perfusionist_name_2;
            txtPerfusionist2.Value = operNote.perfusionist_id_2;

            lbAnesthetistName1.Text = operNote.anesthetist_name_1;
            txtAnesthetist1.Value = operNote.anesthetist_id_1;

            lbAnesthetistAssistName1.Text = operNote.anesthetist_assistant_name_1;
            txtAnesthetistAssist1.Value = operNote.anesthetist_assistant_id_1;
            lbAnesthetistAssistName2.Text = operNote.anesthetist_assistant_name_2;
            txtAnesthetistAssist2.Value = operNote.anesthetist_assistant_id_2;

            txtTimeAnthe.Value = operNote.time_start_anesthesia_techique_1;
            txtTimeFinishAnthe.Value = operNote.time_finish_anesthesia_techique_1;
            txtTotalTimeAnthe.Value = operNote.total_time_anesthesia_1;

            txtAnesthetistTechni1.Value = operNote.anesthesia_techique_id_1;
            txtPreOperation.Value = operNote.pre_operatation_diagnosis;
            txtPostOperation.Value = operNote.post_operation_diagnosis;
            txtOperation1.Value = operNote.operation_1;
            txtOperation2.Value = operNote.operation_2;
            txtOperation3.Value = operNote.operation_3;
            txtOperation4.Value = operNote.operation_4;

            txtComplication.Value = operNote.complication_other;
            txtSpecialSpecimen.Value = operNote.special_specimen;
            txtEstimateBloodlose.Value = operNote.estimated_blood_loss;
            txtTissueBiopsy.Value = operNote.tissue_biopsy_unit;

            chkComplicationYes.Checked = operNote.complication.Equals("1") ? true : false;
            chkComplicationNo.Checked = operNote.complication.Equals("0") ? true : false;
            chkTissueBiopsyYes.Checked = operNote.tissue_biopsy.Equals("1") ? true : false;
            chkTissueBiopsyNo.Checked = operNote.tissue_biopsy.Equals("0") ? true : false;

            //txtFinding.Value = operNote.finding_1;

            setControlFrmDoctorDiag();
            //pnProcidures

            //lbDept.Text = operNote.dept_name;
            //lbDept.Text = operNote.dept_name;
            //lbDept.Text = operNote.dept_name;
            //lbDept.Text = operNote.dept_name;
            //lbDept.Text = operNote.dept_name;
            //lbDept.Text = operNote.dept_name;
        }
        private void setControlFrmDoctorDiag()
        {
            pnProcidures.Controls.Clear();
            FrmDoctorDiag1 frmPrecidures = new FrmDoctorDiag1(bc, "operative_note_precidures_1", operNote.patient_hn, operNote.procidures_1, operNote.operative_note_id);
            FrmDoctorDiag1 frmFinding = new FrmDoctorDiag1(bc, "operative_note_finding_1", operNote.patient_hn, operNote.finding_1, operNote.operative_note_id);
            sCFinding = new C1SplitContainer();
            scFinding = new C1SplitterPanel();
            scPrecidures = new C1SplitterPanel();

            frmPrecidures.FormBorderStyle = FormBorderStyle.None;
            frmPrecidures.TopLevel = false;
            frmPrecidures.Dock = DockStyle.Fill;
            frmPrecidures.AutoScroll = true;

            frmFinding.FormBorderStyle = FormBorderStyle.None;
            frmFinding.TopLevel = false;
            frmFinding.Dock = DockStyle.Fill;
            frmFinding.AutoScroll = true;

            scFinding.Collapsible = true;
            scFinding.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            scFinding.Location = new System.Drawing.Point(0, 21);
            scFinding.Name = "scFinding";
            
            scPrecidures.Collapsible = true;
            scPrecidures.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            scPrecidures.Location = new System.Drawing.Point(0, 21);
            scPrecidures.Name = "scPrecidures";
            //scPrecidures.Controls.Add(pnLeft);
            sCFinding.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCFinding.Name = "sCFinding";
            sCFinding.Dock = System.Windows.Forms.DockStyle.Fill;
            sCFinding.Panels.Add(scFinding);
            sCFinding.Panels.Add(scPrecidures);
            sCFinding.HeaderHeight = 20;
            scPrecidures.Controls.Add(frmPrecidures);
            scFinding.Controls.Add(frmFinding);
            //scPrecidures.Controls.Add(pnOperative);



            //pnProcidures.Controls.Add(frm);
            pnProcidures.Controls.Add(sCFinding);
            frmPrecidures.Show();

            frmFinding.Show();
        }
        private Boolean setOperativeNote()
        {
            Boolean chk = false;
            //chk = operNote.patient_hn.Length <= 0 ? false : true;
            //chk = operNote.an.Length <= 0 ? false : true;
            //chk = operNote.pre_no.Length <= 0 ? false : true;
            //chk = operNote.anesthesia_techique_id_1.Length <= 0 ? false : true;
            if (chk = operNote.attending_stf_id.Length <= 0)
            {
                sep.SetError(txtDtrId, "ไม่พบรหัส");
                txtDtrId.Focus();
                txtDtrId.SelectAll();
                return chk;
            }
            if (chk = operNote.patient_hn.Length <= 0)
            {
                sep.SetError(btnPttSearch, "ไม่พบรหัส");
                btnPttSearch.Focus();
                return chk;
            }
            if (chk = operNote.dept_id.Length <= 0)
            {
                sep.SetError(txtDept, "ไม่พบรหัส");
                txtDept.Focus();
                txtDept.SelectAll();
                return chk;
            }
            if (chk = operNote.ward_id.Length <= 0)
            {
                sep.SetError(txtWard, "ไม่พบรหัส");
                txtWard.Focus();
                txtWard.SelectAll();
                return chk;
            }
            operNote.date_operation = bc.datetoDB(txtDateOperative.Text);
            operNote.time_start = txtTimeInsion.Text.Trim();
            operNote.time_finish = txtTimeFinish.Text.Trim();
            operNote.total_time = txtTotalTime.Text.Trim();
            operNote.time_start_anesthesia_techique_1 = txtTimeAnthe.Text.Trim();
            operNote.time_finish_anesthesia_techique_1 = txtTimeFinishAnthe.Text.Trim();
            operNote.total_time_anesthesia_1 = txtTotalTimeAnthe.Text.Trim();
            operNote.pre_operatation_diagnosis = txtPreOperation.Text.Trim();
            operNote.post_operation_diagnosis = txtPostOperation.Text.Trim();
            operNote.operation_1 = txtOperation1.Text.Trim();
            operNote.operation_2 = txtOperation2.Text.Trim();
            operNote.operation_3 = txtOperation3.Text.Trim();
            operNote.operation_4 = txtOperation4.Text.Trim();
            operNote.procidures_1 = bc.operative_note_precidures_1;
            operNote.finding_1 = bc.operative_note_finding_1;
            operNote.complication = chkComplicationYes.Checked ? "1" :"0";
            operNote.special_specimen = txtSpecialSpecimen.Text.Trim();
            operNote.estimated_blood_loss = txtEstimateBloodlose.Text.Trim();
            operNote.tissue_biopsy = chkTissueBiopsyYes.Checked ? "1" : "0";
            operNote.tissue_biopsy_unit = txtTissueBiopsy.Text.Trim();
            operNote.complication_other = txtComplication.Text.Trim();
            chk = true;
            //chk = operNote.anesthetist_id_1.Length <= 0 ? false : true;
            return chk;
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int gapLine = 16, gapX = 20, gapY = 20, xCol2 = 130, xCol1 = 20, xCol3 = 300, xCol4 = 390, xCol5 = 1030;
            Size size = new Size();

            _c1pdf.Clear();
            _c1pdf.DocumentInfo.Title = "Operative Note HN "+ operNote.patient_hn + " an " + operNote.an;
            //statusBar1.Text = "Creating pdf document...";

            // calculate page rect (discounting margins)
            RectangleF rcPage = GetPageRect();
            RectangleF rc = rcPage;
            //rc.Inflate(-10, 0);

            // add title
            Font titleFont = new Font("Microsoft Sans Serif", 18, FontStyle.Bold);
            Font hdrFont = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
            Font ftrFont = new Font("Microsoft Sans Serif", 8);
            Font txtFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            rc.X = gapX;
            rc.Y = gapY;
            gapY += gapLine;
            _c1pdf.DrawString(_c1pdf.DocumentInfo.Title, titleFont, Brushes.Black, rc);
            //rc = RenderParagraph(_c1pdf.DocumentInfo.Title, titleFont, rcPage, rc, false);
            // Y 600 สุดหน้า
            //rc.X = 600;
            //rc = RenderParagraph("0", titleFont, rcPage, rc, false);
            //rc.X = rcPage.X + 36; // << indent body text by 1/2 inch
            //rc.Width = rcPage.Width - 40;
            ////rc = RenderParagraph("Operative Note", txtFont, rcPage, rc);
            //rc.X = rcPage.X; // << restore indent
            //rc.Width = rcPage.Width;
            //rc.Y += gapLine; // << add 12pt spacing after each quote
            gapY += gapLine;
            gapY += gapLine;
            rc.Y = gapY;
            //rc = RenderParagraph("Department "+ operNote.dept_name, txtFont, rcPage, rc);
            _c1pdf.DrawString("Department " , txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.dept_name, txtFont, Brushes.Black, rc);

            gapX = xCol3;
            rc.X = gapX;
            _c1pdf.DrawString("Ward " , txtFont, Brushes.Black, rc);
            rc.X = gapX + 25;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 27;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.ward_name, txtFont, Brushes.Black, rc);
            gapX = xCol4;
            rc.X = gapX;
            _c1pdf.DrawString("Attending " + operNote.attending_stf_name, txtFont, Brushes.Black, rc);
            rc.X = gapX + 43;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("................................. ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 45;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.attending_stf_name, txtFont, Brushes.Black, rc);

            gapX = xCol1;
            gapY += gapLine;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Patient Name " , txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.patient_fullname + " age " + operNote.age , txtFont, Brushes.Black, rc);
            gapX = xCol3;
            rc.X = gapX;
            _c1pdf.DrawString("Date Operation ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 68;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("..................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 70;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.date_operation, txtFont, Brushes.Black, rc);
            gapX = xCol4+40;
            rc.X = gapX;
            _c1pdf.DrawString("start ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 23;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("............................. ", txtFont, Brushes.Black, rc);
            gapX += 25;
            rc.X = gapX;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.time_start + " - " + operNote.time_finish +" ["+ operNote.total_time+"]", txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Surgeon 1.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.surgeon_name_1, txtFont, Brushes.Black, rc);
            gapX = xCol3;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Assistant 1.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.assistant_name_1, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Surgeon 2.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.surgeon_name_2, txtFont, Brushes.Black, rc);
            gapX = xCol3;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Assistant 2.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.assistant_name_2, txtFont, Brushes.Black, rc);

            //gapY += gapLine;
            //gapX = xCol1;
            //rc.X = gapX;
            //rc.Y = gapY;
            //_c1pdf.DrawString("Surgeon 3.", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 60;
            //rc.Y = gapY + 2;
            //_c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 62;
            //rc.Y = gapY - 2;
            //_c1pdf.DrawString(operNote.surgeon_name_3, txtFont, Brushes.Black, rc);
            //gapX = xCol3;
            //rc.X = gapX;
            //rc.Y = gapY;
            //_c1pdf.DrawString("Assistant 3.", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 60;
            //rc.Y = gapY + 2;
            //_c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 62;
            //rc.Y = gapY - 2;
            //_c1pdf.DrawString(operNote.assistant_name_3, txtFont, Brushes.Black, rc);

            //gapY += gapLine;
            //gapX = xCol1;
            //rc.X = gapX;
            //rc.Y = gapY;
            //_c1pdf.DrawString("Surgeon 4.", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 60;
            //rc.Y = gapY + 2;
            //_c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 62;
            //rc.Y = gapY - 2;
            //_c1pdf.DrawString(operNote.surgeon_name_4, txtFont, Brushes.Black, rc);
            //gapX = xCol3;
            //rc.X = gapX;
            //rc.Y = gapY;
            //_c1pdf.DrawString("Assistant 4.", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 60;
            //rc.Y = gapY + 2;
            //_c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 62;
            //rc.Y = gapY - 2;
            //_c1pdf.DrawString(operNote.assistant_name_4, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Scrub 1.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.scrub_nurse_name_1, txtFont, Brushes.Black, rc);
            gapX = xCol3;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Circulation 1.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.circulation_nurse_name_1, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Scrub 2.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.scrub_nurse_name_2, txtFont, Brushes.Black, rc);
            gapX = xCol3;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Circulation 2.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.circulation_nurse_name_2, txtFont, Brushes.Black, rc);

            //gapY += gapLine;
            //gapX = xCol1;
            //rc.X = gapX;
            //rc.Y = gapY;
            //_c1pdf.DrawString("Scrub 3.", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 60;
            //rc.Y = gapY + 2;
            //_c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 62;
            //rc.Y = gapY - 2;
            //_c1pdf.DrawString(operNote.scrub_nurse_name_3, txtFont, Brushes.Black, rc);
            //gapX = xCol3;
            //rc.X = gapX;
            //rc.Y = gapY;
            //_c1pdf.DrawString("Circulation 3.", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 60;
            //rc.Y = gapY + 2;
            //_c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 62;
            //rc.Y = gapY - 2;
            //_c1pdf.DrawString(operNote.circulation_nurse_name_3, txtFont, Brushes.Black, rc);

            //gapY += gapLine;
            //gapX = xCol1;
            //rc.X = gapX;
            //rc.Y = gapY;
            //_c1pdf.DrawString("Scrub 4.", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 60;
            //rc.Y = gapY + 2;
            //_c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 62;
            //rc.Y = gapY - 2;
            //_c1pdf.DrawString(operNote.scrub_nurse_name_4, txtFont, Brushes.Black, rc);
            //gapX = xCol3;
            //rc.X = gapX;
            //rc.Y = gapY;
            //_c1pdf.DrawString("Circulation 4.", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 60;
            //rc.Y = gapY + 2;
            //_c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 62;
            //rc.Y = gapY - 2;
            //_c1pdf.DrawString(operNote.circulation_nurse_name_4, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Perfusionist 1.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.perfusionist_name_1, txtFont, Brushes.Black, rc);
            gapX = xCol3;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Anesthetist 1.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.anesthetist_name_1, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Perfusionist 2.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.perfusionist_name_2, txtFont, Brushes.Black, rc);
            gapX = xCol3;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Anesthetist 2.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.anesthetist_name_2, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Assistant 1.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.anesthetist_assistant_name_1, txtFont, Brushes.Black, rc);
            gapX = xCol3;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Assistant 2.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString(".......................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.anesthetist_assistant_name_2, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Anesthesia Technique ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 100;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("............................................................................................ ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 102;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.anesthesia_techique_name_1, txtFont, Brushes.Black, rc);
            gapX = xCol4+40;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("start ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 23;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("............................. ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 25;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.time_start_anesthesia_techique_1 + " - " + operNote.time_finish_anesthesia_techique_1 + " [" + operNote.total_time_anesthesia_1 + "]", txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Pre Operation Diagnosis ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 110;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("................................................................................................................................. ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 112;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.pre_operatation_diagnosis, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Post Operation Diagnosis ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 115;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("............................................................................................ ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 117;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.post_operation_diagnosis, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Operation 1.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("..................................................................................................................................................... ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.operation_1, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Operation 2.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("................................................................................................................................. ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.operation_2, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Operation 3.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("................................................................................................................................. ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.operation_3, txtFont, Brushes.Black, rc);

            gapY += gapLine;
            gapX = xCol1;
            rc.X = gapX;
            rc.Y = gapY;
            _c1pdf.DrawString("Operation 4.", txtFont, Brushes.Black, rc);
            rc.X = gapX + 60;
            rc.Y = gapY + 2;
            _c1pdf.DrawString("................................................................................................................................. ", txtFont, Brushes.Black, rc);
            rc.X = gapX + 62;
            rc.Y = gapY - 2;
            _c1pdf.DrawString(operNote.operation_4, txtFont, Brushes.Black, rc);

            //gapY += gapLine;
            //gapX = xCol1;
            //rc.X = gapX;
            //rc.Y = gapY;
            //_c1pdf.DrawString("Finding ", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 60;
            //rc.Y = gapY + 2;
            //_c1pdf.DrawString("..................................................................................................................................................... ", txtFont, Brushes.Black, rc);
            //rc.X = gapX + 62;
            //rc.Y = gapY - 2;
            //_c1pdf.DrawString(operNote.finding_1, txtFont, Brushes.Black, rc);

            OperativeNote operNote1 = new OperativeNote();      // ต้องดึงใหม่ เพื่อ มีการแก้ไข รูป แล้วไม่ได้ save Operative Note save แต่ richtextbox อย่างเดียว
            operNote1 = bc.bcDB.operNoteDB.selectByPk(operNote.operative_note_id);
            DocScan dscProcidures = new DocScan();
            dscProcidures = bc.bcDB.dscDB.selectByPk(operNote1.procidures_1);
            DocScan dscFinding = new DocScan();
            dscFinding = bc.bcDB.dscDB.selectByPk(operNote1.finding_1);

            MemoryStream streamPro, streamFind, streamDiag;
            FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
            streamFind = ftp.download(bc.iniC.folderFTP + "//" + dscFinding.image_path);
            Thread.Sleep(200);
            streamFind.Position = 0;
            int sizertfFinding = 0, sizertfProcidures = 0;
            if (streamFind.Length > 0)
            {
                StreamReader reader = new StreamReader(streamFind, System.Text.Encoding.UTF8, true);
                String aaa = reader.ReadToEnd();
                //RectangleF myRectangle = new Rectangle(0, 0, 600, 400);
                //myRectangle.Size = _c1pdf.MeasureStringRtf(aaa, txtFont, rc.Width);
                
                RichTextBox rtf = new RichTextBox();
                rtf.Dock = System.Windows.Forms.DockStyle.Fill;
                rtf.EnableAutoDragDrop = true;
                rtf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                rtf.Location = new System.Drawing.Point(0, 51);
                rtf.Name = "rtf";
                //rtf.Size = new System.Drawing.Size(667, 262);
                rtf.TabIndex = 0;
                rtf.ContentsResized += Rtf_ContentsResized;
                //rtf.Text = "";
                streamFind.Position = 0;
                rtf.LoadFile(streamFind, RichTextBoxStreamType.RichText);
                size = bc.MeasureString(rtf);
                sizertfFinding = size.Width;

                if ((gapY + rtfFinding) >= _c1pdf.PageSize.Height)
                {
                    _c1pdf.NewPage();
                    gapY = gapLine;
                }
                else
                {
                    gapY += gapLine;
                    gapX = xCol1;
                    rc.X = gapX;
                    rc.Y = gapY;
                }
                _c1pdf.DrawString("Finding ", txtFont, Brushes.Black, rc);
                rc.X = gapX + 60;
                rc.Y = gapY + 2;
                _c1pdf.DrawStringRtf(aaa, txtFont, Brushes.White, rc);
            }
            
            streamPro = ftp.download(bc.iniC.folderFTP + "//" + dscProcidures.image_path);
            Thread.Sleep(200);
            streamPro.Position = 0;
            if (streamPro.Length > 0)
            {
                StreamReader reader = new StreamReader(streamPro, System.Text.Encoding.UTF8, true);
                String aaa = reader.ReadToEnd();
                RichTextBox rtf1 = new RichTextBox();
                rtf1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                rtf1.Name = "rtf1";
                streamPro.Position = 0;
                rtf1.ContentsResized += Rtf1_ContentsResized;
                rtf1.LoadFile(streamPro, RichTextBoxStreamType.RichText);
                size = bc.MeasureString(rtf1);
                sizertfProcidures = size.Width;
                SizeF sizef = _c1pdf.PageSize;
                //operNote1.procidures_1
                if ((gapY + rtfFinding + rtfProcidures) >= _c1pdf.PageSize.Height)
                {
                    _c1pdf.NewPage();
                    gapY = gapLine;
                }
                else
                {
                    gapY += sizertfFinding + 300;
                }

                //RectangleF myRectangle = new Rectangle(0, 0, 600, 400);
                //myRectangle.Size = _c1pdf.MeasureStringRtf(aaa, txtFont, rc.Width);
                
                gapX = xCol1;
                rc.X = gapX;
                rc.Y = gapY;
                _c1pdf.DrawString("Procidures ", txtFont, Brushes.Black, rc);
                rc.X = gapX + 60;
                rc.Y = gapY + 2;
                _c1pdf.DrawStringRtf(aaa, txtFont, Brushes.White, rc);
                //_c1pdf.DrawRectangle(Pens.Black, rc);
            }
            

                //rtbDocument.SaveFile(filename, RichTextBoxStreamType.);
                //// render louvre images
                //rc = RenderParagraph("Louvre Images", hdrFont, rcPage, rc, true);
                //foreach (string res in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                //    if (res.ToLower().IndexOf("louvre") > -1)
                //        rc = RenderImage(rcPage, rc, res);
                //_c1pdf.NewPage();
                //rc.Y = rcPage.Y;

                //// render Escher images
                //rc = RenderParagraph("Escher Images", hdrFont, rcPage, rc, true);
                //foreach (string res in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                //    if (res.ToLower().IndexOf("escher") > -1)
                //        rc = RenderImage(rcPage, rc, res);
                //_c1pdf.NewPage();
                //rc.Y = rcPage.Y;

                //// render icons
                //rc = RenderParagraph("Icons (transparent)", hdrFont, rcPage, rc, true);
                //foreach (string res in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                //    if (res.ToLower().IndexOf("icons") > -1)
                //        rc = RenderImage(rcPage, rc, res);

                //// render everything else
                //rc = RenderParagraph("Other", hdrFont, rcPage, rc, true);
                //foreach (string res in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                //    if (res.ToLower().IndexOf("other") > -1)
                //        rc = RenderImage(rcPage, rc, res);

                //// second pass to number pages
                //AddFooters();
                //PointF pointF = new PointF(gapX, gapY);
                //_c1pdf.DrawString("Operative Note", txtFont, Brushes.Black, pointF);
                String PathName = "medical", datetick="", fileName="";
            datetick = DateTime.Now.Ticks.ToString();
            if (!Directory.Exists("medical"))
            {
                Directory.CreateDirectory("medical");
            }
            fileName = "medical\\"+datetick+".pdf";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                System.Threading.Thread.Sleep(100);
            }
            // save to high-quality file and show it
            _c1pdf.ImageQuality = ImageQualityEnum.High;
            //statusBar1.Text = "Saving high-quality pdf...";
            String path = Path.GetDirectoryName(Application.ExecutablePath) ;
            _c1pdf.Save(path + "\\"+fileName);

            Process.Start(fileName);
        }

        private void Rtf1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            //throw new NotImplementedException();
            var richTextBox = (RichTextBox)sender;
            richTextBox.Width = e.NewRectangle.Width;
            richTextBox.Height = e.NewRectangle.Height;
            rtfProcidures = richTextBox.Height;
        }

        private void Rtf_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            //throw new NotImplementedException();
            var richTextBox = (RichTextBox)sender;
            richTextBox.Width = e.NewRectangle.Width;
            richTextBox.Height = e.NewRectangle.Height;
            rtfFinding = richTextBox.Height;
        }

        private static IEnumerable<string> ReadLines(StreamReader stream)
        {
            StringBuilder sb = new StringBuilder();

            int symbol = stream.Peek();
            while (symbol != -1)
            {
                symbol = stream.Read();
                if (symbol == 13 && stream.Peek() == 10)
                {
                    stream.Read();

                    string line = sb.ToString();
                    sb.Clear();

                    yield return line;
                }
                else
                    sb.Append((char)symbol);
            }

            yield return sb.ToString();
        }
        private void AddFooters()
        {
            Font fontHorz = new Font("Tahoma", 7, FontStyle.Bold);
            Font fontVert = new Font("Viner Hand ITC", 14, FontStyle.Bold);

            StringFormat sfRight = new StringFormat();
            sfRight.Alignment = StringAlignment.Far;

            StringFormat sfVert = new StringFormat();
            sfVert.FormatFlags |= StringFormatFlags.DirectionVertical;
            sfVert.Alignment = StringAlignment.Center;

            for (int page = 0; page < _c1pdf.Pages.Count; page++)
            {
                // select page we want (could change PageSize)
                _c1pdf.CurrentPage = page;

                // build rectangles for rendering text
                RectangleF rcPage = GetPageRect();
                RectangleF rcFooter = rcPage;
                rcFooter.Y = rcFooter.Bottom + 6;
                rcFooter.Height = 12;
                RectangleF rcVert = rcPage;
                rcVert.X = rcPage.Right + 6;

                // add left-aligned footer
                string text = _c1pdf.DocumentInfo.Title;
                _c1pdf.DrawString(text, fontHorz, Brushes.Gray, rcFooter);

                // add right-aligned footer
                text = string.Format("Page {0} of {1}", page + 1, _c1pdf.Pages.Count);
                _c1pdf.DrawString(text, fontHorz, Brushes.Gray, rcFooter, sfRight);

                // add vertical text
                text = _c1pdf.DocumentInfo.Title + " (document created using the C1Pdf .NET component)";
                _c1pdf.DrawString(text, fontVert, Brushes.LightGray, rcVert, sfVert);

                // draw lines on bottom and right of the page
                _c1pdf.DrawLine(Pens.Gray, rcPage.Left, rcPage.Bottom, rcPage.Right, rcPage.Bottom);
                _c1pdf.DrawLine(Pens.Gray, rcPage.Right, rcPage.Top, rcPage.Right, rcPage.Bottom);
            }
        }
        private RectangleF RenderImage(RectangleF rcPage, RectangleF rc, string resName)
        {
            // get image
            Assembly a = Assembly.GetExecutingAssembly();
            System.Drawing.Image img = System.Drawing.Image.FromStream(a.GetManifestResourceStream(resName));

            // calculate image height
            // based on image size and page size
            rc.Height = Math.Min(img.Height / 96f * 72, rcPage.Height * .3f);

            // skip page if necessary
            if (rc.Bottom > rcPage.Bottom)
            {
                _c1pdf.NewPage();
                rc.Y = rcPage.Y;
            }

            // add bookmark
            string[] arr = resName.Split('.');
            string picName = string.Format("{0}.{1}", arr[arr.Length - 2], arr[arr.Length - 1]);
            _c1pdf.AddBookmark(picName, 1, rc.Y);

            // draw solid background (mainly to see transparency)
            rc.Inflate(+2, +2);
            _c1pdf.FillRectangle(Brushes.Gray, rc);
            rc.Inflate(-2, -2);

            // draw image (keep aspect ratio)
            _c1pdf.DrawImage(img, rc, ContentAlignment.MiddleCenter, ImageSizeModeEnum.Scale);

            // draw caption
            Font font = new Font("Tahoma", 9);
            rc.Y = rc.Bottom + 3;
            rc.Height = 2 * font.Size;
            rc.Offset(+.3f, +.3f);
            _c1pdf.DrawString(picName, font, Brushes.Yellow, rc);
            rc.Offset(-.3f, -.3f);
            _c1pdf.DrawString(picName, font, Brushes.Black, rc);

            // update rectangle
            rc.Y = rc.Bottom + 20;
            return rc;
        }
        internal RectangleF GetPageRect()
        {
            RectangleF rcPage = _c1pdf.PageRectangle;
            rcPage.Inflate(-72, -72);
            return rcPage;
        }
        internal RectangleF RenderParagraph(string text, Font font, RectangleF rcPage, RectangleF rc, bool outline, bool linkTarget)
        {
            // if it won't fit this page, do a page break
            rc.Height = _c1pdf.MeasureString(text, font, rc.Width).Height;
            if (rc.Bottom > rcPage.Bottom)
            {
                _c1pdf.NewPage();
                rc.Y = rcPage.Top;
            }

            // draw the string
            _c1pdf.DrawString(text, font, Brushes.Black, rc);

            // show bounds (mainly to check word wrapping)
            //_c1pdf.DrawRectangle(Pens.Sienna, rc);

            // add headings to outline
            if (outline)
            {
                _c1pdf.DrawLine(Pens.Black, rc.X, rc.Y, rc.Right, rc.Y);
                _c1pdf.AddBookmark(text, 0, rc.Y);
            }

            // add link target
            if (linkTarget)
            {
                _c1pdf.AddTarget(text, rc);
            }

            // update rectangle for next time
            rc.Offset(0, rc.Height);
            return rc;
        }
        internal RectangleF RenderParagraph(string text, Font font, RectangleF rcPage, RectangleF rc, bool outline)
        {
            return RenderParagraph(text, font, rcPage, rc, outline, false);
        }
        internal RectangleF RenderParagraph(string text, Font font, RectangleF rcPage, RectangleF rc)
        {
            return RenderParagraph(text, font, rcPage, rc, false, false);
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (MessageBox.Show("ต้องการ บันทึกข้อมูล ", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (setOperativeNote())
                {
                    String re = "";
                    int chk = 0;
                    re = bc.bcDB.operNoteDB.insertOperativeNote(operNote,"");
                    operNote.operative_note_id = re;
                }
            }
        }

        private void TxtAnesthetistAssist2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsername(txtAnesthetistAssist2.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbAnesthetistAssistName2.Text = stf1.fullname;
                    operNote.anesthetist_assistant_id_2 = stf1.staff_id;
                    operNote.anesthetist_assistant_name_2 = stf1.fullname;
                    sep.Clear();
                    txtAnesthetistTechni1.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtAnesthetistAssist1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsername(txtAnesthetistAssist1.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbAnesthetistAssistName1.Text = stf1.fullname;
                    operNote.anesthetist_assistant_id_1 = stf1.staff_id;
                    operNote.anesthetist_assistant_name_1 = stf1.fullname;
                    sep.Clear();
                    txtAnesthetistAssist2.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtAnesthetist1_KeyUp1(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtAnesthetist1.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbAnesthetistName1.Text = stf1.fullname;
                    operNote.anesthetist_id_1 = stf1.staff_id;
                    operNote.anesthetist_name_1 = stf1.fullname;
                    sep.Clear();
                    txtAnesthetistAssist1.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtPerfusionist2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtPerfusionist2.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbPerfusionistName2.Text = stf1.fullname;
                    operNote.perfusionist_id_2 = stf1.staff_id;
                    operNote.perfusionist_name_2 = stf1.fullname;
                    sep.Clear();
                    txtAnesthetist1.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtPerfusionist1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtPerfusionist1.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbPerfusionistName1.Text = stf1.fullname;
                    operNote.perfusionist_id_1 = stf1.staff_id;
                    operNote.perfusionist_name_1 = stf1.fullname;
                    sep.Clear();
                    txtPerfusionist2.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtCircuNurse4_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsername(txtCircuNurse4.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbCircuNurseName4.Text = stf1.fullname;
                    operNote.circulation_nurse_id_4 = stf1.staff_id;
                    operNote.circulation_nurse_name_4 = stf1.fullname;
                    sep.Clear();
                    txtPerfusionist1.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtCircuNurse3_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsername(txtCircuNurse3.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbCircuNurseName3.Text = stf1.fullname;
                    operNote.circulation_nurse_id_3 = stf1.staff_id;
                    operNote.circulation_nurse_name_3 = stf1.fullname;
                    sep.Clear();
                    txtCircuNurse4.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtCircuNurse2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsername(txtCircuNurse2.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbCircuNurseName2.Text = stf1.fullname;
                    operNote.circulation_nurse_id_2 = stf1.staff_id;
                    operNote.circulation_nurse_name_2 = stf1.fullname;
                    sep.Clear();
                    txtPerfusionist1.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtCircuNurse1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsername(txtCircuNurse1.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbCircuNurseName1.Text = stf1.fullname;
                    operNote.circulation_nurse_id_1 = stf1.staff_id;
                    operNote.circulation_nurse_name_1 = stf1.fullname;
                    sep.Clear();
                    txtCircuNurse2.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtScrubNurse4_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsername(txtScrubNurse4.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbScrubNurseName4.Text = stf1.fullname;
                    operNote.scrub_nurse_id_4 = stf1.staff_id;
                    operNote.scrub_nurse_name_4 = stf1.fullname;
                    sep.Clear();
                    txtCircuNurse1.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtScrubNurse3_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsername(txtScrubNurse3.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbScrubNurseName3.Text = stf1.fullname;
                    operNote.scrub_nurse_id_3 = stf1.staff_id;
                    operNote.scrub_nurse_name_3 = stf1.fullname;
                    sep.Clear();
                    txtScrubNurse4.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtScrubNurse2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsername(txtScrubNurse2.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbScrubNurseName2.Text = stf1.fullname;
                    operNote.scrub_nurse_id_2 = stf1.staff_id;
                    operNote.scrub_nurse_name_2 = stf1.fullname;
                    sep.Clear();
                    txtCircuNurse1.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtScrubNurse1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsername(txtScrubNurse1.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbScrubNurseName1.Text = stf1.fullname;
                    operNote.scrub_nurse_id_1 = stf1.staff_id;
                    operNote.scrub_nurse_name_1 = stf1.fullname;
                    sep.Clear();
                    txtScrubNurse2.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtAssistant4_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtAssistant4.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbAssistantName4.Text = stf1.fullname;
                    operNote.assistant_id_4 = stf1.staff_id;
                    operNote.assistant_name_4 = stf1.fullname;
                    sep.Clear();
                    txtScrubNurse1.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtAssistant3_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtAssistant3.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbAssistantName3.Text = stf1.fullname;
                    operNote.assistant_id_3 = stf1.staff_id;
                    operNote.assistant_name_3 = stf1.fullname;
                    sep.Clear();
                    txtAssistant4.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtAssistant2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtAssistant2.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbAssistantName2.Text = stf1.fullname;
                    operNote.assistant_id_2 = stf1.staff_id;
                    operNote.assistant_name_2 = stf1.fullname;
                    sep.Clear();
                    txtScrubNurse1.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtAssistant1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtAssistant1.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbAssistantName1.Text = stf1.fullname;
                    operNote.assistant_id_1 = stf1.staff_id;
                    operNote.assistant_name_1 = stf1.fullname;
                    sep.Clear();
                    txtAssistant2.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void TxtSurgeon4_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtSurgeon4.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbSurgeonName4.Text = stf1.fullname;
                    operNote.surgeon_id_4 = stf1.staff_id;
                    operNote.surgeon_name_4 = stf1.fullname;
                    sep.Clear();
                    txtAssistant1.Focus();
                }
                else
                {
                    sep.SetError(txtSurgeon4, "ไม่พบรหัส");
                    txtSurgeon4.SelectAll();
                }
            }
        }

        private void TxtSurgeon3_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtSurgeon3.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbSurgeonName3.Text = stf1.fullname;
                    operNote.surgeon_id_3 = stf1.staff_id;
                    operNote.surgeon_name_3 = stf1.fullname;
                    sep.Clear();
                    txtSurgeon4.Focus();
                }
                else
                {
                    sep.SetError(txtSurgeon3, "ไม่พบรหัส");
                    txtSurgeon3.SelectAll();
                    //MessageBox.Show("No Found", "");
                }
            }
        }

        private void TxtSurgeon2_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtSurgeon2.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbSurgeonName2.Text = stf1.fullname;
                    operNote.surgeon_id_2 = stf1.staff_id;
                    operNote.surgeon_name_2 = stf1.fullname;
                    sep.Clear();
                    txtAssistant1.Focus();
                }
                else
                {
                    sep.SetError(txtSurgeon2, "ไม่พบรหัส");
                    txtSurgeon2.SelectAll();
                }
            }
        }

        private void TxtSurgeon1_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtSurgeon1.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbSurgeonName1.Text = stf1.fullname;
                    operNote.surgeon_id_1 = stf1.staff_id;
                    operNote.surgeon_name_1 = stf1.fullname;
                    sep.Clear();
                    txtSurgeon2.Focus();
                }
                else
                {
                    sep.SetError(txtSurgeon1, "ไม่พบรหัส");
                    txtSurgeon1.SelectAll();
                }
            }
        }

        private void setContextMenuAnesthesis()
        {
            menuAnesTech = new ContextMenu();
            DataTable dt = new DataTable();
            dt = bc.bcDB.orDB.SelectAnesthesisAll();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MenuItem mitm = new MenuItem();
                    int chk = 0;
                    int.TryParse(row["mnc_oruse_cd"].ToString(), out chk);
                    mitm.Index = chk;
                    mitm.Text = row["mnc_oruse_dsc"].ToString();
                    mitm.Click += Mitm_Click;
                    menuAnesTech.MenuItems.Add(mitm);
                }
            }
        }
        private void setContextMenuDepartment()
        {
            menuDept = new ContextMenu();
            DataTable dt = new DataTable();
            dt = bc.bcDB.orDB.SelectDepartment();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MenuItem mordept = new MenuItem();
                    int chk = 0;
                    int.TryParse(row["mnc_sec_no"].ToString(), out chk);
                    mordept.Index = chk;
                    mordept.Text = row["mnc_md_dep_dsc"].ToString();
                    mordept.Click += MwardDept_Click;
                    menuDept.MenuItems.Add(mordept);
                }
            }
        }
        private void setContextMenuOrDepartment()
        {
            menuOrDeptOpeNote = new ContextMenu();
            DataTable dt = new DataTable();
            dt = bc.bcDB.orDB.SelectOrDepatmentAll();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    MenuItem mordept = new MenuItem();
                    int chk = 0;
                    int.TryParse(row["mnc_diaor_grp_cd"].ToString(), out chk);
                    mordept.Index = chk;
                    mordept.Text = row["mnc_diaor_grp_dsc"].ToString();
                    mordept.Click += MorDept_Click;
                    menuOrDeptOpeNote.MenuItems.Add(mordept);
                }
            }
        }
        private void MwardDept_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //e.ToString();
            MenuItem mitm = new MenuItem();
            mitm = (MenuItem)sender;
            txtWard.Value = mitm.Text;

            operNote.ward_id = mitm.Index.ToString().Length == 1 ? "0" +mitm.Index.ToString(): mitm.Index.ToString();
            operNote.ward_name = mitm.Text;
            sep.Clear();

        }
        private void MorDept_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //e.ToString();
            MenuItem mitm = new MenuItem();
            mitm = (MenuItem)sender;
            txtDept.Value = mitm.Text;

            operNote.dept_id = mitm.Index.ToString().Length == 1 ? "0" + mitm.Index.ToString() : mitm.Index.ToString();
            operNote.dept_name = mitm.Text;
            sep.Clear();
        }
        private void Mitm_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //e.ToString();
            MenuItem mitm = new MenuItem();
            mitm = (MenuItem)sender;
            txtAnesthetistTechni1.Value = mitm.Text;

            operNote.anesthesia_techique_id_1 = mitm.Index.ToString().Length == 1 ? "0" + mitm.Index.ToString() : mitm.Index.ToString();
            operNote.anesthesia_techique_name_1 = mitm.Text;
            sep.Clear();
        }

        private void TxtDtrId_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
            if(e.KeyCode == Keys.Enter)
            {
                Staff stf1 = new Staff();
                stf1 = bc.bcDB.stfDB.selectByUsernameLevelDoctor(txtDtrId.Text.Trim());
                if (stf1.fullname.Length > 0)
                {
                    lbDtrName.Text = stf1.fullname;
                    operNote.attending_stf_id = stf1.staff_id;
                    operNote.attending_stf_name = stf1.fullname;
                    sep.Clear();
                    btnPttSearch.Focus();
                }
                else
                {
                    sep.SetError((Control)sender, "ไม่พบรหัส");
                    ((C1TextBox)sender).SelectAll();
                }
            }
        }

        private void BtnPttSearch_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            FrmSearchHn frm = new FrmSearchHn(bc, FrmSearchHn.StatusConnection.host);
            frm.ShowDialog(this);
            String[] an = bc.sPtt.an.Split('/');
            ptt = bc.sPtt;
            operNote.patient_fullname = ptt.Name;
            operNote.patient_hn = ptt.Hn;
            operNote.an = bc.sPtt.an;
            operNote.age = ptt.AgeStringShort();
            operNote.mnc_date = bc.datetoDB(ptt.visitDate);
            operNote.pre_no = ptt.preno;
            bc.hn = ptt.Hn;     // ใช้ในหน้าจอ FrmDoctorDiag1
            lbPttName.Text = ptt.Hn + " "+ptt.Name+" Age " + ptt.AgeStringShort() + " An " + bc.sPtt.an;

        }
        private void initGrfView()
        {
            grfView = new C1FlexGrid();
            grfView.Font = fEdit;
            grfView.Dock = System.Windows.Forms.DockStyle.Fill;
            grfView.Location = new System.Drawing.Point(0, 0);
            grfView.Rows.Count = 1;
            grfView.DoubleClick += GrfView_DoubleClick;
            //FilterRow fr = new FilterRow(grfExpn);

            //grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
            //grfVs.row
            //grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
            //grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

            //menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
            //menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
            //menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

            pnLeftBotton.Controls.Add(grfView);

            theme1.SetTheme(grfView, bc.iniC.themeApp);

        }

        private void GrfView_DoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (grfView == null) return;
            if (grfView.Row < 1) return;
            if (grfView.Col <= 0) return;
            String id = "";
            id = grfView[grfView.Row, colID].ToString();
            opernote_id = id;
            setControl();
            scOperAdd.SizeRatio = 100;
        }

        private void initCompoment()
        {
            int gapLine = 25, gapX = 20, gapY = 20, xCol2=130, xCol1=80,xCol3=330, xCol4=640, xCol5=950;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            int scrH = Screen.PrimaryScreen.Bounds.Height;

            pnOperative = new Panel();
            pnLeft = new Panel();
            pnLeftTop = new Panel();
            pnLeftBotton = new Panel();
            sCOper = new C1SplitContainer();
            scOperView = new C1SplitterPanel();
            scOperAdd = new C1SplitterPanel();
            sCFinding = new C1SplitContainer();
            scFinding = new C1SplitterPanel();
            scPrecidures = new C1SplitterPanel();
            FrmDoctorDiag1 frmPrecidures = new FrmDoctorDiag1(bc, "operative_note_precidures_1", "");
            FrmDoctorDiag1 frmFinding = new FrmDoctorDiag1(bc, "operative_note_finding_1", "");

            this.SuspendLayout();
            pnOperative.SuspendLayout();
            pnLeft.SuspendLayout();
            pnLeftTop.SuspendLayout();
            pnLeftBotton.SuspendLayout();
            sCOper.SuspendLayout();
            scOperView.SuspendLayout();
            scOperAdd.SuspendLayout();
            sCFinding.SuspendLayout();
            scFinding.SuspendLayout();
            scPrecidures.SuspendLayout();

            frmPrecidures.FormBorderStyle = FormBorderStyle.None;
            frmPrecidures.TopLevel = false;
            frmPrecidures.Dock = DockStyle.Fill;
            frmPrecidures.AutoScroll = true;
            

            frmFinding.FormBorderStyle = FormBorderStyle.None;
            frmFinding.TopLevel = false;
            frmFinding.Dock = DockStyle.Fill;
            frmFinding.AutoScroll = true;
            

            scFinding.Collapsible = true;
            scFinding.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            scFinding.Location = new System.Drawing.Point(0, 21);
            scFinding.Name = "scFinding";
            //scFinding.Controls.Add(pnOperative);
            scPrecidures.Collapsible = true;
            scPrecidures.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            scPrecidures.Location = new System.Drawing.Point(0, 21);
            scPrecidures.Name = "scPrecidures";
            //scPrecidures.Controls.Add(pnLeft);
            sCFinding.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCFinding.Name = "sCFinding";
            sCFinding.Dock = System.Windows.Forms.DockStyle.Fill;
            sCFinding.Panels.Add(scFinding);
            sCFinding.Panels.Add(scPrecidures);
            sCFinding.HeaderHeight = 20;
            scPrecidures.Controls.Add(frmPrecidures);
            scFinding.Controls.Add(frmFinding);

            scOperAdd.Collapsible = true;
            scOperAdd.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Right;
            scOperAdd.Location = new System.Drawing.Point(0, 21);
            scOperAdd.Name = "scOperAdd";
            scOperAdd.Controls.Add(pnOperative);
            scOperView.Collapsible = true;
            scOperView.Dock = C1.Win.C1SplitContainer.PanelDockStyle.Left;
            scOperView.Location = new System.Drawing.Point(0, 21);
            scOperView.Name = "scOperView";
            scOperView.Controls.Add(pnLeft);
            sCOper.AutoSizeElement = C1.Framework.AutoSizeElement.Both;
            sCOper.Name = "sCOper";
            sCOper.Dock = System.Windows.Forms.DockStyle.Fill;
            sCOper.Panels.Add(scOperAdd);
            sCOper.Panels.Add(scOperView);
            sCOper.HeaderHeight = 20;
            pnLeft.Controls.Add(pnLeftBotton);
            pnLeft.Controls.Add(pnLeftTop);
            //pnLeftTop.Controls.Add();



            pnOperative.Dock = DockStyle.Fill;
            pnOperative.Name = "pnOperative";
            pnLeft.Dock = DockStyle.Fill;
            pnLeft.Name = "pnLeft";
            pnLeftTop.Dock = DockStyle.Top;
            pnLeftTop.Name = "pnLeftTop";
            pnLeftBotton.Dock = DockStyle.Fill;
            pnLeftBotton.Name = "pnLeftBotton";

            pnLeftTop.Height = 100;
            //pnLeftTop.BackColor = Color.Red;
            //pnLeftBotton.BackColor = Color.Yellow;

            //gapY += gapLine;

            lbDept = new Label();            
            bc.setControlLabel(ref lbDept, fEdit, "Department :", "lbDept", gapX, gapY);
            size = bc.MeasureString(lbDept);
            txtDept = new C1TextBox();
            txtDept.Font = fEdit;
            txtDept.Location = new System.Drawing.Point(xCol2, lbDept.Location.Y);
            txtDept.Size = new Size(380, 30);

            lbGrfHnSearch = new Label();
            bc.setControlLabel(ref lbGrfHnSearch, fEdit, "HN :", "lbGrfHnSearch", gapX, gapY);
            size = bc.MeasureString(lbGrfHnSearch);
            txtGrfHnSearch = new C1TextBox();
            bc.setControlC1TextBox(ref txtGrfHnSearch, fEdit, "txtGrfHnSearch", 80, lbGrfHnSearch.Location.X + size.Width, lbGrfHnSearch.Location.Y);
            btnNew = new C1Button();
            btnNew.Name = "btnNew";
            btnNew.Text = "New";
            btnNew.Font = fEdit;
            btnNew.Location = new System.Drawing.Point(gapX, gapY + gapLine);
            btnNew.Size = new Size(80, 40);
            btnNew.Font = fEdit;
            btnNew.Image = Resources.TableColumnProperties_small;
            btnNew.TextAlign = ContentAlignment.MiddleRight;
            btnNew.ImageAlign = ContentAlignment.MiddleLeft;

            lbWard = new Label();
            bc.setControlLabel(ref lbWard, fEdit, "Ward :", "lbWard", txtDept.Location.X + txtDept.Width + 15, lbDept.Location.Y);
            size = bc.MeasureString(lbWard);
            txtWard = new C1TextBox();
            txtWard.Font = fEdit;
            txtWard.Location = new System.Drawing.Point(lbWard.Location.X + size.Width + 5, lbDept.Location.Y);
            txtWard.Size = new Size(120, 30);

            lbDtrId = new Label();
            bc.setControlLabel(ref lbDtrId, fEdit, "Attending Staff :", "lbDtrId", txtWard.Location.X + txtWard.Width + 15, lbDept.Location.Y);
            size = bc.MeasureString(lbDtrId);
            txtDtrId = new C1TextBox();
            txtDtrId.Font = fEdit;
            txtDtrId.Location = new System.Drawing.Point(lbDtrId.Location.X + size.Width + 5, lbDept.Location.Y);
            txtDtrId.Size = new Size(100, 30);
            lbDtrName = new Label();
            bc.setControlLabel(ref lbDtrName, fEdit, "...", "lbDtrName", txtDtrId.Location.X + txtDtrId.Width + 5, gapY);

            gapY += gapLine;
            lbPttid = new Label();
            bc.setControlLabel(ref lbPttid, fEdit, "Patient :", "lbPttid", gapX, gapY);
            size = bc.MeasureString(lbPttid);
            btnPttSearch = new C1Button();
            btnPttSearch.Name = "btnPttSearch";
            btnPttSearch.Text = "...";
            btnPttSearch.Font = fEdit;
            btnPttSearch.Location = new System.Drawing.Point(lbPttid.Location.X + size.Width, lbPttid.Location.Y);
            btnPttSearch.Size = new Size(30, 25);
            btnPttSearch.Font = fEdit;
            size = bc.MeasureString(btnPttSearch);
            lbPttName = new Label();
            bc.setControlLabel(ref lbPttName, fEdit, "...", "lbPttName", btnPttSearch.Location.X + btnPttSearch.Width + 5, gapY);
            
            gapY += gapLine;
            lbDateOperative = new Label();
            bc.setControlLabel(ref lbDateOperative, fEdit, "Date of Operative :", "lbDateOperative", gapX, gapY);
            txtDateOperative = new C1DateEdit();
            txtDateOperative.AllowSpinLoop = false;
            txtDateOperative.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtDateOperative.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            txtDateOperative.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDateOperative.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDateOperative.CurrentTimeZone = false;
            txtDateOperative.DisplayFormat.CustomFormat = "dd/MM/yyyy";
            txtDateOperative.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDateOperative.DisplayFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDateOperative.EditFormat.CustomFormat = "dd/MM/yyyy";
            txtDateOperative.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            txtDateOperative.EditFormat.Inherit = ((C1.Win.C1Input.FormatInfoInheritFlags)(((((C1.Win.C1Input.FormatInfoInheritFlags.NullText | C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimStart)
            | C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd)
            | C1.Win.C1Input.FormatInfoInheritFlags.CalendarType)));
            txtDateOperative.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtDateOperative.GMTOffset = System.TimeSpan.Parse("00:00:00");
            txtDateOperative.ImagePadding = new System.Windows.Forms.Padding(0);
            size = bc.MeasureString(lbDateOperative);
            txtDateOperative.Location = new System.Drawing.Point(lbDateOperative.Location.X + size.Width + 15, lbDateOperative.Location.Y);
            txtDateOperative.Name = "txtDateOperative";
            txtDateOperative.Size = new System.Drawing.Size(150, 20);
            txtDateOperative.TabIndex = 12;
            txtDateOperative.Tag = null;
            theme1.SetTheme(this.txtDateOperative, "(default)");
            txtDateOperative.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            txtDateOperative.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;

            lbTimeInsion = new Label();
            bc.setControlLabel(ref lbTimeInsion, fEdit, "Insion Time :", "lbTimeInsion", txtDateOperative.Location.X + txtDateOperative.Width + 15, lbDateOperative.Location.Y);
            size = bc.MeasureString(lbTimeInsion);
            txtTimeInsion = new C1TextBox();
            txtTimeInsion.Font = fEdit;
            txtTimeInsion.Location = new System.Drawing.Point(lbTimeInsion.Location.X + size.Width + 5, lbDateOperative.Location.Y);
            txtTimeInsion.EditMask = "00:00";

            txtTimeInsion.Size = new Size(80, 30);
            lbTimeFinish = new Label();
            bc.setControlLabel(ref lbTimeFinish, fEdit, "Finish Time :", "lbTimeFinish", txtTimeInsion.Location.X + txtTimeInsion.Width + 15, lbDateOperative.Location.Y);
            size = bc.MeasureString(lbTimeFinish);
            txtTimeFinish = new C1TextBox();
            txtTimeFinish.Font = fEdit;
            txtTimeFinish.Location = new System.Drawing.Point(lbTimeFinish.Location.X + size.Width + 5, lbDateOperative.Location.Y);
            txtTimeFinish.Size = new Size(80, 30);
            txtTimeFinish.EditMask = "00:00";
            lbTotalTime = new Label();
            bc.setControlLabel(ref lbTotalTime, fEdit, "Total Time :", "lbTotalTime", txtTimeFinish.Location.X + txtTimeFinish.Width + 15, lbDateOperative.Location.Y);
            size = bc.MeasureString(lbTotalTime);
            txtTotalTime = new C1TextBox();
            txtTotalTime.Font = fEdit;
            txtTotalTime.Location = new System.Drawing.Point(lbTotalTime.Location.X + size.Width, lbDateOperative.Location.Y);
            txtTotalTime.Size = new Size(50, 30);
            txtTotalTime.Name = "txtTotalTime";

            btnSave = new C1Button();
            btnSave.Name = "btnSave";
            btnSave.Text = "Save ";
            btnSave.Font = fEdit;
            btnSave.Location = new System.Drawing.Point(txtTotalTime.Location.X + txtTotalTime.Width + 5, txtTotalTime.Location.Y);
            btnSave.Size = new Size(90, 35);
            btnSave.Font = fEdit;
            btnSave.Image = Resources.Save_large;
            btnSave.TextAlign = ContentAlignment.MiddleRight;
            btnSave.ImageAlign = ContentAlignment.MiddleLeft;
            btnPrint = new C1Button();
            btnPrint.Name = "btnPrint";
            btnPrint.Text = "Print ";
            btnPrint.Font = fEdit;
            btnPrint.Location = new System.Drawing.Point(btnSave.Location.X + btnSave.Width + 35, txtTotalTime.Location.Y);
            btnPrint.Size = new Size(90, 35);
            btnPrint.Font = fEdit;
            btnPrint.Image = Resources.printer_blue24;
            btnPrint.TextAlign = ContentAlignment.MiddleRight;
            btnPrint.ImageAlign = ContentAlignment.MiddleLeft;

            gapY += gapLine;
            lbTitle = new Label();
            lbTitle.Text = "Operative Note ";
            lbTitle.Font = fEdit1B;
            size = bc.MeasureString(lbTitle);
            lbTitle.Location = new System.Drawing.Point((scrW / 2)/2 + (size.Width / 2), gapY);
            lbTitle.AutoSize = true;

            gapY += gapLine;
            lbSurgeon = new Label();
            bc.setControlLabel(ref lbSurgeon, fEdit, "Surgeon ", "lbSurgeon", gapX, gapY);
            //size = bc.MeasureString(lbSurgeon);
            gapY += gapLine;
            lbSurgeon1 = new Label();
            bc.setControlLabel(ref lbSurgeon1, fEdit, "1 :", "lbSurgeon1", gapX, gapY);
            size = bc.MeasureString(lbSurgeon1);
            txtSurgeon1 = new C1TextBox();
            txtSurgeon1.Font = fEdit;
            txtSurgeon1.Location = new System.Drawing.Point(lbSurgeon1.Location.X + size.Width + 5, lbSurgeon1.Location.Y);
            txtSurgeon1.Size = new Size(80, 30);
            txtSurgeon1.Name = "txtSurgeon1";
            lbSurgeonName1 = new Label();
            bc.setControlLabel(ref lbSurgeonName1, fEdit, "...", "lbSurgeonName1", txtSurgeon1.Location.X + txtSurgeon1.Width + 5, lbSurgeon1.Location.Y);

            lbAssistant = new Label();
            bc.setControlLabel(ref lbAssistant, fEdit, "Assistant ", "lbAssistant", xCol3, lbSurgeon.Location.Y);
            size = bc.MeasureString(lbAssistant);
            lbAssistant1 = new Label();
            bc.setControlLabel(ref lbAssistant1, fEdit, "1 :", "lbAssistant1", xCol3, lbSurgeon1.Location.Y);
            lbAssistant1.AutoSize = true;
            size = bc.MeasureString(lbAssistant1);
            txtAssistant1 = new C1TextBox();
            txtAssistant1.Font = fEdit;
            txtAssistant1.Location = new System.Drawing.Point(lbAssistant1.Location.X + size.Width + 5, lbSurgeon1.Location.Y);
            txtAssistant1.Size = new Size(80, 30);
            txtAssistant1.Name = "txtAssistant1";
            lbAssistantName1 = new Label();
            bc.setControlLabel(ref lbAssistantName1, fEdit, "...", "lbAssistantName1", txtAssistant1.Location.X + txtAssistant1.Width + 5, lbSurgeon1.Location.Y);

            lbScrubNurse = new Label();
            bc.setControlLabel(ref lbScrubNurse, fEdit, "Scrub Nurse ", "lbScrubNurse", xCol4, lbSurgeon.Location.Y);
            size = bc.MeasureString(lbScrubNurse);
            lbScrubNurse1 = new Label();
            lbScrubNurse1.Text = "1 :";
            lbScrubNurse1.Font = fEdit;
            lbScrubNurse1.Location = new System.Drawing.Point(xCol4, lbSurgeon1.Location.Y);
            lbScrubNurse1.AutoSize = true;
            size = bc.MeasureString(lbScrubNurse1);
            txtScrubNurse1 = new C1TextBox();
            txtScrubNurse1.Font = fEdit;
            txtScrubNurse1.Location = new System.Drawing.Point(lbScrubNurse1.Location.X + size.Width + 5, lbSurgeon1.Location.Y);
            txtScrubNurse1.Size = new Size(80, 30);
            txtScrubNurse1.Name = "txtScrubNurse1";
            lbScrubNurseName1 = new Label();
            lbScrubNurseName1.Text = "...";
            lbScrubNurseName1.Font = fEdit;
            lbScrubNurseName1.Location = new System.Drawing.Point(txtScrubNurse1.Location.X + txtScrubNurse1.Width + 5, lbSurgeon1.Location.Y);
            lbScrubNurseName1.AutoSize = true;
            lbScrubNurseName1.Name = "lbScrubNurseName1";

            lbCircuNurse = new Label();
            lbCircuNurse.Text = "Circulation Nurse ";
            lbCircuNurse.Font = fEdit;
            lbCircuNurse.Location = new System.Drawing.Point(xCol5, lbSurgeon.Location.Y);
            lbCircuNurse.AutoSize = true;
            size = bc.MeasureString(lbCircuNurse);
            lbCircuNurse1 = new Label();
            lbCircuNurse1.Text = "1 :";
            lbCircuNurse1.Font = fEdit;
            lbCircuNurse1.Location = new System.Drawing.Point(xCol5, lbSurgeon1.Location.Y);
            lbCircuNurse1.AutoSize = true;
            size = bc.MeasureString(lbCircuNurse1);
            txtCircuNurse1 = new C1TextBox();
            txtCircuNurse1.Font = fEdit;
            txtCircuNurse1.Location = new System.Drawing.Point(lbCircuNurse1.Location.X + size.Width + 5, lbSurgeon1.Location.Y);
            txtCircuNurse1.Size = new Size(80, 30);
            txtCircuNurse1.Name = "txtCircuNurse1";
            lbCircuNurseName1 = new Label();
            lbCircuNurseName1.Text = "...";
            lbCircuNurseName1.Font = fEdit;
            lbCircuNurseName1.Location = new System.Drawing.Point(txtCircuNurse1.Location.X + txtCircuNurse1.Width + 5, lbSurgeon1.Location.Y);
            lbCircuNurseName1.AutoSize = true;
            lbCircuNurseName1.Name = "lbCircuNurseName1";

            gapY += gapLine;
            lbSurgeon2 = new Label();
            lbSurgeon2.Text = "2 :";
            lbSurgeon2.Font = fEdit;
            lbSurgeon2.Location = new System.Drawing.Point(lbSurgeon1.Location.X, gapY);
            lbSurgeon2.AutoSize = true;
            lbSurgeon2.Name = "lbSurgeon2";
            size = bc.MeasureString(lbSurgeon2);
            txtSurgeon2 = new C1TextBox();
            txtSurgeon2.Font = fEdit;
            txtSurgeon2.Location = new System.Drawing.Point(lbSurgeon2.Location.X + size.Width + 5, lbSurgeon2.Location.Y);
            txtSurgeon2.Size = new Size(80, 30);
            txtSurgeon2.Name = "txtSurgeon2";
            lbSurgeonName2 = new Label();
            lbSurgeonName2.Text = "...";
            lbSurgeonName2.Font = fEdit;
            lbSurgeonName2.Location = new System.Drawing.Point(txtSurgeon2.Location.X + txtSurgeon2.Width + 5, lbSurgeon2.Location.Y);
            lbSurgeonName2.AutoSize = true;
            lbSurgeonName2.Name = "lbSurgeonName2";
            lbAssistant2 = new Label();
            lbAssistant2.Text = "2 :";
            lbAssistant2.Font = fEdit;
            lbAssistant2.Location = new System.Drawing.Point(lbAssistant1.Location.X , lbSurgeon2.Location.Y);
            lbAssistant2.AutoSize = true;
            size = bc.MeasureString(lbAssistant2);
            txtAssistant2 = new C1TextBox();
            txtAssistant2.Font = fEdit;
            txtAssistant2.Location = new System.Drawing.Point(lbAssistant2.Location.X + size.Width + 5, lbSurgeon2.Location.Y);
            txtAssistant2.Size = new Size(80, 30);
            txtAssistant2.Name = "txtAssistant2";
            lbAssistantName2 = new Label();
            lbAssistantName2.Text = "...";
            lbAssistantName2.Font = fEdit;
            lbAssistantName2.Location = new System.Drawing.Point(txtAssistant2.Location.X + txtAssistant2.Width + 5, lbSurgeon2.Location.Y);
            lbAssistantName2.AutoSize = true;
            lbAssistantName2.Name = "lbAssistantName2";

            lbScrubNurse2 = new Label();
            lbScrubNurse2.Text = "2 :";
            lbScrubNurse2.Font = fEdit;
            lbScrubNurse2.Location = new System.Drawing.Point(lbScrubNurse1.Location.X, lbSurgeon2.Location.Y);
            lbScrubNurse2.AutoSize = true;
            size = bc.MeasureString(lbScrubNurse2);
            txtScrubNurse2 = new C1TextBox();
            txtScrubNurse2.Font = fEdit;
            txtScrubNurse2.Location = new System.Drawing.Point(lbScrubNurse2.Location.X + size.Width + 5, lbSurgeon2.Location.Y);
            txtScrubNurse2.Size = new Size(80, 30);
            txtScrubNurse2.Name = "txtScrubNurse2";
            lbScrubNurseName2 = new Label();
            lbScrubNurseName2.Text = "...";
            lbScrubNurseName2.Font = fEdit;
            lbScrubNurseName2.Location = new System.Drawing.Point(txtScrubNurse2.Location.X + txtScrubNurse2.Width + 5, lbSurgeon2.Location.Y);
            lbScrubNurseName2.AutoSize = true;
            lbScrubNurseName2.Name = "lbScrubNurseName2";

            lbCircuNurse2 = new Label();
            lbCircuNurse2.Text = "2 :";
            lbCircuNurse2.Font = fEdit;
            lbCircuNurse2.Location = new System.Drawing.Point(xCol5, lbSurgeon2.Location.Y);
            lbCircuNurse2.AutoSize = true;
            size = bc.MeasureString(lbCircuNurse2);
            txtCircuNurse2 = new C1TextBox();
            txtCircuNurse2.Font = fEdit;
            txtCircuNurse2.Location = new System.Drawing.Point(lbCircuNurse2.Location.X + size.Width + 5, lbSurgeon2.Location.Y);
            txtCircuNurse2.Size = new Size(80, 30);
            txtCircuNurse2.Name = "txtCircuNurse2";
            lbCircuNurseName2 = new Label();
            lbCircuNurseName2.Text = "...";
            lbCircuNurseName2.Font = fEdit;
            lbCircuNurseName2.Location = new System.Drawing.Point(txtCircuNurse2.Location.X + txtCircuNurse2.Width + 5, lbSurgeon2.Location.Y);
            lbCircuNurseName2.AutoSize = true;
            lbCircuNurseName2.Name = "lbCircuNurseName2";

            //gapY += gapLine;
            //lbSurgeon3 = new Label();
            //lbSurgeon3.Text = "3 :";
            //lbSurgeon3.Font = fEdit;
            //lbSurgeon3.Location = new System.Drawing.Point(lbSurgeon1.Location.X , gapY);
            //lbSurgeon3.AutoSize = true;
            //lbSurgeon3.Name = "lbSurgeon3";
            //size = bc.MeasureString(lbSurgeon2);
            //txtSurgeon3 = new C1TextBox();
            //txtSurgeon3.Font = fEdit;
            //txtSurgeon3.Location = new System.Drawing.Point(lbSurgeon3.Location.X + size.Width + 5, lbSurgeon3.Location.Y);
            //txtSurgeon3.Size = new Size(80, 30);
            //txtSurgeon3.Name = "txtSurgeon3";
            //lbSurgeonName3 = new Label();
            //lbSurgeonName3.Text = "...";
            //lbSurgeonName3.Font = fEdit;
            //lbSurgeonName3.Location = new System.Drawing.Point(txtSurgeon3.Location.X + txtSurgeon3.Width + 15, lbSurgeon3.Location.Y);
            //lbSurgeonName3.AutoSize = true;
            //lbSurgeonName3.Name = "lbSurgeonName3";
            //lbAssistant3 = new Label();
            //lbAssistant3.Text = "3 :";
            //lbAssistant3.Font = fEdit;
            //lbAssistant3.Location = new System.Drawing.Point(lbAssistant1.Location.X, lbSurgeon3.Location.Y);
            //lbAssistant3.AutoSize = true;
            //size = bc.MeasureString(lbAssistant3);
            //txtAssistant3 = new C1TextBox();
            //txtAssistant3.Font = fEdit;
            //txtAssistant3.Location = new System.Drawing.Point(lbAssistant3.Location.X + size.Width + 5, lbSurgeon3.Location.Y);
            //txtAssistant3.Size = new Size(80, 30);
            //txtAssistant3.Name = "txtAssistant3";
            //lbAssistantName3 = new Label();
            //lbAssistantName3.Text = "...";
            //lbAssistantName3.Font = fEdit;
            //lbAssistantName3.Location = new System.Drawing.Point(txtAssistant3.Location.X + txtAssistant3.Width + 15, lbSurgeon3.Location.Y);
            //lbAssistantName3.AutoSize = true;
            //lbAssistantName3.Name = "lbAssistantName3";

            //lbScrubNurse3 = new Label();
            //lbScrubNurse3.Text = "3 :";
            //lbScrubNurse3.Font = fEdit;
            //lbScrubNurse3.Location = new System.Drawing.Point(lbScrubNurse1.Location.X, lbSurgeon3.Location.Y);
            //lbScrubNurse3.AutoSize = true;
            //size = bc.MeasureString(lbScrubNurse3);
            //txtScrubNurse3 = new C1TextBox();
            //txtScrubNurse3.Font = fEdit;
            //txtScrubNurse3.Location = new System.Drawing.Point(lbScrubNurse3.Location.X + size.Width + 5, lbSurgeon3.Location.Y);
            //txtScrubNurse3.Size = new Size(80, 30);
            //txtScrubNurse3.Name = "txtScrubNurse3";
            //lbScrubNurseName3 = new Label();
            //lbScrubNurseName3.Text = "...";
            //lbScrubNurseName3.Font = fEdit;
            //lbScrubNurseName3.Location = new System.Drawing.Point(txtScrubNurse3.Location.X + txtScrubNurse3.Width + 15, lbSurgeon3.Location.Y);
            //lbScrubNurseName3.AutoSize = true;
            //lbScrubNurseName3.Name = "lbScrubNurseName3";

            //lbCircuNurse3 = new Label();
            //lbCircuNurse3.Text = "3 :";
            //lbCircuNurse3.Font = fEdit;
            //lbCircuNurse3.Location = new System.Drawing.Point(xCol5, lbSurgeon3.Location.Y);
            //lbCircuNurse3.AutoSize = true;
            //size = bc.MeasureString(lbCircuNurse3);
            //txtCircuNurse3 = new C1TextBox();
            //txtCircuNurse3.Font = fEdit;
            //txtCircuNurse3.Location = new System.Drawing.Point(lbCircuNurse3.Location.X + size.Width + 5, lbSurgeon3.Location.Y);
            //txtCircuNurse3.Size = new Size(80, 30);
            //txtCircuNurse3.Name = "txtCircuNurse3";
            //lbCircuNurseName3 = new Label();
            //lbCircuNurseName3.Text = "...";
            //lbCircuNurseName3.Font = fEdit;
            //lbCircuNurseName3.Location = new System.Drawing.Point(txtCircuNurse3.Location.X + txtCircuNurse3.Width + 15, lbSurgeon3.Location.Y);
            //lbCircuNurseName3.AutoSize = true;
            //lbCircuNurseName3.Name = "lbCircuNurseName3";

            //gapY += gapLine;
            //lbSurgeon4 = new Label();
            //lbSurgeon4.Text = "4 :";
            //lbSurgeon4.Font = fEdit;
            //lbSurgeon4.Location = new System.Drawing.Point(lbSurgeon1.Location.X, gapY);
            //lbSurgeon4.AutoSize = true;
            //lbSurgeon4.Name = "lbSurgeon4";
            //size = bc.MeasureString(lbSurgeon4);
            //txtSurgeon4 = new C1TextBox();
            //txtSurgeon4.Font = fEdit;
            //txtSurgeon4.Location = new System.Drawing.Point(lbSurgeon4.Location.X + size.Width + 5, lbSurgeon4.Location.Y);
            //txtSurgeon4.Size = new Size(80, 30);
            //txtSurgeon4.Name = "txtSurgeon4";
            //lbSurgeonName4 = new Label();
            //lbSurgeonName4.Text = "...";
            //lbSurgeonName4.Font = fEdit;
            //lbSurgeonName4.Location = new System.Drawing.Point(txtSurgeon4.Location.X + txtSurgeon4.Width + 15, lbSurgeon4.Location.Y);
            //lbSurgeonName4.AutoSize = true;
            //lbSurgeonName4.Name = "lbSurgeonName4";
            //lbAssistant4 = new Label();
            //lbAssistant4.Text = "4 :";
            //lbAssistant4.Font = fEdit;
            //lbAssistant4.Location = new System.Drawing.Point(lbAssistant1.Location.X, lbSurgeon4.Location.Y);
            //lbAssistant4.AutoSize = true;
            //size = bc.MeasureString(lbAssistant4);
            //txtAssistant4 = new C1TextBox();
            //txtAssistant4.Font = fEdit;
            //txtAssistant4.Location = new System.Drawing.Point(lbAssistant4.Location.X + size.Width + 5, lbSurgeon4.Location.Y);
            //txtAssistant4.Size = new Size(80, 30);
            //txtAssistant4.Name = "txtAssistant4";
            //lbAssistantName4 = new Label();
            //lbAssistantName4.Text = "...";
            //lbAssistantName4.Font = fEdit;
            //lbAssistantName4.Location = new System.Drawing.Point(txtAssistant4.Location.X + txtAssistant4.Width + 15, lbSurgeon4.Location.Y);
            //lbAssistantName4.AutoSize = true;
            //lbAssistantName4.Name = "lbAssistantName4";

            //lbScrubNurse4 = new Label();
            //lbScrubNurse4.Text = "4 :";
            //lbScrubNurse4.Font = fEdit;
            //lbScrubNurse4.Location = new System.Drawing.Point(lbScrubNurse1.Location.X, lbSurgeon4.Location.Y);
            //lbScrubNurse4.AutoSize = true;
            //size = bc.MeasureString(lbScrubNurse4);
            //txtScrubNurse4 = new C1TextBox();
            //txtScrubNurse4.Font = fEdit;
            //txtScrubNurse4.Location = new System.Drawing.Point(lbScrubNurse4.Location.X + size.Width + 5, lbSurgeon4.Location.Y);
            //txtScrubNurse4.Size = new Size(80, 30);
            //txtScrubNurse4.Name = "txtScrubNurse4";
            //lbScrubNurseName4 = new Label();
            //lbScrubNurseName4.Text = "...";
            //lbScrubNurseName4.Font = fEdit;
            //lbScrubNurseName4.Location = new System.Drawing.Point(txtScrubNurse4.Location.X + txtScrubNurse4.Width + 15, lbSurgeon4.Location.Y);
            //lbScrubNurseName4.AutoSize = true;
            //lbScrubNurseName4.Name = "lbScrubNurseName4";

            //lbCircuNurse4 = new Label();
            //lbCircuNurse4.Text = "4 :";
            //lbCircuNurse4.Font = fEdit;
            //lbCircuNurse4.Location = new System.Drawing.Point(xCol5, lbSurgeon4.Location.Y);
            //lbCircuNurse4.AutoSize = true;
            //size = bc.MeasureString(lbCircuNurse4);
            //txtCircuNurse4 = new C1TextBox();
            //txtCircuNurse4.Font = fEdit;
            //txtCircuNurse4.Location = new System.Drawing.Point(lbCircuNurse4.Location.X + size.Width + 5, lbSurgeon4.Location.Y);
            //txtCircuNurse4.Size = new Size(80, 30);
            //txtCircuNurse4.Name = "txtCircuNurse4";
            //lbCircuNurseName4 = new Label();
            //lbCircuNurseName4.Text = "...";
            //lbCircuNurseName4.Font = fEdit;
            //lbCircuNurseName4.Location = new System.Drawing.Point(txtCircuNurse4.Location.X + txtCircuNurse4.Width + 15, lbSurgeon4.Location.Y);
            //lbCircuNurseName4.AutoSize = true;
            //lbCircuNurseName4.Name = "lbCircuNurseName4";

            gapY += gapLine;
            lbPerfusionist = new Label();
            lbPerfusionist.Text = "Perfusionist ";
            lbPerfusionist.Font = fEdit;
            lbPerfusionist.Location = new System.Drawing.Point(gapX, gapY);
            lbPerfusionist.AutoSize = true;
            //size = bc.MeasureString(lbSurgeon);
            gapY += gapLine;
            lbPerfusionist1 = new Label();
            lbPerfusionist1.Text = "1 :";
            lbPerfusionist1.Font = fEdit;
            lbPerfusionist1.Location = new System.Drawing.Point(gapX, gapY);
            lbPerfusionist1.AutoSize = true;
            size = bc.MeasureString(lbPerfusionist1);
            txtPerfusionist1 = new C1TextBox();
            txtPerfusionist1.Font = fEdit;
            txtPerfusionist1.Location = new System.Drawing.Point(lbPerfusionist1.Location.X + size.Width + 5, lbPerfusionist1.Location.Y);
            txtPerfusionist1.Size = new Size(80, 30);
            txtPerfusionist1.Name = "txtPerfusionist1";
            lbPerfusionistName1 = new Label();
            lbPerfusionistName1.Text = "...";
            lbPerfusionistName1.Font = fEdit;
            lbPerfusionistName1.Location = new System.Drawing.Point(txtPerfusionist1.Location.X + txtPerfusionist1.Width + 5, lbPerfusionist1.Location.Y);
            lbPerfusionistName1.AutoSize = true;
            lbPerfusionistName1.Name = "lbPerfusionistName1";

            gapY += gapLine;
            lbPerfusionist2 = new Label();
            lbPerfusionist2.Text = "2 :";
            lbPerfusionist2.Font = fEdit;
            lbPerfusionist2.Location = new System.Drawing.Point(lbPerfusionist1.Location.X, gapY);
            lbPerfusionist2.AutoSize = true;
            lbPerfusionist2.Name = "lbPerfusionist2";
            size = bc.MeasureString(lbSurgeon2);
            txtPerfusionist2 = new C1TextBox();
            txtPerfusionist2.Font = fEdit;
            txtPerfusionist2.Location = new System.Drawing.Point(lbPerfusionist2.Location.X + size.Width + 5, lbPerfusionist2.Location.Y);
            txtPerfusionist2.Size = new Size(80, 30);
            txtPerfusionist2.Name = "txtPerfusionist2";
            lbPerfusionistName2 = new Label();
            lbPerfusionistName2.Text = "...";
            lbPerfusionistName2.Font = fEdit;
            lbPerfusionistName2.Location = new System.Drawing.Point(txtPerfusionist2.Location.X + txtPerfusionist2.Width + 5, lbPerfusionist2.Location.Y);
            lbPerfusionistName2.AutoSize = true;
            lbPerfusionistName2.Name = "lbPerfusionistName2";

            lbAnesthetist = new Label();
            lbAnesthetist.Text = "Anesthetist ";
            lbAnesthetist.Font = fEdit;
            lbAnesthetist.Location = new System.Drawing.Point(xCol3, lbPerfusionist.Location.Y);
            lbAnesthetist.AutoSize = true;
            size = bc.MeasureString(lbAnesthetist);
            lbAnesthetist1 = new Label();
            lbAnesthetist1.Text = "1 :";
            lbAnesthetist1.Font = fEdit;
            lbAnesthetist1.Location = new System.Drawing.Point(xCol3, lbPerfusionist1.Location.Y);
            lbAnesthetist1.AutoSize = true;
            size = bc.MeasureString(lbAnesthetist1);
            txtAnesthetist1 = new C1TextBox();
            txtAnesthetist1.Font = fEdit;
            txtAnesthetist1.Location = new System.Drawing.Point(lbAssistant1.Location.X + size.Width + 5, lbPerfusionist1.Location.Y);
            txtAnesthetist1.Size = new Size(80, 30);
            txtAnesthetist1.Name = "txtAnesthetist1";
            lbAnesthetistName1 = new Label();
            lbAnesthetistName1.Text = "...";
            lbAnesthetistName1.Font = fEdit;
            lbAnesthetistName1.Location = new System.Drawing.Point(txtAnesthetist1.Location.X + txtAnesthetist1.Width + 5, lbPerfusionist1.Location.Y);
            lbAnesthetistName1.AutoSize = true;
            lbAnesthetistName1.Name = "lbAnesthetistName1";

            lbAnesthetistAssist = new Label();
            bc.setControlLabel(ref lbAnesthetistAssist, fEdit, "Anesthetist assistant", "lbAnesthetistAssist", xCol4, lbPerfusionist.Location.Y);
            size = bc.MeasureString(lbAnesthetistAssist);
            lbAnesthetistAssist1 = new Label();
            bc.setControlLabel(ref lbAnesthetistAssist1, fEdit, "1 :", "lbAnesthetistAssist1", xCol4, lbPerfusionist1.Location.Y);
            size = bc.MeasureString(lbAnesthetist1);
            txtAnesthetistAssist1 = new C1TextBox();
            txtAnesthetistAssist1.Font = fEdit;
            txtAnesthetistAssist1.Location = new System.Drawing.Point(lbAnesthetistAssist1.Location.X + size.Width + 5, lbPerfusionist1.Location.Y);
            txtAnesthetistAssist1.Size = new Size(80, 30);
            txtAnesthetistAssist1.Name = "txtAnesthetistAssist1";
            lbAnesthetistAssistName1 = new Label();
            bc.setControlLabel(ref lbAnesthetistAssistName1, fEdit, "...", "lbAnesthetistAssistName1", txtAnesthetistAssist1.Location.X + txtAnesthetistAssist1.Width + 5, lbPerfusionist1.Location.Y);

            lbAnesthetistAssist2 = new Label();
            bc.setControlLabel(ref lbAnesthetistAssist2, fEdit, "2 :", "lbAnesthetistAssist2", xCol4, lbPerfusionist2.Location.Y);
            size = bc.MeasureString(lbAnesthetist1);
            txtAnesthetistAssist2 = new C1TextBox();
            txtAnesthetistAssist2.Font = fEdit;
            txtAnesthetistAssist2.Location = new System.Drawing.Point(lbAnesthetistAssist1.Location.X + size.Width + 5, lbPerfusionist2.Location.Y);
            txtAnesthetistAssist2.Size = new Size(80, 30);
            txtAnesthetistAssist2.Name = "txtAnesthetistAssist2";
            lbAnesthetistAssistName2 = new Label();
            bc.setControlLabel(ref lbAnesthetistAssistName2, fEdit, "...", "lbAnesthetistAssistName2", txtAnesthetistAssist1.Location.X + txtAnesthetistAssist1.Width + 5, lbPerfusionist2.Location.Y);

            gapY += gapLine;
            lbAnesthetistTechni = new Label();
            bc.setControlLabel(ref lbAnesthetistTechni, fEdit, "Anesthetist Technique", "lbAnesthetistTechni", gapX+100, gapY);
            size = bc.MeasureString(lbAnesthetistTechni);
            txtAnesthetistTechni1 = new C1TextBox();
            txtAnesthetistTechni1.Font = fEdit;
            txtAnesthetistTechni1.Location = new System.Drawing.Point(lbAnesthetistTechni.Location.X + size.Width + 5, lbAnesthetistTechni.Location.Y);
            txtAnesthetistTechni1.Size = new Size(300, 30);
            txtAnesthetistTechni1.Name = "txtAnesthetistTechni1";

            lbTimeAnthe = new Label();
            bc.setControlLabel(ref lbTimeAnthe, fEdit, "Anthesia Time :", "lbTimeAnthe", txtAnesthetistTechni1.Location.X + txtAnesthetistTechni1.Width + 15, lbAnesthetistTechni.Location.Y);
            size = bc.MeasureString(lbTimeAnthe);
            txtTimeAnthe = new C1TextBox();
            txtTimeAnthe.Font = fEdit;
            txtTimeAnthe.Location = new System.Drawing.Point(lbTimeAnthe.Location.X + size.Width + 5, lbAnesthetistTechni.Location.Y);
            txtTimeAnthe.Size = new Size(80, 30);
            txtTimeAnthe.Name = "txtTimeAnthe";
            txtTimeAnthe.EditMask = "00:00";
            lbTimeFinishAnthe = new Label();
            bc.setControlLabel(ref lbTimeFinishAnthe, fEdit, "Finish :", "lbTimeFinishAnthe", txtTimeAnthe.Location.X + txtTimeAnthe.Width + 15, lbAnesthetistTechni.Location.Y);
            size = bc.MeasureString(lbTimeFinishAnthe);
            txtTimeFinishAnthe = new C1TextBox();
            txtTimeFinishAnthe.Font = fEdit;
            txtTimeFinishAnthe.Location = new System.Drawing.Point(lbTimeFinishAnthe.Location.X + size.Width + 5, lbAnesthetistTechni.Location.Y);
            txtTimeFinishAnthe.Size = new Size(80, 30);
            txtTimeFinishAnthe.Name = "txtTimeFinishAnthe";
            txtTimeFinishAnthe.EditMask = "00:00";
            lbTotalTimeAnthe = new Label();
            bc.setControlLabel(ref lbTotalTimeAnthe, fEdit, "Total :", "lbTotalTimeAnthe", txtTimeFinishAnthe.Location.X + txtTimeFinishAnthe.Width + 15, lbAnesthetistTechni.Location.Y);
            size = bc.MeasureString(lbTotalTimeAnthe);
            txtTotalTimeAnthe = new C1TextBox();
            txtTotalTimeAnthe.Font = fEdit;
            txtTotalTimeAnthe.Location = new System.Drawing.Point(lbTotalTimeAnthe.Location.X + size.Width, lbAnesthetistTechni.Location.Y);
            txtTotalTimeAnthe.Size = new Size(50, 30);
            txtTotalTimeAnthe.Name = "txtTotalTimeAnthe";

            int widthoperation = 500;
            gapY += gapLine;
            lbPreOperation = new Label();
            bc.setControlLabel(ref lbPreOperation, fEdit, "Pre Operation :", "lbPreOperation", gapX, gapY);
            size = bc.MeasureString(lbPreOperation);
            txtPreOperation = new C1TextBox();
            txtPreOperation.Font = fEdit;
            txtPreOperation.Location = new System.Drawing.Point(lbPreOperation.Location.X + size.Width + 5, lbPreOperation.Location.Y);
            txtPreOperation.Size = new Size(widthoperation, 30);
            txtPreOperation.Name = "txtPreOperation";

            //lbFinding = new Label();
            //bc.setControlLabel(ref lbFinding, fEdit, "Finding :", "lbFinding", xCol4, gapY);
            //size = bc.MeasureString(lbFinding);
            //txtFinding = new C1TextBox();
            //txtFinding.Font = fEdit;
            //txtFinding.Location = new System.Drawing.Point(lbFinding.Location.X + size.Width + 5, lbFinding.Location.Y);
            //txtFinding.Size = new Size(300, 30);
            //txtFinding.Name = "txtFinding";

            //gapY += gapLine;
            lbPostOperation = new Label();
            bc.setControlLabel(ref lbPostOperation, fEdit, "Post Operation :", "lbPostOperation", xCol4, gapY);
            size = bc.MeasureString(lbPostOperation);
            txtPostOperation = new C1TextBox();
            txtPostOperation.Font = fEdit;
            txtPostOperation.Location = new System.Drawing.Point(lbPostOperation.Location.X + size.Width + 5, lbPostOperation.Location.Y);
            txtPostOperation.Size = new Size(widthoperation, 30);
            txtPostOperation.Name = "txtPostOperation";

            gapY += gapLine;
            lbOperation1 = new Label();
            bc.setControlLabel(ref lbOperation1, fEdit, "Operation1 :", "lbOperation1", gapX, gapY);
            size = bc.MeasureString(lbOperation1);
            txtOperation1 = new C1TextBox();
            txtOperation1.Font = fEdit;
            txtOperation1.Location = new System.Drawing.Point(lbOperation1.Location.X + size.Width + 5, lbOperation1.Location.Y);
            txtOperation1.Size = new Size(widthoperation, 30);
            txtOperation1.Name = "txtOperation1";
            
            lbOperation3 = new Label();
            bc.setControlLabel(ref lbOperation3, fEdit, "Operation3 :", "lbOperation3", xCol4, gapY);
            size = bc.MeasureString(lbOperation3);
            txtOperation3 = new C1TextBox();
            txtOperation3.Font = fEdit;
            txtOperation3.Location = new System.Drawing.Point(lbOperation3.Location.X + size.Width + 5, lbOperation3.Location.Y);
            txtOperation3.Size = new Size(widthoperation, 30);
            txtOperation3.Name = "txtOperation3";

            gapY += gapLine;
            lbOperation2 = new Label();
            bc.setControlLabel(ref lbOperation2, fEdit, "Operation2 :", "lbOperation2", gapX, gapY);
            size = bc.MeasureString(lbOperation2);
            txtOperation2 = new C1TextBox();
            txtOperation2.Font = fEdit;
            txtOperation2.Location = new System.Drawing.Point(lbOperation2.Location.X + size.Width + 5, lbOperation2.Location.Y);
            txtOperation2.Size = new Size(widthoperation, 30);
            txtOperation2.Name = "txtOperation2";

            lbOperation4 = new Label();
            bc.setControlLabel(ref lbOperation4, fEdit, "Operation4 :", "lbOperation4", xCol4, gapY);
            size = bc.MeasureString(lbOperation4);
            txtOperation4 = new C1TextBox();
            txtOperation4.Font = fEdit;
            txtOperation4.Location = new System.Drawing.Point(lbOperation4.Location.X + size.Width + 5, lbOperation4.Location.Y);
            txtOperation4.Size = new Size(widthoperation, 30);
            txtOperation4.Name = "txtOperation4";

            gapY += gapLine;
            lbComplication = new Label();
            bc.setControlLabel(ref lbComplication, fEdit, "Complication ", "lbComplication", gapX, gapY);
            size = bc.MeasureString(lbComplication);
            pnComplication = new Panel();
            pnComplication.Location = new System.Drawing.Point(lbComplication.Location.X + size.Width + 5, lbComplication.Location.Y-8);
            pnComplication.Size = new Size(130, 40);
            chkComplicationNo = new RadioButton();
            chkComplicationNo.Name = "chkComplicationNo";
            chkComplicationNo.Font = fEdit;
            chkComplicationNo.Text = "NO";
            size = bc.MeasureString(chkComplicationNo);
            chkComplicationNo.Location = new System.Drawing.Point(10, 10);
            chkComplicationNo.Width = size.Width+20;
            chkComplicationYes = new RadioButton();
            chkComplicationYes.Name = "chkComplicationYes";
            chkComplicationYes.Font = fEdit;
            chkComplicationYes.Text = "YES";
            chkComplicationYes.Location = new System.Drawing.Point(size.Width +40, 10);
            size = bc.MeasureString(chkComplicationYes);
            chkComplicationYes.Width = size.Width + 20;
            pnComplication.Controls.Add(chkComplicationNo);
            pnComplication.Controls.Add(chkComplicationYes);
            txtComplication = new C1TextBox();
            txtComplication.Font = fEdit;
            txtComplication.Location = new System.Drawing.Point(pnComplication.Location.X + pnComplication.Width + 5, lbComplication.Location.Y);
            txtComplication.Size = new Size(100, 30);
            txtComplication.Name = "txtComplication";
            lbEstimateBloodlose = new Label();
            bc.setControlLabel(ref lbEstimateBloodlose, fEdit, "Estimate Blood lose ", "lbEstimateBloodlose", txtComplication.Location.X + txtComplication.Width + 30, lbComplication.Location.Y);
            size = bc.MeasureString(lbEstimateBloodlose);
            txtEstimateBloodlose = new C1TextBox();
            txtEstimateBloodlose.Font = fEdit;
            txtEstimateBloodlose.Location = new System.Drawing.Point(lbEstimateBloodlose.Location.X + size.Width + 5, lbComplication.Location.Y);
            txtEstimateBloodlose.Size = new Size(60, 30);
            txtEstimateBloodlose.Name = "txtEstimateBloodlose";
            lbEstimateBloodloseUnit = new Label();
            bc.setControlLabel(ref lbEstimateBloodloseUnit, fEdit, "ML. ", "lbEstimateBloodloseUnit", txtEstimateBloodlose.Location.X + txtEstimateBloodlose.Width + 5, lbComplication.Location.Y);
            size = bc.MeasureString(lbEstimateBloodloseUnit);
            lbTissueBiopsy = new Label();
            bc.setControlLabel(ref lbTissueBiopsy, fEdit, "Tissue Biopsy ", "lbTissueBiopsy", lbEstimateBloodloseUnit.Location.X+ size.Width + 35, lbComplication.Location.Y);
            size = bc.MeasureString(lbTissueBiopsy);
            pnTissue = new Panel();
            pnTissue.Location = new System.Drawing.Point(lbTissueBiopsy.Location.X + size.Width + 5, lbTissueBiopsy.Location.Y - 8);
            pnTissue.Size = new Size(130, 40);
            chkTissueBiopsyNo = new RadioButton();
            chkTissueBiopsyNo.Name = "chkTissueBiopsyNo";
            chkTissueBiopsyNo.Font = fEdit;
            chkTissueBiopsyNo.Text = "NO";
            size = bc.MeasureString(chkTissueBiopsyNo);
            chkTissueBiopsyNo.Location = new System.Drawing.Point(10, 10);
            chkTissueBiopsyNo.Width = size.Width + 20;
            chkTissueBiopsyYes = new RadioButton();
            chkTissueBiopsyYes.Name = "chkTissueBiopsyYes";
            chkTissueBiopsyYes.Font = fEdit;
            chkTissueBiopsyYes.Text = "YES";
            chkTissueBiopsyYes.Location = new System.Drawing.Point(size.Width + 40, 10);
            size = bc.MeasureString(chkTissueBiopsyYes);
            chkTissueBiopsyYes.Width = size.Width + 20;
            pnTissue.Controls.Add(chkTissueBiopsyNo);
            pnTissue.Controls.Add(chkTissueBiopsyYes);
            txtTissueBiopsy = new C1TextBox();
            txtTissueBiopsy.Font = fEdit;
            txtTissueBiopsy.Location = new System.Drawing.Point(pnTissue.Location.X + pnTissue.Width + 5, lbTissueBiopsy.Location.Y);
            txtTissueBiopsy.Size = new Size(100, 30);
            txtTissueBiopsy.Name = "txtTissueBiopsy";

            lbSpecialSpecimen = new Label();
            bc.setControlLabel(ref lbSpecialSpecimen, fEdit, "Unit     Special Specimen ", "lbSpecialSpecimen", txtTissueBiopsy.Location.X + txtTissueBiopsy.Width + 5, lbTissueBiopsy.Location.Y);
            size = bc.MeasureString(lbSpecialSpecimen);
            txtSpecialSpecimen = new C1TextBox();
            txtSpecialSpecimen.Font = fEdit;
            txtSpecialSpecimen.Location = new System.Drawing.Point(lbSpecialSpecimen.Location.X + size.Width + 5, lbTissueBiopsy.Location.Y);
            txtSpecialSpecimen.Size = new Size(200, 30);
            txtSpecialSpecimen.Name = "txtSpecialSpecimen";

            gapY += gapLine;
            pnProcidures = new Panel();
            pnProcidures.Dock = DockStyle.Bottom;
            //pnProcidures.Height = 300;
            //pnProcidures.BackColor = Color.Red;
            pnProcidures.Location = new System.Drawing.Point(gapX, gapY);
            
            pnProcidures.Controls.Add(sCFinding);
            setControlpnOperativeAdd();
            this.Controls.Add(sCOper);

            pnLeft.ResumeLayout(false);
            pnLeftTop.ResumeLayout(false);
            pnLeftBotton.ResumeLayout(false);
            pnOperative.ResumeLayout(false);
            sCOper.ResumeLayout(false);
            scOperView.ResumeLayout(false);
            scOperAdd.ResumeLayout(false);
            sCFinding.ResumeLayout(false);
            scFinding.ResumeLayout(false);
            scPrecidures.ResumeLayout(false);
            this.ResumeLayout(false);

            sCFinding.PerformLayout();
            scFinding.PerformLayout();
            scPrecidures.PerformLayout();
            sCOper.PerformLayout();
            scOperView.PerformLayout();
            scOperAdd.PerformLayout();
            pnLeft.PerformLayout();
            pnLeftTop.PerformLayout();
            pnLeftBotton.PerformLayout();
            pnOperative.PerformLayout();
            this.PerformLayout();

            frmPrecidures.Show();
            frmFinding.Show();
        }
        
        private void setControlpnOperativeAdd()
        {
            pnOperative.Controls.Add(pnProcidures);
            pnOperative.Controls.Add(lbFinding);
            pnOperative.Controls.Add(txtFinding);

            pnOperative.Controls.Add(lbOperation1);
            pnOperative.Controls.Add(txtOperation1);
            pnOperative.Controls.Add(lbOperation2);
            pnOperative.Controls.Add(txtOperation2);
            pnOperative.Controls.Add(lbOperation3);
            pnOperative.Controls.Add(txtOperation3);
            pnOperative.Controls.Add(lbOperation4);
            pnOperative.Controls.Add(txtOperation4);

            pnOperative.Controls.Add(lbPreOperation);
            pnOperative.Controls.Add(txtPreOperation);
            pnOperative.Controls.Add(lbPostOperation);
            pnOperative.Controls.Add(txtPostOperation);

            pnOperative.Controls.Add(lbAnesthetistTechni);
            pnOperative.Controls.Add(txtAnesthetistTechni1);
            pnOperative.Controls.Add(lbTimeAnthe);
            pnOperative.Controls.Add(txtTimeAnthe);
            pnOperative.Controls.Add(lbTimeFinishAnthe);
            pnOperative.Controls.Add(txtTimeFinishAnthe);
            pnOperative.Controls.Add(lbTotalTimeAnthe);
            pnOperative.Controls.Add(txtTotalTimeAnthe);

            pnOperative.Controls.Add(lbAnesthetistAssist);
            pnOperative.Controls.Add(lbAnesthetistAssist1);
            pnOperative.Controls.Add(txtAnesthetistAssist1);
            pnOperative.Controls.Add(lbAnesthetistAssistName1);
            pnOperative.Controls.Add(lbAnesthetistAssist2);
            pnOperative.Controls.Add(txtAnesthetistAssist2);
            pnOperative.Controls.Add(lbAnesthetistAssistName2);

            pnOperative.Controls.Add(lbAnesthetist);
            pnOperative.Controls.Add(lbAnesthetist1);
            pnOperative.Controls.Add(txtAnesthetist1);
            pnOperative.Controls.Add(lbAnesthetistName1);

            pnOperative.Controls.Add(lbPerfusionist);
            pnOperative.Controls.Add(lbPerfusionist1);
            pnOperative.Controls.Add(txtPerfusionist1);
            pnOperative.Controls.Add(lbPerfusionistName1);
            pnOperative.Controls.Add(lbPerfusionist2);
            pnOperative.Controls.Add(txtPerfusionist2);
            pnOperative.Controls.Add(lbPerfusionistName2);

            pnOperative.Controls.Add(lbPttName);
            pnOperative.Controls.Add(lbSurgeon);
            pnOperative.Controls.Add(lbSurgeon1);
            pnOperative.Controls.Add(txtSurgeon1);
            pnOperative.Controls.Add(lbSurgeonName1);
            pnOperative.Controls.Add(lbSurgeon2);
            pnOperative.Controls.Add(txtSurgeon2);
            pnOperative.Controls.Add(lbSurgeonName2);
            pnOperative.Controls.Add(lbSurgeon3);
            pnOperative.Controls.Add(txtSurgeon3);
            pnOperative.Controls.Add(lbSurgeonName3);
            pnOperative.Controls.Add(lbSurgeon4);
            pnOperative.Controls.Add(txtSurgeon4);
            pnOperative.Controls.Add(lbSurgeonName4);
            pnOperative.Controls.Add(lbAssistant);
            pnOperative.Controls.Add(lbAssistant1);
            pnOperative.Controls.Add(txtAssistant1);
            pnOperative.Controls.Add(lbAssistantName1);
            pnOperative.Controls.Add(lbAssistant2);
            pnOperative.Controls.Add(txtAssistant2);
            pnOperative.Controls.Add(lbAssistantName2);
            pnOperative.Controls.Add(lbAssistant3);
            pnOperative.Controls.Add(txtAssistant3);
            pnOperative.Controls.Add(lbAssistantName3);
            pnOperative.Controls.Add(lbAssistant4);
            pnOperative.Controls.Add(txtAssistant4);
            pnOperative.Controls.Add(lbAssistantName4);
            pnOperative.Controls.Add(lbScrubNurse);
            pnOperative.Controls.Add(lbScrubNurse1);
            pnOperative.Controls.Add(txtScrubNurse1);
            pnOperative.Controls.Add(lbScrubNurseName1);
            pnOperative.Controls.Add(lbScrubNurse2);
            pnOperative.Controls.Add(txtScrubNurse2);
            pnOperative.Controls.Add(lbScrubNurseName2);
            pnOperative.Controls.Add(lbScrubNurse3);
            pnOperative.Controls.Add(txtScrubNurse3);
            pnOperative.Controls.Add(lbScrubNurseName3);
            pnOperative.Controls.Add(lbScrubNurse4);
            pnOperative.Controls.Add(txtScrubNurse4);
            pnOperative.Controls.Add(lbScrubNurseName4);
            pnOperative.Controls.Add(lbCircuNurse);
            pnOperative.Controls.Add(lbCircuNurse1);
            pnOperative.Controls.Add(txtCircuNurse1);
            pnOperative.Controls.Add(lbCircuNurseName1);
            pnOperative.Controls.Add(lbCircuNurse2);
            pnOperative.Controls.Add(txtCircuNurse2);
            pnOperative.Controls.Add(lbCircuNurseName2);
            pnOperative.Controls.Add(lbCircuNurse3);
            pnOperative.Controls.Add(txtCircuNurse3);
            pnOperative.Controls.Add(lbCircuNurseName3);
            pnOperative.Controls.Add(lbCircuNurse4);
            pnOperative.Controls.Add(txtCircuNurse4);
            pnOperative.Controls.Add(lbCircuNurseName4);

            pnOperative.Controls.Add(lbTotalTime);
            pnOperative.Controls.Add(txtTotalTime);
            pnOperative.Controls.Add(lbTimeFinish);
            pnOperative.Controls.Add(txtTimeFinish);
            pnOperative.Controls.Add(lbTimeInsion);
            pnOperative.Controls.Add(txtTimeInsion);
            pnOperative.Controls.Add(lbDateOperative);
            pnOperative.Controls.Add(txtDateOperative);
            //pnOperative.Controls.Add(lbDtrName);

            pnOperative.Controls.Add(lbPttid);
            pnOperative.Controls.Add(btnPttSearch);
            pnOperative.Controls.Add(lbDtrName);

            pnOperative.Controls.Add(lbTitle);
            pnOperative.Controls.Add(lbDept);
            pnOperative.Controls.Add(lbWard);
            pnOperative.Controls.Add(lbDtrId);
            pnOperative.Controls.Add(txtWard);
            pnOperative.Controls.Add(txtDept);
            pnOperative.Controls.Add(txtDtrId);

            pnOperative.Controls.Add(lbComplication);
            pnOperative.Controls.Add(txtComplication);
            pnOperative.Controls.Add(pnComplication);
            pnOperative.Controls.Add(pnTissue);
            pnOperative.Controls.Add(lbEstimateBloodlose);
            pnOperative.Controls.Add(lbTissueBiopsy);
            pnOperative.Controls.Add(lbSpecialSpecimen);
            pnOperative.Controls.Add(txtEstimateBloodlose);
            pnOperative.Controls.Add(txtTissueBiopsy);
            pnOperative.Controls.Add(txtSpecialSpecimen);
            pnOperative.Controls.Add(lbEstimateBloodloseUnit);

            pnOperative.Controls.Add(btnPrint);
            pnOperative.Controls.Add(btnSave);

            pnLeftTop.Controls.Add(lbGrfHnSearch);
            pnLeftTop.Controls.Add(txtGrfHnSearch);
            pnLeftTop.Controls.Add(btnNew);
        }
        private void setLayout()
        {
            int gapLine = 20, gapX = 20, gapY=20;
            Size size = new Size();
            int scrW = Screen.PrimaryScreen.Bounds.Width;
            int scrH = Screen.PrimaryScreen.Bounds.Height;

            size = bc.MeasureString(lbTitle);
            //lbTitle.Location = new System.Drawing.Point((scrW / 2) + (size.Width / 2), gapY);
            pnProcidures.Location = new System.Drawing.Point(gapX, lbComplication.Location.Y + gapLine);
            pnProcidures.Height = scrH - lbComplication.Location.Y -100;
            scOperAdd.SizeRatio = 100;
            scOperView.SizeRatio = 0;
            //scOperView.
        }
        private void FrmOrOperativeNote_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Last Update 2020-08-06 ";
            setLayout();
        }
    }
}
