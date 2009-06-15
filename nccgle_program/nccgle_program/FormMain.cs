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
        ClusterBase FinalTree;

        ManyMatrixToShow Coefficients;

        public FormMain()
        {
            InitializeComponent();
            comboBox1.Enabled = false;

            GlobalControls.debugOutputScreen = textBox1;
            GlobalControls.progress = (ToolStripProgressBar)statusStrip.Items["toolStripProgressBar"];
            GlobalControls.log_bar = (ToolStripStatusLabel)statusStrip.Items["toolStripStatusLabel"];
            GlobalControls.table = resultBrowser;

            D = new Matrix(Constant.DocumentsNumber, Constant.TermsNumber);
            S = new Matrix(Constant.DocumentsNumber, Constant.TermsNumber);
            S_Shtrih = new Matrix(Constant.DocumentsNumber, Constant.TermsNumber);
            C = new Matrix(Constant.TermsNumber, Constant.TermsNumber);
            C_Shtrih = new Matrix(Constant.DocumentsNumber, Constant.DocumentsNumber);
            FinalTree = new ClusterBase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JustDoIt.ForMatrixD(D);
            JustDoIt.ForMatrixS(D, S);
            JustDoIt.ForMatrixS_Shtrih(D, S_Shtrih);

            C = JustDoIt.MultiplyMatrix(JustDoIt.Transposition(S), S_Shtrih);
            C_Shtrih = JustDoIt.MultiplyMatrix(S, JustDoIt.Transposition(S_Shtrih));

            Coefficients = JustDoIt.CalculateCoeff(D, C, S_Shtrih);

            FinalTree = JustDoIt.ClusterBuilder(D, S, S_Shtrih, C, C_Shtrih, Coefficients);

            comboBox1.Enabled = true;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    D.Comment = "Матрица D<br>Горизонталь - термины \\ вертикаль - документы";
                    Render.DoOne(D);
                    break;
                case 1:
                    S.Comment = "Матрица S";
                    Render.DoOne(S);
                    break;
                case 2:
                    S_Shtrih.Comment = "Матрица S_shtrih";
                    Render.DoOne(S_Shtrih);
                    break;
                case 3:
                    C.Comment = "Матрица C";
                    Render.DoOne(C);
                    break;
                case 4:
                    C_Shtrih.Comment = "Матрица C_shtrih";
                    Render.DoOne(C_Shtrih);
                    break;
                
                case 5:                    
                    Render.AddToRenderList(Coefficients.body);
                    Render.DoList();
                    break;

                case 6:
                    Render.RenderTree(FinalTree);
                    break;
            }
            resultBrowser.Select();
        }
    }
}