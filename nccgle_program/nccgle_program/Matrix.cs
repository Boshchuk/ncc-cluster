using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace nccgle_program
{
    public class Matrix
    {
        private int dimX = 0, dimY = 0;
        private double[,] body;

        public Matrix()
        {
            throw(new Exception("��� ������������ ��"));
        }
        public Matrix(int dimX, int dimY)
        {
            body = new Double[dimX, dimY];
            this.dimX = dimX;
            this.dimY = dimY;
        }

        //..���������� � get set
        public void SetElement(int x, int y, double e)
        {//..����� ������ ����������� ��������, � �� ������������... ��
            if (((x >= 0) && (y >= 0)) && ((x < dimX) && (y < dimY)))
            {
                this.body[x, y] = e;
            }
        }

        public double GetElement(int x, int y)
        {//������ ����������� � � ����� ����� ��� �� ����� ���
            if (((x >= 0) && (y >= 0)) && ((x < dimX) && (y < dimY)))
            {
                return this.body[x, y];
            }
            else return -999.999;
        }

        //..���� ����������� �������� �� ��������... ����� ��������� ����� , ��� ����� ... ��� �����
        public int DimX
        {
            get
            {
                return dimX;
            }
        }

        public int DimY
        {
            get
            {
                return dimY;
            }
        }

        public void FillZero()
        {
            for (int i = 0; i < dimX; i++)
                for (int j = 0; j < dimY; j++)
                {
                    SetElement(i, j, 0);
                }
        }

        public void FillRandom()
        {
            Random randObj = new Random();
            for (int i = 0; i < dimX; i++)
                for (int j = 0; j < dimY; j++)
                {
                    SetElement(i, j, randObj.NextDouble());
                }
        }

        public Matrix(Matrix byWhat)
        {
            for (int i = 0; i < DimX; i++)
            {
                for (int j = 0; j < DimY; j++)
                {
                    SetElement(i, j, GetElement(i, j));   
                }
            }
        }
    }
}
