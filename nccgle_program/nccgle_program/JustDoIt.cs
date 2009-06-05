using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace nccgle_program
{
    public static class JustDoIt
    {
        /// <summary>
        /// �������� ���
        /// </summary>
        public static ToolStripProgressBar progress;
        /// <summary>
        /// ����� ������ ���...
        /// </summary>
        public static ToolStripStatusLabel log_bar;

        public static List<string> TermNames;

        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <param name="m1">������� ����</param>
        /// <param name="m2">������� ���</param>
        /// <returns>��������� ��������� ������</returns>
        public static Matrix MultiplyMatrix(Matrix m1, Matrix m2)
        {
            if (m1.DimX == m2.DimY)
            {
                int m1x = m1.DimX;
                int m1y = m1.DimY;

                int m2x = m2.DimX;
                int m2y = m2.DimY;

                Matrix m3 = new Matrix(m1y, m2x);

                double tmp = 0;
                for (int i = 0; i < m1y; i++)
                    for (int j = 0; j < m2x; j++)
                    {
                        for (int g = 0; g < m1x; g++)
                        {
                            tmp = tmp + (m1[g, i] * m2[j, g]);
                        }
                        m3[i, j] = tmp;
                        tmp = 0;
                    }

                return m3;
            }
            else return null;
        }


        public static Matrix Transposition(Matrix m)
        {
            Matrix result = new Matrix(m.DimY, m.DimX);
            for (int i = 0; i < m.DimX; i++)
                for (int j = 0; j < m.DimY; j++)
                {
                    result[j, i] = m[i, j];
                }
            return result;
        }

        /// <summary>
        /// ����� �������
        /// </summary>
        /// <param name="message">��������� ����������</param>
        /// <param name="table">���� ��������</param>
        /// <param name="m">��� ��������</param>
        /// <param name="TermNames">���������� ����� ��������</param>
        public static void RenderMatrix(string message, WebBrowser table, Matrix m)
        {
            int dx = m.DimX;
            int dy = m.DimY;
            bool square = (dx == dy);
            bool markDocsDY = (dy == Constant.TermsNumber);
            bool markDocsDX = (dx == Constant.TermsNumber);

            string page = "";
            page += "<p>" + message + "</p>";
            page += "<table border=1 width=100%>";

            if (dy > 1)
            {
                page += "<tr bgcolor=#ccffcc>";
                for (int i = 0; i < dy + 1; i++)
                {
                    // �������������� "�������". ������ � 20:27. ������� �� ���� ��������� :/
                    page += "<th>";
                    page += (markDocsDY) ? ("<a title=" + ((i > 0) ? TermNames[i - 1] : "") + ">" + i.ToString() + "</a>") : i.ToString();
                    page += "</th>";                        
                }
            }

            for (int i = 0; i < dx; i++)
            {
                page += (i % 2 == 0) ? "<tr>" : "<tr bgcolor=#DFDFDF>";

                page += "<th bgcolor=#ccffcc>";
                page += (markDocsDX) ? ("<a title=" + TermNames[i] + ">" + (i+1).ToString() + "</a>") : (i+1).ToString();
                page += "</th>";

                for (int j = 0; j < dy; j++)
                {
                    page += "<td align=center";
                    page += ((i == j) && square) ? " bgcolor=#cccccc>" : ">";
                    page += Math.Round(m[i, j], Constant.RoundSymbolsCountInRender) + "</td>";
                }
                page += "</tr>";
            }
            page += "</table>";

            table.DocumentText = page;
        }
        /// <summary>
        /// ����� ������������� ��������
        /// </summary>
        /// <param name="message">��������� ����������</param>
        /// <param name="table">���� ��������</param>
        /// <param name="element">��� ��������</param>
        public static void RenderMatrix(string message, WebBrowser table, double element)
        {
            string page = "";
            page += "<p>" + message + "</p>";
            page += "<table border=1 width=100%>";
            page += "<td align=center>" + element.ToString();
            page += "</tr>";
            page += "</table>";
            table.DocumentText = page;
        }

        public static void RenderTree(WebBrowser table, ClusterBase cb)
        {
            string page = "";

            //int depth = 0; //todo: 
            foreach (ClusterBase.Cluster item in cb.Tree)
            {
                page += "<p>" + item.num + " ";
                for (int i = 0; i < item.g.DimX; i++)
                {
                    page += item.g[i].ToString();
                }
                page += "</p>";
            }

            table.DocumentText = page;
        }

        public static void FillTermsName()
        {            
            RichTextBox Slovar = new RichTextBox();
            Slovar.LoadFile(Constant.PathToDictionary);
            TermNames = new List<string>();

            for (int i = 0; i < Constant.TermsNumber; i++)
            {
                TermNames.Add(Slovar.Lines[i]);
            }
        }

        /// <summary>
        /// ����� ������ ���������� ������� D
        /// (������ ��������� ����������)
        /// </summary>
        /// <param name="m">������ �� ������� ��� ����������</param>
        public static void ForMatrixD(Matrix m) // ���������� ������� D (������ ��������� ����������)
        {
            FillTermsName();
            RichTextBox Slovar = new RichTextBox();
            RichTextBox Doc = new RichTextBox();

            progress.Minimum = 0;
            progress.Maximum = Constant.DocumentsNumber;
            progress.Step = 1;

            Slovar.LoadFile(Constant.PathToDictionary);
            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {

                Doc.LoadFile(Constant.PathToDocuments + (i + 1).ToString() + Constant.DocumentsFileExtension);
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    string text = Slovar.Lines[j];
                    int current_position = 0;

                    while (text.Length != 0)
                    {
                        current_position = text.IndexOf("/");
                        string template = text.Substring(0, current_position);
                        text = text.Remove(0, current_position + 1);

                        m[i, j] = (Doc.Find(template) != -1) ? 1 : 0;
                    }
                }

                progress.PerformStep();
                Doc.Clear();
            }

            progress.Value = 0;


        }

        //private static string ChoseFile()
        //{
        //    OpenFileDialog dialog = new OpenFileDialog();
        //    dialog.InitialDirectory = Application.ExecutablePath;
        //    dialog.Filter = "rtf files only(*.rtf)|*.rtf";
        //    dialog.ShowDialog();
        //    return (dialog.FileName);
        //}

        /// <summary>
        /// ���������� ������� ���������� �������� ��� ������� ���������
        /// </summary>
        /// <param name="d">������� �������� ����������</param>
        /// <param name="s">������� ���������� �������� (����������� � ��������)</param>
        public static void ForMatrixS(Matrix d, Matrix s)
        {
            double[] summa = new double[Constant.TermsNumber];

            for (int j = 0; j < Constant.TermsNumber; j++)
            {
                for (int i = 0; i < Constant.DocumentsNumber; i++)
                {
                    summa[j] += d[i, j];
                }
            }
            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    s[i, j] = d[i, j] / summa[j];
                }
            }
        }

        /// <summary>
        /// ���������� ������� ���������� ���������� ��� ������� �������
        /// </summary>
        /// <param name="d">������� �������� ����������</param>
        /// <param name="s">������� ���������� ����������</param>
        public static void ForMatrixS_Shtrih(Matrix d, Matrix s)
        {
            double[] summa = new double[Constant.DocumentsNumber];

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    summa[i] += d[i, j];
                }
            }

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    s[i, j] = d[i, j] / summa[i];
                }
            }
        }
        /// <summary>
        /// ������� ��� ��� �������������� ������������
        /// </summary>
        /// <param name="d">������� �������� ����������</param>
        /// <param name="c">������� ������������� ���������� ����������</param>
        /// <param name="c_shtrih">������� ������������� ���������� ��������</param>
        /// <returns>�� ������ ��������� � ������������ ������</returns>
        public static ManyMatrixToShow CalculateCoeff(Matrix d, Matrix c, Matrix c_shtrih)
        {
            // ������� ����������� ������������ (��� ����������)
            Matrix koef_uniq_i = new Matrix(Constant.DocumentsNumber);

            // ����� ���������� ������������ 
            double koef_uniq_obschiy = 0;

            // ������� ����������� ����� (��� ����������)
            Matrix koef_svyazi_i = new Matrix(Constant.DocumentsNumber);

            // ����� ���������� �����
            double koef_svyazi_obschiy = 0;
            
            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                koef_uniq_i[i] = c[i, i];
                koef_uniq_obschiy += koef_uniq_i[i];

                koef_svyazi_i[i] = 1 - koef_uniq_i[i];
                koef_svyazi_obschiy += koef_svyazi_i[i];
            }

            koef_uniq_obschiy = koef_uniq_obschiy / Constant.DocumentsNumber;
            koef_svyazi_obschiy = koef_svyazi_obschiy / Constant.DocumentsNumber;

            // ������ �� (������������� ����������� ������� ���������)
            Matrix p = new Matrix(Constant.DocumentsNumber);

            // t[i] - (��������������� �� ����) ��� ������������ ���������� p. �������� ���������� �������� � ������ ����
            int[] t = new int[Constant.DocumentsNumber];

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    t[i] += (int)d[i, j];
                }
                p[i] = koef_uniq_i[i] * koef_svyazi_i[i] * t[i];
            }
            

            //������: ������ � ��� � ������� �� ���� ������������� ����������� ��� ������� �� ����������. �� ����� �� ����� ������� nu_c ����� - ��� ����� ������ ��� ���������.

            // ������� ����������� ������������ (��� ��������)
            Matrix koef_uniq_j_shtrih = new Matrix(Constant.TermsNumber);

            // ����� ���������� ������������ (�� �������)
            double koef_uniq_obschiy_shtrih = 0;

            // ������� ����������� ����� (��� ��������)
            Matrix koef_svyazi_j_shtrih = new Matrix(Constant.TermsNumber);

            // ����� ���������� ����� (�� �������)
            double koef_svyazi_obschiy_shtrih = 0;

            for (int j = 0; j < Constant.TermsNumber; j++)
            {
                koef_uniq_j_shtrih[j, 0] = c_shtrih[j, j];
                koef_uniq_obschiy_shtrih += koef_uniq_j_shtrih[j];

                koef_svyazi_j_shtrih[j, 0] = 1 - koef_uniq_j_shtrih[j];
                koef_svyazi_obschiy_shtrih += koef_svyazi_j_shtrih[j];
            }

            koef_uniq_obschiy_shtrih = koef_uniq_obschiy_shtrih / Constant.TermsNumber;
            koef_svyazi_obschiy_shtrih = koef_svyazi_obschiy_shtrih / Constant.TermsNumber;
