using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace nccgle_program
{
    /// <summary>
    /// »нкапсулирует двухмерную матрицу элементов типа double
    /// тут еще рекламы надо записать
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// размерность матрицы
        /// </summary>
        private int dimX, dimY;

        /// <summary>
        /// собственно тело матрицы
        /// </summary>
        private double[,] body;

        private string comment;
        /// <summary>
        /// ѕо€снительный текст. »спользуетс€ при выводе матрицы.
        /// </summary>
        public string Comment
        {
            set
            {
                comment = value;
            }
            get
            {
                return comment;
            }
        }

        /// <summary>
        ///  онструктор
        /// dimX - размерность х
        /// dimY - размерность y
        /// </summary>
        /// <param name="dimX">«начение должно быть целым и больше чем 0 </param>
        /// <param name="dimY">«начение должно быть целым и больше чем 0 </param>
        public Matrix(int dimX, int dimY)
        {
            body = new Double[dimX, dimY];
            this.dimX = dimX;
            this.dimY = dimY;
        }

        /// <summary>
        ///  онструктор дл€ вектора
        /// </summary>
        /// <param name="dimX">–азмерность вектора (больше 1)</param>
        public Matrix(int dimX)
        {
            body = new Double[dimX, 1];
            this.dimX = dimX;
            this.dimY = 1;
        }

        /// <summary>
        /// »ндесатор дл€ вектора
        /// </summary>
        /// <param name="x">ѕозици€ к которой происходит обращение. »ндексаци€ с 0</param>
        /// <returns>«начение в указанной позиции</returns>
        public double this[int x]
        {
            get
            {
                if (dimY == 1)
                {
                    return this[x, 0];
                }
                else
                    throw new ArgumentException("вообще-то это не вектор...");
            }

            set
            {
                if (dimY == 1)
                {
                    if ((x >= 0) && (x < dimX))
                    {
                        this.body[x, 0] = (double)value;
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
                else
                    throw new ArgumentException("вообще-то это не вектор..."); 
            }
        }

        /// <summary>
        /// »ндексатор дл€ прохождени€ по элементам матрицы
        /// </summary>
        /// <param name="x">i - ый </param>
        /// <param name="y">j - ый</param>
        /// <returns>¬озвращает элемент матрицы</returns>
        public double this[int x, int y]
        {
            get
            {
                if (((x >= 0) && (y >= 0)) && ((x < dimX) && (y < dimY)))
                {
                    return this.body[x, y];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (((x >= 0) && (y >= 0)) && ((x < dimX) && (y < dimY)))
                {
                    this.body[x, y] = (double)value;
                }
            }
        }

        /// <summary>
        /// Ўирина матрицы, или размерность i
        /// </summary>
        public int DimX
        {
            get
            {
                return dimX;
            }
        }

        /// <summary>
        /// ¬ысота матрицы, или рамерность j
        /// </summary>
        public int DimY
        {
            get
            {
                return dimY;
            }
        }

        /// <summary>
        /// —ортирует по возрастанию вектор
        /// </summary>
        public void vSort()
        {
            int[] help = new int[dimX];
            SortedList ht = new SortedList();

            if (dimY == 1)
            {
                for (int index = 0; index < dimX; index++)
                {
                    ht.Add(body[index, 0], index);
                }
            }
            else System.Windows.Forms.MessageBox.Show("ћетод написан дл€ сортировки векторов.");

            for (int i = 0; i < dimX; i++)
            {
                body[i,0] = (double)ht.GetKey(i);
            }
        }

        /// <summary>
        /// —ортирует массив1 и возвращает массив2 индексов из исходного массива1 дл€ уже упор€доченных элементов
        /// </summary>
        /// <returns></returns>
        public List<int> vSortAndGetIndexes(int how)
        {
            if (dimY == 1)
            {
                List<int> result = new List<int>();

                SortedList row = new SortedList();

                for (int index = 0; index < dimX; index++)
                {
                    double item = body[index, 0];
                    if (row.ContainsKey(item))
                    {
                        ArrayList tmpColumn = (ArrayList)row[item];
                        tmpColumn.Add(index);
                    }
                    else
                    {
                        ArrayList column = new ArrayList();
                        column.Add(index);
                        row.Add(body[index, 0], column);
                    }
                }

                // извлечение
                int i = how;
                int j = row.Count - 1;
                while (i > 0)
                {
                    // берем с конца
                    ArrayList tmp = (ArrayList)row.GetByIndex(j);

                    if (tmp != null)
                    {           // пока стобец не закончилс€ и еще надо брать
                        while ((tmp.Count > 0) && (i > 0))
                        {
                            int k = (int)tmp[0];
                            result.Add(k);

                            tmp.Remove(tmp[0]);
                            i--;
                        }
                    }
                    if (j > 0) j--;
                    else { return null; }
                }
                return result;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("ћетод написан дл€ сортировки векторов.");
                return null;
            }
        }

        /// <summary>
        /// «аполн€ет матрицу нулевыми значени€ми
        /// </summary>
        public void FillZero()
        {
            for (int i = 0; i < dimX; i++)
                for (int j = 0; j < dimY; j++)
                {
                    this[i, j] = 0;
                }
        }

        /// <summary>
        /// «аполн€ет матрицу псевдо-случайными числами типа double
        /// </summary>
        public void FillRandom()
        {
            Random randObj = new Random();
            for (int i = 0; i < dimX; i++)
                for (int j = 0; j < dimY; j++)
                {
                    this[i, j] = (Double)randObj.NextDouble();
                }
        }

        /// <summary>
        ///  онструктор матрицы с параметром матрицей
        /// ( копирование короче)
        /// </summary>
        /// <param name="byWhat">ћатрица, такую которую нужно сделать </param>
        public Matrix(Matrix byWhat)
        {
            body = new Double[byWhat.DimX, byWhat.DimY];
            this.dimX = byWhat.dimX;
            this.dimY = byWhat.dimY;
            for (int i = 0; i < byWhat.DimX; i++)
            {
                for (int j = 0; j < byWhat.DimY; j++)
                {
                    this[i, j] = byWhat[i, j];
                }
            }
        }
    }
}