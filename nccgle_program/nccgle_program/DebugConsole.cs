using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace nccgle_program
{
    /// <summary>
    /// Класс для отладочного вывода на форму
    /// </summary>
    public static class DebugConsole
    {
        public static TextBox screen;

        public static void Print(string message)
        {
            if (screen != null)
            {
                screen.AppendText(message + '\r' + '\n');
            }
            else
                MessageBox.Show("Не указана ссылка на объект для вывода");
        }

        public static void Print(string text ,double message)
        {
            Print(text + " " + Math.Round(message, Constant.RoundSymbolsCountInRender).ToString());
        }

        public static void Print(string text, int message)
        {
            Print(text+ " " +message.ToString());
        }
    }
}
