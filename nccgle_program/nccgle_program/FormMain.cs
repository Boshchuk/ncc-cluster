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

        ManyMatrixToShow Cooficients;

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JustDoIt.ForMatrixD(D);
            JustDoIt.ForMatrixS(D, S);
            JustDoIt.ForMatrixS_Shtrih(D, S_Shtrih);

            C = JustDoIt.MultiplyMatrix(JustDoIt.Transposition(S), S_Shtrih);
            C_Shtrih = JustDoIt.MultiplyMatrix(S, JustDoIt.Transposition(S_Shtrih));

            Cooficients = JustDoIt.CalculateCoeff(D, C, S_Shtrih);

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
                
                // �����������
                case 5:
                    JustDoIt.RenderMatrix("Delta", resultBrowser, Cooficients.Delta);
                    break;
                case 6:
                    JustDoIt.RenderMatrix("Pi", resultBrowser, Cooficients.Pi);
                    break;
                case 7:
                    JustDoIt.RenderMatrix("DeltaS", resultBrowser, Cooficients.DeltaS);
                    break;

                case 8:
                    //JustDoIt.RenderMatrix("Clusters", resultBrowser, Cooficients.Clusters);
                    break;

                case 9:
                    JustDoIt.RenderMatrix("���� ������������", resultBrowser, Cooficients.KoeficientUniqI);
                    break;
                case 10:
                    JustDoIt.RenderMatrix("���� �����", resultBrowser, Cooficients.KoeficientSvyaziI);
                    break;

                case 11:
                    JustDoIt.RenderMatrix("����� ���� ������������", resultBrowser, Cooficients.ObschKoeficientUnic);
                    break;
                case 12:
                    JustDoIt.RenderMatrix("����� ���� �����", resultBrowser, Cooficients.ObschKoeficientSvyazi);
                    break;

                case 13:
                    JustDoIt.RenderMatrix("����� ���������", resultBrowser, Cooficients.ClustersNumber);
                    break;
                
                case 14:
                    JustDoIt.RenderMatrix("����� ��������", resultBrowser, Cooficients.TerminsNumber);
                    break;
                     
                /*        
Delta+ 5
Pi+6
DeltaS+7
Clusters  8
���� ������������  + 9
���� �����  + 10
����� ���� ������������  + 11
����� ���� ����� + 12
����� ��������� +- 13
����� ��������  +-  14
                 */

            }
        }
    }
}