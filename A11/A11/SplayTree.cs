using System;
using System.Diagnostics;

namespace A11
{
    public class SplayTree: BST
    {
        public SplayTree(Node r=null)
            :base(r)
        {
            //SetSum(r);
        }

        public long SetSum(Node node)
        {
            if (node == null)
                return 0;

            node.Sum = node.Key +
                    SetSum(node.Right) + SetSum(node.Left);

            return node.Sum;
        }

        public void Splay(long key) => STFind(key);

        //public override Node Find(long key) => base.Find(key);

        public Node STFind(long key)
        {
            Node n = base.Find(key);
            Splay(n);
            return n;
        }

        public override void Insert(long key)
        {
            base.Insert(key);
            STFind(key);
        }

        public override void Delete(long key) => base.Delete(key);

        public override void Delete(Node n)
        {
            Splay(Next(n));
            Splay(n);
            var left = n.Left;
            var right = n.Right;
            if(right == null)
            {
                Root = left;
                if(left != null)
                {
                    left.Parent = null;
                    //Root.UpdateSum();
                }
                    
            }
            else
            {
                right.Left = left;
                if (left != null)
                    left.Parent = right;
                Root = right;
                right.Parent = null;
                //Root.UpdateSum();
            }
            
        }

        public void Splay(Node n)
        {
            while (n != null && n.Parent != null)
            {
                if (n.IsLeftChild)
                {
                    if (n.Parent.Parent == null)
                        RotateRight(n);
                    else if (n.Parent.IsLeftChild)
                        ApplyZigZig(n);
                    else if (n.Parent.IsRightChild)
                        ApplyZigZag(n);
                }
                else
                {
                    if (n.Parent.Parent == null)
                        RotateLeft(n);
                    else if (n.Parent.IsRightChild)
                        ApplyZagZag(n);
                    else if (n.Parent.IsLeftChild)
                        ApplyZagZig(n);
                }
            }

            if (DebugMode && !EnsureBSTConsistency(this.Root))
                Debugger.Break();
        }

        /// <summary>
        /// node is right child of its parent and
        /// parent is left child of grand parent 
        /// (Right Rotation followed by left rotation)
        /// </summary>
        /// <param name="n"></param>
        private void ApplyZigZag(Node n)
        {
            var green = n.Left;
            var blue = n.Right;
            var p = n.Parent;
            var red = p.Right;
            var q = p.Parent;
            var black = q.Left;

            var topParent = q.Parent;

            UpdateParentWithNewNode(topParent, q, n);

            q.Right = green;
            n.Left = q;
            p.Left = blue;
            n.Right = p;
            //UpdateSums(topParent, n, p, q);
        }

        /// <summary>
        /// node is right child of its parent
        /// and parent is also right child of grand parent
        /// (Two Left Rotations)
        /// </summary>
        /// <param name="n"></param>
        private void ApplyZagZag(Node n)
        {
            var red = n.Left;
            var green = n.Right;
            var p = n.Parent;
            var blue = p.Left;
            var q = p.Parent;
            var black = q.Left;

            var topParent = q.Parent;

            UpdateParentWithNewNode(topParent, q, n);

            n.Left = p;
            p.Left = q;
            p.Right = red;
            q.Right = blue;
            //UpdateSums(topParent, n, p, q);
        }


        /// <summary>
        /// Node is left child of parent
        /// and parent is right child of grand parent 
        /// (Left Rotation followed by right rotation)
        /// </summary>
        /// <param name="n"></param>
        private void ApplyZagZig(Node n)
        {
            var green = n.Left;
            var blue = n.Right;
            var p = n.Parent;
            var red = p.Left;
            var q = p.Parent;
            var black = q.Right;

            var topParent = q.Parent;

            UpdateParentWithNewNode(topParent, q, n);

            n.Left = p;
            n.Right = q;
            p.Right = green;
            q.Left = blue;
            //UpdateSums(topParent, n, p, q);
        }

        /// <summary>
        /// Node is left child of parent
        /// and parent is also left child of grand parent
        /// (Two right rotations) 
        /// </summary>
        /// <param name="n"></param>
        private void ApplyZigZig(Node n)
        {
            var green = n.Right;
            var red = n.Left;
            var p = n.Parent;
            var blue = p.Right;
            var q = p.Parent;
            var black = q.Right;

            var topParent = q.Parent;

            UpdateParentWithNewNode(topParent, q, n);

            n.Right = p;
            p.Right = q;
            p.Left = green;
            q.Left = blue;

            //UpdateSums(topParent, n, p, q);

        }


        private void UpdateSums(Node topParent, Node n, Node p, Node q)
        {
            q.UpdateSum();
            p.UpdateSum();
            n.UpdateSum();
            if (topParent != null)
                topParent.UpdateSum();
        }

        public new long RangeSearch(long x, long y)
        {
            long sum = 0;
            foreach (var n in base.RangeSearch(x, y))
                sum += n.Key;
            return sum;
        }

    }
}
