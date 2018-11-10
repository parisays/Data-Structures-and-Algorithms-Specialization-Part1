using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    /// <summary>
    /// Computes the minimum number of operations needed to obtain the number 𝑛
    /// starting from the number 1.
    /// </summary>
    public class PrimitiveCalculator: Processor
    {
        public PrimitiveCalculator(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[]>)Solve);

        public List<int> MinNumOperations = new List<int>() {0, 0 };

        public long[] Solve(long n)
        {
            GetMinimumOperations(n);
            return GetNumbersSequence(n).ToArray();
        }

        private void GetMinimumOperations(long n)
        {
            if (n <= MinNumOperations.Count - 1) // we already have the minimum number of operations for n
                return;

            int steps1;
            int steps2;
            int steps3;
            for(long i = MinNumOperations.Count; i <= n; i++)
            {
                steps1 = int.MaxValue;
                steps2 = int.MaxValue;
                steps3 = int.MaxValue;
                if (i % 3 == 0)
                    steps1 = MinNumOperations.ElementAt((int)i / 3) + 1;
                if (i % 2 == 0)
                    steps2 = MinNumOperations.ElementAt((int)i / 2) + 1;
                
                steps3 = MinNumOperations.ElementAt((int)i - 1) + 1;

                MinNumOperations.Add(Math.Min(steps1, Math.Min(steps2, steps3)));

            }
        }

        private List<long> GetNumbersSequence(long n)
        {
            List<long> operations = new List<long>();
            List<long> index = new List<long>();

            operations.Add(n);

            while(n>1)
            {
                if (n % 3 == 0)
                    index.Add(n / 3);
                if (n % 2 == 0)
                    index.Add( n / 2);
                
                index.Add(n - 1);

                n = index.OrderBy(i => MinNumOperations.ElementAt((int)i)).First();
                operations.Add(n);
                index.Clear();
            }

            operations.Reverse();

            return operations;
        }
        
    }
}
