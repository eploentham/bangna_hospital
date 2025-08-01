using bangna_hospital.control;
using bangna_hospital.gui;
using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //MessageBox.Show("error Main try{} ", "");
            //new LogWriter("d", "Program Start Form "+ args.Length);
            BangnaControl bc = new BangnaControl();
            //MessageBox.Show("error Main try{} " + bc.iniC.programLoad, "");
            if (args.Length == 1)
            {
                //MessageBox.Show("hn "+ args[0], "");
                //new LogWriter("d", "Program Start Form args[0] " + args[0]);
                bc.hn = args[0];
                //bc.hn = args[0];
            }
            //for (int i = 0; i < args.Length; i++)
            //{
            //    string argument = args[i];
            //    new LogWriter("d","i "+i+ " argument " + argument);
            //    //bc.hn = argument;

            //}
            //string[] arguments = Environment.GetCommandLineArgs();
            //foreach(String aaa in arguments)
            //{
            //    new LogWriter("d", "aaa " + aaa);
            //}
            //FrmSplash spl = new FrmSplash();
            //spl.Show();
            //Application.Run(new gui.MainMenu(bc, spl));

            //Application.Run(new gui.FrmBillLabCheck(bc));
            //MessageBox.Show("error Main " , "");
            //Application.Run(new gui.FrmXrayViewDaily(bc));
            var exists = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1;
            if(exists) { MessageBox.Show("โปรแกรมเปิดอยู่แล้ว", "");                Environment.Exit(0); }
            String err = "";
            try
            {
                //MessageBox.Show("error Main try{} "+ bc.iniC.programLoad, "");
                if (bc.iniC.programLoad.Equals("ScanView"))
                {
                    if (bc.iniC.statusdoctorold.Equals("new"))
                    {
                        FrmPatient frm = new FrmPatient(bc,"");
                        //Form1 frm = new Form1();
                        frm.WindowState = FormWindowState.Maximized;
                        Application.Run(frm);
                    }
                    else
                    {
                        FrmScanView1 frm = new FrmScanView1(bc, bc.hn, "show");
                        //Form1 frm = new Form1();
                        frm.WindowState = FormWindowState.Maximized;
                        Application.Run(frm);
                    }
                }
                else if (bc.iniC.programLoad.Equals("ScanAdd"))
                {
                    FrmSplash spl = new FrmSplash();
                    spl.Show();
                    Application.Run(new gui.MainMenu(bc, spl));
                    //Application.Run(frm);
                }
                else if (bc.iniC.programLoad.Equals("XrayImportToPACs"))
                {
                    Application.Run(new gui.FrmXrayViewDaily(bc));
                }
                else if (bc.iniC.programLoad.Equals("LabOut"))
                {
                    //Application.Run(new gui.FrmScanAddLabOut(bc));
                    Application.Run(new gui.FrmOPD(bc,"outlab"));
                }
                else if (bc.iniC.programLoad.Equals("nhsoPrint"))
                {
                    Application.Run(new gui.frmNHSOPrint(bc));
                }
                else if (bc.iniC.programLoad.Equals("screenCapture"))
                {
                    Application.Run(new gui.FrmScreenCapture(bc));
                }
                else if (bc.iniC.programLoad.Equals("doctorView"))
                {
                    //new LogWriter("e", "Program doctorView Start Form");
                    //FrmSplash spl = new FrmSplash();
                    //spl.Show();
                    Application.Run(new gui.FrmDoctor(ref bc));
                }
                else if (bc.iniC.programLoad.Equals("PrintCri"))
                {
                    FrmSplash spl = new FrmSplash();
                    spl.Show();
                    Application.Run(new gui.FrmPrintCri(bc, spl));
                }
                else if (bc.iniC.programLoad.Equals("LabOutReceive"))
                {
                    //FrmSplash spl = new FrmSplash();
                    //spl.Show();
                    //Application.Run(new gui.FrmLabOutReceive(bc));
                    Application.Run(new gui.FrmLabOutReceive1(bc));
                }
                else if (bc.iniC.programLoad.Equals("LabOutReceiveView"))
                {
                    Application.Run(new gui.FrmLabOutReceiveView1(bc));
                }
                else if (bc.iniC.programLoad.Equals("NurseScanView"))
                {
                    Application.Run(new gui.FrmNurseScanView(bc));
                }
                else if (bc.iniC.programLoad.Equals("XrayPACsAdd"))
                {
                    Application.Run(new gui.FrmXrayPACsAdd(bc));
                }
                else if (bc.iniC.programLoad.Equals("OrOperativeNote"))
                {
                    Application.Run(new gui.FrmOrOperativeNote(bc,""));
                }
                else if (bc.iniC.programLoad.Equals("BillLabCheck"))
                {
                    Application.Run(new gui.FrmBillLabCheck(bc));
                }
                else if (bc.iniC.programLoad.Equals("createPDF"))
                {
                    //new LogWriter("d", "args " + args.Length);
                    Application.Run(new gui.FrmDocCreatePDF(bc, "",""));
                }
                else if (bc.iniC.programLoad.Equals("OPBKKClaim"))
                {
                    //new LogWriter("d", "args " + args.Length);
                    Application.Run(new gui.FrmOPBKKClaim(bc));
                }
                else if (bc.iniC.programLoad.Equals("OPDCovid"))
                {
                    //new LogWriter("d", "args " + args.Length);
                    Application.Run(new gui.FrmOPD2CheckUPMain());
                }
                else if (bc.iniC.programLoad.Equals("DfDoctor"))
                {
                    //new LogWriter("d", "args " + args.Length);
                    Application.Run(new gui.FrmDfDoctor1(bc));
                }
                else if (bc.iniC.programLoad.Equals("PatientNewSmartCard"))
                {
                    //new LogWriter("d", "args " + args.Length);
                    Application.Run(new gui.FrmSmartCard(bc));
                }
                else if (bc.iniC.programLoad.Equals("ReceiptionCovidSend"))
                {
                    //new LogWriter("d", "args " + args.Length);
                    Application.Run(new gui.FrmReceptionCovidSend(bc));
                }
                else if (bc.iniC.programLoad.Equals("VaccineApprove"))
                {
                    //new LogWriter("d", "args " + args.Length);
                    Application.Run(new gui.FrmVaccineApprove(bc));
                }
                else if (bc.iniC.programLoad.Equals("PatientNewSmartCardNoteBook"))
                {
                    //new LogWriter("d", "args " + args.Length);
                    Application.Run(new gui.FrmSmartCardNoteBook(bc));
                }
                else if (bc.iniC.programLoad.Equals("vaccineExcel"))
                {
                    //new LogWriter("d", "args " + args.Length);
                    Application.Run(new gui.FrmVaccineInsurExcel(bc));
                }
                else if (bc.iniC.programLoad.Equals("Checkup"))
                {
                    //new LogWriter("d", "args " + args.Length);
                    Application.Run(new gui.FrmCheckup(bc));
                }
                else if (bc.iniC.programLoad.Equals("ssndata"))
                {
                    //new LogWriter("d", "args " + args.Length);
                    Application.Run(new gui.frmSsnData(bc));
                }
                else if (bc.iniC.programLoad.Equals("Reception"))
                {
                    Application.Run(new gui.FrmReception(bc));
                }
                else if (bc.iniC.programLoad.Equals("importMDB"))
                {
                    Application.Run(new gui.FrmImportMDB(bc));
                }
                else if (bc.iniC.programLoad.Equals("colposcopy"))
                {
                    Application.Run(new gui.FrmColposcopyReport(bc));
                }
                else if (bc.iniC.programLoad.Equals("epidemCovid"))
                {
                    Application.Run(new gui.FrmEpidem(bc));
                }
                else if (bc.iniC.programLoad.Equals("excel"))
                {
                    Application.Run(new gui.FrmExcel(bc));
                }
                else if (bc.iniC.programLoad.Equals("PatientNewSmartCardB1"))
                {
                    err = "PatientNewSmartCardB1";
                    Application.Run(new gui.FrmSmartCardB1(bc));
                }
                else if (bc.iniC.programLoad.Equals("PatientNewSmartCardB1Check"))
                {
                    err = "PatientNewSmartCardB1Check";
                    Application.Run(new gui.FrmFingerScanCheck(bc));
                }
                else if (bc.iniC.programLoad.Equals("aipn"))
                {
                    err = "aipn";
                    Application.Run(new gui.FrmAipn(bc));
                }
                else if (bc.iniC.programLoad.Equals("LisLink"))
                {
                    err = "LisLink";
                    Application.Run(new gui.FrmLisLink(bc));
                }
                else if (bc.iniC.programLoad.Equals("certdoctor"))
                {
                    err = "LisLink";
                    Application.Run(new gui.FrmCertDoctor(bc));
                }
                else if (bc.iniC.programLoad.Equals("EditJobNo"))
                {
                    err = "EditJobNo";
                    Application.Run(new gui.FrmEditJobNo(bc));
                }
                else if (bc.iniC.programLoad.Equals("OPD"))
                {
                    err = "OPD";
                    Application.Run(new gui.FrmOPD(bc));
                }
                else if (bc.iniC.programLoad.Equals("QUEOPD"))
                {
                    err = "QUEOPD";
                    Application.Run(new gui.FrmQueueShow(bc));
                }
                else if (bc.iniC.programLoad.Equals("pharmacyOPD"))
                {
                    err = "pharmacyOPD";
                    Application.Run(new gui.FrmPharmacy(bc));
                }
                else if (bc.iniC.programLoad.Equals("medscan"))
                {
                    err = "medscan";
                    Application.Run(new gui.FrmMedScanExport(bc));
                }
                else if (bc.iniC.programLoad.Equals("ward"))
                {
                    err = "ward";
                    Application.Run(new gui.FrmWard(bc,"outlab"));
                }
                else if (bc.iniC.programLoad.Equals("xray"))
                {
                    err = "xray";
                    Application.Run(new gui.FrmXray(bc));
                }
                else if (bc.iniC.programLoad.Equals("lab"))
                {
                    err = "lab";
                    Application.Run(new gui.FrmLab(bc));
                }
                else if (bc.iniC.programLoad.Equals("pharmacyIPD"))
                {
                    err = "pharmacyIPD";
                    Application.Run(new gui.FrmPharmacy(bc));
                }
                else if (bc.iniC.programLoad.Equals("ssop"))
                {
                    err = "ssop";
                    Application.Run(new gui.FrmSSO(bc));
                }
                else if (bc.iniC.programLoad.Equals("stock"))
                {
                    err = "stock";
                    Application.Run(new gui.FrmStock(bc));
                }
                else if (bc.iniC.programLoad.Equals("cashier"))
                {
                    err = "cashier";
                    Application.Run(new gui.FrmCashier(bc));
                }
                else
                {
                    if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("bangna_hospital_scan_capture"))
                    {
                        //MessageBox.Show("labex", "labex");
                        Application.Run(new FrmScreenCapture(bc));
                    }
                }
            }
            catch(Exception ex)
            {
                new LogWriter("e", "Program Start Form "+ bc.iniC.programLoad+" "+ ex.Message+" err "+err);
                MessageBox.Show("error Main " + ex.Message, "");
            }
            
        }
    }
}
