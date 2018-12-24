using System;
using System.Collections;
using System.Linq;

namespace E2
{
    public class Q3BloomFilter
    {
        BitArray Filter;
        Func<string, int>[] HashFunctions;
        private readonly int ConstNumber = 1000000007;

        public Q3BloomFilter(int filterSize, int hashFnCount)
        {
            Random rnd = new Random();
            Filter = new BitArray(filterSize);
            HashFunctions = new Func<string, int>[hashFnCount];

            for (int i = 0; i < HashFunctions.Length; i++)
            {
                HashFunctions[i] = str => MyHashFunction(str, ConstNumber);
            }
        }

        public int MyHashFunction(string str, int num)
        {
            return (((str.GetHashCode() + num) % Filter.Length) + Filter.Length) % Filter.Length;
        }

        public void Add(string str)
        {
            for(int i=0; i<HashFunctions.Length; i++)
            {
                int index = HashFunctions[i](str);
                Filter[index] = true;
            }
        }

        public bool Test(string str)
        {
            for (int i = 0; i < HashFunctions.Length; i++)
            {
                int index = HashFunctions[i](str);
                if (!Filter[index])
                    return false;
            }
            return true;
        }
    }
}