using System.Collections.Generic;
using nccgle_program;
namespace nccgle_program
{
    /// <summary>
    /// Структура для передачи данных 
    /// для упрощения последующего отображения
    /// </summary>
    public class ManyMatrixToShow
    {
        private const int MATRIX_COUNT = 11;
        public List<Matrix> body;
        //private List<string> body;

        public ManyMatrixToShow()
        {
            body = new List<Matrix>(MATRIX_COUNT);            

            #region Так получилось
            /// <summary>
            /// частные коэфициенты уникальности (для документов)
            /// </summary> 
            body.Add(new Matrix(Constant.DocumentsNumber));
            body[0].Comment = "Частные коэфициенты уникальности";
            /// <summary>
            /// общий коэфициент уникальности 
            /// </summary>
            body.Add(new Matrix(1));
            body[1].Comment = "Общий коэфициент уникальности";
            /// <summary>
            /// частные коэфициенты связи (для документов) 
            /// </summary>
            body.Add(new Matrix(Constant.DocumentsNumber));
            body[2].Comment = "Частные коэфициенты связи (для документов)";
            /// <summary>
            /// общий коэфициент связи 
            /// </summary>
            body.Add(new Matrix(1));
            body[3].Comment = "Общий коэфициент связи";
            /// <summary>
            /// частные коэфициенты уникальности (для терминов, со штрихом)
            /// </summary>
            body.Add(new Matrix(Constant.TermsNumber));
            body[4].Comment = "Частные коэфициенты уникальности (для терминов, со штрихом)";
            /// <summary>
            /// общий коэфициент уникальности (со штрихом)
            /// </summary>
            body.Add(new Matrix(1));
            body[5].Comment = "Общий коэфициент уникальности (со штрихом)";
            /// <summary>
            /// частные коэфициенты связи (для терминов) 
            /// </summary>
            body.Add(new Matrix(Constant.TermsNumber));
            body[6].Comment = "Частные коэфициенты связи (для терминов) ";
            /// <summary>
            /// общий коэфициент связи (со штрихом) 
            /// </summary>
            body.Add(new Matrix(1));
            body[7].Comment = "Общий коэфициент связи (со штрихом) ";
            /// <summary>
            /// Теоретическое число кластеров
            /// </summary>
            body.Add(new Matrix(1));
            body[8].Comment = "Теоретическое число кластеров";
            /// <summary>
            /// Число документов в кластере
            /// </summary>
            body.Add(new Matrix(1));
            body[9].Comment = "Число документов в кластере";
            /// <summary>
            /// Собирательные способности документов
            /// </summary>
            body.Add(new Matrix(Constant.DocumentsNumber));
            body[10].Comment = "Собирательные способности документов";
            #endregion
        }


        #region
        /// <summary>
        /// частные коэфициенты уникальности (для документов)
        /// </summary> 
        public Matrix koef_uniq_i // 0 
        {
            get
            {
                return body[0];
            }
            set
            {
                body[0] = value;
            }
        }

        /// <summary>
        /// общий коэфициент уникальности 
        /// </summary>
        public Matrix koef_uniq_obschiy // 1
        {
            get
            {
                return body[1];
            }
            set
            {
                body[1] = value;
            }
        }

        /// <summary>
        /// частные коэфициенты связи (для документов) 
        /// </summary>
        public Matrix koef_svyazi_i // 2
        {
            get
            {
                return body[2];
            }
            set
            {
                body[2] = value;
            }
        }
        /// <summary>
        /// общий коэфициент связи 
        /// </summary>
        public Matrix koef_svyazi_obschiy // 3
        {
            get
            {
                return body[3];
            }
            set
            {
                body[3] = value;
            }
        }
        //--------------------------------------------------------------------

        /// <summary>
        /// частные коэфициенты уникальности (для терминов, со штрихом)
        /// </summary>
        public Matrix koef_uniq_j_shtrih // 4
        {
            get
            {
                return body[4];
            }
            set
            {
                body[4] = value;
            }
        }

        /// <summary>
        /// общий коэфициент уникальности (со штрихом)
        /// </summary>
        public Matrix koef_uniq_obschiy_shtrih  //5
        {
            get
            {
                return body[5];
            }
            set
            {
                body[5] = value;
            }
        }

        /// <summary>
        /// частные коэфициенты связи (для терминов) 
        /// </summary>
        public Matrix koef_svyazi_j_shtrih // 6
        {
            get
            {
                return body[6];
            }
            set
            {
                body[6] = value;
            }
        }

        /// <summary>
        /// общий коэфициент связи (со штрихом) 
        /// </summary>
        public Matrix koef_svyazi_obschiy_shtrih // 7
        {
            get
            {
                return body[7];
            }
            set
            {
                body[7] = value;
            }
        }

        /// <summary>
        /// Теоретическое число кластеров
        /// </summary>
        public Matrix Nu_C// 8 
        {
            get
            {
                return body[8];
            }
            set
            {
                body[8] = value;
            }
        }

        /// <summary>
        /// Число документов в кластере
        /// </summary>
        public Matrix M_C // 9
        {
            get
            {
                return body[9];
            }
            set
            {
                body[9] = value;
            }
        }

        public Matrix p // 10
        {
            get
            {
                return body[10];
            }
            set
            {
                body[10] = value;
            }
        }

        #endregion
    }
}
