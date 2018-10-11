using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3
{
    public class Program
    {
        static void Main(string[] args)
        {
        }
        
        /// <summary>
        /// Since the pisano period of 10 is 60
        /// </summary>
        public static long PisanoNum = 60;

        public static string Process(string inStr,Func<long, long> longProcessor )
        {
            long n = long.Parse(inStr);
            return longProcessor(n).ToString();
        }

        public static string Process(string inStr, Func<long, long, long> longProcessor)
        {
            var toks = inStr.Split(new char[] { ' ', '\n', '\r' },
                    StringSplitOptions.RemoveEmptyEntries);
            long a = long.Parse(toks[0]);
            long b = long.Parse(toks[1]);

            return longProcessor(a, b).ToString();

        }

        /// <summary>
        /// computes a small Fibonacci number
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long Fibonacci(long n)
        {
            if (n <= 1)
                return n;

            long fib1 = 0;
            long fib2 = 1;
            
            for (long i = 2; i <= n; i++)
            {
                fib1 = fib1 + fib2;
                (fib1, fib2) = (fib2 , fib1);
            }

            return fib2;
        }

        public static string ProcessFibonacci(string inStr)
            => Process(inStr, Fibonacci);


        /// <summary>
        /// computes the last digit of a large Fibonacci number
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long Fibonacci_LastDigit(long n)
        {
            n = n % PisanoNum;
            if (n <= 1)
                return n;

            long fib1 = 0;
            long fib2 = 1;
            
            for (long i = 2; i <= n; i++)
            {
                fib1 = (fib1 + fib2)%10;
                (fib1, fib2) = (fib2%10, fib1);
            }

            return fib2;
        }

        public static string ProcessFibonacci_LastDigit(string inStr) =>
            Process(inStr, Fibonacci_LastDigit);

        /// <summary>
        /// computes the greatest common divisor of two long numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long GCD(long a, long b)
        {
            long max = Math.Max(a, b);
            long min = Math.Min(a, b);
            
            while(min!=0)
            {
                max = max % min;
                (max, min) = (min, max);
            }

            return max;
        }
        

        public static string ProcessGCD(string inStr) => Process(inStr, GCD);

        /// <summary>
        /// computes the least common multiple of two long numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long LCM(long a, long b)
        {
            return (a / Program.GCD(a, b)) * b;
        }
        
        public static string ProcessLCM(string inStr) =>
            Process(inStr, LCM);

        /// <summary>
        /// computes a huge Fibonacci number modulo 𝑚
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static long Fibonacci_Mod(long n, long m)
        {
            List<long> period = new List<long>();
            long fib1 = 0;
            long fib2 = 1;
            long pisanoPeriod = 0;

            period.Add(fib1);
            period.Add(fib2);

            for (; pisanoPeriod <= long.MaxValue; pisanoPeriod++)
            {
                fib1 = (fib1 + fib2) % m;
                (fib1, fib2) = (fib2 , fib1);
                period.Add(fib2);
                if (fib1 == 0 && fib2 == 1)
                {
                    pisanoPeriod++;
                    break;
                }
            }
            
            return period.ElementAt(int.Parse((n % pisanoPeriod).ToString())) % m;
        }

        public static string ProcessFibonacci_Mod(string inStr) =>
            Program.Process(inStr, Fibonacci_Mod);

        /// <summary>
        /// computes the last digit of a sum of Fibonacci numbers
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long Fibonacci_Sum(long n)
        {
            long fib1 = 0;
            long fib2 = 1;

            n = (n + 2) % PisanoNum;

            for (long i = 2; i <= n; i++)
            {
                fib1 = (fib1 + fib2) % 10;
                (fib1, fib2) = (fib2 % 10, fib1);
            }

            return fib2 == 0 ? 9 : fib2-1;
        }

        public static string ProcessFibonacci_Sum(string inStr) =>
            Process(inStr, Fibonacci_Sum);

        /// <summary>
        /// computes the last digit of a partial sum of Fibonacci numbers
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static long Fibonacci_Partial_Sum(long n, long m)
        {
            //long lastDigit1 = Fibonacci((Math.Max(n, m) + 2) % PisanoNum) % 10;
            //long lastDigit2 = Fibonacci((Math.Min(n, m) + 1) % PisanoNum) % 10;

            long lastDigit1 = Program.Fibonacci_LastDigit(Math.Max(n, m) + 2);
            long lastDigit2 = Program.Fibonacci_LastDigit(Math.Min(n, m) + 1);


            return lastDigit1 < lastDigit2 ?
                lastDigit1 + 10 - lastDigit2 : lastDigit1 - lastDigit2;
        }

        public static string ProcessFibonacci_Partial_Sum(string inStr) =>
            Process(inStr, Fibonacci_Partial_Sum);

        /// <summary>
        /// computes last digit of the sum of squares of Fibonacci numbers
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long Fibonacci_Sum_Squares(long n)
        {
            long fib1 = 0;
            long fib2 = 1;
            n = (n + 1) % PisanoNum;
            for (long i = 2; i <= n; i++)
            {
                fib1 = (fib1 + fib2) % 10;
                (fib1, fib2) = (fib2 % 10, fib1);;
            }

            return (fib1 * fib2)%10;
        }

        public static string ProcessFibonacci_Sum_Squares(string inStr) =>
            Process(inStr, Fibonacci_Sum_Squares);
    }
}
