namespace nccgle_program
{
    /// <summary>
    /// Структура для передачи данных 
    /// для упрощения последующего отображения
    /// </summary>
    public struct ManyMatrixToShow
    {
        /// <summary>
        /// А что это? ))
        /// </summary>
        public Matrix KoeficientUniqI;
        public Matrix KoeficientSvyaziI;

        public Matrix KolInKlaster;
        public Matrix SrInKlaster;

        /// <summary>
        ///  Собирательная способность i-го документа
        /// </summary>
        public Matrix Pi;

        /// <summary>
        /// Кластеры
        /// </summary>
        public Matrix Clusters;

        /// <summary>
        /// Дэлтьта
        /// </summary>
        public Matrix Delta;

        /// <summary>
        /// Дэльта S
        /// </summary>
        public Matrix DeltaS;

        /// <summary>
        /// Общий коэфициент уникальности 
        /// </summary>
        public double ObschKoeficientUnic;

        /// <summary>
        /// Общий коэфициент связи
        /// </summary>
        public double ObschKoeficientSvyazi;

        /// <summary>
        /// Вектор G
        /// </summary>  
        public Matrix OneAndZero;

        /// <summary>
        /// Число терминов
        /// теоретическое
        /// </summary>
        public double TerminsNumber;

        /// <summary>
        /// Число кластеров
        /// теоретическое
        /// </summary>
        public double ClustersNumber;
    }
}