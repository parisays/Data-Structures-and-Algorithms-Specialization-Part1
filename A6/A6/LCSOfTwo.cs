using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    /// <summary>
    /// Computes the length of a longest common subsequence of two sequences.
    /// </summary>
    public class LCSOfTwo: Processor
    {
        public LCSOfTwo(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long>)Solve);

        public long Solve(long[] seq1, long[] seq2)
        {
            int x = seq1.Length;
            int y = seq2.Length;
            long[,] LCS = new long[x + 1, y + 1];

            for (int i = 0; i <= x; i++)
                LCS[i, 0] = 0;
            for (int j = 0; j <= y; j++)
                LCS[0, j] = 0;

            for(int j=1; j<=y; j++)
                for(int i=1; i<=x; i++)
                {
                    if (seq1[i - 1] == seq2[j - 1])
                        LCS[i, j] = LCS[i - 1, j - 1] + 1;
                    else
                        LCS[i, j] = Math.Max(LCS[i - 1, j], LCS[i, j - 1]);
                }

            return LCS[x,y];
        }
    }
}
