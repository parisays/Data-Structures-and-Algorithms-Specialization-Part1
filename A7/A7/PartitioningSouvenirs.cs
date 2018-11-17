using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    /// <summary>
    /// Outputs 1, if it possible to partition souvenirs into three subsets with equal sums,
    /// otherwise outputs 0.
    /// </summary>
    public class PartitioningSouvenirs : Processor
    {
        public PartitioningSouvenirs(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long souvenirsCount, long[] souvenirs)
        {
            if (souvenirsCount < 3 || souvenirs.Sum() % 3 != 0)
                return 0;

            long sum = souvenirs.Sum() / 3;
            long[,] table = new long[sum + 1, souvenirsCount + 1];

            for (int i = 0; i <= sum; i++)
                table[i, 0] = 0;
            for (int i = 0; i <= souvenirsCount; i++)
                table[0, i] = 0;

            for(int i=1; i<=sum;i++)
                for(int j=1; j<=souvenirsCount; j++)
                {
                    int previousSum = i - (int)souvenirs[j - 1];
                    if (souvenirs[j - 1] == i || (previousSum > 0 && table[previousSum, j - 1] > 0))
                        table[i, j] = (table[i, j - 1] == 0) ? 1 : 2;
                    else
                        table[i, j] = table[i, j - 1];
                }

            return (table[sum, souvenirsCount] == 2) ? 1 : 0;
        }
    }
}
