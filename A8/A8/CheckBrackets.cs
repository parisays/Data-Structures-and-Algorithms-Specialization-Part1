using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class CheckBrackets : Processor
    {
        public CheckBrackets(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, long>)Solve);

        private Dictionary<char, char> Pairs = new Dictionary<char, char>()
        {
            [']'] = '[' ,
            [')'] = '(' ,
            ['}'] = '{'
         };

        public long Solve(string str)
        {
            Stack<(char, int)> stack = new Stack<(char, int)>();
            for(int i=0; i<str.Length; i++)
            {
                if(Pairs.ContainsValue(str[i]))
                    stack.Push((str[i], i+1));

                else if(Pairs.ContainsKey(str[i]))
                {
                    if (!stack.Any())
                        return (long)i + 1;

                    var top = stack.Pop();

                    if(Pairs[str[i]] != top.Item1)
                        return (long)i + 1;
                }

            }

            return (stack.Any())? stack.First().Item2 : -1 ;
        }
    }
}
