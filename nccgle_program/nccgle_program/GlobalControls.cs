using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace nccgle_program
{
    public static class GlobalControls
    {
        public static WebBrowser table;
        /// <summary>
        /// Прогресс бар
        /// </summary>
        public static ToolStripProgressBar progress;
        /// <summary>
        /// Вроде статус бар...
        /// </summary>
        public static ToolStripStatusLabel log_bar;
        public static TextBox debugOutputScreen;


        public static List<string> TermNames;
    }
}
