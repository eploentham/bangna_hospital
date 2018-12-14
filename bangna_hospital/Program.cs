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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BangnaControl bc = new BangnaControl();
            FrmSplash spl = new FrmSplash();
            spl.Show();
            Application.Run(new gui.MainMenu(bc, spl));
        }
    }
}
