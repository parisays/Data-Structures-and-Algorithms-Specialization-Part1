using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A11
{
    public class BinaryTreeTraversals : Processor
    {
        public BinaryTreeTraversals(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], long[][]>)Solve);


        public long[][] Solve(long[][] nodes)
        {
            long[][] result = new long[3][];
            Tree tree = new Tree(nodes);

            //List<Node> myNodes = new List<Node>();
            //// make nodes
            //for (int i = nodes.Length - 1; i >= 0; i--)
            //{
            //    if (nodes[i][1] == -1 && nodes[i][2] == -1)
            //        myNodes.Add(new Node(nodes[i][0], null, null, i));
            //}

            //while(myNodes.Count != nodes.Length)
            //{
            //    for (int i = nodes.Length - 1; i >= 0; i--)
            //    {
            //        if( !myNodes.Exists(n => n.Index == i))
            //        {
            //            long key = nodes[i][0];
            //            Node left;
            //            Node right;
            //            if (nodes[i][1] == -1)
            //                left = null;
            //            else if (myNodes.Exists(n => n.Index == nodes[i][1]))
            //                left = myNodes.Single(n => n.Index == nodes[i][1]);
            //            else
            //                continue;

            //            if (nodes[i][2] == -1)
            //                right = null;
            //            else if (myNodes.Exists(n => n.Index == nodes[i][2]))
            //                right = myNodes.Single(n => n.Index == nodes[i][2]);
            //            else
            //                continue;

            //            myNodes.Add(new Node(key, left, right, i));
            //        }

            //    }
            //}
            //Node node = myNodes.Single(n => n.Index == 0);

            result[0] = InOrderTraveral(tree.Root);
            result[1] = PreOrderTraversal(tree.Root);
            result[2] = PostOrderTraversal(tree.Root);

            return result;
        }

        private long[] PostOrderTraversal(Node node)
        {
            Stack<Node> stack = new Stack<Node>();
            List<long> visited = new List<long>();
            stack.Push(node);

            while(stack.Count!=0)
            {
                node = stack.Pop();
                visited.Add(node.Key);

                if (node.Left != null)
                    stack.Push(node.Left);
                if (node.Right != null)
                    stack.Push(node.Right);

            }

            visited.Reverse();
            return visited.ToArray();
        }

        private long[] PreOrderTraversal(Node node)
        {
            Stack<Node> stack = new Stack<Node>();
            List<long> visited = new List<long>();

            stack.Push(node);
            while (stack.Count != 0)
            {
                node = stack.Pop();
                visited.Add(node.Key);
                if (node.Right != null)
                    stack.Push(node.Right);
                if (node.Left != null)
                    stack.Push(node.Left);
            }

           return visited.ToArray();
        
        }

        
        private long[] InOrderTraveral(Node currentNode)
        {
            Stack<Node> stack = new Stack<Node>();
            List<long> visited = new List<long>();

            while( currentNode!=null || stack.Count!=0)
            {
                if(currentNode != null)
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

            return visited.ToArray();
        }
    }

    public class Node
    {
        public Node Left;
        public Node Right;
        public long Key;
        public int Index;
        
        public Node()
        {
        }

        public Node(long key, Node left, Node right, int index)
        {
            this.Key = key;
            this.Left = left;
            this.Right = right;
            this.Index = index;
        }

    }
}
