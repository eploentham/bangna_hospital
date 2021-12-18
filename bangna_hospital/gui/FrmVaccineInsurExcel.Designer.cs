
namespace bangna_hospital.gui
{
    partial class FrmVaccineInsurExcel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBrowe = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pB1 = new System.Windows.Forms.ProgressBar();
            this.btnExcel = new System.Windows.Forms.Button();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtPID = new System.Windows.Forms.TextBox();
            this.txtDOB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProv = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBrowe
            // 
            this.btnBrowe.Location = new System.Drawing.Point(604, 41);
            this.btnBrowe.Name = "btnBrowe";
            this.btnBrowe.Size = new System.Drawing.Size(38, 23);
            this.btnBrowe.TabIndex = 0;
            this.btnBrowe.Text = "...";
            this.btnBrowe.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Path Excel ";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(76, 44);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(522, 20);
            this.txtPath.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "yyyy-MM-dd";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Excel PID";
            // 
            // pB1
            // 
            this.pB1.Location = new System.Drawing.Point(12, 363);
            this.pB1.Name = "pB1";
            this.pB1.Size = new System.Drawing.Size(776, 23);
            this.pB1.TabIndex = 5;
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(667, 41);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(107, 23);
            this.btnExcel.TabIndex = 6;
            this.btnExcel.Text = "Excel Update";
            this.btnExcel.UseVisualStyleBackColor = true;
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(152, 98);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(134, 20);
            this.txtDate.TabIndex = 7;
            // 
            // txtPID
            // 
            this.txtPID.Location = new System.Drawing.Point(152, 135);
            this.txtPID.Name = "txtPID";
            this.txtPID.Size = new System.Drawing.Size(134, 20);
            this.txtPID.TabIndex = 8;
            this.txtPID.Text = "33";
            // 
            // txtDOB
            // 
            this.txtDOB.Location = new System.Drawing.Point(152, 172);
            this.txtDOB.Name = "txtDOB";
            this.txtDOB.Size = new System.Drawing.Size(134, 20);
            this.txtDOB.TabIndex = 10;
            this.txtDOB.Text = "31";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(85, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Excel DOB";
            // 
            // txtProv
            // 
            this.txtProv.Location = new System.Drawing.Point(168, 209);
            this.txtProv.Name = "txtProv";
            this.txtProv.Size = new System.Drawing.Size(134, 20);
            this.txtProv.TabIndex = 12;
            this.txtProv.Text = "37";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(85, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Excel province";
            // 
            // FrmVaccineInsurExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtProv);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDOB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPID);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.pB1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowe);
            this.Name = "FrmVaccineInsurExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmVaccineInsurExcel";
            this.Load += new System.EventHandler(this.FrmVaccineInsurExcel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pB1;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.TextBox txtPID;
        private System.Windows.Forms.TextBox txtDOB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProv;
        private System.Windows.Forms.Label label5;
    }
}