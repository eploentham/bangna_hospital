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
            //new LogWriter("d", "Program Start Form "+ args.Length);
            BangnaControl bc = new BangnaControl();
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
            try
            {
                if (bc.iniC.programLoad.Equals("ScanView"))
                {
                    FrmScanView1 frm = new FrmScanView1(bc, bc.hn,"show");
                    //Form1 frm = new Form1();
                    frm.WindowState = FormWindowState.Maximized;
                    Application.Run(frm);
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
                    Application.Run(new gui.FrmScanAddLabOut(bc));
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
                    FrmSplash spl = new FrmSplash();
                    spl.Show();
                    Application.Run(new gui.FrmDoctorView1(bc, spl));
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
                new LogWriter("e", "Program doctorView Start Form");
                MessageBox.Show("error Main " + ex.Message, "");
            }
            
        }
    }
}
