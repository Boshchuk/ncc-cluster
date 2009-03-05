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
                            tmp = tmp + (m1[g, i] * m2[j, g]);
                        }
                        m3[i, j]= tmp;
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
                else { page += "<tr bgcolor=#cccccc>"; } // делает строчки разного цвета

                for (int j = 0; j < dy; j++)
                {   
                    page += "<td align=center>" + Math.Round(m[i, j], 4) + "</td>";
                }
                page += "</tr>";
            }
            page += "</table>";

            table.DocumentText = page;
        }
        //..кстати в  summary можно тэги юзать 

        /// <summary>
        /// Вызов метода заполнения матрицы D
        /// (модели множества документов)
        /// </summary>
        /// <param name="m"></param>
        public static void ForMatrixD(Matrix m) // заполнение матрицы D (модели множества документов)
        {   // тут был быдлокод
            RichTextBox Slovar = new RichTextBox(); 
            RichTextBox Doc = new RichTextBox();

            progress.Minimum = 0;
            progress.Maximum = Constant.DocumentsNumber;
            progress.Step = 1;

            try
            {

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
                                m[i, j] = 1;
                            }
                            else m[i, j] = 0;
                        }
                    }

                    progress.PerformStep();
                    Doc.Clear();
                }

                progress.Value = 0;
            }
            catch (System.IO.IOException IOEx)
            {
                MessageBox.Show(IOEx.Message+" Это так на будующее");
            }
        }

        public static void ForMatrixS(Matrix d, Matrix s)
        {           
            double[] summa = new double[Constant.TermsNumber];

            for (int j = 0; j < Constant.TermsNumber; j++)
            {
                for (int i = 0; i < Constant.DocumentsNumber; i++)
                {
                    summa[j] += d[i,j];
                }
            }
            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    s[i,j]= d[i,j] / summa[j] ;
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
                    summa[i] += d[i, j];
                }
            }

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    s[i, j]= d[i, j] / summa[i];
                }
            }
        }

        public static void Coeff(Matrix d, Matrix c, Matrix c_shtrih)
        {
            // частные коэфициенты уникальности
            Matrix koef_uniq_i = new Matrix(Constant.DocumentsNumber, 1);
            // общий коэфициент уникальности
            double koef_uniq_obschiy = 0;

            // частные коэфициенты связи 
            Matrix koef_svyazi_i = new Matrix(Constant.DocumentsNumber, 1);
            // общие коэфициент связи
            double koef_svyazi_obschiy = 0;
            

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                koef_uniq_i[i,0]= c[i, i];
                koef_uniq_obschiy += koef_uniq_i[i,0];

                koef_svyazi_i[i, i]= 1 - koef_uniq_i[i,0];
                koef_svyazi_obschiy += koef_svyazi_i[i,0];
            }
            //собирательная способность
            int[] t = new int[Constant.DocumentsNumber];
            
            //Pi
            //double[] p = new double[Constant.DocumentsNumber];
            Matrix p = new Matrix(Constant.DocumentsNumber,0); // 

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    t[i] += (int)  d[i,j]; //быдло код
                }
                p[i,0]= koef_uniq_i[i,0] * koef_svyazi_i[i, i] * t[i];
            }

            koef_uniq_obschiy = koef_uniq_obschiy / Constant.DocumentsNumber;
            koef_svyazi_obschiy = koef_svyazi_obschiy / Constant.DocumentsNumber;

            //DeltaS
            double[] bus = new double[Constant.TermsNumber];
            //общий коэфициент уникальности
            double Bus = 0;
            //коэфициент связи
            double Fis = 0;

            double[] fis = new double[Constant.TermsNumber];
            

            for (int i = 0; i < Constant.TermsNumber; i++)
            {
                bus[i] = c_shtrih[i, i];
                Bus += bus[i];
                fis[i] = 1 - bus[i];
                Fis += fis[i];
            }
            Bus = Math.Round(Bus / Constant.TermsNumber, 3);
            Fis = Math.Round(Fis / Constant.TermsNumber, 3);

            //*теоретическое число кластеров*

            //число кластеров
            double nuc = 0;
            for (int i = 0; i < Constant.DocumentsNumber; i++)
                nuc += Math.Round(koef_uniq_i[i,0], 3);
            //число терминов
            double nc = Math.Round(Constant.DocumentsNumber / nuc, 3);

            //построение центроидов

            int nuc_ = Convert.ToInt32(nuc);

            //из него делают кластер, если n>6
            Matrix cent1 = new Matrix( nuc_,0);


            Matrix tmp = new Matrix( p);
            for (int i = 0; i < nuc_; i++)
            {
                double max = tmp[0,0];
                int maxIndex = 0;
                for (int j = 0; j < Constant.DocumentsNumber; j++)
                {
                    if (tmp[j,0] > max)
                    {
                        max = tmp[ j,0];
                        maxIndex = j;
                    }
                }
                cent1[i, 0]= maxIndex;
                tmp[maxIndex,0]= -1;
                maxIndex = 0;
            }


          //  for (int n = 0; n < Constant.DocumentsNumber; n++)
          //  {
          //      iRow = Others.NewRow();
          //      iRow[DeltaCol1] = Math.Round(bu[n], 3);
            //     iRow[PCol] = Math.Round(p[n], 5);

            #region Gipotyza
            // вывод только первой половины Дельта S?
            int n=0;
            //..есть предположение ,что их просто мало
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
           
            //принадлеж юзать для поиска...
            int NekaInt = 6;// так у врагов
 
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
                    if (c[(int) cent1[j,0], i] > MaxValue)
                    {
                        MaxValue = c[(int) cent1[j,0], i];
                        MaxNumber = j;
                    }
                    if (c[ (int)cent1[j,0], i] == MaxValue)
                    {
                        if (p[i,0] > p[MaxNumber,0])
                        {
                            MaxValue = c[(int)cent1[j,0], i];
                            MaxNumber = j;
                        }
                    }
                }

                for (int k = 0; k < 6; k++)
                {
                    if (k == MaxNumber)
                    { 
                        MaxInRow[i, k]= 1; 
                    }
                    else
                    {
                        MaxInRow[i, k]=0; 
                    }                    
                }
                //DocsKlasters.Rows.Add(iRow);
            }
        
            
             
            //////////////////////////////////////////////////////////////////////////////////////
          

            //центроид и ср
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
                        if (d[j, i] == 1)
                        {
                            tempmas[iCount] = j;
                            iCount++;
                        }
                    }
                    for (int r = 0; r < iCount; r++)
                    {
                        if (MaxInRow[ tempmas[r], h] == 1) KolKlaster++;
                        tempmas[r] = 0;
                    }
                    if (KolKlaster > 0) m++;
                    KolInKlaster[i, h] = KolKlaster;
         
                    TermDocs += KolKlaster;
                }
                SrInKlaster[i,0]= TermDocs / m;
            }
    
         

          //  G
            Matrix OneAndZero = new Matrix(26, 6);
            double NumberOne;
            double NumberTwo;
            

            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    NumberOne = KolInKlaster[i, j] * bus[i];
                    NumberTwo = Bus * 0.5 * SrInKlaster[i,0];
                    if (NumberOne > NumberTwo) OneAndZero[i, j]= 1;
                    else OneAndZero[i, j]= 0;
                }
            }           
        }
    }
}
