using System;
using TestCommon;

namespace A11
{
    public class IsItBSTHard : Processor
    {
        public IsItBSTHard(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], bool>)Solve);


        public bool Solve(long[][] nodes)
        {
            Tree tree = new Tree(nodes);
            return BTSChecker(tree.Root, long.MinValue, long.MaxValue);
        }

        private bool BTSChecker(Node root, long minValue, long maxValue)
        {
            if (root == null)
                return true;
            if (root.Key < minValue || root.Key > maxValue)
                return false;

            return BTSChecker(root.Left, minValue, root.Key - 1) &&
                                BTSChecker(root.Right, root.Key, maxValue);
        }
    }
}
