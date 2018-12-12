using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A10
{
    public class HashingWithChain : Processor
    {
        public HashingWithChain(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, string[], string[]>)Solve);

        protected LinkedList<string>[] Chains;

        public string[] Solve(long bucketCount, string[] commands)
        {
            Chains = new LinkedList<string>[bucketCount];
            List<string> result = new List<string>();

            for (int i = 0; i < bucketCount; i++)
                Chains[i] = new LinkedList<string>();

            foreach (var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var arg = toks[1];
                
                switch (cmdType)
                {
                    case "add":
                        Add(arg, bucketCount);
                        break;
                    case "del":
                        Delete(arg, bucketCount);
                        break;
                    case "find":
                        result.Add(Find(arg, bucketCount));
                        break;
                    case "check":
                        result.Add(Check(int.Parse(arg)));
                        break;
                }
            }
            return result.ToArray();
        }

        public const long BigPrimeNumber = 1000000007;
        public const long ChosenX = 263;
        
        public static long PolyHash(
            string str, int start, int count,
            long p = BigPrimeNumber, long x = ChosenX)
        {
            long hash = 0;

            for (int i = start + count - 1; i >= start; i--)
               hash = ((hash * x) + str[i]) % p;
            
            return hash;
        }

        public void Add(string str, long bucketCount)
        {
            long chain = ((PolyHash(str, 0, str.Length) % bucketCount) 
                                                    + bucketCount) % bucketCount;

            if (!Chains[chain].Contains(str))
                Chains[chain].AddFirst(str);
        }

        public string Find(string str, long bucketCount)
        {
            long chain = ((PolyHash(str, 0, str.Length) % bucketCount)
                                                    + bucketCount) % bucketCount;

            if (Chains[chain].Contains(str))
                return "yes";

            return "no";
        }

        public void Delete(string str, long bucketCount)
        {
            long chain = ((PolyHash(str, 0, str.Length) % bucketCount)
                                                     + bucketCount) % bucketCount;

            if (Chains[chain].Contains(str))
                Chains[chain].Remove(str);
        }

        public string Check(int i)
        {
            return (Chains[i].Count == 0) ? "-" :
                    String.Join(" ", Chains[i].ToArray());
        }
    }
}
