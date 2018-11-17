using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    /// <summary>
    /// Finds the maximum weight of gold that fits into a bag of capacity 𝑊.
    /// </summary>
    public class MaximumGold : Processor
    {
        public MaximumGold(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long W, long[] goldBars)
        {
            int barsCount = goldBars.Length;

            long[,] weights = new long[W + 1, barsCount + 1];

            for (int i = 0; i <= W; i++)
                weights[i, 0] = 0;
            for (int i = 0; i <= barsCount; i++)
                weights[0, i] = 0;


            for (int w = 1; w <= W; w++)
                for (int barIndex = 1; barIndex<=barsCount; barIndex++)
                {
                    weights[w, barIndex] = weights[w, barIndex - 1];

                    if(goldBars[barIndex - 1]<=w)
                    {
                        long maxWeight = weights[w - goldBars[barIndex - 1], barIndex - 1] 
                                            + goldBars[barIndex - 1];

                        if (maxWeight > weights[w, barIndex])
                            weights[w, barIndex] = maxWeight;
                    }
                }

            return weights[W, barsCount];
        }
    }
}
