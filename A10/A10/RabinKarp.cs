using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A10
{
    public class RabinKarp : Processor
    {
        public RabinKarp(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long[]>)Solve);



        public long[] Solve(string pattern, string text)
        {
            Random r = new Random();
            long x = r.Next(1, (int)BigPrimeNumber - 1);
            List<long> occurrences = new List<long>();

            long pHash = HashingWithChain.PolyHash(pattern, 0, pattern.Length, x: x);

            long[] tHashes = PreComputeHashes(text, pattern.Length, BigPrimeNumber, x);

            for(int i=0; i<= text.Length - pattern.Length; i++)
            {
                if (pHash == tHashes[i])
                        if (pattern.Equals(text.Substring(i, pattern.Length)))
                            occurrences.Add(i);
            }
            
            return occurrences.ToArray();
        }
        

        public const long BigPrimeNumber = 1000000007;

        public static long[] PreComputeHashes(
            string T, 
            int P,
            long p,
            long x
            )
        {
            int textLength = T.Length;

            long[] hashes = new long[textLength - P + 1];
            hashes[T.Length - P] = HashingWithChain.PolyHash(
                                T, start : textLength - P, count : P , p: p , x: x);
            long y = 1;
            for (int i = 1; i <= P; i++)
                y = (y * x) % p;

            for (int i = textLength - P - 1; i >= 0; i--)
                hashes[i] = ( ( (x * hashes[i + 1])  + T[i] - (y * T[i + P]) ) % p + p )% p;

            return hashes;
        }
    }
}
