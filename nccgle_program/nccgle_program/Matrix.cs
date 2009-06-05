using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace nccgle_program
{
    /// <summary>
    /// ������������� ���������� ������� ��������� ���� double
    /// ��� ��� ������� ���� ��������
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// ����������� �������
        /// </summary>
        private int dimX, dimY;

        /// <summary>
        /// ���������� ���� �������
        /// </summary>
        private double[,] body;

        private string comment;
        /// <summary>
        /// ������������� �����. ������������ ��� ������ �������.
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
        /// �����������
        /// dimX - ����������� �
        /// dimY - ����������� y
        /// </summary>
        /// <param name="dimX">�������� ������ ���� ����� � ������ ��� 0 </param>
        /// <param name="dimY">�������� ������ ���� ����� � ������ ��� 0 </param>
        public Matrix(int dimX, int dimY)
        {
            body = new Double[dimX, dimY];
            this.dimX = dimX;
            this.dimY = dimY;
        }

        /// <summary>
        /// ����������� ��� �������
        /// </summary>
        /// <param name="dimX">����������� ������� (������ 1)</param>
        public Matrix(int dimX)
        {
            body = new Double[dimX, 1];
            this.dimX = dimX;
            this.dimY = 1;
        }

        /// <summary>
        /// ��������� ��� �������
        /// </summary>
        /// <param name="x">������� � ������� ���������� ���������. ���������� � 0</param>
        /// <returns>�������� � ��������� �������</returns>
        public double this[int x]
        {
            get
            {
                if (dimY == 1)
                {
                    return this[x, 0];
                }
                else
                    throw new ArgumentException("������-�� ��� �� ������...");
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
                    throw new ArgumentException("������-�� ��� �� ������..."); 
            }
        }

        /// <summary>
        /// ���������� ��� ����������� �� ��������� �������
        /// </summary>
        /// <param name="x">i - �� </param>
        /// <param name="y">j - ��</param>
        /// <returns>���������� ������� �������</returns>
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
        /// ������ �������, ��� ����������� i
        /// </summary>
        public int DimX
        {
            get
            {
                return dimX;
            }
        }

        /// <summary>
        /// ������ �������, ��� ���������� j
        /// </summary>
        public int DimY
        {
            get
            {
                return dimY;
            }
        }

        /// <summary>
        /// ��������� �� ����������� ������
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
            else System.Windows.Forms.MessageBox.Show("����� ������� ��� ���������� ��������.");

            for (int i = 0; i < dimX; i++)
            {
                body[i,0] = (double)ht.GetKey(i);
            }
        }

        /// <summary>
        /// ��������� ������1 � ���������� ������2 �������� �� ��������� �������1 ��� ��� ������������� ���������
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

                // ����������
                int i = how;
                int j = row.Count - 1;
                while (i > 0)
                {
                    // ����� � �����
                    ArrayList tmp = (ArrayList)row.GetByIndex(j);

                    if (tmp != null)
                    {           // ���� ������ �� ���������� � ��� ���� �����
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
                System.Windows.Forms.MessageBox.Show("����� ������� ��� ���������� ��������.");
                return null;
            }
        }

        /// <summary>
        /// ��������� ������� �������� ����������
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
        /// ��������� ������� ������-���������� ������� ���� double
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
        /// ����������� ������� � ���������� ��������
        /// ( ����������� ������)
        /// </summary>
        /// <param name="byWhat">�������, ����� ������� ����� ������� </param>
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