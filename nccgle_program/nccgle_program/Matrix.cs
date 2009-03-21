using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace nccgle_program
{
    /// <summary>
    /// ������������� ��������� ������� ��������� ���� double
    /// ��� ��� ������� ���� ��������
    /// </summary>
    public class Matrix
    {
        private int dimX , dimY ;
        private double[,] body;
         
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
                    this.body[x, y] = (double) value;
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
                    this[i, j] =  randObj.NextDouble();
                }
        }

        public Matrix(Matrix byWhat) 
        {
            body = new Double[byWhat.DimX, byWhat.DimY];
            this.dimX = byWhat.dimX;
            this.dimY = byWhat.dimY;
            for (int i = 0; i < byWhat.DimX; i++)
            {
                for (int j = 0; j < byWhat.DimY; j++)
                {
                     this[i, j]= byWhat[i, j];   
                }
            }
        }
    }
}
