using bangna_hospital.object1;
using C1.C1Pdf;
using C1.Win.C1Document;
using C1.Win.C1FlexGrid;
using C1.Win.C1Input;
using C1.Win.FlexViewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
	/// <summary>
	/// Partial class สำหรับจัดการ C1FlexGrid ทั้งหมด
	/// </summary>
	partial class FrmOPD
    {
		#region Grid Fields - C1FlexGrid Controls
		// Operation & Finish Grids
		C1FlexGrid grfOperList, grfOperFinish, grfOperFinishDrug, grfOperFinishLab, grfOperFinishXray, grfOperFinishProcedure;

		// Patient & Appointment Grids
		C1FlexGrid grfCheckUPList, grfPttApm, grfApm, grfApmOrder, grfApmMulti;

		// Order & Lab & Xray Grids
		C1FlexGrid grfOrder, grfOrderPrenoProc, grfHisOrder, grfLab, grfXray, grfOperLab, grfOperXray, grfSearchLab;

		// IPD & OPD & OutLab Grids
		C1FlexGrid grfIPD, grfIPDScan, grfOPD, grfOutLab, grfTodayOutLab;

		// Procedure & Search Grids
		C1FlexGrid grfHisProcedure, grfSrc, grfSrcVs, grfSrcOrder, grfSrcLab, grfSrcXray, grfSrcProcedure;

		// Scan & Certificate Grids
		C1FlexGrid grfEKG, grfDocOLD, grfEST, grfHolter, grfECHO, grfCertMed;

		// Package & Report Grids
		C1FlexGrid grfMapPackage, grfPackage, grfMapPackageViewhelp, grfpackageitems, grfChkPackItems;
		C1FlexGrid grfRpt;
		#endregion
		#region Grid Column Indexes - grfSrc
		int colgrfSrcHn = 1, colgrfSrcFullNameT = 2, colgrfSrcPID = 3, colgrfSrcDOB = 4, colgrfSrcPttid = 5, colgrfSrcAge = 6, colgrfSrcVisitReleaseOPD = 7, colgrfSrcVisitReleaseIPD = 8, colgrfSrcVisitReleaseIPDDischarge = 9;
		#endregion
		#region Grid Column Indexes - grfPttApm
		int colgrfPttApmVsDate = 1, colgrfPttApmApmDateShow = 2, colgrfPttApmApmTime = 3, colgrfPttApmHN = 4, colgrfPttApmPttName = 5, colgrfPttApmDeptR = 6, colgrfPttApmDeptMake = 7, colgrfPttApmNote = 8, colgrfPttApmOrder = 9, colgrfPttApmDocYear = 10, colgrfPttApmDocNo = 11, colgrfPttApmDtrname = 12, colgrfPttApmPhone = 13, colgrfPttApmPaidName = 14, colgrfPttApmRemarkCall = 15, colgrfPttApmStatusRemarkCall = 16, colgrfPttApmRemarkCallDate = 17, colgrfPttApmApmDate1 = 18;
		#endregion
		int colgrfOrderCode = 1, colgrfOrderName = 2, colgrfOrderStatus = 3, colgrfOrderQty = 4, colgrfOrderID = 5, colgrfOrderReqNO = 6, colgrfOrdFlagSave = 7, colgrfOrdStatusControl = 8, colgrfOrdControlYear = 9, colgrfOrdSupervisor = 10, colgrfOrdPassSupervisor = 11, colgrfOrdControlRemark = 12;
		int colgrfCheckUPHn = 1, colgrfCheckUPFullNameT = 2, colgrfCheckUPSymtom = 3, colgrfCheckUPEmployer = 4, colgrfCheckUPVsDate = 5, colgrfCheckUPPreno = 6;
		int colgrfOperListFullNameT = 1, colgrfOperListSymptoms = 2, colgrfOperListPaidName = 3, colgrfOperListPreno = 4, colgrfOperListVsDate = 5, colgrfOperListVsTime = 6, colgrfOperListHn = 7, colgrfOperListVN = 8, colgrfOperListActNo = 9, colgrfOperListDtrName = 10, colgrfOperListLab = 11, colgrfOperListXray = 12;
		int colIPDDate = 1, colIPDDept = 2, colIPDAnShow = 4, colIPDStatus = 3, colIPDPreno = 5, colIPDVn = 6, colIPDAndate = 7, colIPDAnYr = 8, colIPDAn = 9, colIPDDtrName = 10;
		int colPic1 = 1, colPic2 = 2, colPic3 = 3, colPic4 = 4;
		int colVsVsDate = 1, colVsDept = 2, colVsVn = 3, colVsStatus = 4, colVsPreno = 5, colVsAn = 6, colVsAndate = 7, colVsPaidType = 8, colVsHigh = 9, colVsWeight = 10, colVsTemp = 11, colVscc = 12, colVsccin = 13, colVsccex = 14, colVsabc = 15, colVshc16 = 16, colVsbp1r = 17, colVsbp1l = 18, colVsbp2r = 19, colVsbp2l = 20, colVsVital = 21, colVsPres = 22, colVsRadios = 23, colVsBreath = 24, colVsVsDate1 = 25, colVsDtrName = 26;
		int colgrfOutLabDscHN = 1, colgrfOutLabDscPttName = 2, colgrfOutLabDscVsDate = 3, colgrfOutLabDscVN = 4, colgrfOutLabDscId = 5, colgrfOutLablabcode = 6, colgrfOutLablabname = 7, colgrfOutLabApmDate = 8, colgrfOutLabApmDesc = 9, colgrfOutLabApmDtr = 10, colgrfOutLabApmReqNo = 11, colgrfOutLabApmReqDate = 12;
		int colOrderId = 1, colOrderDate = 2, colOrderName = 3, colOrderQty = 4, colOrderFre = 5, colOrderIn1 = 6, colOrderMed = 7;
		int colLabDate = 1, colLabName = 2, colLabNameSub = 3, colLabResult = 4, colInterpret = 5, colNormal = 6, colUnit = 7, colLabCode = 8, colLabReqNo = 9, colLabReqDate = 10, colLabReqStatus=11;
		int colXrayDate = 1, colXrayCode = 2, colXrayName = 3, colXrayResult = 4, colXrayReqStatus=5;
		int colHisProcCode = 1, colHisProcName = 2, colHisProcReqDate = 3, colHisProcReqTime = 4, colHisProcReqNo = 5, colHisProcReqStatus=6;
		int colChkPackItemsname = 1, colChkPackItemflag = 2, colChkPackItemsitemcode = 3, colChkPackItemsPackcode = 4;
		int colgrfMapPackageCompCode = 1, colgrfMapPackageCompName = 2;
		int colgrfPackageCode = 1, colgrfPackageName = 2, colgrfPackagePrice = 3, colgrfPackageType = 4, colgrfPackageCompCode = 5;
		int colgrfViewhelpPackCode = 1, colgrfViewhelpPackName = 2, colgrfViewhelpPttName = 3, colgrfViewhelpVsdate = 4, colgrfViewhelpPackType = 5;
		int colgrfpackageitemcode = 1, colgrfpackageitemname = 2, colgrfpackageitemprice = 3, colgrfpackageitemtype = 4;
		
		private void initGrfSearchLab()
		{
			grfSearchLab = new C1FlexGrid();
			grfSearchLab.Font = fEdit;
			grfSearchLab.Dock = System.Windows.Forms.DockStyle.Fill;
			grfSearchLab.Location = new System.Drawing.Point(0, 0);
			grfSearchLab.Rows.Count = 1;
			grfSearchLab.Cols.Count = 9;
			grfSearchLab.Cols[colLabDate].Width = 100;
			grfSearchLab.Cols[colLabName].Width = 400;
			grfSearchLab.Cols[colLabCode].Width = 80;

			grfSearchLab.Cols[colLabDate].Caption = "Date";
			grfSearchLab.Cols[colLabName].Caption = "Lab Name";
			grfSearchLab.Cols[colLabCode].Caption = "code";
			grfSearchLab.Cols[colLabDate].AllowEditing = false;
			grfSearchLab.Cols[colLabName].AllowEditing = false;
			//grfSearchLab.ExtendLastCol = true;
			grfSearchLab.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			grfSearchLab.DoubleClick += GrfSearchLab_DoubleClick;
			pnSearchLab.Controls.Add(grfSearchLab);
		}
		private void GrfSearchLab_DoubleClick(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (grfSearchLab == null) { return; }
			if (grfSearchLab.Row < 1) { return; }
			DateTime dtLab1, dtLab2;
			dtLab2 = DateTime.Now;
			if (chkSearchlab1Year.Checked) { dtLab1 = DateTime.Now.AddYears(-1); }
			else if (chkSearchlab6Month.Checked) { dtLab1 = DateTime.Now.AddMonths(-6); }
			else { dtLab1 = DateTime.Now; }
			String date1 = dtLab1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
			String date2 = dtLab2.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
			String lab = grfSearchLab[grfSearchLab.Row, colLabName].ToString();
			String[] labA = lab.Split('#');
			DataTable dt = bc.bcDB.labT05DB.selectbyLabCode(date1, date2, txtSrcHn.Text.Trim(), labA[1]);

			Form frm = new Form();
			frm.StartPosition = FormStartPosition.CenterScreen;
			frm.Size = new Size(800, 400);
			frm.Text = "เลือก Lab";
			C1FlexGrid grf = new C1FlexGrid();
			grf.Font = fEdit;
			grf.Dock = System.Windows.Forms.DockStyle.Fill;
			grf.Location = new System.Drawing.Point(0, 0);
			grf.Rows.Count = 1;
			grf.Cols.Count = 4;
			grf.Cols[3].Width = 300;
			grf.Cols[1].Width = 100;
			grf.Cols[2].Width = 300;
			grf.Cols[1].Caption = "Date";
			grf.Cols[2].Caption = "Lab Name";
			grf.Cols[3].Caption = "code";
			grf.Cols[1].AllowEditing = false;
			grf.Cols[2].AllowEditing = false;
			grf.Cols[3].AllowEditing = false;
			grf.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			frm.Controls.Add(grf);
			int i = 1;
			grf.Rows.Count = dt.Rows.Count + 1;
			foreach (DataRow dr in dt.Rows)
			{
				Row row = grf.Rows[i];
				row[1] = dr["mnc_req_dat"].ToString();
				row[2] = dr["MNC_LB_DSC"].ToString();
				row[3] = dr["MNC_USR_FULL_usr"].ToString();
				i++;
			}
			frm.ShowDialog(this);
		}
		private void setGrfSearchLab(String hn)
		{
			//throw new NotImplementedException();
			DateTime dtLab1, dtLab2;
			dtLab2 = DateTime.Now;
			if (chkSearchlab1Year.Checked) { dtLab1 = DateTime.Now.AddYears(-1); }
			else if (chkSearchlab6Month.Checked) { dtLab1 = DateTime.Now.AddMonths(-6); }
			else if (chkSearchlab1Jan69.Checked) { dtLab1 = new DateTime(2026, 1, 1); }
			else { dtLab1 = DateTime.Now; }
			String date1 = dtLab1.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
			String date2 = dtLab2.ToString("yyyy-MM-dd", new CultureInfo("en-US"));
			DataTable dt = bc.bcDB.labT05DB.selecCnttbyHn(date1, date2, hn, "");
			grfSearchLab.Rows.Count = 1; grfSearchLab.Rows.Count = dt.Rows.Count + 1;
			int i = 1;
			foreach (DataRow dr in dt.Rows)
			{
				Row row = grfSearchLab.Rows[i];
				row[colLabDate] = dr["cnt"].ToString();
				row[colLabName] = dr["MNC_LB_DSC"].ToString() + "#" + dr["MNC_LB_CD"].ToString();
				//row[colLabCode] = dr["MNC_LB_CD"].ToString();
				row[3] = dr["MNC_LB_PRI01"].ToString();
				i++;
			}
			//grfSearchLab.AutoSizeCols();
			grfSearchLab.Cols[colLabCode].Visible = false;
			grfSearchLab.Cols[7].Visible = false;
			grfSearchLab.Cols[6].Visible = false;
			grfSearchLab.Cols[5].Visible = false;
			grfSearchLab.Cols[4].Visible = false;
			grfSearchLab.Cols[3].Visible = true;
			grfSearchLab.Cols[colLabDate].Caption = "count";
			grfSearchLab.Cols[colLabCode].Width = 80;
			grfSearchLab.Cols[3].AllowEditing = false;
		}
		private void initGrfApmMulti()
		{
			grfApmMulti = new C1FlexGrid();
			grfApmMulti.Font = fEdit;
			grfApmMulti.Dock = System.Windows.Forms.DockStyle.Fill;
			grfApmMulti.Location = new System.Drawing.Point(0, 0);
			grfApmMulti.Rows.Count = 1;
			grfApmMulti.Cols.Count = 5;
			grfApmMulti.Name = "grfApmMulti";

			grfApmMulti.Cols[1].Width = 100; grfApmMulti.Cols[2].Width = 200;
			grfApmMulti.Cols[3].Width = 60; grfApmMulti.Cols[4].Width = 500;

			grfApmMulti.ShowCursor = true;
			grfApmMulti.Cols[1].Caption = "date"; grfApmMulti.Cols[2].Caption = "นัดวันที่";
			grfApmMulti.Cols[3].Caption = "นัดเวลา"; grfApmMulti.Cols[4].Caption = "นัดตรวจที่แผนก";

			grfApmMulti.Cols[1].DataType = typeof(String); grfApmMulti.Cols[2].DataType = typeof(String); grfApmMulti.Cols[3].DataType = typeof(String); grfApmMulti.Cols[4].DataType = typeof(String);

			grfApmMulti.Cols[1].TextAlign = TextAlignEnum.LeftCenter; grfApmMulti.Cols[2].TextAlign = TextAlignEnum.LeftCenter; grfApmMulti.Cols[3].TextAlign = TextAlignEnum.LeftCenter; grfApmMulti.Cols[4].TextAlign = TextAlignEnum.LeftCenter;

			grfApmMulti.Cols[1].AllowEditing = false; grfApmMulti.Cols[2].AllowEditing = false; grfApmMulti.Cols[3].AllowEditing = false; grfApmMulti.Cols[4].AllowEditing = false;
			pnApmMulti.Controls.Add(grfApmMulti);
		}
		private void initGrfMapPackage()
		{
			grfMapPackage = new C1FlexGrid();
			grfMapPackage.Font = fEdit;
			grfMapPackage.Dock = System.Windows.Forms.DockStyle.Fill;
			grfMapPackage.Location = new System.Drawing.Point(0, 0);
			grfMapPackage.Rows.Count = 1;
			grfMapPackage.Cols.Count = 5;
			grfMapPackage.Cols[colgrfMapPackageCompCode].Width = 70;
			grfMapPackage.Cols[colgrfMapPackageCompName].Width = 300;
			grfMapPackage.ShowCursor = true;
			grfMapPackage.Cols[colgrfMapPackageCompCode].Caption = "CODE";
			grfMapPackage.Cols[colgrfMapPackageCompName].Caption = "Company Name";

			grfMapPackage.Cols[colgrfMapPackageCompCode].DataType = typeof(String);
			grfMapPackage.Cols[colgrfMapPackageCompName].DataType = typeof(String);

			grfMapPackage.Cols[colgrfMapPackageCompCode].TextAlign = TextAlignEnum.LeftCenter;
			grfMapPackage.Cols[colgrfMapPackageCompName].TextAlign = TextAlignEnum.LeftCenter;
			grfMapPackage.Cols[colgrfMapPackageCompCode].AllowEditing = false;
			grfMapPackage.Cols[colgrfMapPackageCompName].AllowEditing = false;
			pnMapPackageComp.Controls.Add(grfMapPackage);
			grfMapPackage.Click += GrfMapPackage_Click;
			theme1.SetTheme(grfMapPackage, bc.iniC.themeApp);
		}

		private void GrfMapPackage_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (grfMapPackage.Row <= 0) return;
			String compcode = grfMapPackage[grfMapPackage.Row, colgrfMapPackageCompCode].ToString();
			String compname = grfMapPackage[grfMapPackage.Row, colgrfMapPackageCompName].ToString();
			String txt = "";
			if (compname.Length > 7) { txt = compname.Substring(0, 6); }
			else if (compname.Length > 5) { txt = compname.Substring(0, 4); }
			txtMapPackageCompCode.Value = compcode;
			txtMapPackageCompName.Value = compname;
			txtMapPackagePackagesearch.Value = txt;
		}

		private void setGrfMapPackage(String comcode)
		{
			DataTable dt = new DataTable();
			String vn = "", vsdate = "", an = "";
			grfMapPackage.Rows.Count = 1;
			dt = bc.bcDB.pm24DB.selectCustByName(comcode);
			int i = 0;
			grfMapPackage.Rows.Count = dt.Rows.Count + 1;
			try
			{
				foreach (DataRow row1 in dt.Rows)
				{
					i++;
					Row rowa = grfMapPackage.Rows[i];
					rowa[colgrfMapPackageCompCode] = row1["MNC_COM_CD"].ToString();
					rowa[colgrfMapPackageCompName] = row1["MNC_COM_DSC"].ToString();
					row1[0] = i;
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", this.Name + " setGrfMapPackage " + ex.Message);
			}
		}
		private void initGrfMapPackageItmes()
		{
			grfpackageitems = new C1FlexGrid();
			grfpackageitems.Font = fEdit;
			grfpackageitems.Dock = System.Windows.Forms.DockStyle.Fill;
			grfpackageitems.Location = new System.Drawing.Point(0, 0);
			grfpackageitems.Rows.Count = 1;
			grfpackageitems.Cols.Count = 6;
			grfpackageitems.Cols[colgrfPackageCode].Width = 70;
			grfpackageitems.Cols[colgrfPackageName].Width = 300;
			grfpackageitems.ShowCursor = true;
			grfpackageitems.Cols[colgrfPackageCode].Caption = "CODE";
			grfpackageitems.Cols[colgrfPackageName].Caption = "Package Name";
			grfpackageitems.Cols[colgrfPackageCode].DataType = typeof(String);
			grfpackageitems.Cols[colgrfPackageName].DataType = typeof(String);
			grfpackageitems.Cols[colgrfPackageCode].TextAlign = TextAlignEnum.LeftCenter;
			grfpackageitems.Cols[colgrfPackageName].TextAlign = TextAlignEnum.LeftCenter;
			grfpackageitems.Cols[colgrfPackageCode].AllowEditing = false;
			grfpackageitems.Cols[colgrfPackageName].AllowEditing = false;
			pnPackage.Controls.Add(grfpackageitems);//pnMapPackage
			theme1.SetTheme(grfpackageitems, bc.iniC.themeApp);
		}
		private void setGrfPackageItems(String packcode)
		{
			DataTable dt = new DataTable();
			String vn = "", vsdate = "", an = "";
			grfpackageitems.Rows.Count = 1;
			dt = bc.bcDB.pm40DB.selectByPackCode(packcode);
			int i = 0;
			grfpackageitems.Rows.Count = dt.Rows.Count + 1;
			try
			{
				foreach (DataRow row1 in dt.Rows)
				{
					i++;
					Row rowa = grfpackageitems.Rows[i];
					String type = row1["MNC_OPR_FLAG"].ToString(), name = "";
					name = type.Equals("L") ? row1["MNC_LB_DSC"].ToString() : type.Equals("O") ? row1["MNC_SR_DSC"].ToString()
						: type.Equals("X") ? row1["MNC_XR_DSC"].ToString() : type.Equals("F") ? row1["MNC_DF_DSC"].ToString() : "";
					rowa[colgrfPackageCode] = row1["MNC_OPR_CD"].ToString();
					rowa[colgrfPackageName] = name;
					//rowa[colgrfViewhelpPttName] = row1["fullnamet"].ToString();
					//rowa[colgrfViewhelpVsdate] = row1["MNC_DATE"].ToString();
					//rowa[colgrfViewhelpPackType] = row1["MNC_PAC_TYP"].ToString();
					row1[0] = i;
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", this.Name + " setGrfMapPackageViewhelp " + ex.Message);
			}
		}
		private void initGrfMapPackageViewhelp()
		{
			grfMapPackageViewhelp = new C1FlexGrid();
			grfMapPackageViewhelp.Font = fEdit;
			grfMapPackageViewhelp.Dock = System.Windows.Forms.DockStyle.Fill;
			grfMapPackageViewhelp.Location = new System.Drawing.Point(0, 0);
			grfMapPackageViewhelp.Rows.Count = 1;
			grfMapPackageViewhelp.Cols.Count = 6;
			grfMapPackageViewhelp.Cols[colgrfPackageCode].Width = 70;
			grfMapPackageViewhelp.Cols[colgrfPackageName].Width = 300;
			grfMapPackageViewhelp.ShowCursor = true;
			grfMapPackageViewhelp.Cols[colgrfPackageCode].Caption = "CODE";
			grfMapPackageViewhelp.Cols[colgrfPackageName].Caption = "Package Name";
			grfMapPackageViewhelp.Cols[colgrfPackageCode].DataType = typeof(String);
			grfMapPackageViewhelp.Cols[colgrfPackageName].DataType = typeof(String);
			grfMapPackageViewhelp.Cols[colgrfPackageCode].TextAlign = TextAlignEnum.LeftCenter;
			grfMapPackageViewhelp.Cols[colgrfPackageName].TextAlign = TextAlignEnum.LeftCenter;
			grfMapPackageViewhelp.Cols[colgrfPackageCode].AllowEditing = false;
			grfMapPackageViewhelp.Cols[colgrfPackageName].AllowEditing = false;
			pnMapPackageViewHelp.Controls.Add(grfMapPackageViewhelp);//pnMapPackage
			theme1.SetTheme(grfMapPackageViewhelp, bc.iniC.themeApp);
		}
		private void setGrfMapPackageViewhelp(String comcode)
		{
			DataTable dt = new DataTable();
			String vn = "", vsdate = "", an = "";
			grfMapPackageViewhelp.Rows.Count = 1;
			dt = bc.bcDB.pm39DB.selectViewHelpByCompCode(comcode);
			int i = 0;
			grfMapPackageViewhelp.Rows.Count = dt.Rows.Count + 1;
			try
			{
				foreach (DataRow row1 in dt.Rows)
				{
					i++;
					Row rowa = grfMapPackageViewhelp.Rows[i];
					rowa[colgrfPackageCode] = row1["MNC_PAC_CD"].ToString();
					rowa[colgrfPackageName] = row1["MNC_PAC_DSC"].ToString();
					rowa[colgrfViewhelpPttName] = row1["fullnamet"].ToString();
					rowa[colgrfViewhelpVsdate] = row1["MNC_DATE"].ToString();
					rowa[colgrfViewhelpPackType] = row1["MNC_PAC_TYP"].ToString();
					row1[0] = i;
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", this.Name + " setGrfMapPackageViewhelp " + ex.Message);
			}
		}
		/*
         * grf package
         */
		private void initGrfpackage()
		{
			grfPackage = new C1FlexGrid();
			grfPackage.Font = fEdit;
			grfPackage.Dock = System.Windows.Forms.DockStyle.Fill;
			grfPackage.Location = new System.Drawing.Point(0, 0);
			grfPackage.Rows.Count = 1;
			grfPackage.Cols.Count = 6;
			grfPackage.Cols[colgrfPackageCode].Width = 70;
			grfPackage.Cols[colgrfPackageName].Width = 300;
			grfPackage.ShowCursor = true;
			grfPackage.Cols[colgrfMapPackageCompCode].Caption = "CODE";
			grfPackage.Cols[colgrfPackageName].Caption = "Package Name";
			grfPackage.Cols[colgrfPackageCompCode].Caption = "Comp Code";

			grfPackage.Cols[colgrfPackageCode].DataType = typeof(String);
			grfPackage.Cols[colgrfPackageName].DataType = typeof(String);
			grfPackage.Cols[colgrfPackageCompCode].DataType = typeof(String);

			grfPackage.Cols[colgrfPackageCode].TextAlign = TextAlignEnum.LeftCenter;
			grfPackage.Cols[colgrfPackageName].TextAlign = TextAlignEnum.LeftCenter;
			grfPackage.Cols[colgrfPackageCompCode].TextAlign = TextAlignEnum.CenterCenter;
			grfPackage.Cols[colgrfPackageCode].AllowEditing = false;
			grfPackage.Cols[colgrfPackageName].AllowEditing = false;
			grfPackage.Cols[colgrfPackageCompCode].AllowEditing = false;
			pnMapPackage.Controls.Add(grfPackage);//pnMapPackage
			grfPackage.Click += GrfPackage_Click;
			theme1.SetTheme(grfPackage, bc.iniC.themeApp);
		}

		private void GrfPackage_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (grfPackage.Row <= 0) return;
			txtMapPackagepackageCode.Value = grfPackage[grfPackage.Row, colgrfPackageCode] != null ? grfPackage[grfPackage.Row, colgrfPackageCode].ToString() : "";
			txtMapPackagepackageName.Value = grfPackage[grfPackage.Row, colgrfPackageName] != null ? grfPackage[grfPackage.Row, colgrfPackageName].ToString() : "";
			txtMapPackagepackageType.Value = grfPackage[grfPackage.Row, colgrfPackageType] != null ? grfPackage[grfPackage.Row, colgrfPackageType].ToString() : "";
			setGrfPackageItems(txtMapPackagepackageCode.Text.Trim());


		}
		private void setGrfPackage(String comcode, String flagcode)
		{
			DataTable dt = new DataTable();
			String vn = "", vsdate = "", an = "";
			grfPackage.Rows.Count = 1;
			dt = flagcode.Equals("code") ? bc.bcDB.pm39DB.selectByCompCode(comcode) : bc.bcDB.pm39DB.selectByCompName(comcode);
			int i = 0;
			grfPackage.Rows.Count = dt.Rows.Count + 1;
			try
			{
				foreach (DataRow row1 in dt.Rows)
				{
					i++;
					Row rowa = grfPackage.Rows[i];
					rowa[colgrfPackageCode] = row1["MNC_PAC_CD"].ToString();
					rowa[colgrfPackageName] = row1["MNC_PAC_DSC"].ToString();
					rowa[colgrfPackageType] = row1["MNC_PAC_TYP"].ToString();
					rowa[colgrfPackagePrice] = row1["MNC_PAC_PRI"].ToString();
					rowa[colgrfPackageCompCode] = row1["MNC_COM_CD"].ToString();
					row1[0] = i;
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", this.Name + " setGrfMapPackage " + ex.Message);
			}
		}
        private void initGrfPrenoProcedure()
        {
			if(grfOrderPrenoProc != null) { return; }
            if (pnPrenoProcedure.Contains(grfOrderPrenoProc)) { return; }
            grfOrderPrenoProc = new C1FlexGrid();
            grfOrderPrenoProc.Font = fEdit;
            grfOrderPrenoProc.Dock = System.Windows.Forms.DockStyle.Fill;
            grfOrderPrenoProc.Location = new System.Drawing.Point(0, 0);
            grfOrderPrenoProc.Rows.Count = 1;
            grfOrderPrenoProc.Cols.Count = 7;
            grfOrderPrenoProc.Cols[colHisProcCode].Width = 100;
            grfOrderPrenoProc.Cols[colHisProcName].Width = 200;
            grfOrderPrenoProc.Cols[colHisProcReqDate].Width = 100;
            grfOrderPrenoProc.Cols[colHisProcReqTime].Width = 100;
            grfOrderPrenoProc.Cols[colHisProcReqNo].Width = 70;

            grfOrderPrenoProc.ShowCursor = true;
            grfOrderPrenoProc.Cols[colHisProcCode].Caption = "CODE";
            grfOrderPrenoProc.Cols[colHisProcName].Caption = "Procedure Name";
            grfOrderPrenoProc.Cols[colHisProcReqDate].Caption = "req date";
            grfOrderPrenoProc.Cols[colHisProcReqTime].Caption = "req time";
            grfOrderPrenoProc.Cols[colHisProcReqNo].Caption = "req no";
            grfOrderPrenoProc.Cols[colHisProcReqStatus].Caption = "status";
            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfOrderPrenoProc.Cols[colHisProcCode].DataType = typeof(String);
            grfOrderPrenoProc.Cols[colHisProcName].DataType = typeof(String);
            grfOrderPrenoProc.Cols[colHisProcReqDate].DataType = typeof(String);
            grfOrderPrenoProc.Cols[colHisProcReqTime].DataType = typeof(String);
            grfOrderPrenoProc.Cols[colHisProcReqNo].DataType = typeof(String);

            grfOrderPrenoProc.Cols[colHisProcCode].TextAlign = TextAlignEnum.CenterCenter;
            grfOrderPrenoProc.Cols[colHisProcName].TextAlign = TextAlignEnum.LeftCenter;
            grfOrderPrenoProc.Cols[colHisProcReqDate].TextAlign = TextAlignEnum.CenterCenter;
            grfOrderPrenoProc.Cols[colHisProcReqTime].TextAlign = TextAlignEnum.CenterCenter;
            grfOrderPrenoProc.Cols[colHisProcReqNo].TextAlign = TextAlignEnum.CenterCenter;

            grfOrderPrenoProc.Cols[colgrfOrderCode].Visible = true;
            grfOrderPrenoProc.Cols[colgrfOrderName].Visible = true;
            grfOrderPrenoProc.Cols[colHisProcReqTime].Visible = false;

            grfOrderPrenoProc.Cols[colHisProcCode].AllowEditing = false;
            grfOrderPrenoProc.Cols[colHisProcName].AllowEditing = false;
            grfOrderPrenoProc.Cols[colHisProcReqDate].AllowEditing = false;
            grfOrderPrenoProc.Cols[colHisProcReqTime].AllowEditing = false;
            grfOrderPrenoProc.Cols[colHisProcReqNo].AllowEditing = false;
            grfOrderPrenoProc.Cols[colHisProcReqStatus].AllowEditing = false;
            pnPrenoProcedure.Controls.Add(grfOrderPrenoProc);
            theme1.SetTheme(grfOrderPrenoProc, bc.iniC.themeApp);
        }
        private void setGrfPrenoProc()
        {
            DataTable dt = new DataTable();
            grfOrderPrenoProc.Rows.Count = 1;
            dt = bc.bcDB.pt16DB.SelectProcedureByVisit(HN, PRENO, VSDATE);
            int i = 0;            grfOrderPrenoProc.Rows.Count = dt.Rows.Count + 1;
            try
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    i++;
                    Row rowa = grfOrderPrenoProc.Rows[i];
                    //rowa[colHisProcReqTime] = row1["MNC_REQ_TIM"].ToString();
                    rowa[colHisProcReqDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
                    rowa[colHisProcName] = row1["MNC_SR_DSC"].ToString();
                    rowa[colHisProcCode] = row1["MNC_SR_CD"].ToString();
                    rowa[colHisProcReqNo] = row1["MNC_REQ_NO"].ToString();
                    rowa[colHisProcReqStatus] = row1["MNC_REQ_STS"].ToString();
                    row1[0] = i;
                }
            }
            catch (Exception ex)
            {
                new LogWriter("e", "FrmScanView1 setGrfHisProcedure " + ex.Message);
            }
        }
        private void initGrfSrcProcedure()
		{
			grfSrcProcedure = new C1FlexGrid();
			grfSrcProcedure.Font = fEdit;
			grfSrcProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
			grfSrcProcedure.Location = new System.Drawing.Point(0, 0);
			grfSrcProcedure.Rows.Count = 1;
			grfSrcProcedure.Cols.Count = 7;
			grfSrcProcedure.Cols[colHisProcCode].Width = 100;
			grfSrcProcedure.Cols[colHisProcName].Width = 200;
			grfSrcProcedure.Cols[colHisProcReqDate].Width = 100;
			grfSrcProcedure.Cols[colHisProcReqTime].Width = 100;

			grfSrcProcedure.ShowCursor = true;
			grfSrcProcedure.Cols[colHisProcCode].Caption = "CODE";
			grfSrcProcedure.Cols[colHisProcName].Caption = "Procedure Name";
			grfSrcProcedure.Cols[colHisProcReqDate].Caption = "req date";
			grfSrcProcedure.Cols[colHisProcReqTime].Caption = "req time";
            //grfSrcProcedure.Cols[colHisProcReqStatus].Caption = "status";
            //grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
            grfSrcProcedure.Cols[colHisProcCode].DataType = typeof(String);
			grfSrcProcedure.Cols[colHisProcName].DataType = typeof(String);
			grfSrcProcedure.Cols[colHisProcReqDate].DataType = typeof(String);
			grfSrcProcedure.Cols[colHisProcReqTime].DataType = typeof(String);

			grfSrcProcedure.Cols[colHisProcCode].TextAlign = TextAlignEnum.CenterCenter;
			grfSrcProcedure.Cols[colHisProcName].TextAlign = TextAlignEnum.LeftCenter;
			grfSrcProcedure.Cols[colHisProcReqDate].TextAlign = TextAlignEnum.CenterCenter;
			grfSrcProcedure.Cols[colHisProcReqTime].TextAlign = TextAlignEnum.CenterCenter;

			grfSrcProcedure.Cols[colgrfOrderCode].Visible = true;
			grfSrcProcedure.Cols[colgrfOrderName].Visible = true;
			grfSrcProcedure.Cols[colHisProcReqTime].Visible = false;
            grfSrcProcedure.Cols[colHisProcReqNo].Visible = false;
            grfSrcProcedure.Cols[colHisProcReqStatus].Visible = false;

            grfSrcProcedure.Cols[colHisProcCode].AllowEditing = false;
			grfSrcProcedure.Cols[colHisProcName].AllowEditing = false;
			grfSrcProcedure.Cols[colHisProcReqDate].AllowEditing = false;
			grfSrcProcedure.Cols[colHisProcReqTime].AllowEditing = false;
            grfSrcProcedure.Cols[colHisProcReqStatus].AllowEditing = false;
            pnSrcProcedure.Controls.Add(grfSrcProcedure);
			theme1.SetTheme(grfSrcProcedure, bc.iniC.themeApp);
		}
		private void setGrfSrcProcedure(String vsDate, String preno)
		{
			DataTable dt = new DataTable();
			String vn = "", vsdate = "", an = "";
			grfSrcProcedure.Rows.Count = 1;
			dt = bc.bcDB.pt16DB.SelectProcedureByVisit(HN, preno, vsDate);
			int i = 0;
			grfSrcProcedure.Rows.Count = dt.Rows.Count + 1;
			try
			{
				foreach (DataRow row1 in dt.Rows)
				{
					i++;
					Row rowa = grfSrcProcedure.Rows[i];
					//rowa[colHisProcReqTime] = row1["MNC_REQ_TIM"].ToString();
					rowa[colHisProcReqDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
					rowa[colHisProcName] = row1["MNC_SR_DSC"].ToString();
					rowa[colHisProcCode] = row1["MNC_SR_CD"].ToString();
					//rowa[colHisProcCode] = row1["MNC_SR_CD"].ToString();
					row1[0] = i;
				}
				if (dt.Rows.Count == 1)
				{
					//setXrayResult();
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FrmScanView1 setGrfHisProcedure " + ex.Message);
			}
		}
		private void initGrfProcedure()
		{
			grfHisProcedure = new C1FlexGrid();
			grfHisProcedure.Font = fEdit;
			grfHisProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
			grfHisProcedure.Location = new System.Drawing.Point(0, 0);
			grfHisProcedure.Rows.Count = 1;
			grfHisProcedure.Cols.Count = 7;
			grfHisProcedure.Cols[colHisProcCode].Width = 100;
			grfHisProcedure.Cols[colHisProcName].Width = 200;
			grfHisProcedure.Cols[colHisProcReqDate].Width = 100;
			grfHisProcedure.Cols[colHisProcReqTime].Width = 200;

			grfHisProcedure.ShowCursor = true;
			grfHisProcedure.Cols[colHisProcCode].Caption = "CODE";
			grfHisProcedure.Cols[colHisProcName].Caption = "Procedure Name";
			grfHisProcedure.Cols[colHisProcReqDate].Caption = "req date";
			grfHisProcedure.Cols[colHisProcReqTime].Caption = "req time";

			//grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
			grfHisProcedure.Cols[colHisProcCode].DataType = typeof(String);
			grfHisProcedure.Cols[colHisProcName].DataType = typeof(String);
			grfHisProcedure.Cols[colHisProcReqDate].DataType = typeof(String);
			grfHisProcedure.Cols[colHisProcReqTime].DataType = typeof(String);

			grfHisProcedure.Cols[colHisProcCode].TextAlign = TextAlignEnum.CenterCenter;
			grfHisProcedure.Cols[colHisProcName].TextAlign = TextAlignEnum.LeftCenter;
			grfHisProcedure.Cols[colHisProcReqDate].TextAlign = TextAlignEnum.CenterCenter;
			grfHisProcedure.Cols[colHisProcReqTime].TextAlign = TextAlignEnum.CenterCenter;

			grfHisProcedure.Cols[colHisProcCode].Visible = true;
			grfHisProcedure.Cols[colHisProcName].Visible = true;
			grfHisProcedure.Cols[colHisProcReqTime].Visible = false;
            grfHisProcedure.Cols[colHisProcReqNo].Visible = false;
            grfHisProcedure.Cols[colHisProcReqStatus].Visible = false;
            grfHisProcedure.Cols[colHisProcCode].AllowEditing = false;
			grfHisProcedure.Cols[colHisProcName].AllowEditing = false;
			grfHisProcedure.Cols[colHisProcReqDate].AllowEditing = false;
			grfHisProcedure.Cols[colHisProcReqTime].AllowEditing = false;

			pnHisProcedure.Controls.Add(grfHisProcedure);
			theme1.SetTheme(grfHisProcedure, bc.iniC.themeApp);
		}
		private void setGrfHisProcedure(String hn, String vsDate, String preno, ref C1FlexGrid grf)
		{
			DataTable dt = new DataTable();
			String vn = "", vsdate = "", an = "";
			grf.Rows.Count = 1;
			dt = bc.bcDB.pt16DB.SelectProcedureByVisit(hn, preno, vsDate);
			int i = 0;
			grf.Rows.Count = dt.Rows.Count + 1;
			try
			{
				foreach (DataRow row1 in dt.Rows)
				{
					i++;
					Row rowa = grf.Rows[i];
					//rowa[colHisProcReqTime] = row1["MNC_REQ_TIM"].ToString();
					rowa[colHisProcReqDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
					rowa[colHisProcName] = row1["MNC_SR_DSC"].ToString();
					rowa[colHisProcCode] = row1["MNC_SR_CD"].ToString();
					row1[0] = i;
				}
				if (dt.Rows.Count == 1)
				{
					//setXrayResult();
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FRMOPD GrfSrcVs_AfterRowColChange " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "FRMOPD setGrfHisProcedure  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
		}
		private void initGrfSrc()
		{//tab ค้นหา
			grfSrc = new C1FlexGrid();
			grfSrc.Font = fEdit;
			grfSrc.Dock = System.Windows.Forms.DockStyle.Fill;
			grfSrc.Location = new System.Drawing.Point(0, 0);
			grfSrc.Rows.Count = 1;
			grfSrc.Cols.Count = 10;
			grfSrc.Cols[colgrfSrcHn].DataType = typeof(String);
			grfSrc.Cols[colgrfSrcFullNameT].DataType = typeof(String);
			grfSrc.Cols[colgrfSrcPID].DataType = typeof(String);
			grfSrc.Cols[colgrfSrcDOB].DataType = typeof(String);
			grfSrc.Cols[colgrfSrcVisitReleaseOPD].DataType = typeof(String);
			grfSrc.Cols[colgrfSrcHn].TextAlign = TextAlignEnum.CenterCenter;
			grfSrc.Cols[colgrfSrcFullNameT].TextAlign = TextAlignEnum.LeftCenter;
			grfSrc.Cols[colgrfSrcPID].TextAlign = TextAlignEnum.CenterCenter;
			grfSrc.Cols[colgrfSrcDOB].TextAlign = TextAlignEnum.CenterCenter;
			grfSrc.Cols[colgrfSrcAge].TextAlign = TextAlignEnum.CenterCenter;
			grfSrc.Cols[colgrfSrcVisitReleaseOPD].TextAlign = TextAlignEnum.CenterCenter;
			grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].TextAlign = TextAlignEnum.CenterCenter;
			grfSrc.Cols[colgrfSrcVisitReleaseIPD].TextAlign = TextAlignEnum.CenterCenter;
			grfSrc.Cols[colgrfSrcHn].Width = 100;
			grfSrc.Cols[colgrfSrcFullNameT].Width = 300;
			grfSrc.Cols[colgrfSrcPID].Width = 150;
			grfSrc.Cols[colgrfSrcDOB].Width = 90;
			grfSrc.Cols[colgrfSrcPttid].Width = 60;
			grfSrc.Cols[colgrfSrcAge].Width = 90;
			grfSrc.Cols[colgrfSrcVisitReleaseOPD].Width = 90;
			grfSrc.Cols[colgrfSrcVisitReleaseIPD].Width = 90;
			grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].Width = 90;
			grfSrc.ShowCursor = true;
			grfSrc.Cols[colgrfSrcHn].Caption = "hn";
			grfSrc.Cols[colgrfSrcFullNameT].Caption = "full name";
			grfSrc.Cols[colgrfSrcPID].Caption = "PID";
			grfSrc.Cols[colgrfSrcDOB].Caption = "DOB";
			grfSrc.Cols[colgrfSrcPttid].Caption = "";
			grfSrc.Cols[colgrfSrcAge].Caption = "AGE";
			grfSrc.Cols[colgrfSrcVisitReleaseOPD].Caption = "มาล่าสุดOPD";
			grfSrc.Cols[colgrfSrcVisitReleaseIPD].Caption = "มาล่าสุดIPD";
			grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].Caption = "กลับบ้านIPD";

			grfSrc.Cols[colgrfSrcHn].Visible = true;
			grfSrc.Cols[colgrfSrcFullNameT].Visible = true;
			grfSrc.Cols[colgrfSrcPID].Visible = true;
			grfSrc.Cols[colgrfSrcDOB].Visible = true;
			grfSrc.Cols[colgrfSrcPttid].Visible = false;
			grfSrc.Cols[colgrfSrcAge].Visible = true;

			grfSrc.Cols[colgrfSrcHn].AllowEditing = false;
			grfSrc.Cols[colgrfSrcFullNameT].AllowEditing = false;
			grfSrc.Cols[colgrfSrcPID].AllowEditing = false;
			grfSrc.Cols[colgrfSrcDOB].AllowEditing = false;
			grfSrc.Cols[colgrfSrcAge].AllowEditing = false;
			grfSrc.Cols[colgrfSrcVisitReleaseOPD].AllowEditing = false;
			grfSrc.Cols[colgrfSrcVisitReleaseIPD].AllowEditing = false;
			grfSrc.Cols[colgrfSrcVisitReleaseIPDDischarge].AllowEditing = false;
			grfSrc.AllowFiltering = true;

			grfSrc.Click += GrfSrc_Click;
			//grfSrc.AutoSizeCols();
			//FilterRow fr = new FilterRow(grfExpn);

			//grfHn.AfterRowColChange += GrfHn_AfterRowColChange;
			//grfVs.row
			//grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
			//grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

			//menuGw.MenuItems.Add("&แก้ไข รายการเบิก", new EventHandler(ContextMenu_edit));
			//menuGw.MenuItems.Add("&แก้ไข", new EventHandler(ContextMenu_Gw_Edit));
			//menuGw.MenuItems.Add("&ยกเลิก", new EventHandler(ContextMenu_Gw_Cancel));

			pnSrcGrf.Controls.Add(grfSrc);
			theme1.SetTheme(grfSrc, bc.iniC.themeApp);
		}
		private void GrfSrc_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (grfSrc.Row <= 0) return;
			if (grfSrc.Col <= 0) return;
			HN = grfSrc[grfSrc.Row, colgrfSrcHn] != null ? grfSrc[grfSrc.Row, colgrfSrcHn].ToString() : "";
			//PRENO = grfSrc[grfSrc.Row, colgrfSrcHn] != null ? grfSrc[grfSrc.Row, colgrfSrcHn].ToString() : "";
			//VSDATE = grfSrc[grfSrc.Row, colgrfSrc] != null ? grfSrc[grfSrc.Row, colgrfSrcHn].ToString() : "";

			setControlPatientByGrf(HN);
		}
		private void initGrfSrcVs()
		{//ใช้เหมือน grfOPD
			grfSrcVs = new C1FlexGrid();
			grfSrcVs.Font = fEdit;
			grfSrcVs.Dock = System.Windows.Forms.DockStyle.Fill;
			grfSrcVs.Location = new System.Drawing.Point(0, 0);
			grfSrcVs.Rows.Count = 1;
			grfSrcVs.Cols.Count = 27;
			grfSrcVs.Cols[colVsVsDate].Width = 72;
			grfSrcVs.Cols[colVsVn].Width = 80;
			grfSrcVs.Cols[colVsDept].Width = 170;
			grfSrcVs.Cols[colVsPreno].Width = 100;
			grfSrcVs.Cols[colVsStatus].Width = 60;
			grfSrcVs.Cols[colVsDtrName].Width = 180;
			grfSrcVs.ShowCursor = true;
			//grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
			grfSrcVs.Cols[colVsVsDate].Caption = "Visit Date";
			grfSrcVs.Cols[colVsVn].Caption = "VN";
			grfSrcVs.Cols[colVsDept].Caption = "แผนก";
			grfSrcVs.Cols[colVsPreno].Caption = "";
			//grfOPD.Cols[colVsPreno].Visible = false;
			//grfOPD.Cols[colVsVn].Visible = true;
			//grfOPD.Cols[colVsAn].Visible = true;
			//grfOPD.Cols[colVsAndate].Visible = false;
			grfSrcVs.Rows[0].Visible = false;
			grfSrcVs.Cols[0].Visible = false;
			grfSrcVs.Cols[colVsVsDate].AllowEditing = false;
			grfSrcVs.Cols[colVsVn].AllowEditing = false;
			grfSrcVs.Cols[colVsDept].AllowEditing = false;
			grfSrcVs.Cols[colVsPreno].AllowEditing = false;

			grfSrcVs.Cols[colVsDtrName].AllowEditing = false;
			grfSrcVs.Cols[colVsPreno].Visible = false;
			grfSrcVs.Cols[colVsAn].Visible = false;
			grfSrcVs.Cols[colVsAndate].Visible = false;
			grfSrcVs.Cols[colVsVn].Visible = true;

			grfSrcVs.Cols[colVsbp2r].Visible = false;
			grfSrcVs.Cols[colVsbp2l].Visible = false;
			grfSrcVs.Cols[colVsbp1r].Visible = false;
			grfSrcVs.Cols[colVsbp1l].Visible = false;
			grfSrcVs.Cols[colVshc16].Visible = false;
			grfSrcVs.Cols[colVsabc].Visible = false;
			grfSrcVs.Cols[colVsccin].Visible = false;
			grfSrcVs.Cols[colVsccex].Visible = false;
			grfSrcVs.Cols[colVscc].Visible = false;
			grfSrcVs.Cols[colVsWeight].Visible = false;
			grfSrcVs.Cols[colVsHigh].Visible = false;
			grfSrcVs.Cols[colVsVital].Visible = false;
			grfSrcVs.Cols[colVsPres].Visible = false;
			grfSrcVs.Cols[colVsTemp].Visible = false;
			grfSrcVs.Cols[colVsPaidType].Visible = false;
			grfSrcVs.Cols[colVsRadios].Visible = false;
			grfSrcVs.Cols[colVsBreath].Visible = false;
			grfSrcVs.Cols[colVsStatus].Visible = false;
			grfSrcVs.Cols[colVsVsDate1].Visible = false;
			//FilterRow fr = new FilterRow(grfExpn);
			//grfOPD.AfterScroll += GrfOPD_AfterScroll;
			grfSrcVs.AfterRowColChange += GrfSrcVs_AfterRowColChange;
			//grfVs.row
			//grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
			//grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);

			spSrcVs.Controls.Add(grfSrcVs);

			//theme1.SetTheme(grfOPD, "ExpressionDark");
			theme1.SetTheme(grfSrcVs, bc.iniC.themegrfOpd);
		}
		private void setGrfSrcVs(String hn)
		{
			grfSrcVs.Rows.Count = 1;
			DataTable dt = new DataTable();
			dt = bc.bcDB.vsDB.selectVisitByHn6(hn, "O");
			int i = 1, j = 1; grfSrcVs.Rows.Count = dt.Rows.Count + 1;
			foreach (DataRow row1 in dt.Rows)
			{
				//pB1.Value++;
				Row rowa = grfSrcVs.Rows[i];
				String status = "";
				status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
				VN = row1["MNC_VN_NO"].ToString() + "." + row1["MNC_VN_SEQ"].ToString() + "." + row1["MNC_VN_SUM"].ToString();
				rowa[colVsVsDate] = bc.datetoShowShort(row1["mnc_date"].ToString());
				rowa[colVsVn] = VN;
				rowa[colVsStatus] = status;
				rowa[colVsPreno] = row1["mnc_pre_no"].ToString();
				rowa[colVsDept] = row1["MNC_SHIF_MEMO"].ToString();
				rowa[colVsAn] = row1["mnc_an_no"].ToString() + "." + row1["mnc_an_yr"].ToString();
				rowa[colVsAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
				rowa[colVsPaidType] = row1["MNC_FN_TYP_DSC"].ToString();

				rowa[colVsVsDate1] = row1["mnc_date"].ToString();
				rowa[colVsDtrName] = row1["dtr_name"].ToString();
				i++;
			}
		}
		private void GrfSrcVs_AfterRowColChange(object sender, RangeEventArgs e)
		{
			//throw new NotImplementedException();
			if (e.NewRange.r1 < 0) return;
			if (e.NewRange.Data == null) return;
			if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;

			try
			{
				PRENO = grfSrcVs[grfSrcVs.Row, colVsPreno] != null ? grfSrcVs[grfSrcVs.Row, colVsPreno].ToString() : "";
				VSDATE = grfSrcVs[grfSrcVs.Row, colVsVsDate1] != null ? grfSrcVs[grfSrcVs.Row, colVsVsDate1].ToString() : "";
				VN = grfSrcVs[grfSrcVs.Row, colVsVn] != null ? grfSrcVs[grfSrcVs.Row, colVsVn].ToString() : "";
				//HN = grfSrcVs[grfSrcVs.Row, colVs] != null ? grfSrcVs[grfSrcVs.Row, colVsVn].ToString() : "";

				btnSrcEKGScanSave.Enabled = false;
				lbSrcPreno.Value = PRENO;
				lbSrcVsDate.Value = bc.datetoShow1(VSDATE);
				lbSrcVN.Value = VN;
				setSrcStaffNote(VSDATE, PRENO);
				setGrfSrcLab(VSDATE, PRENO);
				setGrfSrcOrder(VSDATE, PRENO);
				setGrfSrcXray(VSDATE, PRENO);
				setGrfSrcProcedure(VSDATE, PRENO);
				setControlSrcPttSrc(VN, PRENO, VSDATE);
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FRMOPD GrfSrcVs_AfterRowColChange " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "FRMOPD GrfSrcVs_AfterRowColChange  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
			finally
			{
				//frmFlash.Dispose();
			}
		}
		private void initGrfSrcOrder()
		{
			grfSrcOrder = new C1FlexGrid();
			grfSrcOrder.Font = fEdit;
			grfSrcOrder.Dock = System.Windows.Forms.DockStyle.Fill;
			grfSrcOrder.Location = new System.Drawing.Point(0, 0);
			grfSrcOrder.Rows.Count = 1;
			grfSrcOrder.Cols.Count = 9;
			grfSrcOrder.Cols[colgrfOrderCode].Width = 100;
			grfSrcOrder.Cols[colgrfOrderName].Width = 200;

			grfSrcOrder.ShowCursor = true;
			grfSrcOrder.Cols[colgrfOrderCode].Caption = "code";
			grfSrcOrder.Cols[colgrfOrderName].Caption = "name";

			//grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
			grfSrcOrder.Cols[colgrfOrderCode].DataType = typeof(String);
			grfSrcOrder.Cols[colgrfOrderName].DataType = typeof(String);

			grfSrcOrder.Cols[colgrfOrderCode].TextAlign = TextAlignEnum.CenterCenter;
			grfSrcOrder.Cols[colgrfOrderName].TextAlign = TextAlignEnum.LeftCenter;

			grfSrcOrder.Cols[colgrfOrderCode].Visible = true;
			grfSrcOrder.Cols[colgrfOrderName].Visible = true;

			grfSrcOrder.Cols[colgrfOrderCode].AllowEditing = false;
			grfSrcOrder.Cols[colgrfOrderName].AllowEditing = false;

			pnSrcDrug.Controls.Add(grfSrcOrder);
			theme1.SetTheme(grfSrcOrder, bc.iniC.themeApp);
		}
		private void setGrfSrcOrder(String vsDate, String preno)
		{
			DataTable dtOrder = new DataTable();
			dtOrder = bc.bcDB.vsDB.selectDrugOPD(HN, preno, vsDate);
			grfSrcOrder.Rows.Count = 1;
			int i = 0;
			decimal aaa = 0;
			foreach (DataRow row1 in dtOrder.Rows)
			{
				i++;
				Row rowa = grfSrcOrder.Rows.Add();
				rowa[colOrderName] = row1["MNC_PH_TN"].ToString();
				rowa[colOrderMed] = "";
				rowa[colOrderQty] = row1["qty"].ToString();
				rowa[colOrderDate] = bc.datetoShow(row1["mnc_req_dat"]);
				rowa[colOrderFre] = row1["MNC_PH_DIR_DSC"].ToString();
				rowa[colOrderIn1] = row1["MNC_PH_CAU_dsc"].ToString();
				//row1[0] = (i - 2);
			}
		}
		private void initGrfSrcXray()
		{
			grfSrcXray = new C1FlexGrid();
			grfSrcXray.Font = fEdit;
			grfSrcXray.Dock = System.Windows.Forms.DockStyle.Fill;
			grfSrcXray.Location = new System.Drawing.Point(0, 0);
			grfSrcXray.Cols.Count = 5;
			grfSrcXray.Cols[colXrayDate].Caption = "วันที่สั่ง";
			grfSrcXray.Cols[colXrayName].Caption = "ชื่อX-Ray";
			grfSrcXray.Cols[colXrayCode].Caption = "Code X-Ray";
			//grfXray.Cols[colXrayResult].Caption = "ผล X-Ray";

			grfSrcXray.Cols[colXrayDate].Width = 100;
			grfSrcXray.Cols[colXrayName].Width = 250;
			grfSrcXray.Cols[colXrayCode].Width = 100;
			grfSrcXray.Cols[colXrayResult].Width = 200;

			grfSrcXray.Cols[colXrayDate].AllowEditing = false;
			grfSrcXray.Cols[colXrayName].AllowEditing = false;
			grfSrcXray.Cols[colXrayCode].AllowEditing = false;
			grfSrcXray.Cols[colXrayResult].AllowEditing = false;

			grfSrcXray.Name = "grfSrcXray";
			grfSrcXray.Rows.Count = 1;
			pnSrcXray.Controls.Add(grfSrcXray);

			theme1.SetTheme(grfSrcXray, bc.iniC.themeApp);
		}
		private void setGrfSrcXray(String vsDate, String preno)
		{
			DataTable dt = new DataTable();
			String vn = "", vsdate = "", an = "";
			grfSrcXray.Rows.Count = 1;
			dt = bc.bcDB.vsDB.selectResultXraybyVN1(HN, preno, vsDate);
			int i = 0;
			grfSrcXray.Rows.Count = dt.Rows.Count + 1;
			try
			{
				foreach (DataRow row1 in dt.Rows)
				{
					i++;
					Row rowa = grfSrcXray.Rows[i];
					rowa[colXrayDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
					rowa[colXrayName] = row1["MNC_XR_DSC"].ToString();
					rowa[colXrayCode] = row1["MNC_XR_CD"].ToString();
					row1[0] = i;
				}
				if (dt.Rows.Count == 1)
				{
					//setXrayResult();
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FRMOPD setGrfSrcXray " + ex.Message);
			}
		}
		private void initGrfSrcLab()
		{
			grfSrcLab = new C1FlexGrid();
			grfSrcLab.Font = fEdit;
			grfSrcLab.Dock = System.Windows.Forms.DockStyle.Fill;
			grfSrcLab.Location = new System.Drawing.Point(0, 0);
			grfSrcLab.Rows.Count = 1;
			grfSrcLab.Cols.Count = 6;

			grfSrcLab.Cols.Count = 8;
			grfSrcLab.Cols[colLabDate].Caption = "วันที่สั่ง";
			grfSrcLab.Cols[colLabName].Caption = "ชื่อLAB";
			grfSrcLab.Cols[colLabNameSub].Caption = "ชื่อLABย่อย";
			grfSrcLab.Cols[colLabResult].Caption = "ผลLAB";
			grfSrcLab.Cols[colInterpret].Caption = "แปรผล";
			grfSrcLab.Cols[colNormal].Caption = "Normal";
			grfSrcLab.Cols[colUnit].Caption = "Unit";
			grfSrcLab.Cols[colLabDate].Width = 100;
			grfSrcLab.Cols[colLabName].Width = 250;
			grfSrcLab.Cols[colLabNameSub].Width = 200;
			grfSrcLab.Cols[colInterpret].Width = 200;
			grfSrcLab.Cols[colNormal].Width = 200;
			grfSrcLab.Cols[colUnit].Width = 150;
			grfSrcLab.Cols[colLabResult].Width = 150;

			grfSrcLab.Cols[colLabName].AllowEditing = false;
			grfSrcLab.Cols[colInterpret].AllowEditing = false;
			grfSrcLab.Cols[colNormal].AllowEditing = false;

			pnSrcLab.Controls.Add(grfSrcLab);

			//theme1.SetTheme(grfOPD, "ExpressionDark");
			theme1.SetTheme(grfSrcLab, bc.iniC.themegrfOpd);
		}
		private void setGrfSrcLab(String vsDate, String preno)
		{
			DataTable dt = new DataTable();
			DateTime dtt = new DateTime();

			if (vsDate.Length <= 0)
			{
				return;
			}
			dt = bc.bcDB.vsDB.selectLabResultbyVN(HN, preno, vsDate);
			grfSrcLab.Rows.Count = 1; grfSrcLab.Rows.Count = dt.Rows.Count + 1;
			try
			{
				int i = 0, row = grfSrcLab.Rows.Count;
				foreach (DataRow row1 in dt.Rows)
				{
					i++;
					Row rowa = grfSrcLab.Rows[i];
					rowa[colLabName] = row1["MNC_LB_DSC"].ToString();
					rowa[colLabDate] = bc.datetoShow(row1["mnc_req_dat"].ToString());
					rowa[colLabNameSub] = row1["mnc_res"].ToString();
					rowa[colLabResult] = row1["MNC_RES_VALUE"].ToString();
					rowa[colInterpret] = row1["MNC_STS"].ToString();
					rowa[colNormal] = row1["MNC_LB_RES"].ToString();
					rowa[colUnit] = row1["MNC_RES_UNT"].ToString();
					row1[0] = i;
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FrmOPD setGrfSrcLab grfLab " + ex.Message);
			}
		}
		private void initGrfDocOLD()
		{
			grfDocOLD = new C1FlexGrid();
			grfDocOLD.Font = fEdit;
			grfDocOLD.Dock = System.Windows.Forms.DockStyle.Fill;
			grfDocOLD.Location = new System.Drawing.Point(0, 0);
			grfDocOLD.Rows.Count = 1;
			grfDocOLD.Cols.Count = 8;

			grfDocOLD.Cols[colgrfOutLabDscHN].Width = 80;
			grfDocOLD.Cols[colgrfOutLabDscPttName].Width = 250;
			grfDocOLD.Cols[colgrfOutLabDscVsDate].Width = 100;
			grfDocOLD.Cols[colgrfOutLabDscVN].Width = 80;
			grfDocOLD.Cols[colgrfOutLablabcode].Width = 80;
			grfDocOLD.Cols[colgrfOutLablabname].Width = 250;

			grfDocOLD.Cols[colgrfOutLabDscHN].Caption = "HN";
			grfDocOLD.Cols[colgrfOutLabDscPttName].Caption = "Name";
			grfDocOLD.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
			grfDocOLD.Cols[colgrfOutLabDscVN].Caption = "VN";

			grfDocOLD.Cols[colgrfOutLabDscId].Visible = false;
			grfDocOLD.Cols[colgrfOutLabDscHN].AllowEditing = false;
			grfDocOLD.Cols[colgrfOutLabDscPttName].AllowEditing = false;
			grfDocOLD.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
			grfDocOLD.Cols[colgrfOutLabDscVN].AllowEditing = false;
			grfDocOLD.Cols[colgrfOutLablabcode].Visible = false;
			grfDocOLD.Cols[colgrfOutLablabname].Visible = false;
			grfDocOLD.DoubleClick += GrfDocOLD_DoubleClick;
			ContextMenu menuGw = new ContextMenu();
			menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(ContextMenu_voidDocOLD));
			grfDocOLD.ContextMenu = menuGw;
			pnDocOLD.Controls.Add(grfDocOLD);

			//theme1.SetTheme(grfOPD, "ExpressionDark");
			theme1.SetTheme(grfDocOLD, bc.iniC.themegrfOpd);
		}
		private void ContextMenu_voidDocOLD(object sender, System.EventArgs e)
		{
			if (grfDocOLD.Row <= 0) return;
			if (grfDocOLD.Col <= 0) return;
			String dscid = "";
			dscid = grfDocOLD[grfDocOLD.Row, colgrfOutLabDscId] != null ? grfDocOLD[grfDocOLD.Row, colgrfOutLabDscId].ToString() : "";
			if (dscid.Length <= 0)
			{
				lfSbMessage.Text = "ไม่พบข้อมูล EKG";
				return;
			}
			FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
			frm.ShowDialog();
			frm.Dispose();
			if (bc.USERCONFIRMID.Length <= 0)
			{
				lfSbMessage.Text = "Password ไม่ถูกต้อง";
				return;
			}
			String re = bc.bcDB.dscDB.voidDocScan(dscid, bc.userId);
			setGrfDocOLD();
			//pnDocOLD.Controls.Clear();
		}
		private void GrfDocOLD_DoubleClick(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (grfDocOLD.Row <= 0) return;
			if (grfDocOLD.Col <= 0) return;
			String hn = "", vn = "", vsDate = "", dscid = "";
			dscid = grfDocOLD[grfDocOLD.Row, colgrfOutLabDscId] != null ? grfDocOLD[grfDocOLD.Row, colgrfOutLabDscId].ToString() : "";
			try
			{
				pnDocOLDView.Controls.Clear();
				C1FlexViewer fv = new C1FlexViewer();
				fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
				fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
				fv.Dock = System.Windows.Forms.DockStyle.Fill;
				fv.Location = new System.Drawing.Point(0, 0);
				fv.Name = "fvSrcDocOLDScan";
				fv.Size = new System.Drawing.Size(1065, 790);
				fv.TabIndex = 0;
				fv.Ribbon.Minimized = true;
				pnDocOLDView.Controls.Add(fv);
				try
				{
					DocScan dsc = new DocScan();
					dsc = bc.bcDB.dscDB.selectByPk(dscid);
					C1PdfDocumentSource pds = new C1PdfDocumentSource();
					FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
					MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
					if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
					{
						pds.LoadFromStream(streamCertiDtr);
						fv.DocumentSource = pds;
					}
				}
				catch (Exception ex)
				{
					lfSbMessage.Text = ex.Message;
					new LogWriter("e", "FrmOPD GrfDocOLD_DoubleClick " + ex.Message);
					bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfDocOLD_DoubleClick", ex.Message);
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FrmOPD GrfDocOLD_DoubleClick " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfDocOLD_DoubleClick save  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
		}
		private void setGrfDocOLD()
		{
			DataTable dt = new DataTable();
			dt = bc.bcDB.dscDB.selectByDocOLD(txtSrcHn.Text.Trim());
			grfDocOLD.Rows.Count = 1; grfDocOLD.Rows.Count = dt.Rows.Count + 1; int i = 0;
			foreach (DataRow row1 in dt.Rows)
			{
				i++;
				Row rowa = grfDocOLD.Rows[i];
				rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
				rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
				rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
			}
		}
		private void initGrfEKG()
		{
			grfEKG = new C1FlexGrid();
			grfEKG.Font = fEdit;
			grfEKG.Dock = System.Windows.Forms.DockStyle.Fill;
			grfEKG.Location = new System.Drawing.Point(0, 0);
			grfEKG.Rows.Count = 1; grfEKG.Cols.Count = 8;

			grfEKG.Cols[colgrfOutLabDscHN].Width = 80; grfEKG.Cols[colgrfOutLabDscPttName].Width = 250; grfEKG.Cols[colgrfOutLabDscVsDate].Width = 100;
			grfEKG.Cols[colgrfOutLabDscVN].Width = 80; grfEKG.Cols[colgrfOutLablabcode].Width = 80; grfEKG.Cols[colgrfOutLablabname].Width = 250;

			grfEKG.Cols[colgrfOutLabDscHN].Caption = "HN"; grfEKG.Cols[colgrfOutLabDscPttName].Caption = "Name"; grfEKG.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
			grfEKG.Cols[colgrfOutLabDscVN].Caption = "VN";

			grfEKG.Cols[colgrfOutLabDscId].Visible = false; grfEKG.Cols[colgrfOutLabDscHN].AllowEditing = false; grfEKG.Cols[colgrfOutLabDscPttName].AllowEditing = false;
			grfEKG.Cols[colgrfOutLabDscVsDate].AllowEditing = false; grfEKG.Cols[colgrfOutLabDscVN].AllowEditing = false; grfEKG.Cols[colgrfOutLablabcode].Visible = false;
			grfEKG.Cols[colgrfOutLablabname].Visible = false;
			grfEKG.DoubleClick += GrfEKG_DoubleClick;
			ContextMenu menuGw = new ContextMenu();
			menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(ContextMenu_voidEKG));
			grfEKG.ContextMenu = menuGw;
			pnEKG.Controls.Add(grfEKG);

			//theme1.SetTheme(grfOPD, "ExpressionDark");
			theme1.SetTheme(grfEKG, bc.iniC.themegrfOpd);
		}
		private void ContextMenu_voidEKG(object sender, System.EventArgs e)
		{
			if (grfEKG.Row <= 0) return;
			if (grfEKG.Col <= 0) return;
			String dscid = "";
			dscid = grfEKG[grfEKG.Row, colgrfOutLabDscId] != null ? grfEKG[grfEKG.Row, colgrfOutLabDscId].ToString() : "";
			if (dscid.Length <= 0)
			{
				lfSbMessage.Text = "ไม่พบข้อมูล EKG";
				return;
			}
			FrmPasswordConfirm frm = new FrmPasswordConfirm(bc); frm.ShowDialog();
			frm.Dispose();
			if (bc.USERCONFIRMID.Length <= 0) { lfSbMessage.Text = "Password ไม่ถูกต้อง"; return; }
			String re = bc.bcDB.dscDB.voidDocScan(dscid, bc.userId);
			setGrfEKG();
			//pnEKGView.Controls.Clear();
		}
		private void GrfEKG_DoubleClick(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (grfEKG.Row <= 0) return; if (grfEKG.Col <= 0) return;
			String hn = "", vn = "", vsDate = "", dscid = "";
			dscid = grfEKG[grfEKG.Row, colgrfOutLabDscId] != null ? grfEKG[grfEKG.Row, colgrfOutLabDscId].ToString() : "";
			try
			{
				pnEKGView.Controls.Clear();
				C1FlexViewer fv = new C1FlexViewer();
				fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
				fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
				fv.Dock = System.Windows.Forms.DockStyle.Fill;
				fv.Location = new System.Drawing.Point(0, 0);
				fv.Name = "fvSrcEKGScan";
				fv.Size = new System.Drawing.Size(1065, 790);
				fv.TabIndex = 0;
				fv.Ribbon.Minimized = true;
				pnEKGView.Controls.Add(fv);
				try
				{
					DocScan dsc = new DocScan();
					dsc = bc.bcDB.dscDB.selectByPk(dscid);
					C1PdfDocumentSource pds = new C1PdfDocumentSource();
					FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
					MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
					if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
					{
						pds.LoadFromStream(streamCertiDtr);
						fv.DocumentSource = pds;
					}
				}
				catch (Exception ex)
				{
					lfSbMessage.Text = ex.Message;
					new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
					bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick", ex.Message);
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick save  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
		}
		private void setGrfEKG()
		{

			DataTable dt = new DataTable();
			dt = bc.bcDB.dscDB.selectByEKG(txtSrcHn.Text.Trim());
			grfEKG.Rows.Count = 1; grfEKG.Rows.Count = dt.Rows.Count + 1; int i = 0;
			foreach (DataRow row1 in dt.Rows)
			{
				i++;
				Row rowa = grfEKG.Rows[i];
				rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
				rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
				rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
			}
		}
		private void initGrfCertMed()
		{
			grfCertMed = new C1FlexGrid();
			grfCertMed.Font = fEdit; grfCertMed.Dock = System.Windows.Forms.DockStyle.Fill;
			grfCertMed.Location = new System.Drawing.Point(0, 0);
			grfCertMed.Rows.Count = 1; grfCertMed.Cols.Count = 8;

			grfCertMed.Cols[colgrfOutLabDscHN].Width = 80; grfCertMed.Cols[colgrfOutLabDscPttName].Width = 250;
			grfCertMed.Cols[colgrfOutLabDscVsDate].Width = 100; grfCertMed.Cols[colgrfOutLabDscVN].Width = 80;
			grfCertMed.Cols[colgrfOutLablabcode].Width = 80; grfCertMed.Cols[colgrfOutLablabname].Width = 250;

			grfCertMed.Cols[colgrfOutLabDscHN].Caption = "HN"; grfCertMed.Cols[colgrfOutLabDscPttName].Caption = "Name";
			grfCertMed.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date"; grfCertMed.Cols[colgrfOutLabDscVN].Caption = "VN";

			grfCertMed.Cols[colgrfOutLabDscId].Visible = false; grfCertMed.Cols[colgrfOutLabDscHN].AllowEditing = false;
			grfCertMed.Cols[colgrfOutLabDscPttName].AllowEditing = false; grfCertMed.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
			grfCertMed.Cols[colgrfOutLabDscVN].AllowEditing = false; grfCertMed.Cols[colgrfOutLablabcode].Visible = false;
			grfCertMed.Cols[colgrfOutLablabname].Visible = false;
			grfCertMed.Click += GrfCertMed_Click;
			//ContextMenu menuGw = new ContextMenu();
			//menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(GrfCertMed_DoubleClick));
			//grfCertMed.ContextMenu = menuGw;
			pnCertMed1.Controls.Add(grfCertMed);

			//theme1.SetTheme(grfOPD, "ExpressionDark");
			theme1.SetTheme(grfCertMed, bc.iniC.themegrfOpd);
		}

		private void GrfCertMed_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (grfCertMed.Row <= 0) return;
			if (grfCertMed.Col <= 0) return;
			String hn = "", vn = "", vsDate = "", dscid = "";
			dscid = grfCertMed[grfCertMed.Row, colgrfOutLabDscId] != null ? grfCertMed[grfCertMed.Row, colgrfOutLabDscId].ToString() : "";
			try
			{
				DocScan dsc = new DocScan();
				dsc = bc.bcDB.dscDB.selectByPk(dscid);
				pnCertMedView.Controls.Clear();
				try
				{
					C1FlexViewer fv = new C1FlexViewer();
					fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
					fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
					fv.Dock = System.Windows.Forms.DockStyle.Fill;
					fv.Location = new System.Drawing.Point(0, 0);
					fv.Name = "fvSrcCertMEdScan";
					fv.Size = new System.Drawing.Size(1065, 790);
					fv.TabIndex = 0;
					fv.Ribbon.Minimized = true;
					pnCertMedView.Controls.Add(fv);
					C1PdfDocumentSource pds = new C1PdfDocumentSource();
					FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
					MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
					txtdocNo.Value = dsc.doc_scan_id;
					if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
					{
						pds.LoadFromStream(streamCertiDtr);
						fv.DocumentSource = pds;
					}
					else
					{
						var pdf = new C1PdfDocument();
						Image img = Image.FromStream(streamCertiDtr);
						// Replace this line in GrfCertMed_DoubleClick:
						// pdf.DrawImage(img, RectangleF.FromLTRB(0, 0, img.Width, img.Height));

						// With the following code to fit the image to the A4 page size:

						var pageWidth = 595f;  // A4 width in points (8.27 inch * 72)
						var pageHeight = 842f; // A4 height in points (11.69 inch * 72)
						float imgAspect = (float)img.Width / img.Height;
						float pageAspect = pageWidth / pageHeight;
						float drawWidth, drawHeight, offsetX, offsetY;

						if (imgAspect > pageAspect)
						{
							// Image is wider relative to page
							drawWidth = pageWidth;
							drawHeight = pageWidth / imgAspect;
							offsetX = 0;
							offsetY = (pageHeight - drawHeight) / 2;
						}
						else
						{
							// Image is taller relative to page
							drawHeight = pageHeight - 40;
							drawWidth = pageHeight * imgAspect;
							offsetX = (((pageWidth - drawWidth) / 2));
							//offsetX += (float)(0.6);
							offsetY = (float)0.6;
						}
						pdf.DrawImage(img, new RectangleF(offsetX, offsetY, drawWidth, drawHeight));
						//pdf.DrawImage(img, RectangleF.FromLTRB(0, 0, img.Width, img.Height));
						string tempPdf = Path.GetTempFileName() + ".pdf";
						pdf.Save(tempPdf);
						pds.LoadFromFile(tempPdf);
						fv.DocumentSource = pds;
						//PictureBox pic = new PictureBox();
						//pic.Image = Image.FromStream(streamCertiDtr);
						//pic.SizeMode = PictureBoxSizeMode.Zoom;
						//pic.Dock = DockStyle.Fill;
						//pnCertMedView.Controls.Clear();
						//pnCertMedView.Controls.Add(pic);
					}
				}
				catch (Exception ex)
				{
					lfSbMessage.Text = ex.Message;
					new LogWriter("e", "FrmOPD GrfCertMed_Click " + ex.Message);
					bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfCertMed_Click", ex.Message);
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FrmOPD GrfCertMed_Click " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfCertMed_Click save  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
		}
		private void setGrfCertMed()
		{
			grfCertMed.Rows.Count = 1;
			DataTable dt = new DataTable();
			dt = bc.bcDB.dscDB.selectByCertMed(txtSrcHn.Text.Trim());
			foreach (DataRow row1 in dt.Rows)
			{
				Row rowa = grfCertMed.Rows.Add();
				rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
				rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
				rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
			}
		}
		private void initGrfECHO()
		{
			grfECHO = new C1FlexGrid();
			grfECHO.Font = fEdit; grfECHO.Dock = System.Windows.Forms.DockStyle.Fill; grfECHO.Location = new System.Drawing.Point(0, 0);
			grfECHO.Rows.Count = 1; grfECHO.Cols.Count = 8;
			grfECHO.Cols[colgrfOutLabDscHN].Width = 80; grfECHO.Cols[colgrfOutLabDscPttName].Width = 250; grfECHO.Cols[colgrfOutLabDscVsDate].Width = 100;
			grfECHO.Cols[colgrfOutLabDscVN].Width = 80; grfECHO.Cols[colgrfOutLablabcode].Width = 80; grfECHO.Cols[colgrfOutLablabname].Width = 250;
			grfECHO.Cols[colgrfOutLabDscHN].Caption = "HN"; grfECHO.Cols[colgrfOutLabDscPttName].Caption = "Name"; grfECHO.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
			grfECHO.Cols[colgrfOutLabDscVN].Caption = "VN";
			grfECHO.Cols[colgrfOutLabDscId].Visible = false; grfECHO.Cols[colgrfOutLabDscHN].AllowEditing = false; grfECHO.Cols[colgrfOutLabDscPttName].AllowEditing = false;
			grfECHO.Cols[colgrfOutLabDscVsDate].AllowEditing = false; grfECHO.Cols[colgrfOutLabDscVN].AllowEditing = false; grfECHO.Cols[colgrfOutLablabcode].Visible = false;
			grfECHO.Cols[colgrfOutLablabname].Visible = false;
			grfECHO.DoubleClick += GrfECHO_DoubleClick;
			ContextMenu menuGw = new ContextMenu();
			menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(ContextMenu_voidECHO));
			grfECHO.ContextMenu = menuGw;
			pnECHO.Controls.Add(grfECHO);
			//theme1.SetTheme(grfOPD, "ExpressionDark");
			theme1.SetTheme(grfECHO, bc.iniC.themegrfOpd);
		}
		private void ContextMenu_voidECHO(object sender, System.EventArgs e)
		{
			if (grfECHO.Row <= 0) return;
			if (grfECHO.Col <= 0) return;
			String dscid = "";
			dscid = grfECHO[grfECHO.Row, colgrfOutLabDscId] != null ? grfECHO[grfECHO.Row, colgrfOutLabDscId].ToString() : "";
			if (dscid.Length <= 0)
			{
				lfSbMessage.Text = "ไม่พบข้อมูล EKG";
				return;
			}
			FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
			frm.ShowDialog();
			frm.Dispose();
			if (bc.USERCONFIRMID.Length <= 0)
			{
				lfSbMessage.Text = "Password ไม่ถูกต้อง";
				return;
			}
			String re = bc.bcDB.dscDB.voidDocScan(dscid, bc.userId);
			setGrfECHO();
			//pnEKGView.Controls.Clear();
		}
		private void GrfECHO_DoubleClick(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (grfECHO.Row <= 0) return;
			if (grfECHO.Col <= 0) return;
			String hn = "", vn = "", vsDate = "", dscid = "";
			dscid = grfECHO[grfECHO.Row, colgrfOutLabDscId] != null ? grfECHO[grfECHO.Row, colgrfOutLabDscId].ToString() : "";
			try
			{
				pnECHOView.Controls.Clear();
				C1FlexViewer fv = new C1FlexViewer();
				fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
				fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
				fv.Dock = System.Windows.Forms.DockStyle.Fill;
				fv.Location = new System.Drawing.Point(0, 0);
				fv.Name = "fvSrcEKGScan";
				fv.Size = new System.Drawing.Size(1065, 790);
				fv.TabIndex = 0;
				fv.Ribbon.Minimized = true;
				pnECHOView.Controls.Add(fv);
				try
				{
					DocScan dsc = new DocScan();
					dsc = bc.bcDB.dscDB.selectByPk(dscid);
					C1PdfDocumentSource pds = new C1PdfDocumentSource();
					FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
					MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
					if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
					{
						pds.LoadFromStream(streamCertiDtr);
						fv.DocumentSource = pds;
					}
				}
				catch (Exception ex)
				{
					lfSbMessage.Text = ex.Message;
					new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
					bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick", ex.Message);
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick save  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
		}
		private void setGrfECHO()
		{
			DataTable dt = new DataTable();
			dt = bc.bcDB.dscDB.selectByECHO(txtSrcHn.Text.Trim());
			grfECHO.Rows.Count = 1; grfECHO.Rows.Count = dt.Rows.Count + 1; int i = 0;
			foreach (DataRow row1 in dt.Rows)
			{
				i++;
				Row rowa = grfECHO.Rows[i];
				rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
				rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
				rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
			}
		}
		private void initGrfEST()
		{
			grfEST = new C1FlexGrid();
			grfEST.Font = fEdit; grfEST.Dock = System.Windows.Forms.DockStyle.Fill; grfEST.Location = new System.Drawing.Point(0, 0);
			grfEST.Rows.Count = 1; grfEST.Cols.Count = 8;
			grfEST.Cols[colgrfOutLabDscHN].Width = 80; grfEST.Cols[colgrfOutLabDscPttName].Width = 250; grfEST.Cols[colgrfOutLabDscVsDate].Width = 100;
			grfEST.Cols[colgrfOutLabDscVN].Width = 80; grfEST.Cols[colgrfOutLablabcode].Width = 80; grfEST.Cols[colgrfOutLablabname].Width = 250;
			grfEST.Cols[colgrfOutLabDscHN].Caption = "HN"; grfEST.Cols[colgrfOutLabDscPttName].Caption = "Name"; grfEST.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
			grfEST.Cols[colgrfOutLabDscVN].Caption = "VN";
			grfEST.Cols[colgrfOutLabDscId].Visible = false; grfEST.Cols[colgrfOutLabDscHN].AllowEditing = false; grfEST.Cols[colgrfOutLabDscPttName].AllowEditing = false;
			grfEST.Cols[colgrfOutLabDscVsDate].AllowEditing = false; grfEST.Cols[colgrfOutLabDscVN].AllowEditing = false; grfEST.Cols[colgrfOutLablabcode].Visible = false;
			grfEST.Cols[colgrfOutLablabname].Visible = false;
			grfEST.DoubleClick += GrfEST_DoubleClick;
			ContextMenu menuGw = new ContextMenu();
			menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(ContextMenu_voidEST));
			grfEST.ContextMenu = menuGw;
			pnEST.Controls.Add(grfEST);

			//theme1.SetTheme(grfOPD, "ExpressionDark");
			theme1.SetTheme(grfEST, bc.iniC.themegrfOpd);
		}
		private void ContextMenu_voidEST(object sender, System.EventArgs e)
		{
			if (grfEST.Row <= 0) return;
			if (grfEST.Col <= 0) return;
			String dscid = "";
			dscid = grfEST[grfEST.Row, colgrfOutLabDscId] != null ? grfEST[grfEST.Row, colgrfOutLabDscId].ToString() : "";
			if (dscid.Length <= 0)
			{
				lfSbMessage.Text = "ไม่พบข้อมูล EKG";
				return;
			}
			FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
			frm.ShowDialog();
			frm.Dispose();
			if (bc.USERCONFIRMID.Length <= 0)
			{
				lfSbMessage.Text = "Password ไม่ถูกต้อง";
				return;
			}
			String re = bc.bcDB.dscDB.voidDocScan(dscid, bc.userId);
			setGrfEST();
			//pnEKGView.Controls.Clear();
		}
		private void GrfEST_DoubleClick(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (grfEST.Row <= 0) return;
			if (grfEST.Col <= 0) return;
			String hn = "", vn = "", vsDate = "", dscid = "";
			dscid = grfEST[grfEST.Row, colgrfOutLabDscId] != null ? grfEST[grfEST.Row, colgrfOutLabDscId].ToString() : "";
			try
			{
				pnESTView.Controls.Clear();
				C1FlexViewer fv = new C1FlexViewer();
				fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
				fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
				fv.Dock = System.Windows.Forms.DockStyle.Fill;
				fv.Location = new System.Drawing.Point(0, 0);
				fv.Name = "fvSrcEKGScan";
				fv.Size = new System.Drawing.Size(1065, 790);
				fv.TabIndex = 0;
				fv.Ribbon.Minimized = true;
				pnESTView.Controls.Add(fv);
				try
				{
					DocScan dsc = new DocScan();
					dsc = bc.bcDB.dscDB.selectByPk(dscid);
					C1PdfDocumentSource pds = new C1PdfDocumentSource();
					FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
					MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
					if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
					{
						pds.LoadFromStream(streamCertiDtr);
						fv.DocumentSource = pds;
					}
				}
				catch (Exception ex)
				{
					lfSbMessage.Text = ex.Message;
					new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
					bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick", ex.Message);
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick save  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
		}
		private void setGrfEST()
		{
			DataTable dt = new DataTable();
			dt = bc.bcDB.dscDB.selectByEST(txtSrcHn.Text.Trim());
			grfEST.Rows.Count = 1; grfEST.Rows.Count = dt.Rows.Count + 1; int i = 0;
			foreach (DataRow row1 in dt.Rows)
			{
				i++;
				Row rowa = grfEST.Rows[i];
				rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
				rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
				rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
			}
		}
		private void initGrfHolter()
		{
			grfHolter = new C1FlexGrid();
			grfHolter.Font = fEdit; grfHolter.Dock = System.Windows.Forms.DockStyle.Fill; grfHolter.Location = new System.Drawing.Point(0, 0);
			grfHolter.Rows.Count = 1; grfHolter.Cols.Count = 8;
			grfHolter.Cols[colgrfOutLabDscHN].Width = 80; grfHolter.Cols[colgrfOutLabDscPttName].Width = 250; grfHolter.Cols[colgrfOutLabDscVsDate].Width = 100;
			grfHolter.Cols[colgrfOutLabDscVN].Width = 80; grfHolter.Cols[colgrfOutLablabcode].Width = 80; grfHolter.Cols[colgrfOutLablabname].Width = 250;
			grfHolter.Cols[colgrfOutLabDscHN].Caption = "HN"; grfHolter.Cols[colgrfOutLabDscPttName].Caption = "Name"; grfHolter.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date";
			grfHolter.Cols[colgrfOutLabDscVN].Caption = "VN";
			grfHolter.Cols[colgrfOutLabDscId].Visible = false; grfHolter.Cols[colgrfOutLabDscHN].AllowEditing = false; grfEST.Cols[colgrfOutLabDscPttName].AllowEditing = false;
			grfHolter.Cols[colgrfOutLabDscVsDate].AllowEditing = false; grfHolter.Cols[colgrfOutLabDscVN].AllowEditing = false; grfHolter.Cols[colgrfOutLablabcode].Visible = false;
			grfHolter.Cols[colgrfOutLablabname].Visible = false;
			grfHolter.DoubleClick += GrfHolter_DoubleClick;
			ContextMenu menuGw = new ContextMenu();
			menuGw.MenuItems.Add("ต้องการ ยกเลิก", new EventHandler(ContextMenu_voidHolter));
			grfHolter.ContextMenu = menuGw;
			pnHolter.Controls.Add(grfHolter);
			//theme1.SetTheme(grfOPD, "ExpressionDark");
			theme1.SetTheme(grfEST, bc.iniC.themegrfOpd);
		}
		private void ContextMenu_voidHolter(object sender, System.EventArgs e)
		{
			if (grfHolter.Row <= 0) return;
			if (grfHolter.Col <= 0) return;
			String dscid = "";
			dscid = grfHolter[grfHolter.Row, colgrfOutLabDscId] != null ? grfHolter[grfHolter.Row, colgrfOutLabDscId].ToString() : "";
			if (dscid.Length <= 0)
			{
				lfSbMessage.Text = "ไม่พบข้อมูล EKG";
				return;
			}
			FrmPasswordConfirm frm = new FrmPasswordConfirm(bc);
			frm.ShowDialog();
			frm.Dispose();
			if (bc.USERCONFIRMID.Length <= 0)
			{
				lfSbMessage.Text = "Password ไม่ถูกต้อง";
				return;
			}
			String re = bc.bcDB.dscDB.voidDocScan(dscid, bc.userId);
			setGrfHolter();
			//pnEKGView.Controls.Clear();
		}
		private void GrfHolter_DoubleClick(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (grfHolter.Row <= 0) return;
			if (grfHolter.Col <= 0) return;
			String hn = "", vn = "", vsDate = "", dscid = "";
			dscid = grfHolter[grfHolter.Row, colgrfOutLabDscId] != null ? grfHolter[grfHolter.Row, colgrfOutLabDscId].ToString() : "";
			try
			{
				pnHolterView.Controls.Clear();
				C1FlexViewer fv = new C1FlexViewer();
				fv.AutoScrollMargin = new System.Drawing.Size(0, 0);
				fv.AutoScrollMinSize = new System.Drawing.Size(0, 0);
				fv.Dock = System.Windows.Forms.DockStyle.Fill;
				fv.Location = new System.Drawing.Point(0, 0);
				fv.Name = "fvSrcEKGScan";
				fv.Size = new System.Drawing.Size(1065, 790);
				fv.TabIndex = 0;
				fv.Ribbon.Minimized = true;
				pnHolterView.Controls.Add(fv);
				try
				{
					DocScan dsc = new DocScan();
					dsc = bc.bcDB.dscDB.selectByPk(dscid);
					C1PdfDocumentSource pds = new C1PdfDocumentSource();
					FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
					MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
					if (dsc.image_path.ToLower().IndexOf("pdf") > 0)
					{
						pds.LoadFromStream(streamCertiDtr);
						fv.DocumentSource = pds;
					}
				}
				catch (Exception ex)
				{
					lfSbMessage.Text = ex.Message;
					new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
					bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick", ex.Message);
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FrmOPD GrfEKG_DoubleClick " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfEKG_DoubleClick save  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
		}
		private void setGrfHolter()
		{
			DataTable dt = new DataTable();
			dt = bc.bcDB.dscDB.selectByHolter(txtSrcHn.Text.Trim());
			grfHolter.Rows.Count = 1; grfHolter.Rows.Count = dt.Rows.Count + 1; int i = 0;
			foreach (DataRow row1 in dt.Rows)
			{
				i++;
				Row rowa = grfHolter.Rows[i];
				rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["visit_date"].ToString());
				rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
				rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
			}
		}
		private void initGrfIPD()
		{
			grfIPD = new C1FlexGrid();
			grfIPD.Font = fEdit; grfIPD.Dock = System.Windows.Forms.DockStyle.Fill; grfIPD.Location = new System.Drawing.Point(0, 0);
			grfIPD.Rows.Count = 1; grfIPD.Cols.Count = 11;
			grfIPD.Cols[colIPDDate].Width = 90; grfIPD.Cols[colIPDVn].Width = 80; grfIPD.Cols[colIPDDept].Width = 170;
			grfIPD.Cols[colIPDPreno].Width = 100; grfIPD.Cols[colIPDStatus].Width = 60; grfIPD.Cols[colIPDDtrName].Width = 180;
			grfIPD.ShowCursor = true;
			//grfVs.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.RestrictRows;
			grfIPD.Cols[colIPDDate].Caption = "Visit Date"; grfIPD.Cols[colIPDVn].Caption = "VN"; grfIPD.Cols[colIPDDept].Caption = "อาการ";
			grfIPD.Cols[colIPDAnShow].Caption = "AN";
			grfIPD.Cols[colIPDPreno].Visible = false; grfIPD.Cols[colIPDVn].Visible = true; grfIPD.Cols[colIPDAnShow].Visible = true;
			grfIPD.Cols[colIPDAndate].Visible = false; grfIPD.Cols[colIPDPreno].Visible = false; grfIPD.Cols[colIPDVn].Visible = false;
			grfIPD.Cols[colIPDStatus].Visible = false; grfIPD.Cols[colIPDAnYr].Visible = false; grfIPD.Cols[colIPDAn].Visible = false;
			grfIPD.Cols[colIPDDate].AllowEditing = false; grfIPD.Cols[colIPDVn].AllowEditing = false; grfIPD.Cols[colIPDDept].AllowEditing = false;
			grfIPD.Cols[colIPDPreno].AllowEditing = false; grfIPD.Cols[colIPDDtrName].AllowEditing = false;
			//FilterRow fr = new FilterRow(grfExpn);

			grfIPD.AfterRowColChange += GrfIPD_AfterRowColChange;
			//grfVs.row
			//grfExpnC.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellButtonClick);
			//grfExpnC.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.grfDept_CellChanged);
			spMedScanIPD.Controls.Add(grfIPD);
			//theme1.SetTheme(grfIPD, "ExpressionDark");
			theme1.SetTheme(grfIPD, bc.iniC.themegrfIpd);
		}

		private void GrfIPD_AfterRowColChange(object sender, RangeEventArgs e)
		{
			//throw new NotImplementedException();
			if (e.NewRange.r1 < 0) return;
			if (e.NewRange.Data == null) return;

			String an = "", vsDate = "", preno = "";

			an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";
			vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
			preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";

			setGrfIPDScan();
			lfSbMessage.Text = an;
		}
		private void setGrfIPD()
		{
			DataTable dt = new DataTable();
			//MessageBox.Show("hn "+hn, "");
			dt = bc.bcDB.vsDB.selectVisitIPDByHn5(txtSBSearchHN.Text);
			int i = 1, j = 1, row = grfIPD.Rows.Count;
			//txtVN.Value = dt.Rows.Count;
			//txtName.Value = "";
			//txt.Value = "";
			grfIPD.Rows.Count = 1;
			grfIPD.Rows.Count = dt.Rows.Count + 1;
			//pB1.Maximum = dt.Rows.Count;
			foreach (DataRow row1 in dt.Rows)
			{
				//pB1.Value++;
				Row rowa = grfIPD.Rows[i];
				String status = "", vn = "";

				//status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
				status = "IPD";
				vn = row1["MNC_VN_NO"].ToString() + "." + row1["MNC_VN_SEQ"].ToString() + "." + row1["MNC_VN_SUM"].ToString();
				rowa[colIPDDate] = bc.datetoShow1(row1["mnc_date"].ToString());
				rowa[colIPDVn] = vn;
				rowa[colIPDStatus] = status;
				rowa[colIPDPreno] = row1["mnc_pre_no"].ToString();
				rowa[colIPDDept] = row1["MNC_SHIF_MEMO"].ToString();
				rowa[colIPDAnShow] = row1["mnc_an_no"].ToString() + "." + row1["mnc_an_yr"].ToString();
				rowa[colIPDAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
				rowa[colIPDAnYr] = row1["mnc_an_yr"].ToString();
				rowa[colIPDAn] = row1["mnc_an_no"].ToString();
				rowa[colIPDDtrName] = row1["dtr_name"].ToString();
				i++;
			}
		}
		private void setGrfIPDScan()
		{
			//Application.DoEvents();
			//ProgressBar pB1 = new ProgressBar();
			//pB1.Location = new System.Drawing.Point(20, 16);
			//pB1.Name = "pB1";
			//pB1.Size = new System.Drawing.Size(862, 23);
			//gbPtt.Controls.Add(pB1);
			//pB1.Left = txtHn.Left;
			//pB1.Show();
			showLbLoading();
			lStream.Clear();
			//clearGrf();
			String statusOPD = "", vsDate = "", vn = "", an = "", anDate = "", hn = "", preno = "", anyr = "", vn1 = "";
			DataTable dtOrder = new DataTable();

			//new LogWriter("e", "FrmScanView1 setGrfScan 5 ");
			GC.Collect();

			DataTable dt = new DataTable();
			statusOPD = grfIPD[grfIPD.Row, colIPDStatus] != null ? grfIPD[grfIPD.Row, colIPDStatus].ToString() : "";
			preno = grfIPD[grfIPD.Row, colIPDPreno] != null ? grfIPD[grfIPD.Row, colIPDPreno].ToString() : "";
			vsDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";

			an = grfIPD[grfIPD.Row, colIPDAn] != null ? grfIPD[grfIPD.Row, colIPDAn].ToString() : "";
			anDate = grfIPD[grfIPD.Row, colIPDDate] != null ? grfIPD[grfIPD.Row, colIPDDate].ToString() : "";
			anyr = grfIPD[grfIPD.Row, colIPDAnYr] != null ? grfIPD[grfIPD.Row, colIPDAnYr].ToString() : "";
			//label2.Text = "AN :";
			an = grfIPD[grfIPD.Row, colIPDAnShow] != null ? grfIPD[grfIPD.Row, colIPDAnShow].ToString() : "";

			vsDate = bc.datetoDB(vsDate);
			//setStaffNote(vsDate, preno);
			dt = bc.bcDB.dscDB.selectByAn(txtSBSearchHN.Text, an.Replace(".", "/"));
			grfIPDScan.Rows.Count = 0;
			if (dt.Rows.Count > 0)
			{
				try
				{
					int cnt = 0;
					cnt = dt.Rows.Count / 2;

					grfIPDScan.Rows.Count = cnt + 1;

					FtpClient ftp = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP);
					Boolean findTrue = false;
					int colcnt = 0, rowrun = -1;
					foreach (DataRow row1 in dt.Rows)
					{
						if (findTrue) break;
						colcnt++;
						String dgssid = "", filename = "", ftphost = "", id = "", folderftp = "";
						id = row1[bc.bcDB.dscDB.dsc.doc_scan_id].ToString();
						dgssid = row1[bc.bcDB.dscDB.dsc.doc_group_sub_id].ToString();
						filename = row1[bc.bcDB.dscDB.dsc.image_path].ToString();
						ftphost = row1[bc.bcDB.dscDB.dsc.host_ftp].ToString();
						folderftp = row1[bc.bcDB.dscDB.dsc.folder_ftp].ToString();

						//new Thread(() =>
						//{
						String err = "";
						try
						{
							FtpWebRequest ftpRequest = null;
							FtpWebResponse ftpResponse = null;
							Stream ftpStream = null;
							int bufferSize = 2048;
							err = "00";
							Row rowd;
							if ((colcnt % 2) == 0)
							{
								rowd = grfIPDScan.Rows[rowrun];
							}
							else
							{
								rowrun++;
								rowd = grfIPDScan.Rows[rowrun];
								Application.DoEvents();
							}
							MemoryStream stream;
							Image loadedImage, resizedImage;
							stream = new MemoryStream();
							//stream = ftp.download(folderftp + "//" + filename);

							//loadedImage = Image.FromFile(filename);
							err = "01";

							ftpRequest = (FtpWebRequest)FtpWebRequest.Create(ftphost + "/" + folderftp + "/" + filename);
							ftpRequest.Credentials = new NetworkCredential(bc.iniC.userFTP, bc.iniC.passFTP);
							ftpRequest.UseBinary = true;
							ftpRequest.UsePassive = bc.ftpUsePassive;
							ftpRequest.KeepAlive = true;
							ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
							ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
							ftpStream = ftpResponse.GetResponseStream();
							err = "02";
							byte[] byteBuffer = new byte[bufferSize];
							int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
							try
							{
								while (bytesRead > 0)
								{
									stream.Write(byteBuffer, 0, bytesRead);
									bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
								}
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.ToString());
								new LogWriter("e", "FrmScanView1 SetGrfScan try int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize); ex " + ex.Message + " " + err);
							}
							err = "03";

							loadedImage = new Bitmap(stream);
							err = "04";
							int originalWidth = 0;
							originalWidth = loadedImage.Width;
							int newWidth = bc.imgScanWidth;
							resizedImage = loadedImage.GetThumbnailImage(newWidth, (newWidth * loadedImage.Height) / originalWidth, null, IntPtr.Zero);
							//
							err = "05";
							if ((colcnt % 2) == 0)
							{

								rowd[colPic3] = resizedImage;       // + 0001
								err = "061";       // + 0001
								rowd[colPic4] = id;       // + 0001
								err = "071";       // + 0001
							}
							else
							{

								err = "051";       // + 0001
								rowd[colPic1] = resizedImage;       // + 0001
								err = "06";       // + 0001
								rowd[colPic2] = id;       // + 0001
								err = "07";       // + 0001
							}

							strm = new listStream();
							strm.id = id;
							strm.dgsid = row1[bc.bcDB.dscDB.dsc.doc_group_id].ToString();
							err = "08";
							strm.stream = stream;
							err = "09";
							lStream.Add(strm);

							err = "12";

							if (colcnt == 50) GC.Collect();
							if (colcnt == 100) GC.Collect();
						}
						catch (Exception ex)
						{
							String aaa = ex.Message + " " + err;
							new LogWriter("e", "FrmScanView1 SetGrfScan ex " + ex.Message + " " + err + " colcnt " + colcnt + " doc_scan_id " + id);
						}

					}
					ftp = null;
				}
				catch (Exception ex)
				{
					//MessageBox.Show("" + ex.Message, "");
					new LogWriter("e", "FrmScanView1 SetGrfScan if (dt.Rows.Count > 0) ex " + ex.Message);
				}
			}
			grfIPDScan.AutoSizeCols();
			grfIPDScan.AutoSizeRows();
			hideLbLoading();
		}
		private void initGrfHisOrder()
		{
			grfHisOrder = new C1FlexGrid();
			grfHisOrder.Font = fEdit; grfHisOrder.Dock = System.Windows.Forms.DockStyle.Fill; grfHisOrder.Location = new System.Drawing.Point(0, 0);
			grfHisOrder.Cols[colOrderId].Visible = false;
			grfHisOrder.Rows.Count = 1; grfHisOrder.Cols.Count = 8;
			grfHisOrder.Cols[colOrderName].Caption = "Drug Name"; grfHisOrder.Cols[colOrderMed].Caption = "MED"; grfHisOrder.Cols[colOrderQty].Caption = "QTY";
			grfHisOrder.Cols[colOrderDate].Caption = "Date"; grfHisOrder.Cols[colOrderFre].Caption = "วิธีใช้"; grfHisOrder.Cols[colOrderIn1].Caption = "ข้อควรระวัง";
			grfHisOrder.Cols[colOrderName].Width = 400; grfHisOrder.Cols[colOrderMed].Width = 200; grfHisOrder.Cols[colOrderQty].Width = 60;
			grfHisOrder.Cols[colOrderDate].Width = 90; grfHisOrder.Cols[colOrderFre].Width = 500; grfHisOrder.Cols[colOrderIn1].Width = 350;
			grfHisOrder.Cols[colOrderName].AllowEditing = false; grfHisOrder.Cols[colOrderQty].AllowEditing = false; grfHisOrder.Cols[colOrderMed].AllowEditing = false;
			grfHisOrder.Cols[colOrderFre].AllowEditing = false; grfHisOrder.Cols[colOrderIn1].AllowEditing = false; grfHisOrder.Cols[colOrderDate].AllowEditing = false;
			grfHisOrder.Name = "grfHisOrder";
			pnHistoryOrder.Controls.Add(grfHisOrder);
		}
		private void setGrfHisOrder(String hn, String vsDate, String preno, ref C1FlexGrid grf)
		{
			DataTable dtOrder = new DataTable();
			dtOrder = bc.bcDB.vsDB.selectDrugOPD(hn, preno, vsDate);
			grf.Rows.Count = 1; grf.Rows.Count = dtOrder.Rows.Count; int i = 0;
			decimal aaa = 0;
			foreach (DataRow row1 in dtOrder.Rows)
			{
				i++;
				Row rowa = grf.Rows[i];
				rowa[colOrderName] = row1["MNC_PH_TN"].ToString();
				rowa[colOrderMed] = "";
				rowa[colOrderQty] = row1["qty"].ToString();
				rowa[colOrderDate] = bc.datetoShow(row1["mnc_req_dat"]);
				rowa[colOrderFre] = row1["MNC_PH_DIR_DSC"].ToString();
				rowa[colOrderIn1] = row1["MNC_PH_CAU_dsc"].ToString();
				//row1[0] = (i - 2);
			}
		}
		private void initGrfXray(ref C1FlexGrid grf, ref Panel pn)
		{
			grf = new C1FlexGrid();
			grf.Font = fEdit; grf.Dock = System.Windows.Forms.DockStyle.Fill; grf.Location = new System.Drawing.Point(0, 0);
			grf.Cols.Count = 5;
			grf.Cols[colXrayDate].Caption = "วันที่สั่ง"; grf.Cols[colXrayName].Caption = "ชื่อX-Ray"; grf.Cols[colXrayCode].Caption = "Code X-Ray";
			//grfXray.Cols[colXrayResult].Caption = "ผล X-Ray";
			grf.Cols[colXrayDate].Width = 100; grf.Cols[colXrayName].Width = 250; grf.Cols[colXrayCode].Width = 100;
			grf.Cols[colXrayResult].Width = 200;
			grf.Cols[colXrayDate].AllowEditing = false; grf.Cols[colXrayName].AllowEditing = false; grf.Cols[colXrayCode].AllowEditing = false;
			grf.Cols[colXrayResult].AllowEditing = false;
			grf.Name = "grfXray"; grf.Rows.Count = 1;
			pn.Controls.Add(grf);
            if (pn.Name.Equals("pnPrenoXray"))
            {
                grf.Name = "grfOperXray";
				ContextMenu menu = new ContextMenu();
				menu.MenuItems.Add("พิมพ์ใบสั่ง Xray", new EventHandler(ContextMenuOperXray_Click));
				grf.ContextMenu = menu;
                grf.Click += GrfOperXray_Click;
            }else if (pn.Name.Equals("pnHistoryXray"))
			{
				grf.Name = "grfXray";
            }else if (pn.Name.Equals("pnFinishXray"))
			{
                grf.Name = "grfOperFinishXray";
            }
            theme1.SetTheme(grf, bc.iniC.themeApp);
		}
        private void ContextMenuOperXray_Click(object sender, System.EventArgs e)
		{
            if (HN.Length <= 0) { lfSbMessage.Text = "ไม่พบ HN"; return; }
            if (REQNOXRAY.Length <= 0) { lfSbMessage.Text = "ไม่พบ Req No"; return; }
            if (REQDATEXRAY.Length <= 0) { lfSbMessage.Text = "ไม่พบ Req Date"; return; }
            printXrayReqNo(HN, REQNOXRAY, REQDATEXRAY);
        }
        private void GrfOperXray_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //btnOperPrnReq.Text = "พิมพ์ใบสั่ง Xray";
			REQNOXRAY = grfOperXray[grfOperXray.Row, colLabReqNo]?.ToString()?? "";		//set grfOperXray rowa[colLabReqNo] = row1["req_no"].ToString();
            REQDATEXRAY = grfOperXray[grfOperXray.Row, colLabReqDate]?.ToString()?? "";	//set grfOperXray rowa[colLabReqDate] = row1["req_date"].ToString();
        }
        private void setGrfXray(String hn, String vsDate, String preno, ref C1FlexGrid grf)
		{
			DataTable dt = new DataTable();
			String vn = "", vsdate = "", an = "";
			dt = bc.bcDB.vsDB.selectResultXraybyVN1(hn, preno, vsDate);
			int i = 0; grf.Rows.Count = 1; grf.Rows.Count = dt.Rows.Count + 1;
			try
			{
				foreach (DataRow row1 in dt.Rows)
				{
					i++;
					Row rowa = grf.Rows[i];
					rowa[colXrayDate] = bc.datetoShow(row1["MNC_REQ_DAT"].ToString());
					rowa[colXrayName] = row1["MNC_XR_DSC"].ToString();
					rowa[colXrayCode] = row1["MNC_XR_CD"].ToString();
					row1[0] = i;
				}
				if (dt.Rows.Count == 1)
				{
					//setXrayResult();
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FrmScanView1 setGrfXrayOPD " + ex.Message);
			}
		}
		private void initGrfLab(ref C1FlexGrid grf,ref Panel pn)
		{
			grf = new C1FlexGrid();
			grf.Font = fEdit; grf.Dock = System.Windows.Forms.DockStyle.Fill; grf.Location = new System.Drawing.Point(0, 0);
			grf.Rows.Count = 1; grf.Cols.Count = 6;
			grf.Cols.Count = 8;
			grf.Cols[colLabDate].Caption = "วันที่สั่ง"; grf.Cols[colLabName].Caption = "ชื่อLAB"; grf.Cols[colLabNameSub].Caption = "ชื่อLABย่อย";
			grf.Cols[colLabResult].Caption = "ผลLAB"; grf.Cols[colInterpret].Caption = "แปรผล"; grf.Cols[colNormal].Caption = "Normal";
			grf.Cols[colUnit].Caption = "Unit";
			grf.Cols[colLabDate].Width = 100; grf.Cols[colLabName].Width = 250; grf.Cols[colLabNameSub].Width = 200;
			grf.Cols[colInterpret].Width = 200; grf.Cols[colNormal].Width = 200; grf.Cols[colUnit].Width = 150;
			grf.Cols[colLabResult].Width = 150;
			grf.Cols[colLabName].AllowEditing = false; grf.Cols[colInterpret].AllowEditing = false; grf.Cols[colNormal].AllowEditing = false;

			pn.Controls.Add(grf);
			if (pn.Name.Equals("pnPrenoLab")) 
			{ 
				grf.Name = "grfOperLab";                grf.Click += GrfOperLab_Click;
				ContextMenu menu = new ContextMenu();
				menu.MenuItems.Add("พิมพ์ใบสั่ง Lab", new EventHandler(ContextMenuOperLab_Click));
				grf.ContextMenu = menu;
            }
                //theme1.SetTheme(grfOPD, "ExpressionDark");
            theme1.SetTheme(grf, bc.iniC.themegrfOpd);
		}
        private void GrfOperLab_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //btnOperPrnReq.Text = "พิมพ์ใบสั่ง Lab";
			REQNOLAB = grfOperLab[grfOperLab.Row, colLabReqNo]?.ToString()?? "";
			REQDATELAB = grfOperLab[grfOperLab.Row, colLabReqDate]?.ToString()?? "";
        }
		private void ContextMenuOperLab_Click(object sender, System.EventArgs e)
		{
			if(HN.Length <= 0)				{				lfSbMessage.Text = "ไม่พบ HN";				return;			}
			if(REQNOLAB.Length <= 0)		{				lfSbMessage.Text = "ไม่พบ Req No";			return;			}
			if(REQDATELAB.Length <= 0)	{				lfSbMessage.Text = "ไม่พบ Req Date";			return;			}
			printLabReqNo(HN, REQNOLAB, REQDATELAB);
        }
        private void setGrfLab(String hn, String vsDate, String preno, ref C1FlexGrid grf)
		{
			DataTable dt = new DataTable();
			DateTime dtt = new DateTime();

			if (vsDate.Length <= 0)
			{
				return;
			}
			dt = bc.bcDB.vsDB.selectLabResultbyVN(hn, preno, vsDate);
			grf.Rows.Count = 1; grf.Rows.Count = dt.Rows.Count + 1;
			try
			{
				int i = 0, row = grf.Rows.Count;
				foreach (DataRow row1 in dt.Rows)
				{
					i++;
					Row rowa = grf.Rows[i];
					rowa[colLabName] = row1["MNC_LB_DSC"].ToString();
					rowa[colLabDate] = bc.datetoShow(row1["mnc_req_dat"].ToString());
					rowa[colLabNameSub] = row1["mnc_res"].ToString();
					rowa[colLabResult] = row1["MNC_RES_VALUE"].ToString();
					rowa[colInterpret] = row1["MNC_STS"].ToString();
					rowa[colNormal] = row1["MNC_LB_RES"].ToString();
					rowa[colUnit] = row1["MNC_RES_UNT"].ToString();
					row1[0] = i;
				}
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FrmScanView1 setGrfLab grfLab " + ex.Message);
			}
		}
		private void initGrfTodayOutLab()
		{
			grfTodayOutLab = new C1FlexGrid();
			grfTodayOutLab.Font = fEdit; grfTodayOutLab.Dock = System.Windows.Forms.DockStyle.Fill; grfTodayOutLab.Location = new System.Drawing.Point(0, 0);
			grfTodayOutLab.Rows.Count = 1; grfTodayOutLab.Cols.Count = 13;
			grfTodayOutLab.Cols[colgrfOutLabDscHN].Width = 80; grfTodayOutLab.Cols[colgrfOutLabDscPttName].Width = 220; grfTodayOutLab.Cols[colgrfOutLabDscVsDate].Width = 90;
			grfTodayOutLab.Cols[colgrfOutLabDscVN].Width = 80; grfTodayOutLab.Cols[colgrfOutLablabcode].Width = 80; grfTodayOutLab.Cols[colgrfOutLablabname].Width = 200;
			grfTodayOutLab.Cols[colgrfOutLabApmDesc].Width = 200; grfTodayOutLab.Cols[colgrfOutLabApmDtr].Width = 150; grfTodayOutLab.Cols[colgrfOutLabApmDate].Width = 100;
			grfTodayOutLab.Cols[colgrfOutLabDscHN].DataType = typeof(String); grfTodayOutLab.Cols[colgrfOutLabDscVsDate].DataType = typeof(String); grfTodayOutLab.Cols[colgrfOutLablabcode].DataType = typeof(String);
			grfTodayOutLab.Cols[colgrfOutLabApmDate].DataType = typeof(String); grfTodayOutLab.Cols[colgrfOutLabDscVsDate].DataType = typeof(String);
			grfTodayOutLab.Cols[colgrfOutLabDscHN].TextAlign = TextAlignEnum.CenterCenter; grfTodayOutLab.Cols[colgrfOutLabDscVsDate].TextAlign = TextAlignEnum.CenterCenter;
			grfTodayOutLab.Cols[colgrfOutLablabcode].TextAlign = TextAlignEnum.CenterCenter; grfTodayOutLab.Cols[colgrfOutLabApmDate].TextAlign = TextAlignEnum.CenterCenter;
			grfTodayOutLab.Cols[colgrfOutLabDscVsDate].TextAlign = TextAlignEnum.CenterCenter;
			grfTodayOutLab.Cols[colgrfOutLabDscHN].Caption = "HN"; grfTodayOutLab.Cols[colgrfOutLabDscPttName].Caption = "Name";
			grfTodayOutLab.Cols[colgrfOutLabDscVsDate].Caption = "req Date"; grfTodayOutLab.Cols[colgrfOutLabDscVN].Caption = "VN";
			grfTodayOutLab.Cols[colgrfOutLablabcode].Caption = "code"; grfTodayOutLab.Cols[colgrfOutLablabname].Caption = "lab name";
			grfTodayOutLab.Cols[colgrfOutLabDscId].Visible = false; grfTodayOutLab.Cols[colgrfOutLabDscVN].Visible = false;
			grfTodayOutLab.Cols[colgrfOutLabApmReqNo].Visible = true; grfTodayOutLab.Cols[colgrfOutLabApmReqDate].Visible = false;
			grfTodayOutLab.Cols[colgrfOutLabApmReqNo].AllowEditing = false; grfTodayOutLab.Cols[colgrfOutLabDscHN].AllowEditing = false;
			grfTodayOutLab.Cols[colgrfOutLabDscPttName].AllowEditing = false; grfTodayOutLab.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
			grfTodayOutLab.Cols[colgrfOutLabDscVN].AllowEditing = false; grfTodayOutLab.Cols[colgrfOutLablabcode].AllowEditing = false;
			grfTodayOutLab.Cols[colgrfOutLablabname].AllowEditing = false; grfTodayOutLab.Cols[colgrfOutLabApmDate].AllowEditing = false;
			grfTodayOutLab.Cols[colgrfOutLabApmDesc].AllowEditing = false; grfTodayOutLab.Cols[colgrfOutLabApmDtr].AllowEditing = false;
			grfTodayOutLab.AfterRowColChange += GrfTodayOutLab_AfterRowColChange;
			pnTodayOutLabList.Controls.Add(grfTodayOutLab);
			theme1.SetTheme(grfTodayOutLab, bc.iniC.themegrfOpd);
		}

		private void GrfTodayOutLab_AfterRowColChange(object sender, RangeEventArgs e)
		{
			//throw new NotImplementedException();
			if (e.NewRange.r1 < 0) return;
			if (e.NewRange.Data == null) return;
			if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
			String reqno = "", reqdate = "", hn = "", dscid = "";
			try
			{
				reqno = grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabApmReqNo] != null ? grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabApmReqNo].ToString() : "";
				reqdate = grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabApmReqDate] != null ? grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabApmReqDate].ToString() : "";
				hn = grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabDscHN] != null ? grfTodayOutLab[grfTodayOutLab.Row, colgrfOutLabDscHN].ToString() : "";
				//dscid = grfOutLab[grfOutLab.Row, colVsPreno] != null ? grfOutLab[grfOutLab.Row, colgrfOutLabDscId].ToString() : "";
				DocScan dsc = new DocScan();
				dsc = bc.bcDB.dscDB.selectLabOutByHnReqDateReqNo1(hn, reqdate, reqno);
				if (dsc.doc_scan_id.Length <= 0)
				{
					dsc = bc.bcDB.dscDB.selectLabOutByHnReqDateReqNoUnActive(hn, reqdate, reqno);
				}
				C1PdfDocumentSource pds = new C1PdfDocumentSource();

				FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
				MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
				pds.LoadFromStream(streamCertiDtr);
				fvTodayOutLab.DocumentSource = pds;
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FRMOPD GrfOPD_AfterRowColChange " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "FRMOPD GrfOPD_AfterRowColChange  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
			finally
			{
				//frmFlash.Dispose();
			}
		}
		private void setGrfTodayOutLab()
		{
			DateTime.TryParse(txtSBSearchDate.Text, out DateTime datestart);

			DataTable dt = new DataTable();
			dt = bc.bcDB.labT02DB.selectByTodayOutLab(datestart.Year.ToString() + "-" + datestart.ToString("MM-dd"));
			//MessageBox.Show("01 ", "");
			int i = 1, j = 1; grfTodayOutLab.Rows.Count = 1; grfTodayOutLab.Rows.Count = dt.Rows.Count + 1;
			//grfTodayOutLab.Rows.Count = dt.Rows.Count;
			foreach (DataRow row1 in dt.Rows)
			{
				Row rowa = grfTodayOutLab.Rows[i];
				String status = "", vn = "";
				rowa[colgrfOutLabDscHN] = row1["MNC_HN_NO"].ToString();
				//rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
				rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["MNC_REQ_DAT"].ToString());
				rowa[colgrfOutLabDscPttName] = row1["pttfullname"].ToString();
				rowa[colgrfOutLablabcode] = row1["MNC_LB_CD"].ToString();
				rowa[colgrfOutLablabname] = row1["MNC_LB_DSC"].ToString();
				rowa[colgrfOutLabApmDate] = row1["MNC_APP_DAT"].ToString();
				rowa[colgrfOutLabApmDesc] = row1["MNC_APP_DSC"].ToString();
				rowa[colgrfOutLabApmDtr] = row1["dtr_name"].ToString();
				rowa[colgrfOutLabApmReqNo] = row1["MNC_REQ_NO"].ToString();
				rowa[colgrfOutLabApmReqDate] = row1["MNC_REQ_DAT"].ToString();
				i++;
			}
		}
		private void initGrfOutLab()
		{
			grfOutLab = new C1FlexGrid();
			grfOutLab.Font = fEdit; grfOutLab.Dock = System.Windows.Forms.DockStyle.Fill; grfOutLab.Location = new System.Drawing.Point(0, 0);
			grfOutLab.Rows.Count = 1; grfOutLab.Cols.Count = 8;
			grfOutLab.Cols[colgrfOutLabDscHN].Width = 80; grfOutLab.Cols[colgrfOutLabDscPttName].Width = 250; grfOutLab.Cols[colgrfOutLabDscVsDate].Width = 100;
			grfOutLab.Cols[colgrfOutLabDscVN].Width = 80; grfOutLab.Cols[colgrfOutLablabcode].Width = 80; grfOutLab.Cols[colgrfOutLablabname].Width = 250;
			grfOutLab.Cols[colgrfOutLabDscHN].Caption = "HN"; grfOutLab.Cols[colgrfOutLabDscPttName].Caption = "Name";
			grfOutLab.Cols[colgrfOutLabDscVsDate].Caption = "Visit Date"; grfOutLab.Cols[colgrfOutLabDscVN].Caption = "VN";
			grfOutLab.Cols[colgrfOutLablabcode].Caption = "code"; grfOutLab.Cols[colgrfOutLablabname].Caption = "lab name";
			grfOutLab.Cols[colgrfOutLabDscId].Visible = false; grfOutLab.Cols[colgrfOutLabDscHN].AllowEditing = false;
			grfOutLab.Cols[colgrfOutLabDscPttName].AllowEditing = false; grfOutLab.Cols[colgrfOutLabDscVsDate].AllowEditing = false;
			grfOutLab.Cols[colgrfOutLabDscVN].AllowEditing = false; grfOutLab.Cols[colgrfOutLablabcode].AllowEditing = false;
			grfOutLab.Cols[colgrfOutLablabname].AllowEditing = false;
			grfOutLab.AfterRowColChange += GrfOutLab_AfterRowColChange;
			spOutLabList.Controls.Add(grfOutLab);
			theme1.SetTheme(grfOutLab, bc.iniC.themegrfOpd);
		}
		private void GrfOutLab_AfterRowColChange(object sender, RangeEventArgs e)
		{
			//throw new NotImplementedException();
			if (e.NewRange.r1 < 0) return;
			if (e.NewRange.Data == null) return;
			if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
			String dscid = "";
			try
			{
				fvCerti.DocumentSource = null;
				dscid = grfOutLab[grfOutLab.Row, colVsPreno] != null ? grfOutLab[grfOutLab.Row, colgrfOutLabDscId].ToString() : "";
				C1PdfDocumentSource pds = new C1PdfDocumentSource();
				DocScan dsc = new DocScan();
				dsc = bc.bcDB.dscDB.selectByPk(dscid);
				FtpClient ftpc = new FtpClient(bc.iniC.hostFTP, bc.iniC.userFTP, bc.iniC.passFTP, bc.ftpUsePassive);
				MemoryStream streamCertiDtr = ftpc.download(dsc.folder_ftp + "/" + dsc.image_path.ToString());
				pds.LoadFromStream(streamCertiDtr);
				fvCerti.DocumentSource = pds;
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FrmOPD GrfOutLab_AfterRowColChange " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "GrfOutLab_AfterRowColChange save  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
		}
		private void setGrfOutLab()
		{
			if (grfOutLab == null) initGrfOutLab();
			fvCerti.DocumentSource = null;
			DataTable dt = new DataTable();
			dt = bc.bcDB.dscDB.selectOutLabByHn(txtSBSearchHN.Text.Trim());
			//MessageBox.Show("01 ", "");
			int i = 1, j = 1, row = grfOutLab.Rows.Count; grfOutLab.Rows.Count = 1; grfOutLab.Rows.Count = dt.Rows.Count + 1;
			foreach (DataRow row1 in dt.Rows)
			{
				Row rowa = grfOutLab.Rows[i];
				String status = "", vn = "";
				rowa[colgrfOutLabDscHN] = row1["hn"].ToString();
				rowa[colgrfOutLabDscVN] = row1["vn"].ToString();
				rowa[colgrfOutLabDscVsDate] = bc.datetoShow1(row1["date_req"].ToString());
				rowa[colgrfOutLabDscPttName] = row1["patient_fullname"].ToString();
				rowa[colgrfOutLabDscId] = row1["doc_scan_id"].ToString();
				i++;
			}
		}
		private void initGrfOPD()
		{
			grfOPD = new C1FlexGrid();
			grfOPD.Font = fEdit; grfOPD.Dock = System.Windows.Forms.DockStyle.Fill; grfOPD.Location = new System.Drawing.Point(0, 0);
			grfOPD.Rows.Count = 1; grfOPD.Cols.Count = 27; grfOPD.Cols[colVsVsDate].Width = 72;
			grfOPD.Cols[colVsVn].Width = 80; grfOPD.Cols[colVsDept].Width = 170; grfOPD.Cols[colVsPreno].Width = 100;
			grfOPD.Cols[colVsStatus].Width = 60; grfOPD.Cols[colVsDtrName].Width = 180;
			grfOPD.ShowCursor = true;
			grfOPD.Cols[colVsVsDate].Caption = "Visit Date"; grfOPD.Cols[colVsVn].Caption = "VN"; grfOPD.Cols[colVsDept].Caption = "แผนก";
			grfOPD.Cols[colVsPreno].Caption = "";
			grfOPD.Rows[0].Visible = false; grfOPD.Cols[0].Visible = false; grfOPD.Cols[colVsVsDate].AllowEditing = false;
			grfOPD.Cols[colVsVn].AllowEditing = false; grfOPD.Cols[colVsDept].AllowEditing = false; grfOPD.Cols[colVsPreno].AllowEditing = false;
			grfOPD.Cols[colVsDtrName].AllowEditing = false; grfOPD.Cols[colVsPreno].Visible = false; grfOPD.Cols[colVsAn].Visible = false;
			grfOPD.Cols[colVsAndate].Visible = false; grfOPD.Cols[colVsVn].Visible = false; grfOPD.Cols[colVsbp2r].Visible = false;
			grfOPD.Cols[colVsbp2l].Visible = false; grfOPD.Cols[colVsbp1r].Visible = false; grfOPD.Cols[colVsbp1l].Visible = false;
			grfOPD.Cols[colVshc16].Visible = false; grfOPD.Cols[colVsabc].Visible = false; grfOPD.Cols[colVsccin].Visible = false;
			grfOPD.Cols[colVsccex].Visible = false; grfOPD.Cols[colVscc].Visible = false; grfOPD.Cols[colVsWeight].Visible = false;
			grfOPD.Cols[colVsHigh].Visible = false; grfOPD.Cols[colVsVital].Visible = false; grfOPD.Cols[colVsPres].Visible = false;
			grfOPD.Cols[colVsTemp].Visible = false; grfOPD.Cols[colVsPaidType].Visible = false; grfOPD.Cols[colVsRadios].Visible = false;
			grfOPD.Cols[colVsBreath].Visible = false; grfOPD.Cols[colVsStatus].Visible = false; grfOPD.Cols[colVsVsDate1].Visible = false;
			grfOPD.AfterRowColChange += GrfOPD_AfterRowColChange;
			spHistoryVS.Controls.Add(grfOPD);
			theme1.SetTheme(grfOPD, bc.iniC.themegrfOpd);
		}
		private void GrfOPD_AfterRowColChange(object sender, RangeEventArgs e)
		{
			//throw new NotImplementedException();
			if (e.NewRange.r1 < 0) return;
			if (e.NewRange.Data == null) return;
			if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
			if (grfOPD.Row <= 0) return;
			try
			{
				String preno = grfOPD[grfOPD.Row, colVsPreno] != null ? grfOPD[grfOPD.Row, colVsPreno].ToString() : "";
				String vsdate = grfOPD[grfOPD.Row, colVsVsDate1] != null ? grfOPD[grfOPD.Row, colVsVsDate1].ToString() : "";
				setStaffNote(vsdate, preno, picHisL, picHisR);
				setGrfLab(txtOperHN.Text.Trim(), vsdate, preno, ref grfLab);
				setGrfHisOrder(txtOperHN.Text.Trim(), vsdate, preno, ref grfHisOrder);
				setGrfXray(txtOperHN.Text.Trim(), vsdate, preno, ref grfXray);
				setGrfHisProcedure(txtOperHN.Text.Trim(), vsdate, preno, ref grfHisProcedure);
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FRMOPD GrfOPD_AfterRowColChange " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "FRMOPD GrfOPD_AfterRowColChange  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
			finally
			{
				//frmFlash.Dispose();
			}
		}
		private void setGrfOPD()
		{
			DataTable dt = new DataTable();
			dt = bc.bcDB.vsDB.selectVisitByHn6(txtOperHN.Text, "O");
			//MessageBox.Show("01 ", "");
			int i = 1, j = 1, row = grfOPD.Rows.Count; grfOPD.Rows.Count = 1; grfOPD.Rows.Count = dt.Rows.Count + 1;

			foreach (DataRow row1 in dt.Rows)
			{
				//pB1.Value++;
				Row rowa = grfOPD.Rows[i];
				String status = "", vn = "";

				status = row1["MNC_PAT_FLAG"] != null ? row1["MNC_PAT_FLAG"].ToString().Equals("O") ? "OPD" : "IPD" : "-";
				vn = row1["MNC_VN_NO"].ToString() + "/" + row1["MNC_VN_SEQ"].ToString() + "(" + row1["MNC_VN_SUM"].ToString() + ")";
				rowa[colVsVsDate] = bc.datetoShowShort(row1["mnc_date"].ToString());
				rowa[colVsVn] = vn;
				rowa[colVsStatus] = status;
				rowa[colVsPreno] = row1["mnc_pre_no"].ToString();
				rowa[colVsDept] = row1["MNC_SHIF_MEMO"].ToString();
				rowa[colVsAn] = row1["mnc_an_no"].ToString() + "/" + row1["mnc_an_yr"].ToString();
				rowa[colVsAndate] = bc.datetoShow1(row1["mnc_ad_date"].ToString());
				rowa[colVsPaidType] = row1["MNC_FN_TYP_DSC"].ToString();

				rowa[colVsVsDate1] = row1["mnc_date"].ToString();
				rowa[colVsDtrName] = row1["dtr_name"].ToString();
				i++;
			}
		}
		private void initGrfIPDScan()
		{
			Panel pnScanTop = new Panel();
			Panel pnScan = new Panel();

			pnScanTop.Dock = DockStyle.Top;
			pnScanTop.Height = 30;
			pnScan.Dock = DockStyle.Fill;

			grfIPDScan = new C1FlexGrid();
			grfIPDScan.Font = fEdit;
			grfIPDScan.Dock = System.Windows.Forms.DockStyle.Fill;
			grfIPDScan.Location = new System.Drawing.Point(0, 0);
			grfIPDScan.Rows[0].Visible = false;
			grfIPDScan.Cols[0].Visible = false;
			grfIPDScan.Rows.Count = 1;
			grfIPDScan.Name = "grfIPDScan";
			grfIPDScan.Cols.Count = 5;
			Column colpic1 = grfIPDScan.Cols[colPic1];
			colpic1.DataType = typeof(Image);
			Column colpic2 = grfIPDScan.Cols[colPic2];
			colpic2.DataType = typeof(String);
			Column colpic3 = grfIPDScan.Cols[colPic3];
			colpic3.DataType = typeof(Image);
			Column colpic4 = grfIPDScan.Cols[colPic4];
			colpic4.DataType = typeof(String);
			grfIPDScan.Cols[colPic1].Width = bc.grfScanWidth;
			grfIPDScan.Cols[colPic2].Width = bc.grfScanWidth;
			grfIPDScan.Cols[colPic3].Width = bc.grfScanWidth;
			grfIPDScan.Cols[colPic4].Width = bc.grfScanWidth;
			grfIPDScan.ShowCursor = true;
			grfIPDScan.Cols[colPic2].Visible = false;
			grfIPDScan.Cols[colPic3].Visible = true;
			grfIPDScan.Cols[colPic4].Visible = false;
			grfIPDScan.Cols[colPic1].AllowEditing = false;
			grfIPDScan.Cols[colPic3].AllowEditing = false;
			grfIPDScan.DoubleClick += GrfIPDScan_DoubleClick;
			lbDocAll = new Label();
			bc.setControlLabel(ref lbDocAll, fEditB, "All", "lbDocAll", 20, 5);
			lbDocAll.ForeColor = Color.Red;
			lbDocAll.Click += LbDocAll_Click;
			pnScanTop.Controls.Add(lbDocAll);
			int i = 0, width1 = 0;
			colorLbDoc = lbDocAll.ForeColor;
			if (bc.bcDB.dgsDB.lDgs.Count <= 0) bc.bcDB.dgsDB.getlDgs();
			foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
			{
				i++;
				if (i == 1)
				{
					lbDocGrp1 = new Label();
					bc.setControlLabel(ref lbDocGrp1, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
					width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
					if ((i == 1) || (i == 2) || (i == 7))
					{
						width1 += 40;
					}
					if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
					{
						width1 += 20;
					}
					lbDocGrp1.Click += LbDocAll_Click;
					lbDocGrp1.ForeColor = Color.Black;
					pnScanTop.Controls.Add(lbDocGrp1);
				}
				else if (i == 2)
				{
					lbDocGrp2 = new Label();
					bc.setControlLabel(ref lbDocGrp2, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
					width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
					if ((i == 1) || (i == 2) || (i == 7))
					{
						width1 += 40;
					}
					if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
					{
						width1 += 20;
					}
					lbDocGrp2.Click += LbDocAll_Click;
					lbDocGrp2.ForeColor = Color.Black;
					pnScanTop.Controls.Add(lbDocGrp2);
				}
				else if (i == 3)
				{
					lbDocGrp3 = new Label();
					bc.setControlLabel(ref lbDocGrp3, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
					width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
					if ((i == 1) || (i == 2) || (i == 7))
					{
						width1 += 40;
					}
					if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
					{
						width1 += 20;
					}
					lbDocGrp3.Click += LbDocAll_Click;
					lbDocGrp3.ForeColor = Color.Black;
					pnScanTop.Controls.Add(lbDocGrp3);
				}
				else if (i == 4)
				{
					lbDocGrp4 = new Label();
					bc.setControlLabel(ref lbDocGrp4, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
					width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
					if ((i == 1) || (i == 2) || (i == 7))
					{
						width1 += 40;
					}
					if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
					{
						width1 += 20;
					}
					lbDocGrp4.Click += LbDocAll_Click;
					lbDocGrp4.ForeColor = Color.Black;
					pnScanTop.Controls.Add(lbDocGrp4);
				}
				else if (i == 5)
				{
					lbDocGrp5 = new Label();
					bc.setControlLabel(ref lbDocGrp5, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
					width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
					if ((i == 1) || (i == 2) || (i == 7))
					{
						width1 += 40;
					}
					if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
					{
						width1 += 20;
					}
					lbDocGrp5.Click += LbDocAll_Click;
					lbDocGrp5.ForeColor = Color.Black;
					pnScanTop.Controls.Add(lbDocGrp5);
				}
				else if (i == 6)
				{
					lbDocGrp6 = new Label();
					bc.setControlLabel(ref lbDocGrp6, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
					width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
					if ((i == 1) || (i == 2) || (i == 7))
					{
						width1 += 40;
					}
					if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
					{
						width1 += 20;
					}
					lbDocGrp6.Click += LbDocAll_Click;
					lbDocGrp6.ForeColor = Color.Black;
					pnScanTop.Controls.Add(lbDocGrp6);
				}
				else if (i == 7)
				{
					lbDocGrp7 = new Label();
					bc.setControlLabel(ref lbDocGrp7, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
					width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
					if ((i == 1) || (i == 2) || (i == 7))
					{
						width1 += 40;
					}
					if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
					{
						width1 += 20;
					}
					lbDocGrp7.Click += LbDocAll_Click;
					lbDocGrp7.ForeColor = Color.Black;
					pnScanTop.Controls.Add(lbDocGrp7);
				}
				else if (i == 8)
				{
					lbDocGrp8 = new Label();
					bc.setControlLabel(ref lbDocGrp8, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
					width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
					if ((i == 1) || (i == 2) || (i == 7))
					{
						width1 += 40;
					}
					if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
					{
						width1 += 20;
					}
					lbDocGrp8.Click += LbDocAll_Click;
					lbDocGrp8.ForeColor = Color.Black;
					pnScanTop.Controls.Add(lbDocGrp8);
				}
				else if (i == 9)
				{
					lbDocGrp9 = new Label();
					bc.setControlLabel(ref lbDocGrp9, fEditB, dgs.doc_group_name, "lbDocGrp" + i.ToString(), lbDocAll.Width + width1 + 60, 5);
					width1 += bc.MeasureString(dgs.doc_group_name, fEditB).Width;
					if ((i == 1) || (i == 2) || (i == 7))
					{
						width1 += 40;
					}
					if ((i == 3) || (i == 4) || (i == 8) || (i == 6) || (i == 5))
					{
						width1 += 20;
					}
					lbDocGrp9.Click += LbDocAll_Click;
					lbDocGrp9.ForeColor = Color.Black;
					pnScanTop.Controls.Add(lbDocGrp9);
				}
			}
			pnMedScan.Controls.Add(pnScan);
			pnMedScan.Controls.Add(pnScanTop);
			pnScan.Controls.Add(grfIPDScan);
			//initGrfPrn();
			//initGrfHn();
		}
		private void GrfIPDScan_DoubleClick(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colPic2] == null) return;
			if (((C1FlexGrid)sender).Row < 0) return;
			String id = "";
			((C1FlexGrid)sender).AutoSizeCols();
			((C1FlexGrid)sender).AutoSizeRows();
			if (((C1FlexGrid)sender).Col == 1)
			{
				id = grfIPDScan[grfIPDScan.Row, colPic2].ToString();
			}
			else
			{
				id = grfIPDScan[grfIPDScan.Row, colPic4] != null ? grfIPDScan[grfIPDScan.Row, colPic4].ToString() : "";
			}
			//id = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colPic2].ToString();
			if (id.Equals("")) return;
			DSCID = id;
			MemoryStream strm = null;
			foreach (listStream lstrmm in lStream)
			{
				if (lstrmm.id.Equals(id))
				{
					strm = lstrmm.stream;
					break;
				}
			}
			if (strm != null)
			{
				streamPrint = strm;
				IMG = Image.FromStream(strm);
				frmImg = new Form();
				FlowLayoutPanel pn = new FlowLayoutPanel();
				//vScroller = new VScrollBar();
				//vScroller.Height = frmImg.Height;
				//vScroller.Width = 15;
				//vScroller.Dock = DockStyle.Right;
				frmImg.WindowState = FormWindowState.Normal;
				frmImg.StartPosition = FormStartPosition.CenterScreen;
				frmImg.Size = new Size(1024, 764);
				frmImg.AutoScroll = true;
				pn.Dock = DockStyle.Fill;
				pn.AutoScroll = true;
				pic = new C1PictureBox();
				pic.Dock = DockStyle.Fill;
				pic.SizeMode = PictureBoxSizeMode.AutoSize;
				//int newWidth = 440;
				int originalWidth = 0;

				originalHeight = 0;
				originalWidth = IMG.Width;
				originalHeight = IMG.Height;
				//resizedImage = img.GetThumbnailImage(newWidth, (newWidth * img.Height) / originalWidth, null, IntPtr.Zero);
				resizedImage = IMG.GetThumbnailImage((newHeight * IMG.Width) / originalHeight, newHeight, null, IntPtr.Zero);
				pic.Image = resizedImage;
				frmImg.Controls.Add(pn);
				pn.Controls.Add(pic);
				//pn.Controls.Add(vScroller);
				ContextMenu menuGw = new ContextMenu();
				menuGw.MenuItems.Add("ต้องการ Print ภาพนี้", new EventHandler(ContextMenu_print));

				mouseWheel = 0;
				pic.MouseWheel += Pic_MouseWheel;
				pic.ContextMenu = menuGw;
				//vScroller.Scroll += VScroller_Scroll;
				//pic.Paint += Pic_Paint;
				//vScroller.Hide();
				frmImg.ShowDialog(this);
			}
		}
		private void ContextMenu_print(object sender, System.EventArgs e)
		{
			setGrfScanToPrint();
		}
		private void initGrfChkPackItems()
		{
			grfChkPackItems = new C1FlexGrid();
			grfChkPackItems.Font = fEdit;
			grfChkPackItems.Dock = System.Windows.Forms.DockStyle.Fill;
			grfChkPackItems.Location = new System.Drawing.Point(0, 0);
			grfChkPackItems.Rows.Count = 1;
			grfChkPackItems.Cols.Count = 5;
			grfChkPackItems.Cols[colChkPackItemsname].Width = 200;
			grfChkPackItems.Cols[colChkPackItemflag].Width = 30;
			grfChkPackItems.Cols[colChkPackItemsitemcode].Width = 60;
			grfChkPackItems.Cols[colChkPackItemsPackcode].Width = 60;
			grfChkPackItems.ShowCursor = true;
			grfChkPackItems.Cols[colChkPackItemsname].Caption = "name";
			grfChkPackItems.Cols[colChkPackItemflag].Caption = "flag";
			grfChkPackItems.Cols[colChkPackItemsitemcode].Caption = "code";
			grfChkPackItems.Cols[colChkPackItemsPackcode].Caption = "-";

			grfChkPackItems.Cols[colChkPackItemsitemcode].Visible = false;
			grfChkPackItems.Cols[colChkPackItemsname].AllowEditing = false;
			grfChkPackItems.Cols[colChkPackItemflag].AllowEditing = false;
			grfChkPackItems.Cols[colChkPackItemsPackcode].AllowEditing = false;

			grfChkPackItems.Rows.Count = 1;

			gbCheckUPPackage.Controls.Add(grfChkPackItems);
			theme1.SetTheme(grfChkPackItems, bc.iniC.themeApp);
		}
		private void setGrfChkPackItems(String packagecode)
		{
			if (pageLoad) return;
			pageLoad = true;
			if (packagecode.Length <= 0) pageLoad = false; return;
			DataTable dtvs = new DataTable();
			dtvs = bc.bcDB.pm40DB.selectByPackCode(packagecode);
			grfChkPackItems.Rows.Count = 1; grfChkPackItems.Rows.Count = dtvs.Rows.Count + 1; int i = 1, j = 1;
			foreach (DataRow row1 in dtvs.Rows)
			{
				Row rowa = grfChkPackItems.Rows[i];
				rowa[colChkPackItemsitemcode] = row1["MNC_OPR_CD"].ToString();
				rowa[colChkPackItemsname] = row1["MNC_OPR_FLAG"].ToString().Equals("F") ? row1["MNC_DF_DSC"].ToString() : row1["MNC_OPR_FLAG"].ToString().Equals("L") ? row1["MNC_LB_DSC"].ToString() : row1["MNC_OPR_FLAG"].ToString().Equals("X") ? row1["MNC_XR_DSC"].ToString() : row1["MNC_OPR_FLAG"].ToString().Equals("O") ? row1["MNC_SR_DSC"].ToString() : "";
				rowa[colChkPackItemflag] = row1["MNC_OPR_FLAG"].ToString();
				rowa[colChkPackItemsPackcode] = row1["MNC_PAC_CD"].ToString();
				//rowa[colChkPackItemsPackcode] = row1["MNC_PAC_CD"].ToString();

				rowa[0] = i.ToString();
				i++;
			}

			pageLoad = false;
		}
		private void initGrfRpt()
		{
			grfRpt = new C1FlexGrid();
			grfRpt.Font = fEdit;
			grfRpt.Dock = System.Windows.Forms.DockStyle.Fill;
			grfRpt.Location = new System.Drawing.Point(0, 0);
			grfRpt.Rows.Count = 1;
			grfRpt.Cols.Count = 2;
			grfRpt.Cols[1].Width = 300;

			grfRpt.ShowCursor = true;
			grfRpt.Cols[1].Caption = "HN";

			grfRpt.Cols[1].DataType = typeof(String);
			grfRpt.Cols[1].TextAlign = TextAlignEnum.LeftCenter;
			grfRpt.Cols[1].Visible = true;
			grfRpt.Cols[1].AllowEditing = false;
			grfRpt.DoubleClick += GrfRpt_DoubleClick;
			//grfCheckUPList.AllowFiltering = true;
			grfRpt.Rows.Count = 3;
			Row rowa = grfRpt.Rows[1];
			Row row2 = grfRpt.Rows[2];
			rowa[1] = "รายงาน แพทย์นัด";
			row2[1] = "รายงาน จำนวนคนไข้ในแผนก";

			pnRptName.Controls.Add(grfRpt);
			theme1.SetTheme(grfRpt, bc.iniC.themeApp);
		}
		private void GrfRpt_DoubleClick(object sender, EventArgs e)
		{
			//throw new NotImplementedException();

		}
		private void initGrfOrder(ref C1FlexGrid grf, ref Panel pn, String grfname)
		{
			grf = new C1FlexGrid();
			grf.Font = fEdit;
			grf.Dock = System.Windows.Forms.DockStyle.Fill;
			grf.Location = new System.Drawing.Point(0, 0);
			grf.Rows.Count = 1;
			grf.Cols.Count = 13;
			grf.Cols[colgrfOrderCode].Width = 100;
			grf.Cols[colgrfOrderName].Width = 400;
			grf.Cols[colgrfOrderQty].Width = 70;
			grf.Name = grfname;
			grf.ShowCursor = true;
			grf.Cols[colgrfOrderCode].Caption = "code";
			grf.Cols[colgrfOrderName].Caption = "name";
			grf.Cols[colgrfOrderQty].Caption = "qty";
			grf.Cols[colgrfOrderReqNO].Caption = "reqno";

			//grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
			grf.Cols[colgrfOrderCode].DataType = typeof(String);
			grf.Cols[colgrfOrderName].DataType = typeof(String);
			grf.Cols[colgrfOrderQty].DataType = typeof(String);

			grf.Cols[colgrfOrderCode].TextAlign = TextAlignEnum.CenterCenter;
			grf.Cols[colgrfOrderName].TextAlign = TextAlignEnum.LeftCenter;
			grf.Cols[colgrfOrderQty].TextAlign = TextAlignEnum.LeftCenter;
			grf.Cols[colgrfOrderReqNO].TextAlign = TextAlignEnum.CenterCenter;

			grf.Cols[colgrfOrderCode].Visible = true;
			grf.Cols[colgrfOrderName].Visible = true;
			grf.Cols[colgrfOrderStatus].Visible = true;
			grf.Cols[colgrfOrderID].Visible = false;
			grf.Cols[colgrfOrdFlagSave].Visible = false;
			grf.Cols[colgrfOrdControlRemark].Visible = false;
			grf.Cols[colgrfOrdControlYear].Visible = false;
			grf.Cols[colgrfOrdStatusControl].Visible = false;
			grf.Cols[colgrfOrdPassSupervisor].Visible = false;
			grf.Cols[colgrfOrdSupervisor].Visible = false;
			if (grfname.Equals("grfOrder"))
			{
				grf.Cols[colgrfOrderQty].Visible = true;
			}
			else
			{
				grf.Cols[colgrfOrderQty].Visible = false;
			}
			grf.Cols[colgrfOrderCode].AllowEditing = false;
			grf.Cols[colgrfOrderName].AllowEditing = false;
			grf.Cols[colgrfOrderReqNO].AllowEditing = false;
			grf.DoubleClick += GrfOrder_DoubleClick;
			grf.Click += GrfOrder_Click1;
			grf.AllowSorting = AllowSortingEnum.None;
			pn.Controls.Add(grf);
			theme1.SetTheme(grf, bc.iniC.themeApp);
		}

		private void GrfOrder_Click1(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (((C1FlexGrid)sender).Name.Equals("grfOrder"))
			{
				if(((C1FlexGrid)sender).Row<0) return;
                String remark = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrdControlRemark]?.ToString() ?? "";
				lstAutoComplete.Items.Clear();
				// เพิ่มเฉพาะตอนที่มีค่า
				if (!string.IsNullOrWhiteSpace(remark))
				{
					string[] lines = remark.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string line in lines)
					{
						string trimmedLine = line.Trim();
						if (!string.IsNullOrWhiteSpace(trimmedLine)) { lstAutoComplete.Items.Add($"{trimmedLine}"); }
					}
				}
			}
		}

		private void GrfOrder_DoubleClick(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (((C1FlexGrid)sender).Row <= 0) return;
			if (((C1FlexGrid)sender).Col <= 0) return;

			if (((C1FlexGrid)sender).Name.Equals("grfApmOrder"))
			{
				String code = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderCode].ToString();
				String re = bc.bcDB.pt07DB.deleteOrderApm(txtApmDocYear.Text.Trim(), txtApmNO.Text.Trim(), code);
				((C1FlexGrid)sender).Rows.Remove(((C1FlexGrid)sender).Row);
			}
			else if (((C1FlexGrid)sender).Name.Equals("grfOrder"))
			{
				String id = "";
				id = ((C1FlexGrid)sender)[((C1FlexGrid)sender).Row, colgrfOrderID]?.ToString() ?? "";
				((C1FlexGrid)sender).Rows.Remove(((C1FlexGrid)sender).Row);
				lstAutoComplete.Items.Clear();
				if (id.Length > 0)
				{
					String re = bc.bcDB.vsDB.deleteOrderTemp(id);
					setGrfOrderTemp();
				}
			}
		}
		private void initGrfOperFinish()
		{
			grfOperFinish = new C1FlexGrid();
			grfOperFinish.Font = fEdit;
			grfOperFinish.Dock = System.Windows.Forms.DockStyle.Fill;
			grfOperFinish.Location = new System.Drawing.Point(0, 0);
			grfOperFinish.Rows.Count = 1;
			grfOperFinish.Cols.Count = 13;
			grfOperFinish.Cols[colgrfOperListHn].Width = 100;
			grfOperFinish.Cols[colgrfOperListFullNameT].Width = 200;
			grfOperFinish.Cols[colgrfOperListSymptoms].Width = 150;
			grfOperFinish.Cols[colgrfOperListPaidName].Width = 100;
			grfOperFinish.Cols[colgrfOperListPreno].Width = 100;
			grfOperFinish.Cols[colgrfOperListVsDate].Width = 100;
			grfOperFinish.Cols[colgrfOperListVsTime].Width = 70;
			grfOperFinish.Cols[colgrfOperListActNo].Width = 100;
			grfOperFinish.Cols[colgrfOperListDtrName].Width = 100;
			grfOperFinish.Cols[colgrfOperListLab].Width = 50;
			grfOperFinish.Cols[colgrfOperListXray].Width = 50;
			grfOperFinish.ShowCursor = true;
			grfOperFinish.Cols[colgrfOperListHn].Caption = "HN";
			grfOperFinish.Cols[colgrfOperListFullNameT].Caption = "ชื่อ-นามสกุล";
			grfOperFinish.Cols[colgrfOperListSymptoms].Caption = "อาการ";
			grfOperFinish.Cols[colgrfOperListPaidName].Caption = "สิทธิ";
			grfOperFinish.Cols[colgrfOperListPreno].Caption = "preno";
			grfOperFinish.Cols[colgrfOperListVsDate].Caption = "วันที่";
			grfOperFinish.Cols[colgrfOperListVsTime].Caption = "เวลา";
			grfOperFinish.Cols[colgrfOperListActNo].Caption = "สถานะ";
			grfOperFinish.Cols[colgrfOperListDtrName].Caption = "แพทย์";
			grfOperFinish.Cols[colgrfOperListLab].Caption = "lab";
			grfOperFinish.Cols[colgrfOperListXray].Caption = "xray";
			//grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
			grfOperFinish.Cols[colgrfOperListHn].DataType = typeof(String);
			grfOperFinish.Cols[colgrfOperListVsDate].DataType = typeof(String);
			grfOperFinish.Cols[colgrfOperListVsTime].DataType = typeof(String);
			grfOperFinish.Cols[colgrfOperListActNo].DataType = typeof(String);
			grfOperFinish.Cols[colgrfOperListDtrName].DataType = typeof(String);
			grfOperFinish.Cols[colgrfOperListLab].DataType = typeof(String);
			grfOperFinish.Cols[colgrfOperListXray].DataType = typeof(String);

			grfOperFinish.Cols[colgrfOperListHn].TextAlign = TextAlignEnum.CenterCenter;
			grfOperFinish.Cols[colgrfOperListVsTime].TextAlign = TextAlignEnum.CenterCenter;
			grfOperFinish.Cols[colgrfOperListActNo].TextAlign = TextAlignEnum.LeftCenter;
			grfOperFinish.Cols[colgrfOperListDtrName].TextAlign = TextAlignEnum.LeftCenter;
			grfOperFinish.Cols[colgrfOperListLab].TextAlign = TextAlignEnum.CenterCenter;
			grfOperFinish.Cols[colgrfOperListXray].TextAlign = TextAlignEnum.CenterCenter;

			grfOperFinish.Cols[colgrfOperListPreno].Visible = false;
			grfOperFinish.Cols[colgrfOperListVsDate].Visible = false;
			grfOperFinish.Cols[colgrfOperListHn].Visible = false;

			grfOperFinish.Cols[colgrfOperListHn].AllowEditing = false;
			grfOperFinish.Cols[colgrfOperListFullNameT].AllowEditing = false;
			grfOperFinish.Cols[colgrfOperListSymptoms].AllowEditing = false;
			grfOperFinish.Cols[colgrfOperListPaidName].AllowEditing = false;
			grfOperFinish.Cols[colgrfOperListPreno].AllowEditing = false;
			grfOperFinish.Cols[colgrfOperListVsDate].AllowEditing = false;
			grfOperFinish.Cols[colgrfOperListVsTime].AllowEditing = false;
			grfOperFinish.Cols[colgrfOperListActNo].AllowEditing = false;
			grfOperFinish.Cols[colgrfOperListDtrName].AllowEditing = false;
			grfOperFinish.Cols[colgrfOperListLab].AllowEditing = false;
			grfOperFinish.Cols[colgrfOperListXray].AllowEditing = false;

			grfOperFinish.AfterRowColChange += GrfOperFinish_AfterRowColChange;
			//grfCheckUPList.AllowFiltering = true;

			pnOperFinish.Controls.Add(grfOperFinish);
			theme1.SetTheme(grfOperFinish, bc.iniC.themeApp);
		}
		private void GrfOperFinish_AfterRowColChange(object sender, RangeEventArgs e)
		{
			//throw new NotImplementedException();
			if (e.NewRange.r1 < 0) return;
			if (e.NewRange.Data == null) return;
			if (e.NewRange.r1 == e.OldRange.r1 && e.OldRange.r1 != 1) return;
			String hn = "", preno = "", vsdate = "";
			hn = grfOperFinish[grfOperFinish.Row, colgrfOperListHn] != null ? grfOperFinish[grfOperFinish.Row, colgrfOperListHn].ToString() : "";
			preno = grfOperFinish[grfOperFinish.Row, colgrfOperListPreno] != null ? grfOperFinish[grfOperFinish.Row, colgrfOperListPreno].ToString() : "";
			vsdate = grfOperFinish[grfOperFinish.Row, colgrfOperListVsDate] != null ? grfOperFinish[grfOperFinish.Row, colgrfOperListVsDate].ToString() : "";
			setControlTabFinish(hn, vsdate, preno);
		}
		private void setControlTabFinish(String hn, String vsdate, String preno)
		{
			try
			{
				HN = hn;
				PRENO = preno;
				VSDATE = vsdate;
				setStaffNote(VSDATE, PRENO, picFinishL, picFinishR);
				if (grfOperFinishLab != null) setGrfLab(HN, VSDATE, PRENO, ref grfOperFinishLab);       //LAB
				setGrfHisOrder(HN, VSDATE, PRENO, ref grfOperFinishDrug);                               //DRUG
				if (grfOperFinishXray != null) setGrfXray(HN, VSDATE, PRENO, ref grfOperFinishXray);    //XRAY
				if (TCFinishActive.Equals(tabFinishCertMed.Name)) setCertiMed();                        //cert med
				setGrfHisProcedure(HN, VSDATE, PRENO, ref grfOperFinishProcedure);                      //Procedure
			}
			catch (Exception ex)
			{
				new LogWriter("e", "FRMOPD GrfOperFinish_AfterRowColChange " + ex.Message);
				bc.bcDB.insertLogPage(bc.userId, this.Name, "FRMOPD GrfOperFinish_AfterRowColChange  ", ex.Message);
				lfSbMessage.Text = ex.Message;
			}
			finally
			{
				//frmFlash.Dispose();
			}
		}
		private void setGrfOperFinish()
		{
			if (pageLoad) return;
			pageLoad = true;
			timeOperList.Enabled = false;
			DataTable dtvs = new DataTable();
			String deptno = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
			String vsdate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
			dtvs = bc.bcDB.vsDB.selectPttinDeptActNo110(deptno, bc.iniC.station, vsdate, vsdate);

			grfOperFinish.Rows.Count = 1;
			grfOperFinish.Rows.Count = dtvs.Rows.Count + 1;
			int i = 1, j = 1, row = grfOperFinish.Rows.Count;
			foreach (DataRow row1 in dtvs.Rows)
			{
				Row rowa = grfOperFinish.Rows[i];
				rowa[colgrfOperListHn] = row1["MNC_HN_NO"].ToString();
				rowa[colgrfOperListFullNameT] = row1["ptt_fullnamet"].ToString();
				rowa[colgrfOperListSymptoms] = row1["MNC_SHIF_MEMO"].ToString();
				rowa[colgrfOperListPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
				rowa[colgrfOperListPreno] = row1["MNC_PRE_NO"].ToString();

				rowa[colgrfOperListVsDate] = row1["MNC_DATE"].ToString();
				rowa[colgrfOperListVsTime] = bc.showTime(row1["MNC_TIME"].ToString());
				rowa[colgrfOperListActNo] = bc.adjustACTNO(row1["MNC_ACT_NO"].ToString());
				rowa[colgrfOperListDtrName] = row1["dtr_name"].ToString();
				if (row1["MNC_ACT_NO"].ToString().Equals("110")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor); }
				else if (row1["MNC_ACT_NO"].ToString().Equals("114")) { rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6"); }

				rowa[0] = i.ToString();
				i++;
			}
			timeOperList.Enabled = true;
			pageLoad = false;
		}
		private void initGrfOperFinishDrug()
		{
			grfOperFinishDrug = new C1FlexGrid();
			grfOperFinishDrug.Font = fEdit;
			grfOperFinishDrug.Dock = System.Windows.Forms.DockStyle.Fill;
			grfOperFinishDrug.Location = new System.Drawing.Point(0, 0);
			//grfOrder.Rows[0].Visible = false;
			//grfOrder.Cols[0].Visible = false;
			grfOperFinishDrug.Cols[colOrderId].Visible = false;
			grfOperFinishDrug.Rows.Count = 1;
			grfOperFinishDrug.Cols.Count = 8;
			grfOperFinishDrug.Cols[colOrderName].Caption = "Drug Name";
			grfOperFinishDrug.Cols[colOrderMed].Caption = "MED";
			grfOperFinishDrug.Cols[colOrderQty].Caption = "QTY";
			grfOperFinishDrug.Cols[colOrderDate].Caption = "Date";
			grfOperFinishDrug.Cols[colOrderFre].Caption = "วิธีใช้";
			grfOperFinishDrug.Cols[colOrderIn1].Caption = "ข้อควรระวัง";
			grfOperFinishDrug.Cols[colOrderName].Width = 400;
			grfOperFinishDrug.Cols[colOrderMed].Width = 200;
			grfOperFinishDrug.Cols[colOrderQty].Width = 60;
			grfOperFinishDrug.Cols[colOrderDate].Width = 90;
			grfOperFinishDrug.Cols[colOrderFre].Width = 500;
			grfOperFinishDrug.Cols[colOrderIn1].Width = 350;
			grfOperFinishDrug.Cols[colOrderName].AllowEditing = false;
			grfOperFinishDrug.Cols[colOrderQty].AllowEditing = false;
			grfOperFinishDrug.Cols[colOrderMed].AllowEditing = false;
			grfOperFinishDrug.Cols[colOrderFre].AllowEditing = false;
			grfOperFinishDrug.Cols[colOrderIn1].AllowEditing = false;
			grfOperFinishDrug.Cols[colOrderDate].AllowEditing = false;
			grfOperFinishDrug.Name = "grfOperFinishDrug";
			pnFinishDrug.Controls.Add(grfOperFinishDrug);
		}
		private void initGrfOperFinishProcedure()
		{
			grfOperFinishProcedure = new C1FlexGrid();
			grfOperFinishProcedure.Font = fEdit;
			grfOperFinishProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
			grfOperFinishProcedure.Location = new System.Drawing.Point(0, 0);
			grfOperFinishProcedure.Rows.Count = 1;
			grfOperFinishProcedure.Cols.Count = 5;
			grfOperFinishProcedure.Cols[colHisProcCode].Width = 100;
			grfOperFinishProcedure.Cols[colHisProcName].Width = 200;
			grfOperFinishProcedure.Cols[colHisProcReqDate].Width = 100;
			grfOperFinishProcedure.Cols[colHisProcReqTime].Width = 100;

			grfOperFinishProcedure.ShowCursor = true;
			grfOperFinishProcedure.Cols[colHisProcCode].Caption = "CODE";
			grfOperFinishProcedure.Cols[colHisProcName].Caption = "Procedure Name";
			grfOperFinishProcedure.Cols[colHisProcReqDate].Caption = "req date";
			grfOperFinishProcedure.Cols[colHisProcReqTime].Caption = "req time";

			//grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
			grfOperFinishProcedure.Cols[colHisProcCode].DataType = typeof(String);
			grfOperFinishProcedure.Cols[colHisProcName].DataType = typeof(String);
			grfOperFinishProcedure.Cols[colHisProcReqDate].DataType = typeof(String);
			grfOperFinishProcedure.Cols[colHisProcReqTime].DataType = typeof(String);

			grfOperFinishProcedure.Cols[colHisProcCode].TextAlign = TextAlignEnum.CenterCenter;
			grfOperFinishProcedure.Cols[colHisProcName].TextAlign = TextAlignEnum.LeftCenter;
			grfOperFinishProcedure.Cols[colHisProcReqDate].TextAlign = TextAlignEnum.CenterCenter;
			grfOperFinishProcedure.Cols[colHisProcReqTime].TextAlign = TextAlignEnum.CenterCenter;

			grfOperFinishProcedure.Cols[colgrfOrderCode].Visible = true;
			grfOperFinishProcedure.Cols[colgrfOrderName].Visible = true;
			grfOperFinishProcedure.Cols[colHisProcReqTime].Visible = false;

			grfOperFinishProcedure.Cols[colHisProcCode].AllowEditing = false;
			grfOperFinishProcedure.Cols[colHisProcName].AllowEditing = false;
			grfOperFinishProcedure.Cols[colHisProcReqDate].AllowEditing = false;
			grfOperFinishProcedure.Cols[colHisProcReqTime].AllowEditing = false;

			pnFinishProcedure.Controls.Add(grfOperFinishProcedure);
			theme1.SetTheme(grfOperFinishProcedure, bc.iniC.themeApp);
		}
		private void initGrfOperList()
		{
			grfOperList = new C1FlexGrid();
			grfOperList.Font = fEdit;
			grfOperList.Dock = System.Windows.Forms.DockStyle.Fill;
			grfOperList.Location = new System.Drawing.Point(0, 0);
			grfOperList.Rows.Count = 1;
			grfOperList.Cols.Count = 13;
			grfOperList.Cols[colgrfOperListHn].Width = 80;
			grfOperList.Cols[colgrfOperListVN].Width = 60;
			grfOperList.Cols[colgrfOperListFullNameT].Width = 200;
			grfOperList.Cols[colgrfOperListSymptoms].Width = 150;
			grfOperList.Cols[colgrfOperListPaidName].Width = 100;
			grfOperList.Cols[colgrfOperListPreno].Width = 100;
			grfOperList.Cols[colgrfOperListVsDate].Width = 100;
			grfOperList.Cols[colgrfOperListVsTime].Width = 70;
			grfOperList.Cols[colgrfOperListActNo].Width = 100;
			grfOperList.Cols[colgrfOperListDtrName].Width = 100;
			grfOperList.Cols[colgrfOperListLab].Width = 50;
			grfOperList.Cols[colgrfOperListXray].Width = 50;
			grfOperList.ShowCursor = true;
			grfOperList.Cols[colgrfOperListHn].Caption = "HN";
			grfOperList.Cols[colgrfOperListFullNameT].Caption = "ชื่อ-นามสกุล";
			grfOperList.Cols[colgrfOperListSymptoms].Caption = "อาการ";
			grfOperList.Cols[colgrfOperListPaidName].Caption = "สิทธิ";
			grfOperList.Cols[colgrfOperListPreno].Caption = "preno";
			grfOperList.Cols[colgrfOperListVsDate].Caption = "วันที่";
			grfOperList.Cols[colgrfOperListVsTime].Caption = "เวลา";
			grfOperList.Cols[colgrfOperListActNo].Caption = "สถานะ";
			grfOperList.Cols[colgrfOperListDtrName].Caption = "แพทย์";
			grfOperList.Cols[colgrfOperListLab].Caption = "lab";
			grfOperList.Cols[colgrfOperListXray].Caption = "xray";
			//grfOperList.Cols[colgrfOperListPaidName].Caption = "นายจ้าง";
			grfOperList.Cols[colgrfOperListHn].DataType = typeof(String);
			grfOperList.Cols[colgrfOperListVsDate].DataType = typeof(String);
			grfOperList.Cols[colgrfOperListVsTime].DataType = typeof(String);
			grfOperList.Cols[colgrfOperListActNo].DataType = typeof(String);
			grfOperList.Cols[colgrfOperListDtrName].DataType = typeof(String);
			grfOperList.Cols[colgrfOperListLab].DataType = typeof(String);
			grfOperList.Cols[colgrfOperListXray].DataType = typeof(String);

			grfOperList.Cols[colgrfOperListHn].TextAlign = TextAlignEnum.CenterCenter;
			grfOperList.Cols[colgrfOperListVsTime].TextAlign = TextAlignEnum.CenterCenter;
			grfOperList.Cols[colgrfOperListActNo].TextAlign = TextAlignEnum.LeftCenter;
			grfOperList.Cols[colgrfOperListDtrName].TextAlign = TextAlignEnum.LeftCenter;
			grfOperList.Cols[colgrfOperListLab].TextAlign = TextAlignEnum.CenterCenter;
			grfOperList.Cols[colgrfOperListXray].TextAlign = TextAlignEnum.CenterCenter;

			grfOperList.Cols[colgrfOperListPreno].Visible = false;
			grfOperList.Cols[colgrfOperListVsDate].Visible = false;
			grfOperList.Cols[colgrfOperListHn].Visible = true;

			grfOperList.Cols[colgrfOperListHn].AllowEditing = false;
			grfOperList.Cols[colgrfOperListFullNameT].AllowEditing = false;
			grfOperList.Cols[colgrfOperListSymptoms].AllowEditing = false;
			grfOperList.Cols[colgrfOperListPaidName].AllowEditing = false;
			grfOperList.Cols[colgrfOperListPreno].AllowEditing = false;
			grfOperList.Cols[colgrfOperListVsDate].AllowEditing = false;
			grfOperList.Cols[colgrfOperListVsTime].AllowEditing = false;
			grfOperList.Cols[colgrfOperListActNo].AllowEditing = false;
			grfOperList.Cols[colgrfOperListDtrName].AllowEditing = false;
			grfOperList.Cols[colgrfOperListLab].AllowEditing = false;
			grfOperList.Cols[colgrfOperListXray].AllowEditing = false;
			grfOperList.Name = "grfOperList";
			grfOperList.AfterRowColChange += GrfOperList_AfterRowColChange;
			//grfCheckUPList.AllowFiltering = true;

			pnOperList.Controls.Add(grfOperList);
			theme1.SetTheme(grfOperList, bc.iniC.themeApp);
		}

		private void GrfOperList_AfterRowColChange(object sender, RangeEventArgs e)
		{
			//throw new NotImplementedException();
			if (pageLoad) return;
			if (grfOperList.Row <= 0) return;
			if (grfOperList.Col <= 0) return;
			if (grfOperList[grfOperList.Row, colgrfOperListPreno] == null) return;
			if (grfOperList.Row == ROWGrfOper) return;
			PRENO = grfOperList[grfOperList.Row, colgrfOperListPreno].ToString();
			VSDATE = grfOperList[grfOperList.Row, colgrfOperListVsDate].ToString();
			HN = grfOperList[grfOperList.Row, colgrfOperListHn].ToString();
			setControlOper(grfOperList.Name);
		}
		private void initGrfCheckUPList()
		{
			grfCheckUPList = new C1FlexGrid();
			grfCheckUPList.Font = fEdit;
			grfCheckUPList.Dock = System.Windows.Forms.DockStyle.Fill;
			grfCheckUPList.Location = new System.Drawing.Point(0, 0);
			grfCheckUPList.Rows.Count = 1;
			grfCheckUPList.Cols.Count = 7;
			grfCheckUPList.Cols[colgrfCheckUPHn].Width = 100;
			grfCheckUPList.Cols[colgrfCheckUPFullNameT].Width = 200;
			grfCheckUPList.Cols[colgrfCheckUPSymtom].Width = 150;
			grfCheckUPList.Cols[colgrfCheckUPEmployer].Width = 100;
			grfCheckUPList.ShowCursor = true;
			grfCheckUPList.Cols[colgrfCheckUPHn].Caption = "HN";
			grfCheckUPList.Cols[colgrfCheckUPFullNameT].Caption = "full name";
			grfCheckUPList.Cols[colgrfCheckUPSymtom].Caption = "อาการ";
			grfCheckUPList.Cols[colgrfCheckUPEmployer].Caption = "นายจ้าง";

			grfCheckUPList.Cols[colgrfCheckUPVsDate].Visible = false;
			grfCheckUPList.Cols[colgrfCheckUPPreno].Visible = false;

			grfCheckUPList.Cols[colgrfCheckUPHn].AllowEditing = false;
			grfCheckUPList.Cols[colgrfCheckUPFullNameT].AllowEditing = false;
			grfCheckUPList.Cols[colgrfCheckUPSymtom].AllowEditing = false;
			grfCheckUPList.Cols[colgrfCheckUPEmployer].AllowEditing = false;
			grfCheckUPList.AllowFiltering = true;

			grfCheckUPList.Click += GrfCheckUPList_Click;

			spCheckUpList.Controls.Add(grfCheckUPList);
			theme1.SetTheme(grfCheckUPList, bc.iniC.themeApp);
		}
		private void setGrfCheckUPList(String flagselect)
		{
			showLbLoading();
			DateTime datestar = DateTime.Now;			DateTime dateend = DateTime.Now;
			if (datestar.Year < 1900) { datestar = datestar.AddYears(543); }
			else if (dateend.Year < 1900) { dateend = dateend.AddYears(543); }
			DataTable dtcheckup = new DataTable();
			if (flagselect.Length == 0)
			{
				dtcheckup = bc.bcDB.vsDB.selectPttinDeptOrderByNameT(bc.iniC.sectioncheckup, datestar.Year.ToString() + "-" + datestar.ToString("MM-dd"), dateend.Year.ToString() + "-" + dateend.ToString("MM-dd"));
			}
			else if (flagselect.Equals("sso"))
			{
				dtcheckup = bc.bcDB.vsDB.selectPttinCheckSSO(datestar.Year.ToString() + "-" + datestar.ToString("MM-dd"), dateend.Year.ToString() + "-" + dateend.ToString("MM-dd"));
			}
			else if (flagselect.Equals("doe"))
			{
				dtcheckup = bc.bcDB.vsDB.selectPttinCheckDOE(datestar.Year.ToString() + "-" + datestar.ToString("MM-dd"), dateend.Year.ToString() + "-" + dateend.ToString("MM-dd"));
			}
			else if (flagselect.Equals("doeonline"))
			{
				dtcheckup = bc.bcDB.vsDB.selectPttinCheckDOE(datestar.Year.ToString() + "-" + datestar.ToString("MM-dd"), dateend.Year.ToString() + "-" + dateend.ToString("MM-dd"));
			}
			int i = 1, j = 1, row = grfCheckUPList.Rows.Count;

			grfCheckUPList.Rows.Count = 1;
			grfCheckUPList.Rows.Count = dtcheckup.Rows.Count + 1;
			//pB1.Maximum = dt.Rows.Count;
			foreach (DataRow row1 in dtcheckup.Rows)
			{
				//pB1.Value++;
				Row rowa = grfCheckUPList.Rows[i];
				rowa[colgrfCheckUPHn] = row1["MNC_HN_NO"].ToString();
				rowa[colgrfCheckUPFullNameT] = row1["pttfullname"].ToString();
				rowa[colgrfCheckUPSymtom] = row1["MNC_SHIF_MEMO"].ToString();
				rowa[colgrfCheckUPEmployer] = "";
				rowa[colgrfCheckUPVsDate] = row1["MNC_DATE"].ToString();
				rowa[colgrfCheckUPPreno] = row1["MNC_PRE_NO"].ToString();
				rowa[0] = i.ToString();
				i++;
			}
			//ContextMenu menuGw = new ContextMenu();
			//menuGw.MenuItems.Add("&ยกเลิก รูปภาพนี้", new EventHandler(ContextMenu_Void));
			//menuGw.MenuItems.Add("&Update ข้อมูล", new EventHandler(ContextMenu_Update));
			//foreach (DocGroupScan dgs in bc.bcDB.dgsDB.lDgs)
			//{
			//    menuGw.MenuItems.Add("&เลือกประเภทเอกสาร และUpload Image [" + dgs.doc_group_name + "]", new EventHandler(ContextMenu_upload));
			//}
			//grfVs.ContextMenu = menuGw;
			hideLbLoading();
		}
		private void GrfCheckUPList_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (grfCheckUPList == null) return;
			if (grfCheckUPList.Row <= 0) return;
			if (grfCheckUPList.Col <= 0) return;
			sep.Clear();
			SYMPTOMS = grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPSymtom] != null ? grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPSymtom].ToString() : "";
			setControlCheckUP(grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPHn].ToString(), grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPVsDate].ToString()
				, grfCheckUPList[grfCheckUPList.Row, colgrfCheckUPPreno].ToString());
		}
		private void GrfApm_DoubleClick(object sender, EventArgs e)
		{
			//throw new NotImplementedException();

		}

		private void GrfPttApm_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			if (((C1FlexGrid)sender).Row <= 0) return;
			if (((C1FlexGrid)sender).Col <= 0) return;
			if (((C1FlexGrid)sender).Name.Equals("grfPttApm"))
			{
				String apmno = "", apmyear = "";
				apmno = grfPttApm[grfPttApm.Row, colgrfPttApmDocNo].ToString();//colgrfPttApmDocNo
				apmyear = grfPttApm[grfPttApm.Row, colgrfPttApmDocYear].ToString();
				setControlApm(apmyear, apmno);
			}
			else if (((C1FlexGrid)sender).Name.Equals("grfApm"))
			{

			}
		}
		private void setGrfPttApm()
		{
			DataTable dtvs = new DataTable();
			dtvs = bc.bcDB.pt07DB.selectByHnAll(txtOperHN.Text.Trim(), "desc");
			grfPttApm.Rows.Count = 1;
			grfPttApm.Rows.Count = dtvs.Rows.Count + 1;
			int i = 1, j = 1, row = grfPttApm.Rows.Count;
			String time = "";
			foreach (DataRow row1 in dtvs.Rows)
			{
				Row rowa = grfPttApm.Rows[i];
				rowa[colgrfPttApmApmDateShow] = bc.datetoShow1(row1["MNC_APP_DAT"].ToString());
				rowa[colgrfPttApmApmTime] = bc.showTime(row1["MNC_APP_TIM"].ToString());
				rowa[colgrfPttApmDeptR] = row1["mnc_md_dep_dsc"].ToString();//นัดตรวจที่แผนก
				rowa[colgrfPttApmDeptMake] = bc.bcDB.pm32DB.getDeptNameOPD(row1["mnc_sec_no"].ToString());
				rowa[colgrfPttApmNote] = row1["MNC_APP_DSC"].ToString();
				rowa[colgrfPttApmOrder] = row1["MNC_REM_MEMO"].ToString();

				rowa[colgrfPttApmDocNo] = row1["MNC_DOC_NO"].ToString();
				rowa[colgrfPttApmDocYear] = row1["MNC_DOC_YR"].ToString();
				rowa[0] = i.ToString();
				i++;
			}
			if (tabApm.IsSelected)
			{
				txtApmDtr.Value = txtOperDtr.Text.Trim();
				lbApmDtrName.Text = lbOperDtrName.Text.Trim();
			}
		}
		private void initGrfPttApm(ref C1FlexGrid grf, ref Panel pn, String grfname)
		{
			grf = new C1FlexGrid();
			grf.Font = fEdit;
			grf.Dock = System.Windows.Forms.DockStyle.Fill;
			grf.Location = new System.Drawing.Point(0, 0);
			grf.Rows.Count = 1;
			grf.Cols.Count = 19;
			grf.Name = grfname;

			grf.Cols[colgrfPttApmVsDate].Width = 100;
			grf.Cols[colgrfPttApmApmDateShow].Width = 100;
			grf.Cols[colgrfPttApmApmTime].Width = 60;
			grf.Cols[colgrfPttApmNote].Width = 500;
			grf.Cols[colgrfPttApmOrder].Width = 500;
			grf.Cols[colgrfPttApmHN].Width = 80;
			grf.Cols[colgrfPttApmPttName].Width = 250;
			grf.Cols[colgrfPttApmDeptR].Width = 120;
			grf.Cols[colgrfPttApmDeptMake].Width = 150;

			grf.ShowCursor = true;
			grf.Cols[colgrfPttApmVsDate].Caption = "date";
			grf.Cols[colgrfPttApmApmDateShow].Caption = "นัดวันที่";
			grf.Cols[colgrfPttApmApmTime].Caption = "นัดเวลา";
			grf.Cols[colgrfPttApmDeptR].Caption = "นัดตรวจที่แผนก";
			grf.Cols[colgrfPttApmDeptMake].Caption = "แผนกทำนัด";
			grf.Cols[colgrfPttApmNote].Caption = "รายละเอียด";
			grf.Cols[colgrfPttApmOrder].Caption = "Order";

			grf.Cols[colgrfPttApmApmDateShow].DataType = typeof(String);
			grf.Cols[colgrfPttApmApmTime].DataType = typeof(String);
			grf.Cols[colgrfPttApmDeptR].DataType = typeof(String);
			grf.Cols[colgrfPttApmNote].DataType = typeof(String);
			grf.Cols[colgrfPttApmOrder].DataType = typeof(String);
			grf.Cols[colgrfPttApmHN].DataType = typeof(String);
			grf.Cols[colgrfPttApmPttName].DataType = typeof(String);
			grf.Cols[colgrfPttApmDeptMake].DataType = typeof(String);

			grf.Cols[colgrfPttApmApmDateShow].TextAlign = TextAlignEnum.CenterCenter;
			grf.Cols[colgrfPttApmApmTime].TextAlign = TextAlignEnum.CenterCenter;
			grf.Cols[colgrfPttApmDeptR].TextAlign = TextAlignEnum.LeftCenter;
			grf.Cols[colgrfPttApmDeptMake].TextAlign = TextAlignEnum.LeftCenter;
			grf.Cols[colgrfPttApmNote].TextAlign = TextAlignEnum.LeftCenter;
			grf.Cols[colgrfPttApmOrder].TextAlign = TextAlignEnum.LeftCenter;

			grf.Cols[colgrfPttApmVsDate].Visible = true;
			grf.Cols[colgrfPttApmApmDateShow].Visible = true;
			grf.Cols[colgrfPttApmDeptR].Visible = true;
			grf.Cols[colgrfPttApmNote].Visible = true;
			grf.Cols[colgrfPttApmDocNo].Visible = false;
			grf.Cols[colgrfPttApmDocYear].Visible = false;
			grf.Cols[colgrfPttApmVsDate].Visible = false;
			grf.Cols[colgrfPttApmHN].Visible = false;
			grf.Cols[colgrfPttApmPttName].Visible = true;
			grf.Cols[colgrfPttApmApmDate1].Visible = false;

			grf.Cols[colgrfPttApmVsDate].AllowEditing = false;
			grf.Cols[colgrfPttApmApmDateShow].AllowEditing = false;
			grf.Cols[colgrfPttApmDeptR].AllowEditing = false;
			grf.Cols[colgrfPttApmNote].AllowEditing = false;
			grf.Cols[colgrfPttApmApmTime].AllowEditing = false;
			grf.Cols[colgrfPttApmOrder].AllowEditing = false;
			grf.Cols[colgrfPttApmDeptMake].AllowEditing = false;

			if (grf.Name.Equals("grfPttApm")) grfPttApm.Click += GrfPttApm_Click;

			if (grf.Name.Equals("grfApm"))
			{
				ContextMenu menuGw = new ContextMenu();
				menuGw.MenuItems.Add("ต้องการออกvisit ตามนัด", new EventHandler(ContextMenu_VisitNew));
				menuGw.MenuItems.Add("แก้ไขนัด", new EventHandler(ContextMenu_EditAppoinment));
				grfApm.ContextMenu = menuGw;
				grfApm.DoubleClick += GrfApm_DoubleClick;
			}
			else if (grf.Name.Equals("grfPttApm"))
			{
				ContextMenu menuGw = new ContextMenu();
				menuGw.MenuItems.Add("ต้องการยกเลิก ใบนัดพบแพพย์", new EventHandler(ContextMenu_VoidAppoinment));
				//menuGw.MenuItems.Add("Download Certificate Medical", new EventHandler(ContextMenu_CertiMedical_Download));
				grfPttApm.ContextMenu = menuGw;
			}
			pn.Controls.Add(grf);
			theme1.SetTheme(grf, bc.iniC.themeApp);
		}
		private void ContextMenu_EditAppoinment(object sender, System.EventArgs e)
		{
			String docyear = "", docno = "";
			try
			{
				PatientT07 apm = new PatientT07();
				docno = grfApm[grfApm.Row, colgrfPttApmDocNo].ToString();
				docyear = grfApm[grfApm.Row, colgrfPttApmDocYear].ToString();
				apm = bc.bcDB.pt07DB.selectAppointment(docyear, docno);
				FrmApmVisitNew frm = new FrmApmVisitNew(bc, apm, "edit");
				frm.ShowDialog(this);
				frm.Dispose();
			}
			catch (Exception ex)
			{

			}
		}
		private void ContextMenu_VoidAppoinment(object sender, System.EventArgs e)
		{
			String docyear = "", docno = "";
			try
			{
				docno = grfPttApm[grfPttApm.Row, colgrfPttApmDocNo]?.ToString() ?? "";
				docyear = grfPttApm[grfPttApm.Row, colgrfPttApmDocYear]?.ToString() ?? "";
				if (MessageBox.Show("ต้องการยกเลิก ใบนัดพบแพพย์ เลขที่ " + docyear.Substring(2) + "-" + docno, "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					//docno = grfPttApm[grfPttApm.Row, colgrfPttApmDocNo].ToString();
					String re = bc.bcDB.pt07DB.voidAppoinment(docyear, docno);
					setGrfPttApm();
					clearControlTabApm(false);
				}
			}
			catch (Exception ex)
			{

			}
		}
		private void ContextMenu_VisitNew(object sender, System.EventArgs e)
		{
			PatientT07 apm = new PatientT07();
			String docyear = "", docno = "";
			docyear = grfApm[grfApm.Row, colgrfPttApmDocYear].ToString();
			docno = grfApm[grfApm.Row, colgrfPttApmDocNo].ToString();
			apm = bc.bcDB.pt07DB.selectAppointment(docyear, docno);
			FrmApmVisitNew frm = new FrmApmVisitNew(bc, apm);
			frm.ShowDialog(this);
			frm.Dispose();
		}
		private void setGrfOperList(String flagsearch)
		{
			if (pageLoad) return;
			if (grfOperList == null) return;
			if (grfOperList.Rows == null) return;
			pageLoad = true;			timeOperList.Enabled = false;
			string criteria = ((ComboBoxItem)cboOperCritiria.SelectedItem).Value?.ToString() ?? "";
			DataTable dtvs = new DataTable();
			String deptno = bc.bcDB.pm32DB.getDeptNoOPD(bc.iniC.station);
			String vsdate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.ToString("MM-dd");
			DateTime dateTime = DateTime.Now.AddDays(-1);
            //vsdate = dateTime.Year.ToString() + "-" + dateTime.ToString("MM-dd");
            //ต้องเอา 110 มาด้วย ส่งเข้าห้องหมอ
            if (bc.iniC.branchId.Equals("005"))
				dtvs = bc.bcDB.vsDB.selectPttinDeptActNo101(deptno, bc.iniC.station, vsdate, vsdate, txtOperHN.Text.Trim(), criteria.Length > 0 ? criteria : flagsearch);
			else
				dtvs = bc.bcDB.vsDB.selectPttinDeptActNo101(deptno, "", vsdate, vsdate, txtOperHN.Text.Trim(), criteria.Length > 0 ? criteria : flagsearch);
			grfOperList.Rows.Count = 1; grfOperList.Rows.Count = dtvs.Rows.Count + 1;
			int i = 1, j = 1, row = grfOperList.Rows.Count;
			foreach (DataRow row1 in dtvs.Rows)
			{
				TimeSpan curtime = new TimeSpan();
				TimeSpan vstime = new TimeSpan();
				curtime = TimeSpan.Parse(row1["cur_time"].ToString());
				vstime = TimeSpan.Parse(bc.showTime(row1["MNC_TIME"].ToString()));
				Row rowa = grfOperList.Rows[i];
				rowa[colgrfOperListHn] = row1["MNC_HN_NO"].ToString();
				rowa[colgrfOperListVN] = row1["MNC_VN_NO"].ToString() + "." + row1["MNC_VN_SEQ"].ToString() + "." + row1["MNC_VN_SUM"].ToString();
				rowa[colgrfOperListFullNameT] = row1["ptt_fullnamet"].ToString();
				rowa[colgrfOperListSymptoms] = row1["MNC_SHIF_MEMO"].ToString().Replace(Environment.NewLine, "");
				rowa[colgrfOperListPaidName] = row1["MNC_FN_TYP_DSC"].ToString();
				rowa[colgrfOperListPreno] = row1["MNC_PRE_NO"].ToString();

				rowa[colgrfOperListVsDate] = row1["MNC_DATE"].ToString();
				rowa[colgrfOperListVsTime] = bc.showTime(row1["MNC_TIME"].ToString());
				rowa[colgrfOperListActNo] = bc.adjustACTNO(row1["MNC_ACT_NO"].ToString());
				rowa[colgrfOperListDtrName] = row1["dtr_name"].ToString();
				if (row1["MNC_ACT_NO"].ToString().Equals("110"))
				{
					TimeSpan difference = curtime - vstime;
					if (difference.TotalMinutes >= 5)
					{
						rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#DDA0DD"); // Lavender อ่อน
					}
					else if (difference.TotalMinutes >= 3)
					{
						rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#E6E6FA"); // Plum
					}
					else
					{
						rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
					}
					CellNote note = new CellNote(rowa[colgrfOperListActNo].ToString() + Environment.NewLine + "VN " + rowa[colgrfOperListVN].ToString() + Environment.NewLine + "แพทย์ " + rowa[colgrfOperListDtrName].ToString() + Environment.NewLine + "เวลา " + rowa[colgrfOperListVsTime].ToString() + Environment.NewLine + "HN " + rowa[colgrfOperListHn].ToString() + Environment.NewLine + rowa[colgrfOperListSymptoms].ToString());
					CellRange rg = grfOperList.GetCellRange(i, colgrfOperListFullNameT);
					rg.UserData = note;
				}
				else if (row1["MNC_ACT_NO"].ToString().Equals("114"))
				{
					rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6");
					CellNote note = new CellNote(rowa[colgrfOperListActNo].ToString() + " " + rowa[colgrfOperListVN].ToString());
					CellRange rg = grfOperList.GetCellRange(i, colgrfOperListFullNameT);
					rg.UserData = note;
				}
				rowa[0] = i.ToString();
				i++;
			}
			CellNoteManager mgr = new CellNoteManager(grfOperList);
			timeOperList.Enabled = true;
			pageLoad = false;
		}
		private void setGrfSrc(String flagsearch)
		{
			DataTable dt = new DataTable();
			dt = bc.bcDB.pttDB.selectPatinetBySearch(txtSrcHn.Text.Trim());
			int i = 1, j = 1;			grfSrc.Rows.Count = 1;			grfSrc.Rows.Count = dt.Rows.Count + 1;
			//pB1.Maximum = dt.Rows.Count;
			foreach (DataRow row1 in dt.Rows)
			{
				//pB1.Value++;
				try
				{
					Patient ptt = new Patient();
					Row rowa = grfSrc.Rows[i];
					HN = row1["MNC_HN_NO"].ToString();
					ptt.patient_birthday = row1["MNC_bday"].ToString();
					rowa[colgrfSrcHn] = row1["MNC_HN_NO"].ToString();
					rowa[colgrfSrcFullNameT] = row1["pttfullname"].ToString();
					rowa[colgrfSrcPID] = row1["mnc_id_no"].ToString();
					rowa[colgrfSrcDOB] = bc.datetoShowShort(row1["MNC_bday"].ToString());
					rowa[colgrfSrcAge] = ptt.AgeStringShort1();
					rowa[colgrfSrcVisitReleaseOPD] = bc.datetoShowShort(row1["MNC_LAST_CONT"].ToString());
					rowa[colgrfSrcVisitReleaseIPD] = bc.datetoShowShort(row1["MNC_LAST_CONT_I"].ToString());
					rowa[colgrfSrcVisitReleaseIPDDischarge] = bc.datetoShowShort(row1["ipd_discharge_release"].ToString());
					rowa[colgrfSrcPttid] = "";
					rowa[0] = i.ToString();
					i++;
				}
				catch (Exception ex)
				{
					lfSbMessage.Text = ex.Message;
					new LogWriter("e", "FrmOPD setGrfSrc " + ex.Message);
					bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfSrc ", ex.Message);
				}
			}
		}
		private void setGrfApm()
		{
			if (pageLoad) return;
			String deptcode = "", dtrcode = "";
			DataTable dtvs = new DataTable();
			if (chkApmDate.Checked)
			{
				DateTime.TryParse(txtApmDate.Value.ToString(), out DateTime dtapm);//txtApmDate
				if (dtapm.Year < 1900) { dtapm.AddYears(543); }
				deptcode = cboApmDept1.SelectedItem == null ? "" : ((ComboBoxItem)cboApmDept1.SelectedItem).Value.ToString();
				dtrcode = cboApmDtr.SelectedItem != null ? ((ComboBoxItem)cboApmDtr.SelectedItem).Value : "";
				dtvs = bc.bcDB.pt07DB.selectByDateDept(dtapm.Year.ToString() + "-" + dtapm.ToString("MM-dd"), deptcode, dtrcode, "");
			}
			else if (chkApmHn.Checked)
			{
				dtvs = bc.bcDB.pt07DB.selectByHnAll(txtApmHn.Text.Trim(), "desc");
			}
			HashSet<ComboBoxItem> lstdtr = new HashSet<ComboBoxItem>();
			grfApm.Rows.Count = 1; grfApm.Rows.Count = dtvs.Rows.Count + 1;
			cboApmDtr.Items.Clear();
			cboApmDtr.SelectedItem = null;
			int i = 1, j = 1;
			foreach (DataRow row1 in dtvs.Rows)
			{
				if (dtrcode.Length <= 0)
				{
					ComboBoxItem dtr = new ComboBoxItem();
					dtr.Value = row1["MNC_DOT_CD"].ToString();
					dtr.Text = row1["dtr_name"].ToString();
					if (!lstdtr.Any(d => d.Value == dtr.Value)) { lstdtr.Add(dtr); cboApmDtr.Items.Add(dtr); }
				}
				Row rowa = grfApm.Rows[i];
				rowa[colgrfPttApmApmDateShow] = bc.datetoShowShort(row1["MNC_APP_DAT"].ToString());
				rowa[colgrfPttApmApmDateShow] = row1["MNC_APP_DAT"].ToString();
				rowa[colgrfPttApmApmTime] = bc.showTime(row1["MNC_APP_TIM"].ToString());
				rowa[colgrfPttApmDeptR] = row1["mnc_md_dep_dsc"].ToString();
				rowa[colgrfPttApmDeptMake] = bc.bcDB.pm32DB.getDeptNameOPD(row1["mnc_sec_no"].ToString());
				rowa[colgrfPttApmNote] = row1["MNC_APP_DSC"].ToString();
				rowa[colgrfPttApmOrder] = row1["MNC_REM_MEMO"].ToString();

				rowa[colgrfPttApmDocNo] = row1["MNC_DOC_NO"].ToString();
				rowa[colgrfPttApmDocYear] = row1["MNC_DOC_YR"].ToString();
				rowa[colgrfPttApmHN] = row1["MNC_HN_NO"].ToString();
				rowa[colgrfPttApmPttName] = row1["ptt_fullnamet"].ToString();
				rowa[colgrfPttApmDtrname] = row1["dtr_name"].ToString();
				rowa[colgrfPttApmPhone] = row1["MNC_CUR_TEL"].ToString();
				rowa[colgrfPttApmPaidName] = row1["MNC_FN_TYP_DSC"].ToString();

				rowa[colgrfPttApmRemarkCall] = row1["remark_call"].ToString();
				rowa[colgrfPttApmRemarkCallDate] = row1["remark_call_date"].ToString();
				if (row1["status_remark_call"].ToString().Equals("1")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย รับสาย บุคคลอื่นเป็นคนรับ"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor); }
				else if (row1["status_remark_call"].ToString().Equals("2")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย ไม่รับสาย"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#EBBDB6"); }//#EBBDB6
				else if (row1["status_remark_call"].ToString().Equals("3")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรไม่ติด สายไม่ว่าง"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#CCCCFF"); }
				else if (row1["status_remark_call"].ToString().Equals("4")) { rowa[colgrfPttApmStatusRemarkCall] = "โทรเรียบร้อย รับสาย แจ้งคนไข้ ครบถ้วน"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#9FE2BF"); }
				else if (row1["status_remark_call"].ToString().Equals("5")) { rowa[colgrfPttApmStatusRemarkCall] = "ไม่สามารถโทรได้ ไม่มีเบอร์โทร"; rowa.StyleNew.BackColor = ColorTranslator.FromHtml("#FF7F50"); }
				else rowa[colgrfPttApmStatusRemarkCall] = "";

				rowa[0] = i.ToString();
				i++;
			}
			lfSbMessage.Text = "พบ " + dtvs.Rows.Count + "รายการ";
		}
        private void setGrfOrderLab()
        {
            if (grfOperLab == null) { initGrfLab(ref grfOperLab, ref pnPrenoLab); }
            //grfOperXray.Rows.Clear();
            grfOperLab.Rows.Count = 1;            grfOperLab.Cols.Count = 12;
            grfOperLab.Cols[colLabResult].Visible = false;            grfOperLab.Cols[colInterpret].Visible = false;
            grfOperLab.Cols[colNormal].Visible = false;            grfOperLab.Cols[colUnit].Visible = false;
            grfOperLab.Cols[colLabCode].Visible = false;            grfOperLab.Cols[colLabReqDate].Visible = false;
            grfOperLab.Cols[colLabDate].Visible = false;            grfOperLab.Cols[colLabNameSub].Visible = false;
            grfOperLab.Cols[colLabReqNo].AllowEditing = false;            grfOperLab.Cols[colLabReqStatus].AllowEditing = false;
            DataTable dt = new DataTable();
            dt = bc.bcDB.labT02DB.selectbyHN(txtOperHN.Text.Trim(), VSDATE, PRENO);
            if (dt.Rows.Count > 0)
            {
                grfOperLab.Rows.Count = dt.Rows.Count + 1;                int i = 1;
                foreach (DataRow row1 in dt.Rows)
                {
                    try
                    {
                        Row rowa = grfOperLab.Rows[i];
                        rowa[colLabCode] = row1["order_code"].ToString();
                        rowa[colLabName] = row1["order_name"].ToString();
                        rowa[colLabReqStatus] = row1["MNC_REQ_STS"].ToString();
                        rowa[colLabReqNo] = row1["req_no"].ToString();
                        rowa[colLabReqDate] = row1["req_date"].ToString();
                        rowa[0] = i.ToString();
                        rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        lfSbMessage.Text = ex.Message;
                        new LogWriter("e", "FrmOPD setGrfOrderLab " + ex.Message);
                        bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfOrderLab ", ex.Message);
                    }
                }
            }
            grfOperLab.Refresh();
        }
        private void setGrfOrderXray()
        {
			if (grfOperXray == null) { initGrfXray(ref grfOperXray, ref pnPrenoXray); }
            grfOperXray.Rows.Count = 1;            grfOperXray.Cols.Count = 11;
            grfOperXray.Cols[colLabResult].Visible = false;            grfOperXray.Cols[colInterpret].Visible = false;
            grfOperXray.Cols[colNormal].Visible = false;            grfOperXray.Cols[colUnit].Visible = false;
            grfOperXray.Cols[colLabCode].Visible = false;            grfOperXray.Cols[colLabReqDate].Visible = false;
            grfOperXray.Cols[colLabDate].Visible = false;            grfOperXray.Cols[colLabNameSub].Visible = false;
            grfOperXray.Cols[colLabReqNo].AllowEditing = false;
            DataTable dt = new DataTable();
            dt = bc.bcDB.xrayT02DB.selectbyHN(txtOperHN.Text.Trim(), VSDATE, PRENO);
            if (dt.Rows.Count >= 0)
            {
                grfOperXray.Rows.Count = dt.Rows.Count + 1;                int i = 1;
                foreach (DataRow row1 in dt.Rows)
                {
                    try
                    {
                        Row rowa = grfOperXray.Rows[i];
                        rowa[colLabCode] = row1["order_code"].ToString();
                        rowa[colLabName] = row1["order_name"].ToString();
                        //rowa[colLabNameSub] = row1["MNC_LABT2_STATUS"].ToString();
                        rowa[colLabReqNo] = row1["req_no"].ToString();
                        rowa[colLabReqDate] = row1["req_date"].ToString();
                        rowa[0] = i.ToString();
                        rowa.StyleNew.BackColor = ColorTranslator.FromHtml(bc.iniC.grfRowColor);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        lfSbMessage.Text = ex.Message;
                        new LogWriter("e", "FrmOPD setGrfOrderXray " + ex.Message);
                        bc.bcDB.insertLogPage(bc.userId, this.Name, "setGrfOrderXray ", ex.Message);
                    }
                }
            }
            grfOperXray.Refresh();
        }
    }
}
