using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class FastLM
    {
        public readonly WordCount[] WordCounts;


        public FastLM(WordCount[] wordCounts)
        {
            this.WordCounts = wordCounts.OrderBy(wc => wc.Word).ToArray();
        }

        public bool GetCount(string word, out ulong count)
        {
            count = 0;
            //TODO
            var result = BinarySearch(word, 0, this.WordCounts.Length - 1);
            count = result.Item2;
            return result.Item1;
        }

        private (bool, ulong) BinarySearch(string word, int left, int right)
        {
            if (left > right)
                return (false, 0);
            int mid = (left + right) / 2;
            int compare = string.Compare(word, this.WordCounts[mid].Word);
            if (compare == 0)
                return (true, this.WordCounts[mid].Count);

            else if (compare < 0)
                return BinarySearch(word, left, mid - 1);
            else
                return BinarySearch(word, mid + 1, right);
        }
    }
}
