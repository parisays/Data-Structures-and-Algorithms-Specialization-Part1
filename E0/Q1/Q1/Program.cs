using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class Program
    {
        private static Dictionary<int, char[]> D =
            new Dictionary<int, char[]>
            {
                [0] = new char[] { '+' },
                [1] = new char[] { '_', ',', '@' },
                [2] = new char[] { 'A', 'B', 'C' },
                [3] = new char[] { 'D', 'E', 'F' },
                [4] = new char[] { 'G', 'H', 'I' },
                [5] = new char[] { 'J', 'K', 'L' },
                [6] = new char[] { 'M', 'N', 'O' },
                [7] = new char[] { 'P', 'Q', 'R', 'S' },
                [8] = new char[] { 'T', 'U', 'V' },
                [9] = new char[] { 'W', 'X', 'Y', 'Z' },
            };


        public static string[] GetNames(int[] phone)
        {
            return MakeStrings(phone.ToList(), 0, phone.Length - 1).ToArray();
        }


        /// <summary>
        /// Creats a list of strings from given numbers
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private static List<string> MakeStrings(List<int> numbers, int left, int right)
        {
            if (left >= right)
                return D[numbers[left]].Select(c => c.ToString()).ToList();

            int mid = (left + right)/2;

            List<string> leftHalf = MakeStrings(numbers, left, mid);
            List<string> rightHalf = MakeStrings(numbers, mid + 1, right);

            StringBuilder builder = new StringBuilder();
            List<string> result = new List<string>();

            for(int i=0;i<leftHalf.Count; i++)
            {
                builder.Append(leftHalf.ElementAt(i));
                for(int j=0; j<rightHalf.Count; j++)
                {
                    builder.Append(rightHalf.ElementAt(j));
                    result.Add(builder.ToString());
                    builder.Remove(leftHalf.ElementAt(i).Count(), rightHalf.ElementAt(j).Count());
                }
                builder.Clear();
            }

            return result;
        }

        static void Main(string[] args)
        {
            int[] phoneNumber = new int[] {0, 9, 1, 2, 2, 2, 4, 2, 5, 2, 5 };

            // چاپ یک رشته حرفی برای شماره تلفن
            for (int i=0; i< phoneNumber.Length; i++)
                Console.Write(D[phoneNumber[i]][0]);
            Console.WriteLine();
        }


    }
}
