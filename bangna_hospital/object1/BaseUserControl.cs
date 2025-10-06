// Create a base UserControl with shared font variables
using bangna_hospital.control;
using System.Drawing;
using System.Windows.Forms;

public class BaseUserControl : UserControl
{
    protected Font fEdit, fEditB, fEdit3B, fEdit5B, famt1, famt2, famt2B, famt4B, famt2BL, famt5, famt5B, famt5BL, famt7, famt7B, ftotal, fPrnBil, fEditS, fEditS1, fEdit2, fEdit2B, famtB14, famtB30, fque, fqueB, fPDF, fPDFs2, fPDFs6, fPDFs8, fPDFl2;
    BangnaControl BC;
    protected void initFont(BangnaControl BC)
    {
        //this.BC = bc;
        fEdit = new System.Drawing.Font(BC.iniC.grdViewFontName, BC.grdViewFontSize, FontStyle.Regular);
        fEditB = new System.Drawing.Font(BC.iniC.grdViewFontName, BC.grdViewFontSize, FontStyle.Bold);
        fEdit2 = new System.Drawing.Font(BC.iniC.grdViewFontName, BC.grdViewFontSize + 2, FontStyle.Regular);
        fEdit2B = new System.Drawing.Font(BC.iniC.grdViewFontName, BC.grdViewFontSize + 2, FontStyle.Bold);
        fEdit5B = new System.Drawing.Font(BC.iniC.grdViewFontName, BC.grdViewFontSize + 5, FontStyle.Bold);

        famt1 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 1, FontStyle.Regular);
        famt5 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 5, FontStyle.Regular);
        famt5BL = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 5, FontStyle.Underline);
        famt5B = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 5, FontStyle.Bold);
        famt2 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 2, FontStyle.Regular);
        famt2B = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 2, FontStyle.Bold);
        famt2BL = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 2, FontStyle.Underline);
        famt4B = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 4, FontStyle.Bold);

        famt7 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 7, FontStyle.Regular);
        famt7B = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 7, FontStyle.Bold);
        famtB14 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 14, FontStyle.Bold);
        famtB30 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 30, FontStyle.Bold);
        ftotal = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 60, FontStyle.Bold);
        fPrnBil = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize, FontStyle.Regular);
        fque = new System.Drawing.Font(BC.iniC.queFontName, BC.queFontSize + 3, FontStyle.Bold);
        fqueB = new System.Drawing.Font(BC.iniC.queFontName, BC.queFontSize + 7, FontStyle.Bold);
        fEditS = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 2, FontStyle.Regular);
        fEditS1 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 1, FontStyle.Regular);

        fPDF = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize, FontStyle.Regular);
        fPDFs2 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 2, FontStyle.Regular);
        fPDFl2 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize + 2, FontStyle.Regular);
        fPDFs6 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 6, FontStyle.Regular);
        fPDFs8 = new System.Drawing.Font(BC.iniC.pdfFontName, BC.pdfFontSize - 8, FontStyle.Regular);
    }
}