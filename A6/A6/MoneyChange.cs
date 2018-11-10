using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    /// <summary>
    /// Gets the minimum number of coins with denominations 1, 3, 4 that changes money.
    /// </summary>
    public class MoneyChange: Processor
    {
        private static readonly int[] COINS = new int[] {1, 3, 4};

        public List<long> MinNumCoins = new List<long>();

        public MoneyChange(string testDataName) : base(testDataName)
        {
            Preparations();
        }

        private void Preparations()
        {
            for (int i = 0; i < COINS.Min(); i++)
                MinNumCoins.Add(0);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long>) Solve);
        
        public long Solve(long n)
        {
            if (n <= MinNumCoins.Count - 1) // we have already computed the money change for n
                return MinNumCoins.ElementAt((int)n);

            long numberOfCoins = 0;

            for (int m=MinNumCoins.Count; m<=n; m++)
            {
                MinNumCoins.Add(long.MaxValue);
                foreach(int coin in COINS)
                {
                    if (m >= coin)
                    {
                        numberOfCoins = MinNumCoins.ElementAt(m - coin) + 1;
                        if (numberOfCoins < MinNumCoins.ElementAt(m))
                            MinNumCoins[m] = numberOfCoins;
                    }
                }
            }

            return MinNumCoins.ElementAt((int)n);
        }
    }
}
