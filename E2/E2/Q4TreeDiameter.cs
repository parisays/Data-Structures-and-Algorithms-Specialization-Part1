using System;
using System.Collections.Generic;
using System.Linq;

namespace E2
{
    public class Q4TreeDiameter
    {
        /// <summary>
        /// ریشه همیشه نود صفر است.
        ///توی این آرایه در مکان صفر لیستی از بچه های ریشه موجودند.
        ///و در مکانه آی از این آرایه لیست بچه های نود آیم هستند
        ///اگر لیست خالی بود، بچه ندارد
        /// </summary>
        public readonly List<int>[] Nodes;
        private List<int>[] NewNodes;

        public Q4TreeDiameter(int nodeCount, int seed = 0)
        {
            Nodes = GenerateRandomTree(size: nodeCount, seed: seed);
            
        }

        public int TreeHeight() => SimpleTreeHeight(0);

        private int SimpleTreeHeight(int node)
        {
            int[] answers = new int[Nodes[node].Count];
            int i = 0;
            foreach (int childIndex in Nodes[node])
            {
                if (Nodes[childIndex].Count == 0)
                    answers[i] = 1;
                else
                    answers[i] += SimpleTreeHeight(childIndex);
                i++;
            }

            return answers.Max() + 1;
        }

        public int TreeHeightFromNode(int node)
        {
            ChangeTree(node);
            int height = ModifiedTreeHeight(node);
            return height;
        }

        private int ModifiedTreeHeight(int node)
        {
            int[] answers = new int[NewNodes[node].Count];
            int i = 0;
            foreach (int childIndex in NewNodes[node])
            {
                if (NewNodes[childIndex].Count == 0)
                    answers[i] = 1;
                else
                    answers[i] += ModifiedTreeHeight(childIndex);
                i++;
            }

            return (answers.Max() + 1);
        }

        private void ChangeTree(int node)
        {
            NewNodes = new List<int>[Nodes.Length];
            
            for(int i=0; i<Nodes.Length; i++)
            {
                NewNodes[i] = new List<int>();
                NewNodes[i].AddRange(Nodes[i].GetRange(0,Nodes[i].Count));
            }

            while(node != 0)
            {
                for (int i = 0; i < NewNodes.Length; i++)
                    if (NewNodes[i].Contains(node))
                    {
                        NewNodes[i].Remove(node);
                        NewNodes[node].Add(i);
                        node = i;
                        break;
                    }

            }
            
        }

        public int TreeDiameterN2()
        {
            List<int> result = new List<int>();
            for(int i = 0; i < Nodes.Length; i++)
            {
                result.Add(TreeHeightFromNode(i));
            }

            return result.Max();
        }

        public int TreeDiameterN()
        {
            return 0;
        }

        private static List<int>[] GenerateRandomTree(int size, int seed)
        {
            Random rnd = new Random(seed);
            List<int>[] nodes = Enumerable.Range(0, size)
                .Select(n => new List<int>())
                .ToArray();
            
            List<int> orphans = 
                new List<int>(Enumerable.Range(1, size-1)); // 0 is root it will remain orphan
            Queue<int> parentsQ = new Queue<int>();
            parentsQ.Enqueue(0);
            while (orphans.Count > 0)
            {
                int parent = parentsQ.Dequeue();
                int childCount = rnd.Next(1, 4);
                for (int i=0; i< Math.Min(childCount, orphans.Count); i++)
                {
                    int orphanIdx = rnd.Next(0, orphans.Count-1);
                    int orphan = orphans[orphanIdx];
                    orphans.RemoveAt(orphanIdx);
                    nodes[parent].Add(orphan);
                    parentsQ.Enqueue(orphan);
                }
            }
            return nodes;
        }
    }
}