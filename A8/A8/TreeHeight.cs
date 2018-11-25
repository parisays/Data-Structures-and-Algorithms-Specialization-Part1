using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class TreeHeight : Processor
    {
        public TreeHeight(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long nodeCount, long[] tree)
        {
            List<long>[] nodes = new List<long>[nodeCount];

            for (int i = 0; i < nodeCount; i++)
                nodes[i] = new List<long>();

            long root = 0;
            
            for (int i = 0; i < nodeCount; i++) //build the tree
            {
                if (tree[i] == -1)
                    root = i;
                else
                    nodes[tree[i]].Add(i);
            }

            Queue<long> queue = new Queue<long>();
            queue.Enqueue(root);
            long height = 0;

            while(queue.Any()) // compute tree height
            {
                int count = queue.Count();
                for(int i=0; i<count; i++)
                {
                    long currentNode = queue.Dequeue();

                    if (nodes[currentNode] != null)
                        foreach (long n in nodes[currentNode])
                            queue.Enqueue(n);
                }

                height++;
            }

            return height;
        }
    }
}
