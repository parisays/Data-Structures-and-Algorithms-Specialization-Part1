using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class SpellChecker
    {
        public readonly FastLM LanguageModel;

        public SpellChecker(FastLM lm)
        {
            this.LanguageModel = lm;
        }

        public string[] Check(string misspelling)
        {
            List<WordCount> candidates = 
                new List<WordCount>();

            // TODO
            List<string> words = CandidateGenerator.GetCandidates(misspelling).ToList();
            foreach(string word in words)
            {
                if (this.LanguageModel.GetCount(word, out ulong count) == true)
                    candidates.Add(new WordCount(word, count));
            }
            return candidates
                    .OrderByDescending(x => x.Count)
                    .Select(x => x.Word)
                    .Distinct()
                    .ToArray();
        }

        public string[] SlowCheck(string misspelling)
        {
            List<WordCount> candidates =
                new List<WordCount>();

            // TODO
            foreach(var word in this.LanguageModel.WordCounts)
            {
                if (EditDistance(misspelling, word.Word) <= 1)
                    candidates.Add(word);
            }
            return candidates
                    .OrderByDescending(x => x.Count)
                    .Select(x => x.Word)
                    .Distinct()
                    .ToArray();
        }

        public int EditDistance(string str1, string str2)
        {
            int n = str1.Length;
            int m = str2.Length;

            int[,] Distance = new int[n + 1, m + 1];
            for (int i = 0; i < n + 1; i++)
            {
                Distance[i, 0] = i;
            }

            for (int j = 0; j < m + 1; j++)
            {
                Distance[0, j] = j;
            }

            int insertion = 0;
            int deletion = 0;
            int match = 0;
            int mismatch = 0;


            for (int j = 1; j <= m; j++)
            {
                for (int i = 1; i <= n; i++)
                {
                    // TODO
                    insertion = Distance[i, j - 1] + 1;
                    deletion = Distance[i - 1, j] + 1;
                    match = Distance[i - 1, j - 1];
                    mismatch = Distance[i - 1, j - 1] + 1;
                    if (str1[i-1] == str2[j-1])
                        Distance[i, j] = Math.Min(Math.Min(insertion, deletion), match);
                    else
                        Distance[i, j] = Math.Min(Math.Min(insertion, deletion), mismatch);
                }
            }
            return Distance[n, m];
        }
    }
}
