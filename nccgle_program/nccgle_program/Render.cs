using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace nccgle_program
{
    public static class Render
    {
        private static List<Matrix> renderList = new List<Matrix>();

        /// <summary>
        /// Вывод матрицы
        /// </summary>
        /// <param name="message">Текстовое примечание</param>
        /// <param name="table">Куда выводить</param>
        /// <param name="m">Что выводить</param>
        /// <param name="TermNames">Физические имена терминов</param>
        private static string RenderMatrix(Matrix m)
        {
            int dx = m.DimX;
            int dy = m.DimY;
            bool square = (dx == dy);
            bool markDocsDY = (dy == Constant.TermsNumber);
            bool markDocsDX = (dx == Constant.TermsNumber);

            string page = "";
            page += "<p>" + m.Comment + "</p>";
            page += "<table border=1 width=100%>";

            if (dy > 1)
            {
                page += "<tr bgcolor=#ccffcc>";
                for (int i = 0; i < dy + 1; i++)
                {
                    // нечеловеческий "выворот". рожден в 20:27. Индийцы бы мной гордились :/
                    page += "<th>";
                    page += (markDocsDY) ? ("<a title=" + ((i > 0) ? GlobalControls.TermNames[i - 1] : "") + ">" + i.ToString() + "</a>") : i.ToString();
                    page += "</th>";
                }
            }

            for (int i = 0; i < dx; i++)
            {
                page += (i % 2 == 0) ? "<tr>" : "<tr bgcolor=#DFDFDF>";

                page += "<th bgcolor=#ccffcc>";
                page += (markDocsDX) ? ("<a title=" + GlobalControls.TermNames[i] + ">" + (i + 1).ToString() + "</a>") : (i + 1).ToString();
                page += "</th>";

                for (int j = 0; j < dy; j++)
                {
                    page += "<td align=center";
                    page += ((i == j) && square) ? " bgcolor=#cccccc>" : ">";
                    page += Math.Round(m[i, j], Constant.RoundSymbolsCountInRender) + "</td>";
                }
                page += "</tr>";
            }
            page += "</table>";

            return page;
        }

        public static void AddToRenderList(Matrix m)
        {
            renderList.Add(m);
        }

        public static void AddToRenderList(List<Matrix> m)
        {
            renderList.AddRange(m);
        }

        public static void DoOne(Matrix m)
        {
            GlobalControls.table.DocumentText = RenderMatrix(m);
        }

        public static void DoList()
        {
            if (GlobalControls.table != null)
            {
                string res = "";
                foreach (Matrix item in renderList)
                {
                    res += RenderMatrix(item);
                }
                GlobalControls.table.DocumentText = res;
                renderList.Clear();
            }
            else
                MessageBox.Show("wb = null");
        }

        public static void RenderTree(ClusterBase cb)
        {
            string page = "";

            //int depth = 0; //todo: 
            foreach (ClusterBase.Cluster item in cb.Tree)
            {
                page += "<p>" + item.num + " ";
                for (int i = 0; i < item.g.DimX; i++)
                {
                    page += item.g[i].ToString();
                }
                page += "</p>";
            }

            GlobalControls.table.DocumentText = page;
        }
    }
}
