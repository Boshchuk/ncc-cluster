using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace nccgle_program
{
    public static class JustDoIt
    {
        public static ToolStripProgressBar progress;
        public static ToolStripStatusLabel log_bar;

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
                            tmp = tmp + (m1.GetElement(g, i) * m2.GetElement(j, g));
                        }
                        m3.SetElement(i, j, tmp);
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
                    result.SetElement(j, i, m.GetElement(i, j));
                }
            return result;
        }

        public static void RenderMatrix(string message, WebBrowser table, Matrix m)
        {
            string page = "";
            int dx = m.DimX;
            int dy = m.DimY;
            page += "<p>" + message + "</p>";
            page += "<table border=1 width=100%>";

            for (int i = 0; i < dx; i++)
            {
                if (i % 2 == 0){ page += "<tr>";} // 
                else { page += "<tr bgcolor=#cccccc>"; } // ������ ������� ������� �����

                for (int j = 0; j < dy; j++)
                {   
                    page += "<td align=center>" + Math.Round(m.GetElement(i, j), 4) + "</td>";
                }
                page += "</tr>";
            }
            page += "</table>";

            table.DocumentText = page;
        }

        public static void ForMatrixD(Matrix m) // ���������� ������� D (������ ��������� ����������)
        {   // ��� ��� ��������
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

                        if (Doc.Find(template) != -1)
                        {
                            m.SetElement(i, j, 1);
                        }
                        else m.SetElement(i, j, 0);
                    }
                }

                progress.PerformStep();
                Doc.Clear();
            }
            progress.Value = 0;
        }

        public static void ForMatrixS(Matrix d, Matrix s)
        {           
            double[] summa = new double[Constant.TermsNumber];

            for (int j = 0; j < Constant.TermsNumber; j++)
            {
                for (int i = 0; i < Constant.DocumentsNumber; i++)
                {
                    summa[j] += d.GetElement(i,j);
                }
            }
            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    s.SetElement(i,j, d.GetElement(i,j) / summa[j] );
                }
            }
        }

        public static void ForMatrixS_Shtrih(Matrix d, Matrix s)
        {
            double[] summa = new double[Constant.DocumentsNumber];

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    summa[i] += d.GetElement(i, j);
                }
            }

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    s.SetElement(i, j, d.GetElement(i, j) / summa[i]);
                }
            }
        }

        public static void Coeff(Matrix d, Matrix c, Matrix c_shtrih)
        {
            // ������� ����������� ������������
            Matrix koef_uniq_i = new Matrix(Constant.DocumentsNumber, 1);
            // ����� ���������� ������������
            double koef_uniq_obschiy = 0;

            // ������� ����������� ����� 
            Matrix koef_svyazi_i = new Matrix(Constant.DocumentsNumber, 1);
            // ����� ���������� �����
            double koef_svyazi_obschiy = 0;
            

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                koef_uniq_i.SetElement( i,0, c.GetElement(i, i));
                koef_uniq_obschiy += koef_uniq_i.GetElement(i,0);

                koef_svyazi_i.SetElement(i, i, 1 - koef_uniq_i.GetElement(i,0));
                koef_svyazi_obschiy += koef_svyazi_i.GetElement(i,0);
            }
            //������������� �����������

            int[] t = new int[Constant.DocumentsNumber];
            
            //Pi
            //double[] p = new double[Constant.DocumentsNumber];
            Matrix p = new Matrix(Constant.DocumentsNumber,0);

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    t[i] += (int)  d.GetElement(i,j); //����� ���
                }
                p.SetElement (i,0, koef_uniq_i.GetElement(i,0) * koef_svyazi_i.GetElement(i, i) * t[i]);
            }

            koef_uniq_obschiy = koef_uniq_obschiy / Constant.DocumentsNumber;
            koef_svyazi_obschiy = koef_svyazi_obschiy / Constant.DocumentsNumber;

            //DeltaS
            double[] bus = new double[Constant.TermsNumber];

            //����� ���������� ������������
            double Bus = 0;
            //���������� �����
            double Fis = 0;

            double[] fis = new double[Constant.TermsNumber];
            

            for (int i = 0; i < Constant.TermsNumber; i++)
            {
                bus[i] = c_shtrih.GetElement(i, i);
                Bus += bus[i];
                fis[i] = 1 - bus[i];
                Fis += fis[i];
            }
            Bus = Math.Round(Bus / Constant.TermsNumber, 3);
            Fis = Math.Round(Fis / Constant.TermsNumber, 3);

            //*������������� ����� ���������*

            //����� ���������
            double nuc = 0;
            for (int i = 0; i < Constant.DocumentsNumber; i++)
                nuc += Math.Round(koef_uniq_i.GetElement(i,0), 3);
            //����� ��������
            double nc = Math.Round(Constant.DocumentsNumber / nuc, 3);

            //���������� ����������

            int nuc_ = Convert.ToInt32(nuc);

            //�� ���� ������ �������, ���� n>6
            Matrix cent1 = new Matrix( nuc_,0);


            Matrix tmp = new Matrix( p);
            for (int i = 0; i < nuc_; i++)
            {
                double max = tmp.GetElement(0,0);
                int maxIndex = 0;
                for (int j = 0; j < Constant.DocumentsNumber; j++)
                {
                    if (tmp.GetElement( j,0) > max)
                    {
                        max = tmp.GetElement( j,0);
                        maxIndex = j;
                    }
                }
                cent1.SetElement(i, 0, maxIndex);
                tmp.SetElement(maxIndex,0, -1);
                maxIndex = 0;
            }


          //  for (int n = 0; n < Constant.DocumentsNumber; n++)
          //  {
          //      iRow = Others.NewRow();
          //      iRow[DeltaCol1] = Math.Round(bu[n], 3);
            //     iRow[PCol] = Math.Round(p[n], 5);

            #region Gipotyza
            // ����� ������ ������ �������� ������ S?
            int n=0;
            //..���� ������������� ,��� �� ������ ����
                if (n < 26) {
              //      iRow[Delta_Col] = Math.Round(bus[n], 3); 
                }
            //

            //
             //   if (n < 6) { iRow[Klasters] = cent1[n]; }
             //   if (n < 1)
             //   {
            //        iRow[KfUnic1] = Math.Round(Bu, 3);
            //        iRow[KfSvyaz1] = Math.Round(Fi, 3);
            //        iRow[KfUnic2] = Math.Round(Bus, 3);
            //        iRow[KfSvyaz2] = Math.Round(Fis, 3);
            //        iRow[KolKlasters] = Math.Round(nuc, 3);
            //        iRow[KolTerms] = Math.Round(nc, 3);
            //    }
            //    Others.Rows.Add(iRow);
            #endregion
            /* dataGridView6.DataSource = Others;
            */
            //////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////
            
            //��������� ����� ��� ������...
            int NekaInt = 6;// ��� � ������
 
            Matrix MaxInRow = new Matrix(Constant.DocumentsNumber, NekaInt);
            double MaxValue;
            int MaxNumber;
            /*for (int i = 0; i < NekaInt; i++)
            {
                iCol = new DataColumn(cent1[i].ToString());
                DocsKlasters.Columns.Add(iCol);
            }*/

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                MaxValue = 0;
                MaxNumber = 0;
                //iRow = DocsKlasters.NewRow();
                for (int j = 0; j < NekaInt; j++)
                {
                    if (c.GetElement((int) cent1.GetElement(j,0), i) > MaxValue)
                    {
                        MaxValue = c.GetElement((int) cent1.GetElement (j,0), i);
                        MaxNumber = j;
                    }
                    if (c.GetElement( (int)cent1.GetElement(j,0), i) == MaxValue)
                    {
                        if (p.GetElement(i,0) > p.GetElement(MaxNumber,0))
                        {
                            MaxValue = c.GetElement((int)cent1.GetElement(j,0), i);
                            MaxNumber = j;
                        }
                    }
                }

                for (int k = 0; k < 6; k++)
                {
                    if (k == MaxNumber)
                    { MaxInRow.SetElement(i, k, 1); }
                    else
                    { MaxInRow.SetElement(i, k ,0); }
                    //iRow[cent1[k].ToString()] = MaxInRow.GetElement(i, k);
                }
                //DocsKlasters.Rows.Add(iRow);
            }
            //dataGridView7.DataSource = DocsKlasters;
            
             
            //////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////

            //�������� � ��
            Matrix KolInKlaster = new Matrix(26, 6);
            int[] tempmas = new int[49];
            
            //sr
            Matrix SrInKlaster = new Matrix(Constant.DocumentsNumber,0);
            int iCount;
            int KolKlaster;
            double TermDocs;
            double m;


          
