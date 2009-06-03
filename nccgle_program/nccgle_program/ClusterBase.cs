using System;
using System.Collections.Generic;
using System.Text;

namespace nccgle_program
{
    public class ClusterBase
    {   
        /// <summary>
        /// Элемент дерева кластеров (он же кластер)
        /// </summary>
        public struct Cluster
        {
            /// <summary>
            /// Документ (номер документа), являющегося ядром кластера
            /// </summary>
            public int num;
            /// <summary>
            /// Список документов, включенных в кластер
            /// </summary>
            public List<int> docs;
            /// <summary>
            /// Обобщенный образ документов, содержащихся в кластере (он же центроид)
            /// </summary>
            public Matrix g;
        }
        /// <summary>
        /// Множество всех документов
        /// </summary>
        public List<int> allDocs = new List<int>();
        /// <summary>
        /// Дерево кластеров
        /// </summary>
        public List<Cluster> Tree = new List<Cluster>();

        /// <summary>
        /// Выделяет кластера и их ядра, группирует документы по кластерам, создает дерево кластеров.
        /// </summary>
        public ClusterBase()
        {
            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                allDocs.Add(i); // создаем коллекцию из всех доков
            }
        }
        /// <summary>
        /// Из общего числа документов выделяем ядра для кластеров
        /// </summary>
        /// <param name="kerr">Множество всех документов (их номеров)</param>
        public void SetKernels(List<int> kerr)
        {
            for (int i = 0; i < kerr.Count; i++)
            {
                Cluster item = new Cluster();
                item.num = kerr[i];
                item.docs = new List<int>();
                item.g = new Matrix(Constant.TermsNumber);
                Tree.Add(item);

                allDocs.Remove(kerr[i]); // убираем доки из общего множества
            }
        }
        /// <summary>
        /// Распределяем документы по кластерам
        /// </summary>
        /// <param name="c"></param>
        public void SortDocs(Matrix c)
        {
            foreach (int doc in allDocs) // для каждого дока из оставшихся делаем:
            {
                double max = 0;
                int resIndex = 0;

                foreach (Cluster item in Tree) // для каждого кластера из дерева кластеров:
                {       // с(номер дока_ядра, конкретный док)
                    if (c[item.num, doc] >= max) 
                    {
                        max = c[item.num, doc];
                        resIndex = item.num;
                    }
                }

                // здесь мы находим нужный кластер и вставляем в него док
                foreach (Cluster item in Tree)
                {
                    if (item.num == resIndex)
                    {
                        item.docs.Add(doc);
                        break;
                    }
                }
            }
            return;
        }

        public void BuildCentroid(Matrix d, Matrix c_shtrih, ManyMatrixToShow coef)
        {
            foreach (Cluster item in Tree) // смысл цикла: построить центроиды для каждого кластера
            {
                for (int i = 0; i < Constant.TermsNumber; i++) // смысл цикла: сформировать g_i
                {
                    int f = 0; // количество вхождений i-го термина в документы кластера
                    foreach (int doc_num in item.docs)
                    {
                        if (d[doc_num, i] == 1) f++;
                    }
                    double left_part = f * coef.koef_uniq_j_shtrih[i];
                    // левая часть посчитана.

                    // считаем f_avg
                    int m=0;
                    foreach (Cluster item_tmp in Tree)
                    {
                        foreach (int j in item_tmp.docs)
                        {
                            if (d[j, i] == 1) m++;
                            break;
                        }
                    }
                    double f_avg = (m != 0) ? f / m : 0;
                    double right_part = f_avg * coef.koef_uniq_obschiy_shtrih * Constant.alpha;

                    item.g[i] = left_part > right_part ? 1 : 0;
                }
                
            }
        }
    }
}