//---------------------------------------------------------------------------------------------
            // ������������� ����� ���������
            double Nu_C = koef_uniq_obschiy * Constant.DocumentsNumber;
            //double Nu_C_tmp = Math.Round(Nu_C);
            //Nu_C = (Nu_C - Nu_C_tmp) > 0 ? Nu_C_tmp + 1 : Nu_C_tmp;
            
            // ����� ���������� � ��������
            double M_C = Math.Round(1 / koef_uniq_obschiy);
            //double M_C_tmp = Math.Round(M_C);
            //M_C = (M_C - M_C_tmp) > 0 ? M_C_tmp + 1 : M_C_tmp;

//----------------------------------------------------------------
            ManyMatrixToShow ForReturn = new ManyMatrixToShow();
            ForReturn.koef_svyazi_i = koef_svyazi_i;
            ForReturn.koef_svyazi_j_shtrih = koef_svyazi_j_shtrih;
            ForReturn.koef_svyazi_obschiy = koef_svyazi_obschiy;
            ForReturn.koef_svyazi_obschiy_shtrih = koef_svyazi_obschiy_shtrih;
            ForReturn.koef_uniq_i = koef_uniq_i;
            ForReturn.koef_uniq_j_shtrih = koef_uniq_j_shtrih;
            ForReturn.koef_uniq_obschiy = koef_uniq_obschiy;
            ForReturn.koef_uniq_obschiy_shtrih = koef_uniq_obschiy_shtrih;
            ForReturn.Nu_C = Nu_C;
            ForReturn.M_C = M_C;
            ForReturn.p = p;

            return ForReturn;
        }

        public static ClusterBase ClusterBuilder(Matrix d, Matrix s, Matrix s_shtih, Matrix c, Matrix c_shtrih, ManyMatrixToShow coefSet)
        {
            ClusterBase cb = new ClusterBase();

            // �������� ������ �� ��_�� ��������
            List<int> tmp = coefSet.p.vSortAndGetIndexes((int)coefSet.Nu_C);

            cb.SetKernels(tmp); // ������� Nu_C ���������
            cb.SortDocs(c);
            cb.BuildCentroid(d, c_shtrih, coefSet);

            return cb;
        }
    }
}