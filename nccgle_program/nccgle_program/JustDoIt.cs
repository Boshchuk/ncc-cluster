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

        public static void RenderMatrix(string message, WebBrowser table, Matrix m)
        {
            int dx = m.DimX;
            int dy = m.DimY;
            bool square = (dx == dy);

            string page = "";
            page += "<p>" + message + "</p>";
            page += "<table border=1 width=100%>";
            for (int i = 0; i < dx; i++)
            {
                if (i % 2 == 0) { page += "<tr>"; }
                else { page += "<tr bgcolor=#cccccc>"; }

                for (int j = 0; j < dy; j++)
                {
                    if ((i == j) && square)
                    {
                        page += "<td align=center bgcolor=#aaaaaa>" + Math.Round(m[i, j], Constant.RoundSymbolsCountInRender) + "</td>";
                    }
                    else page += "<td align=center>" + Math.Round(m[i, j], Constant.RoundSymbolsCountInRender) + "</td>";
                }
                page += "</tr>";
            }
            page += "</table>";

            table.DocumentText = page;
        }

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

        /// <summary>
        /// Вызов метода заполнения матрицы D
        /// (модели множества документов)
        /// </summary>
        /// <param name="m">Ссылка на матрицу для заполнения</param>
        public static void ForMatrixD(Matrix m) // заполнение матрицы D (модели множества документов)
        {   // тут был быдлокод
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

        //private static string ChoseFile()
        //{
        //    OpenFileDialog dialog = new OpenFileDialog();

        //    dialog.InitialDirectory = Application.ExecutablePath;

        //    dialog.Filter = "rtf files only(*.rtf)|*.rtf";
        //    dialog.ShowDialog();

        //    return (dialog.FileName);

        //}

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

        public static ManyMatrixToShow CalculateCoeff(Matrix d, Matrix c, Matrix c_shtrih)
        {

            ManyMatrixToShow ForReturn = new ManyMatrixToShow();

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
                koef_uniq_i[i, 0] = c[i, i];
                koef_uniq_obschiy += koef_uniq_i[i, 0];

                koef_svyazi_i[i, 0] = 1 - koef_uniq_i[i, 0];
                koef_svyazi_obschiy += koef_svyazi_i[i, 0];
            }
            //собирательная способность
            int[] t = new int[Constant.DocumentsNumber];

            Matrix p = new Matrix(Constant.DocumentsNumber, 1); // вектор Пи

            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                for (int j = 0; j < Constant.TermsNumber; j++)
                {
                    t[i] += (int)d[i, j];
                }
                p[i, 0] = koef_uniq_i[i, 0] * koef_svyazi_i[i, 0] * t[i];
            }

            koef_uniq_obschiy = koef_uniq_obschiy / Constant.DocumentsNumber;
            koef_svyazi_obschiy = koef_svyazi_obschiy / Constant.DocumentsNumber;

            //DeltaS
            Matrix bus = new Matrix(Constant.TermsNumber, 1);

            //общий коэфициент уникальности
            double Bus = 0;
            //общий коэфициент связи
            double Fis = 0;

            Matrix fis = new Matrix(Constant.TermsNumber, 1);

            for (int i = 0; i < Constant.TermsNumber; i++)
            {
                bus[i, 0] = c_shtrih[i, i];
                Bus += bus[i, 0];
                fis[i, 0] = 1 - bus[i, 0];
                Fis += fis[i, 0];
            }
            Bus = Math.Round(Bus / Constant.TermsNumber, Constant.RoundSymbolsCount);
            Fis = Math.Round(Fis / Constant.TermsNumber, Constant.RoundSymbolsCount);

            //*теоретическое число кластеров*
            //число кластеров
            double nuc = 0;
            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                nuc += Math.Round(koef_uniq_i[i, 0], Constant.RoundSymbolsCount);
            }

            //число терминов
            double nc = Math.Round(Constant.DocumentsNumber / nuc, Constant.RoundSymbolsCount);

            //построение центроидов

            int nuc_ = Convert.ToInt32(nuc);

            //из него делают кластер, если n>6
            Matrix cent1 = new Matrix(nuc_, 1);


            Matrix tmp = new Matrix(p);
            for (int i = 0; i < nuc_; i++)
            {
                double max = tmp[0, 0];
                int maxIndex = 0;
                for (int j = 0; j < Constant.DocumentsNumber; j++)
                {
                    if (tmp[j, 0] > max)
                    {
                        max = tmp[j, 0];
                        maxIndex = j;
                    }
                }
                cent1[i, 0] = maxIndex;
                tmp[maxIndex, 0] = -1;
                maxIndex = 0;
            }

            #region Gipotyza
            // вывод только первой половины Дельта S?
            int n = 0;
            //..есть предположение ,что их просто мало
            if (n < 26)
            {
            #endregion

                //принадлеж юзать для поиска...
                int NekaInt = nuc_;    // 6;// так у врагов

                Matrix MaxInRow = new Matrix(Constant.DocumentsNumber, NekaInt);
                double MaxValue;
                int MaxNumber;

                for (int i = 0; i < Constant.DocumentsNumber; i++)
                {
                    MaxValue = 0;
                    MaxNumber = 0;
                    for (int j = 0; j < NekaInt; j++)
                    {
                        if (c[(int)cent1[j, 0], i] > MaxValue)
                        {
                            MaxValue = c[(int)cent1[j, 0], i];
                            MaxNumber = j;
                        }
                        if (c[(int)cent1[j, 0], i] == MaxValue)
                        {
                            if (p[i, 0] > p[MaxNumber, 0])
                            {
                                MaxValue = c[(int)cent1[j, 0], i];
                                MaxNumber = j;
                            }
                        }
                    }

                    for (int k = 0; k < nuc_; k++)
                    {
                        if (k == MaxNumber)
                        {
                            MaxInRow[i, k] = 1;
                        }
                        else
                        {
                            MaxInRow[i, k] = 0;
                        }
                    }
                }

                #region центроид и ср                       //26,6
                Matrix KolInKlaster = new Matrix(d.DimX, nuc_);
                int[] tempmas = new int[Constant.DocumentsNumber];

                Matrix SrInKlaster = new Matrix(Constant.DocumentsNumber, 1);
                int iCount;
                int KolKlaster;
                double TermDocs;
                double m;

                for (int i = 0; i < d.DimY; i++)
                {
                    TermDocs = 0;
                    m = 0;
                    for (int h = 0; h < nuc_; h++)
                    {
                        iCount = 0;
                        KolKlaster = 0;
                        for (int j = 0; j < d.DimX; j++)
                        {
                            if (d[j, i] == 1d)
                            {
                                tempmas[iCount] = j;
                                iCount++;
                            }
                        }
                        for (int r = 0; r < iCount; r++)
                        {
                            if (MaxInRow[tempmas[r], h] == 1) KolKlaster++;
                            tempmas[r] = 0;
                        }
                        if (KolKlaster > 0) m++;
                        KolInKlaster[i, h] = KolKlaster;

                        TermDocs += KolKlaster;
                    }
                    SrInKlaster[i, 0] = TermDocs / m;
                }
                #endregion


                #region G
                //  G
                Matrix OneAndZero = new Matrix(d.DimX, nuc_);
                double NumberOne;
                double NumberTwo;


                for (int i = 0; i < Constant.TermsNumber; i++)
                {
                    for (int j = 0; j < nuc_; j++)
                    {
                        NumberOne = KolInKlaster[i, j] * bus[i, 0];
                        NumberTwo = Bus * 0.5 * SrInKlaster[i, 0];
                        if (NumberOne > NumberTwo) OneAndZero[i, j] = 1;
                        else OneAndZero[i, j] = 0;
                    }
                }
                #endregion

                ForReturn.TerminsNumber = nc;
                ForReturn.ClustersNumber = nuc;


                ForReturn.Delta = fis;
                ForReturn.DeltaS = bus;

                ForReturn.KoeficientUniqI = koef_uniq_i;
                ForReturn.KoeficientSvyaziI = koef_svyazi_i;

                ForReturn.ObschKoeficientUnic = koef_uniq_obschiy;
                ForReturn.ObschKoeficientSvyazi = koef_svyazi_obschiy;

                ForReturn.OneAndZero = OneAndZero;
                ForReturn.Pi = p;

                ForReturn.KolInKlaster = KolInKlaster;
                ForReturn.SrInKlaster = SrInKlaster;

                ForReturn.Clusters = cent1;
            }
                return ForReturn;
            
        }
    }
}
