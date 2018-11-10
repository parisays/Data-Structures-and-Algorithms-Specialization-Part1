using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    /// <summary>
    /// Computes the edit distance between two strings.
    /// </summary>
    public class EditDistance : Processor
    {
        public EditDistance(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long>)Solve);

        public long Solve(string str1, string str2)
        {
            int n = str1.Length;
            int m = str2.Length;

            int[,] Distance = new int[n + 1, m + 1];

            for (int i = 0; i < n + 1; i++)
                Distance[i, 0] = i;

            for (int j = 0; j < m + 1; j++)
                Distance[0, j] = j;

            int insertion = 0;
            int deletion = 0;
            int match = 0;
            int mismatch = 0;

            for (int j = 1; j <= m; j++)
            {
                for (int i = 1; i <= n; i++)
                {
                    insertion = Distance[i, j - 1] + 1;
                    deletion = Distance[i - 1, j] + 1;
                    match = Distance[i - 1, j - 1];
                    mismatch = Distance[i - 1, j - 1] + 1;
                    if (str1[i - 1] == str2[j - 1])
                        Distance[i, j] = Math.Min(Math.Min(insertion, deletion), match);
                    else
                        Distance[i, j] = Math.Min(Math.Min(insertion, deletion), mismatch);
                }
            }
            return Distance[n, m];
        }
    }


}
