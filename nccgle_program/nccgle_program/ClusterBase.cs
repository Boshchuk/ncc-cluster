using System;
using System.Collections.Generic;
using System.Text;

namespace nccgle_program
{
    public class ClusterBase
    {
        public struct Cluster
        {
            public int num;
            public List<int> docs;
            public Matrix g;
        }

        public List<int> allDocs = new List<int>();
        public List<Cluster> Tree = new List<Cluster>();

        public ClusterBase()
        {
            for (int i = 0; i < Constant.DocumentsNumber; i++)
            {
                allDocs.Add(i); // создаем коллекцию из всех доков
            }
        }

        public void SetKernels(List<int> kerr)
        {
            for (int i = 0; i < kerr.Count; i++)
            {
                Cluster item = new Cluster();
                item.num = kerr[i];
                item.docs = new List<int>();
                item.g = new Matrix(Constant.TermsNumber, 1);
                Tree.Add(item);

                allDocs.Remove(kerr[i]); // убираем доки из общего множества
            }
        }

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
            foreach (Cluster item in Tree)
            {
                for (int t = 0; t < Constant.TermsNumber; t++) // по всему списку терминов
                {
                    int f; // количество вхождений термина в документы кластера
                    foreach (int doc in item.docs)
                    {
                        if (d[t,doc] == 1) f++;
                    }

                    for (int i = 0; i < item.docs.Count; i++)
                    if (
                    item.g[i,1] = 
                }
            }
        }
    }
}
