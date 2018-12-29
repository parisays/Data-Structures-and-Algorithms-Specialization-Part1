using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A11
{
    public class IsItBST : Processor
    {
        public IsItBST(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], bool>)Solve);


        public bool Solve(long[][] nodes)
        {
            Tree tree = new Tree(nodes);
            List<long> result = InOrderTraveral(tree.Root);
            List<long> expectd = tree.AllNodes.Select(n => n.Key).ToList();

            expectd.Sort();

            bool answer = (expectd.SequenceEqual(result)
                        && expectd.SequenceEqual(expectd.Distinct()));
            return answer;
        }

        private List<long> InOrderTraveral(Node currentNode)
        {
            Stack<Node> stack = new Stack<Node>();
            List<long> visited = new List<long>();

            while (currentNode != null || stack.Count != 0)
            {
                if (currentNode != null)
                {
                    stack.Push(currentNode);
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = stack.Pop();
                    visited.Add(currentNode.Key);
                    currentNode = currentNode.Right;
                }
            }

            return visited;
        }
    }


    public class Tree
    {
        public Node Root;
        public List<Node> AllNodes = new List<Node>();

        public Tree(long[][] nodes)
        {
            if (nodes.Length == 0)
                AllNodes.Add(null);
            else
            {
                for (int i = 0; i < nodes.Length; i++)
                    AllNodes.Add(new Node());

                for (int i = 0; i < nodes.Length; i++)
                {

                    AllNodes[i].Key = nodes[i][0];
                    if (nodes[i][1] != -1)
                        AllNodes[i].Left = AllNodes[(int)nodes[i][1]];

                    if (nodes[i][2] != -1)
                        AllNodes[i].Right = AllNodes[(int)nodes[i][2]];


                }
            }

            this.Root = AllNodes.ElementAt(0);
        }
    }
}
