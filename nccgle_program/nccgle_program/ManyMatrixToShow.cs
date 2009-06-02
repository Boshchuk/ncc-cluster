namespace nccgle_program
{
    /// <summary>
    /// Структура для передачи данных 
    /// для упрощения последующего отображения
    /// </summary>
    public struct ManyMatrixToShow
    {

        /// <summary>
        /// частные коэфициенты уникальности (для документов)
        /// </summary> 
        public Matrix koef_uniq_i;

        /// <summary>
        /// общий коэфициент уникальности 
        /// </summary>
        public double koef_uniq_obschiy;

        /// <summary>
        /// частные коэфициенты связи (для документов) 
        /// </summary>
        public Matrix koef_svyazi_i;

        /// <summary>
        /// общий коэфициент связи 
        /// </summary>
        public double koef_svyazi_obschiy;
//--------------------------------------------------------------------

        /// <summary>
        /// частные коэфициенты уникальности (для терминов, со штрихом)
        /// </summary>
        public Matrix koef_uniq_j_shtrih;

        /// <summary>
        /// общий коэфициент уникальности (со штрихом)
        /// </summary>
        public double koef_uniq_obschiy_shtrih;

        /// <summary>
        /// частные коэфициенты связи (для терминов) 
        /// </summary>
        public Matrix koef_svyazi_j_shtrih;

        /// <summary>
        /// общий коэфициент связи (со штрихом) 
        /// </summary>
        public double koef_svyazi_obschiy_shtrih;

        /// <summary>
        /// Теоретическое число кластеров
        /// </summary>
        public double Nu_C;

        /// <summary>
        /// Число документов в кластере
        /// </summary>
        public double M_C;

        public Matrix p;
    }
}