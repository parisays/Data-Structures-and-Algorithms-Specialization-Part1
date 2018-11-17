using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    /// <summary>
    /// Finds the maximum possible value of the given arithmetic expression among different
    /// orders of applying arithmetic operations.
    /// </summary>
    public class MaximizingArithmeticExpression : Processor
    {
        public readonly Dictionary<char, Func<long, long, long>> PerformOperation =
            new Dictionary<char, Func<long, long, long>>()
            {
                ['+'] = (x, y) => x + y,
                ['-'] = (x, y) => x - y,
                ['*'] = (x, y) => x * y
            };


        private long[,] Min;
        private long[,] Max;
        private List<char> Operations;
        
        public MaximizingArithmeticExpression(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, long>)Solve);

        
        public long Solve(string expression)
        {
            List<long> numbers = expression.Where((str, ix) => ix % 2 == 0).
                            Select( str => long.Parse(str.ToString())).ToList();

            Operations = expression.Where((str, ix) => ix % 2 == 1).ToList();

            int n = numbers.Count;
            Min = new long[n, n];
            Max = new long[n, n];

            for(int i=0; i<n; i++)
            {
                Min[i, i] = numbers.ElementAt(i);
                Max[i, i] = numbers.ElementAt(i);
            }

            for(int s = 1; s<=n - 1; s++)
                for(int i = 0; i<=n - 1 - s; i++)
                {
                    int j = i + s;
                    (Min[i, j], Max[i, j]) = MinAndMax(i, j);
                }


            return Max[0,n-1];
        }

        private (long, long) MinAndMax(int i, int j)
        {
            long max = long.MinValue, min = long.MaxValue;

            for(int k = i; k<=j - 1; k++)
            {
                PerformOperation.TryGetValue(Operations.ElementAt(k),out var func);
                long a = func.Invoke(Max[i, k], Max[k + 1, j]);
                long b = func.Invoke(Max[i, k], Min[k + 1, j]);
                long c = func.Invoke(Min[i, k], Max[k + 1, j]);
                long d = func.Invoke(Min[i, k], Min[k + 1, j]);

                min = Math.Min( min ,
                         Math.Min(Math.Min(a, b), Math.Min(c, d)));
                max = Math.Max( max , 
                         Math.Max(Math.Max(a, b),Math.Max(c, d)));
            }


            return (min, max);
        }
        
    }
}
