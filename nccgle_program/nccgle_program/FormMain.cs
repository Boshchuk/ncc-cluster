using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace nccgle_program
{
    public partial class FormMain : Form
    {
        Matrix D;
        Matrix S;
        Matrix S_Shtrih;
        Matrix C;
        Matrix C_Shtrih;

     /*   Matrix DeltaCol1;
        Matrix PCol;
        Matrix Delta_Col;
        Matrix Klasters;
        Matrix KfUnic1;
        Matrix KfSvyaz1;
        Matrix KfUnic2;
        Matrix KfSvyaz2;
        Matrix KolKlasters;
        Matrix KolTerms;*/

        public FormMain()
        {
            InitializeComponent();
            JustDoIt.progress = (ToolStripProgressBar)statusStrip.Items["toolStripProgressBar"];
            JustDoIt.log_bar = (ToolStripStatusLabel)statusStrip.Items["toolStripStatusLabel"];

            D = new Matrix(Constant.DocumentsNumber, Constant.TermsNumber);
            S = new Matrix(Constant.DocumentsNumber, Constant.TermsNumber);
            S_Shtrih = new Matrix(Constant.DocumentsNumber, Constant.TermsNumber);
            C = new Matrix(Constant.TermsNumber, Constant.TermsNumber);
            C_Shtrih = new Matrix(Constant.DocumentsNumber, Constant.DocumentsNumber);

            /*Matrix DeltaCol1 = new Matrix("Delta");
            Matrix PCol = new Matrix("P");
            Matrix Delta_Col = new Matrix("DeltaS");
            Matrix Klasters = new Matrix("Klasters");
            Matrix KfUnic1 = new Matrix("Общ_коэф_уник.");
            Matrix KfSvyaz1 = new Matrix("Коэф_связи");
            Matrix KfUnic2 = new Matrix("Общ. коэф. уник.");
            Matrix KfSvyaz2 = new Matrix("Коэф. связи");
            Matrix KolKlasters = new Matrix("Число кластеров");
            Matrix KolTerms = new Matrix("Число терминов");*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            JustDoIt.ForMatrixD(D);
            JustDoIt.ForMatrixS(D, S);
            JustDoIt.ForMatrixS_Shtrih(D, S_Shtrih);

            C = JustDoIt.MultiplyMatrix(JustDoIt.Transposition(S), S_Shtrih);
            C_Shtrih = JustDoIt.MultiplyMatrix(S, JustDoIt.Transposition(S_Shtrih));

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    JustDoIt.RenderMatrix("Матрица D", resultBrowser, D);
                    break;
                case 1:
                    JustDoIt.RenderMatrix("Матрица S", resultBrowser, S);
                    break;
                case 2:
                    JustDoIt.RenderMatrix("Матрица S_shtrih", resultBrowser, S_Shtrih);
                    break;
                case 3:
                    JustDoIt.RenderMatrix("Матрица C", resultBrowser, C);
                    break;
                case 4:
                    JustDoIt.RenderMatrix("Матрица C_shtrih", resultBrowser, C_Shtrih);
                    break;

            }
        }
    }
}