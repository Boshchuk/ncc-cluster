using System;
using System.Collections.Generic;
using System.Text;

namespace nccgle_program
{
    public static class Constant
    {
        /// <summary>
        /// Число документов с которыми пытается работать программа
        /// пытается, по тому что точно сказать никогда нельзя
        /// </summary>
        public static int DocumentsNumber = 15;//62;
        public static int TermsNumber = 10;//41;
        public static string PathToDictionary = "docs/slovar.rtf";
        public static string PathToDocuments = "docs/";
        public static string DocumentsFileExtension = ".rtf";
        /// <summary>
        /// Округление чисел при делении
        /// </summary>
        public static int RoundSymbolsCount = 3;
        /// <summary>
        /// Округление чисел при выводе их на экран
        /// </summary>
        public static int RoundSymbolsCountInRender = 4;
    }
}
