using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    /// <summary>
    /// Compute the length of a longest common subsequence of three sequences.
    /// </summary>
    public class LCSOfThree: Processor
    {
        public LCSOfThree(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[], long>)Solve);

        public long Solve(long[] seq1, long[] seq2, long[] seq3)
        {
            int x = seq1.Length;
            int y = seq2.Length;
            int z = seq3.Length;

            long[,,] LCS = new long[x + 1, y + 1, z + 1];

            for (int i = 0; i <= x; i++)
                LCS[i, 0, 0] = 0;
            for (int j = 0; j <= y; j++)
                LCS[0, j, 0] = 0;
            for (int k = 0; k <= z; k++)
                LCS[0, 0, k] = 0;

            for(int k=1; k<=z; k++)
                for(int j=1; j<=y; j++)
                    for(int i=1; i<=x; i++)
                    {
                        if (seq1[i - 1] == seq2[j - 1] && seq2[j - 1] == seq3[k - 1])
                            LCS[i, j, k] = LCS[i - 1, j - 1, k - 1] + 1;
                        else
                            LCS[i, j, k] = Math.Max(
                                Math.Max(LCS[i - 1, j, k], LCS[i, j - 1, k]),
                                        LCS[i, j, k - 1]);
                    }

            return LCS[x,y,z];
        }
    }
}
