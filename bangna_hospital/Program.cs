using bangna_hospital.control;
using bangna_hospital.gui;
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

            BangnaControl bc = new BangnaControl();
            if (args.Length == 1)
            {
                //MessageBox.Show("hn "+ args[0], "");
                bc.hn = args[0];
                //bc.hn = args[0];
            }
            //for (int i = 0; i < args.Length; i++)
            //{
            //    string argument = args[i];
            //    bc.hn = argument;

            //}

            //FrmSplash spl = new FrmSplash();
            //spl.Show();
            //Application.Run(new gui.MainMenu(bc, spl));

            //Application.Run(new gui.FrmBillLabCheck(bc));

            //Application.Run(new gui.FrmXrayViewDaily(bc));
            try
            {
                FrmScanView1 frm = new FrmScanView1(bc, bc.hn);
                frm.WindowState = FormWindowState.Maximized;
                Application.Run(frm);
            }
            catch(Exception ex)
            {
                MessageBox.Show("error "+ex.Message, "");
            }
            
        }
    }
}
