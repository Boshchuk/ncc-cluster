using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nccgle_program;


namespace RExpertone
{
    public class RExpertoneService
    {
        //для матрицы размерностью 2*n
        //создаем матрицу с частотами 2*11 (можно юзать как авто генерируемую ... и т.д.)
        // по скольку идея общая, то общее делаем частным 
        // статичность потом можно убрать
        /// <summary>
        /// Строит матрицу частот
        /// </summary>
        /// <param name="whereDoing">Матрица для которой считаем частоты границ</param>
        /// <param name="n">количество экспертов(просто на всякий случай)</param>
        /// <returns>объект Matrix - матрица в которой лежат все частоты</returns>
        public static Matrix Rfrequency(Matrix whereDoing, int n)
        {
            Matrix answer = new Matrix(2, 11);

            int index;

            for (int r = 0; r < 2; r++)
            {
                for (int i = 0; i < n; i++)
                {
                    index = (int)whereDoing[r, i] * 10;

                    answer[r, index] = answer[r, index] + 1;
                }
            }

            return answer;
        }
        /// <summary>
        /// Возвращение накопленной частоты
        /// </summary>
        /// <param name="frequency">Матрица частот</param>
        /// <returns>Матрица накопленных частот</returns>
        public Matrix ChangeForSummFrequency(Matrix frequency)
        {
            Matrix answer = new Matrix(2, 11);
                      
            double stack = 0;

            for (int r = 0; r < 2; r++)
            {
                for (int i = 11; i > 0; i--)
                {
                   answer[r, i] = frequency[r, i] + stack;
                   stack = answer[r, i];
                }
                stack = 0;
            }
            return answer;
        }

        //Строим относительную частоту Sji=Kji^/n
        public static Matrix MakeRelativeFrequency(Matrix Summfrequency,int n)
        {
            Matrix answer = new Matrix(2, 11);
            
            for (int r = 0; r < 2; r++)
            {
                for (int i = 0; i < 11; i++)
                    
                {
                    answer[r, i] = Summfrequency[r, i]/n ;                    
                }                
            }
            return answer;
        }

        public static Matrix MakeIntervalView(Matrix RelativeFrequency, double delta,double t1)
        {
            Matrix answer = new Matrix(2, 11);

            for (int r = 0; r < 2; r++)
            {
                for (int i = 0; i < 11; i++)
                {
                    answer[r, i] = t1+ RelativeFrequency[r, i] * delta;
                }
            }
            return answer;
        }
    }
}
