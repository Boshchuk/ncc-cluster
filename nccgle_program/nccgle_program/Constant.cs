using System;
using System.Collections.Generic;
using System.Text;

namespace nccgle_program
{
    public static class Constant
    {
        /// <summary>
        /// ����� ���������� � �������� �������� �������� ���������
        /// ��������, �� ���� ��� ����� ������� ������� ������
        /// </summary>
        // ����� TermsNumber < Documents number
        public static int DocumentsNumber = 61;  //61;
        public static int TermsNumber = 41; // 41
        public static string PathToDictionary = "docs/slovar.rtf";
        public static string PathToDocuments = "docs/";
        public static string DocumentsFileExtension = ".rtf";

        /// <summary>
        /// ���������� ����� ��� �������
        /// </summary>
        public static int RoundSymbolsCount = 3;

        /// <summary>
        /// ���������� ����� ��� ������ �� �� �����
        /// </summary>
        public static int RoundSymbolsCountInRender = 4;

        public static double alpha = 0.89;
    }
}
