using System;
using System.Collections.Generic;
using TestCommon;

namespace A11
{
    public class SetWithRageSums : Processor
    {
        SplayTree splayTree;
        public SetWithRageSums(string testDataName) : base(testDataName)
        {
            CommandDict =
                        new Dictionary<char, Func<string, string>>()
                        {
                            ['+'] = Add,
                            ['-'] = Del,
                            ['?'] = Find,
                            ['s'] = Sum
                        };
            
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string[]>)Solve);

        public readonly Dictionary<char, Func<string, string>> CommandDict;

        protected const long M = 1_000_000_001;

        protected long X = 0;

        //protected List<long> Data;

        public string[] Solve(string[] lines)
        {
            X = 0;
            //Data = new List<long>();
            splayTree = new SplayTree();
            
            List<string> result = new List<string>();
            foreach(var line in lines)
            {
                char cmd = line[0];
                string args = line.Substring(1).Trim();
                var output = CommandDict[cmd](args);
                if (null != output)
                    result.Add(output);
            }
            return result.ToArray();
        }

        private long Convert(long i)
            => i = (i + X) % M;       

        private string Add(string arg)
        {
            long i = Convert(long.Parse(arg));
            splayTree.Insert(i);
            return null;
        }

        private string Del(string arg)
        {
            long i = Convert(long.Parse(arg));
            splayTree.Delete(i);
            return null;
        }

        private string Find(string arg)
        {
            long i = Convert(int.Parse(arg));
            var n = splayTree.STFind(i);
            if (n == null)
                return "Not found";

            return (n.Key != i) ?
                "Not found" : "Found";
        }

        private string Sum(string arg)
        {
            var toks = arg.Split();
            long l = Convert(long.Parse(toks[0]));
            long r = Convert(long.Parse(toks[1]));

            long sum = splayTree.RangeSearch(l, r);
            X = sum;

            return sum.ToString();
        }
    }
}
