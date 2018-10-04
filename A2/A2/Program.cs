using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        public static string Process(string input)
        {
            var inData = input.Split(new char[] { ' ', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .ToList();

            return FastMaxPairwiseProduct(inData).ToString();

        }

        /// <summary>
        /// O(n^2)
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int NaiveMaxPairwiseProduct(List<int> numbers)
        {
            int product = 0;
            for(int i=0; i<numbers.Count; i++)
                for(int j=i+1; j<numbers.Count; j++)
                    product = Math.Max(product, numbers[i] * numbers[j]);
                
            return product;
        }

        /// <summary>
        /// O(2*n)
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int FastMaxPairwiseProduct(List<int> numbers)
        {
            int index1 = 0;
            for (int i = 1; i < numbers.Count; i++)
                if (numbers[i] > numbers[index1])
                    index1 = i;

            int index2 = (index1==0)? 1 : 0;
            for (int i = 0; i < numbers.Count; i++)
                if (i != index1 && numbers[i] > numbers[index2]) // Change numbers[i] != numbers[index1] to i!=index1
                    index2 = i;

            return numbers[index1] * numbers[index2];
        }
    }
}
