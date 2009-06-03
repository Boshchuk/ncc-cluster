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
            JustDoIt.progress = (ToolStripProgressBar)statusStrip.Items["toolStripProgressBar"];
            JustDoIt.log_bar = (ToolStripStatusLabel)statusStrip.Items["toolStripStatusLabel"];

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
                    JustDoIt.RenderMatrix("������� D<br>����������� - ������� \\ ��������� - ���������", resultBrowser, D);
                    break;
                case 1:
                    JustDoIt.RenderMatrix("������� S", resultBrowser, S);
                    break;
                case 2:
                    JustDoIt.RenderMatrix("������� S_shtrih", resultBrowser, S_Shtrih);
                    break;
                case 3:
                    JustDoIt.RenderMatrix("������� C", resultBrowser, C);
                    break;
                case 4:
                    JustDoIt.RenderMatrix("������� C_shtrih", resultBrowser, C_Shtrih);
                    break;
                
                case 5:
                    JustDoIt.RenderMatrix("������������ ����� ��� ����������", resultBrowser, Coefficients.koef_svyazi_i);
                    break;
                case 6:
                    JustDoIt.RenderMatrix("����������� ����� �����", resultBrowser, Coefficients.koef_svyazi_obschiy);
                    break;
                case 7:
                    JustDoIt.RenderMatrix("������������ ������������ ��� ����������", resultBrowser, Coefficients.koef_uniq_i);
                    break;

                case 8:
                    JustDoIt.RenderMatrix("����������� ������������ �����", resultBrowser, Coefficients.koef_uniq_obschiy);
                    break;

                case 9:
                    JustDoIt.RenderMatrix("����� ���������", resultBrowser, Coefficients.Nu_C);
                    break;

                case 10:
                    JustDoIt.RenderMatrix("����� ����� � ��������", resultBrowser, Coefficients.M_C);
                    break;

                case 11:
                    JustDoIt.RenderTree(resultBrowser, FinalTree);
                    break;
            }
            resultBrowser.Select();
        }
    }
}