using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A11
{
    public class BST
    {
        public class Node
        {
            public long Key { get; set; }
            public long Sum { get; set; }
            protected Node _LeftChild;

            public Node Left
            {
                get => _LeftChild;
                set
                {
                    _LeftChild = value;
                    if (value != null)
                        _LeftChild.Parent = this;
                }
            }
            protected Node _RightChild;
            public Node Right
            {
                get => _RightChild;
                set
                {
                    _RightChild = value;
                    if (value != null)
                        _RightChild.Parent = this;
                }
            }
            public const string NullChar = "-";

            public Node Parent { get; set; }
            public bool IsLeftChild => Parent != null && ReferenceEquals(Parent.Left, this);
            public bool IsRightChild => Parent != null && ReferenceEquals(Parent.Right, this);

            public override string ToString()
            {
                try
                {
                    return ($"{Key}({(Left != null ? Left.ToString() : NullChar)}," +
                           $"{(Right != null ? Right.ToString() : NullChar)})")
                        .Replace("(-,-)", "")
                        .Replace("(-,-)", "")
                        .Trim();
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

            public Node(long key,
                Node leftChild = null,
                Node rightChild = null,
                Node parent = null,
                long sum = 0)
            {
                Key = key;
                Left = leftChild;
                Right = rightChild;
                Parent = parent;
                Sum = sum;
            }

            public void UpdateSum()
            {
                long ls = (Left == null) ? 0 : Left.Sum;
                long rs = (Right == null) ? 0 : Right.Sum;
                Sum = Key + ls + rs;
            }
        }

        public void Clear() => Root = null;

        public Node Root { get; protected set; }

        /// <summary>
        /// If DebugMode is on, the entire tree will be checked
        /// for parent-child consistence and making sure there are 
        /// no loops. It adds a huge performance cost, so only turn
        /// it on when you are trying to find a bug.
        /// You can add more calls to EnsureBSTConsistency where needed.
        /// </summary>
        public static bool DebugMode { get; set; } = false;

        public virtual Node Find(long key)
        {
            Node root = Root;

            if (root == null)
                return root;

            while(true)
            {
                if (root.Key == key)
                    return root;

                else if (root.Key > key)
                {
                    if (root.Left != null)
                        root = root.Left;
                    else
                        return root;
                }

                else
                {
                    if (root.Right != null)
                        root = root.Right;
                    else
                        return root;
                }
            }
        }

        public static BST ParseBST(IEnumerable<long> preOrderList)
        {
            var root = ParseBST(ref preOrderList);
            return new BST(root);
        }

        public static Node ParseBST(ref IEnumerable<long> preOrderList)
        {
            if (!preOrderList.Any())
                return null;

            long nextNode = preOrderList.First();
            preOrderList = preOrderList.Skip(1);

            if (nextNode == -1)
                return null;

            Node n = new Node(nextNode)
            {
                Left = ParseBST(ref preOrderList),
                Right = ParseBST(ref preOrderList)
            };

            return n;
        }

        public BST(Node root = null)
        {
            this.Root = root;
        }
        public override string ToString()
            => Root?.ToString();

        public virtual void Insert(long key)
        {
            Node n = Find(key);

            if (n == null) // the tree doesn't have any nodes
            {
                Root = new Node(key);
                return;
            }

            if (n.Key == key) // we already have this node
                return;

            else if (n.Key > key)
                n.Left = new Node(key, parent: n);
            else
                n.Right = new Node(key, parent: n);
        }

        public virtual void Delete(Node n)
        {
                if (n.Right == null)
                {
                    if(n.Left == null)
                    {
                        if (n.Key == Root.Key)
                        {
                            Clear();
                            return;
                        }
                    }
                    
                    else
                        n.Left.Parent = n.Parent;


                    if (n.Key != Root.Key)
                    {
                        if (n.IsLeftChild)
                            n.Parent.Left = n.Left;

                        else
                            n.Parent.Right = n.Left;
                    }
                    else
                        Root = n.Left;
                }
                else
                {
                    Node x = Next(n);
                    if(n.Right == x)
                    {
                        x.Parent = n.Parent;
                        x.Left = n.Left;
                        x.Right = null;
                        
                    }
                    else
                    {
                        x.Parent.Left = null;
                        x.Left = n.Left;
                        //n.Left.Parent = x;
                        x.Right = n.Right;
                        //n.Right.Parent = x;
                        x.Parent = n.Parent;
                        
                    }

                    if (n.Key != Root.Key)
                    {
                        if (n.IsLeftChild)
                            n.Parent.Left = x;
                        else
                            n.Parent.Right = x;
                    }
                    else
                        Root = x;
                }

                n = null;
        }


        public virtual void Delete(long key)
        {
            if (Root == null)
                return;

            Node n = Find(key);
            if (n.Key != key) //the requested node isn't in the tree
                return;

            else
                Delete(n);
            
        }

        
        public Node Next(Node n)
        {
            if (n.Right != null)
                return LeftDescendant(n.Right);

            return RightAncestor(n);
        }

        private Node RightAncestor(Node n)
        {
            if (n.Parent == null)
                return null;

            if (n.Key < n.Parent.Key)
                return n.Parent;
            else
                return RightAncestor(n.Parent);
        }

        private Node LeftDescendant(Node n)
        {
            while (n.Left != null)
                n = n.Left;
            return n;
        }

        public Node Next(long key)
        {
            Node n = this.Find(key);
            return Next(n);
        }

        public IEnumerable<Node> RangeSearch(long x, long y)
        {
            Node n = Find(x);
            while(n!=null && n.Key <= y)
            {
                if (n.Key >= x)
                    yield return n;
                n = Next(n);
            }
        }

        public static bool EnsureBSTConsistency(BST.Node r)
        {
            if (r == null)
                return true;

            Queue<Node> nodes = new Queue<Node>();
            nodes.Enqueue(r);

            while (nodes.Count > 0)
            {

                var n = nodes.Dequeue();

                // Make sure left child points back to parent
                if (n.Left != null && !ReferenceEquals(n.Left.Parent, n))
                    return false;

                // Make sure right child points back to parent
                if (n.Right != null && !ReferenceEquals(n.Right.Parent, n))
                    return false;

                // Make sure no node is its own parent
                if (n.Parent != null && ReferenceEquals(n, n.Parent))
                    return false;

                if (n.Left != null)
                    nodes.Enqueue(n.Left);

                if (n.Right != null)
                    nodes.Enqueue(n.Right);

            }
            return true;
        }

        protected void UpdateParentWithNewNode(Node parent, Node n, Node newNode)
        {
            if (parent == null)
            {
                Root = newNode;
                if (Root != null) 
                    Root.Parent = null;

                return;
            }

            if (ReferenceEquals(parent.Left,n))
                parent.Left = newNode;
            else
                parent.Right = newNode;
        }

        /// <summary>
        /// node is a right child of its parent
        /// (one left rotation)
        /// Zag
        /// </summary>
        /// <param name="n"></param>
        protected void RotateLeft(Node n)
        {
            var green = n.Left;
            var blue = n.Right;
            var p = n.Parent;
            var red = p.Left;

            var topParent = p.Parent;

            UpdateParentWithNewNode(topParent, p, n);

            n.Left = p;
            p.Right = green;
            //UpdateSums(topParent, n, p);
        }


        /// <summary>
        /// Node is a left child of root
        /// (one right rotation) 
        /// Zig
        /// </summary>
        /// <param name="n"></param>
        protected void RotateRight(Node n)
        {
            var green = n.Left;
            var blue = n.Right;
            var p = n.Parent;
            var red = p.Left;

            var topParent = p.Parent;

            UpdateParentWithNewNode(topParent, p, n);

            n.Right = p;
            p.Left = blue;

            //UpdateSums(topParent, n, p);
        }

        private void UpdateSums(Node topParent, Node n, Node p)
        {
            p.UpdateSum();
            n.UpdateSum();
            if (topParent != null)
                topParent.UpdateSum();
        }
    }
}