//            iCol = new DataColumn("SR");
//            Centroid.Columns.Add(iCol);
            for (int i = 0; i < 26; i++)
            {


               // iRow = Centroid.NewRow();
                TermDocs = 0;
                m = 0;
                for (int h = 0; h < 6; h++)
                {
                    iCount = 0;
                    KolKlaster = 0;
                    for (int j = 0; j < 49; j++)
                    {
                        if (d.GetElement(j, i) == 1)
                        {
                            tempmas[iCount] = j;
                            iCount++;
                        }
                    }
                    for (int r = 0; r < iCount; r++)
                    {
                        if (MaxInRow.GetElement( tempmas[r], h) == 1) KolKlaster++;
                        tempmas[r] = 0;
                    }
                    if (KolKlaster > 0) m++;
                    KolInKlaster.SetElement(i, h, KolKlaster);
                   // iRow[cent1[h].ToString()] = KolKlaster;
                    TermDocs += KolKlaster;
                }
                SrInKlaster.SetElement(i,0, TermDocs / m);
               // iRow["SR"] = Math.Round(SrInKlaster[i], 3);
               // Centroid.Rows.Add(iRow);
            }
    
          //  dataGridView8.DataSource = Centroid;
            //////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////

          //  G
            Matrix OneAndZero = new Matrix(26, 6);
            double NumberOne;
            double NumberTwo;
            for (int i = 0; i < 6; i++)
            {
                //iCol = new DataColumn(cent1[i].ToString());
                //OneOrZero.Columns.Add(iCol);
            }

            for (int i = 0; i < 26; i++)
            {
                //iRow = OneOrZero.NewRow();
                for (int j = 0; j < 6; j++)
                {
                    NumberOne = KolInKlaster.GetElement(i, j) * bus[i];
                    NumberTwo = Bus * 0.5 * SrInKlaster.GetElement(i,0);
                    if (NumberOne > NumberTwo) OneAndZero.SetElement(i, j, 1);
                    else OneAndZero.SetElement(i, j , 0);
                   // iRow[cent1[j].ToString()] = OneAndZero.GetElement(i, j0;
                }
                //OneOrZero.Rows.Add(iRow);
            }
 
           // dataGridView9.DataSource = OneOrZero;
           
        }
    }
}
