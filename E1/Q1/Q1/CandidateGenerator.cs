using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public static class CandidateGenerator
    {
        public static readonly char[] Alphabet =
            Enumerable.Range('a', 'z' - 'a' + 1)
                      .Select(c => (char)c)
                      .ToArray();

        public static string[] GetCandidates(string word)
        {
            List<string> candidates = new List<string>();
            //TODO
            for(int i=0; i<=word.Length; i++)
            {
                foreach(char letter in Alphabet)
                {
                    candidates.Add(Insert(word , i, letter));
                    if(i != word.Length)
                        candidates.Add(Substitute(word, i, letter));

                }
                if (i != word.Length)
                    candidates.Add(Delete(word, i));
                
            }
            
            return candidates.ToArray();
        }
        
        private static string Insert(string word, int pos, char c)
        {
            //char[] wordChars = word.ToCharArray();
            //char[] newWord = new char[wordChars.Length+1];
            StringBuilder builder = new StringBuilder();
            builder.Append(word);
            builder.Insert(pos, c);
            return builder.ToString();
        }

        private static string Delete(string word, int pos)
        {
            //char[] wordChars = word.ToCharArray();
            //char[] newWord = new char[wordChars.Length-1];
            StringBuilder builder = new StringBuilder();
            builder.Append(word);
            builder.Remove(pos, 1);
            //TODO
            return builder.ToString();
        }

        private static string Substitute(string word, int pos, char c)
        {
            //char[] wordChars = word.ToCharArray();
            //char[] newWord = new char[wordChars.Length];
            //TODO
            StringBuilder builder = new StringBuilder();
            builder.Append(word);
            builder.Replace(word[pos], c, pos, 1);
            //builder.Replace(word, c.ToString(), pos, 1);
            return builder.ToString();
        }

    }
}
