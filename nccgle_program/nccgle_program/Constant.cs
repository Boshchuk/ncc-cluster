using System;
using System.Collections.Generic;
using System.Text;

namespace nccgle_program
{
    public static class Constant
    {
        public static int DocumentsNumber = 62;
        public static int TermsNumber = 41;
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
