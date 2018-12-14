using bangna_hospital.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.gui
{
    public class FrmWaiting: Form
    {
        public ProgressBar pB;
        Panel panel1;
        public Label lb;
        PictureBox pic;
        public FrmWaiting()
        {
            initConfig();
        }
        private void initConfig()
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;

            this.Size = new System.Drawing.Size(300, 200);
            this.Text = "please Waiting";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            panel1 = new Panel();
            panel1.Dock = DockStyle.Fill;
            pB = new ProgressBar();
            pB.Dock = DockStyle.Bottom;

            lb = new Label();
            lb.Dock = DockStyle.Top;
            pic = new PictureBox();
            pic.Dock = DockStyle.Top;
            pic.Image = Resources.loading_transparent;

            Controls.Add(panel1);
            panel1.Controls.Add(pB);
            panel1.Controls.Add(lb);
            panel1.Controls.Add(pic);
        }
    }
}
