using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        /// <summary>
        /// changing money with a minimum number of coins
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static long ChangingMoney1(long money)
        {
            long[] Coins = new long[] { 10, 5, 1 };
            long numberOfCoins = 0;
            foreach(long coin in Coins)
            {
                numberOfCoins += money / coin;
                money %= coin;
            }

            return numberOfCoins;
        }

        /// <summary>
        /// Process Function for ChangingMoney1
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string ProcessChangingMoney1(string inStr) =>
            TestCommon.TestTools.Process(inStr, (Func<long, long>)ChangingMoney1);


        /// <summary>
        /// maximizing the total value of a loot
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="weights"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static long MaximizingLoot2(long capacity, long[] weights, long[] values)
        {
            List<double> valuePerUnit = new List<double>(weights.Length);
            int index;
            long maxLoot = 0;
            for(int i=0; i<weights.Length; i++)
                valuePerUnit.Add((double)values[i] / weights[i]);

            while(capacity>0)
            {
                index = valuePerUnit.IndexOf(valuePerUnit.Max());
                if((weights[index] - capacity)<0)
                {
                    maxLoot += values[index];
                    capacity -= weights[index];
                }
                else
                {
                    maxLoot += (long)(valuePerUnit[index]* capacity);
                    capacity -= capacity;
                }
                valuePerUnit[index] = -1;
            }
            return maxLoot;
        }

        /// <summary>
        /// Process Function for MaximizingLoot2
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string ProcessMaximizingLoot2(string inStr) =>
            TestCommon.TestTools.Process(inStr, (Func<long, long[], long[], long>)MaximizingLoot2);


        /// <summary>
        /// maximizing revenue in online ad placement
        /// </summary>
        /// <param name="slotCount"></param>
        /// <param name="adRevenue"></param>
        /// <param name="averageDailyClick"></param>
        /// <returns></returns>
        public static long MaximizingOnlineAdRevenue3(long slotCount,
            long[] adRevenue, long[] averageDailyClick)
        {
            long totalRevenue = 0;

            List<long> profits = adRevenue.ToList();
            List<long> clicks = averageDailyClick.ToList();

            int price;
            int click;

            for(int i=0; i<slotCount; i++)
            {
                price = profits.IndexOf(profits.Max());
                click = clicks.IndexOf(clicks.Max());
                totalRevenue += profits[price] * clicks[click];

                profits[price] = long.MinValue;
                clicks[click] = long.MinValue;
            }

            return totalRevenue;
        }

        /// <summary>
        /// Process function for MaximizingOnlineAdRevenue3
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string ProcessMaximizingOnlineAdRevenue3(string inStr) =>
            TestCommon.TestTools.Process(inStr,
                (Func<long, long[], long[], long>)MaximizingOnlineAdRevenue3);


        /// <summary>
        /// minimizing work while collecting signatures
        /// </summary>
        /// <param name="tenantCount"></param>
        /// <param name="startTimes"></param>
        /// <param name="endTimes"></param>
        /// <returns></returns>
        public static long CollectingSignatures4(long tenantCount,
            long[] startTimes, long[] endTimes)
        {
            List<Tuple<long, long>> points = new List<Tuple<long, long>>();
            for(int i=0; i<tenantCount; i++)
                points.Add(Tuple.Create(startTimes[i], endTimes[i]));

            points = points.OrderBy(p => p.Item2).ToList();

            long currentTime = points.ElementAt(0).Item2;
            int numberOfVisits = 1;
            for(int i=1; i<tenantCount; i++)
            {
                if (currentTime < points.ElementAt(i).Item1 ||
                        currentTime > points.ElementAt(i).Item2)
                {
                    numberOfVisits++;
                    currentTime = points.ElementAt(i).Item2;
                }
            }
            
            return numberOfVisits;
        }

        /// <summary>
        /// Process function for CollectingSignatures4
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string ProcessCollectingSignatures4(string inStr) =>
            TestCommon.TestTools.Process(inStr,
                (Func<long, long[], long[], long>)CollectingSignatures4);

        /// <summary>
        /// maximizing the number of prize places in a competition
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long[] MaximizeNumberOfPrizePlaces5(long n)
        {
            long k = 1;
            List<long> operands = new List<long>();
            while(true)
            {
                if(n<=2*k)
                {
                    operands.Add(n);
                    break;
                }
                else
                {
                    operands.Add(k);
                    n -= k;
                    k++;
                }
            }
            return operands.ToArray();
        }

        /// <summary>
        /// Process function for MaximizeNumberOfPrizePlaces5
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string ProcessMaximizeNumberOfPrizePlaces5(string inStr) =>
            TestCommon.TestTools.Process(inStr, (Func<long, long[]>)MaximizeNumberOfPrizePlaces5);


        /// <summary>
        /// maximizing your salary
        /// </summary>
        /// <param name="n"></param>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static string MaximizeSalary6(long n, long[] numbers)
        {
            long maxDigit;
            List<long> digits = numbers.ToList();
            StringBuilder answer = new StringBuilder();
            while(digits.Count>0)
            {
                maxDigit = long.MinValue;
                for(int i = 0; i <digits.Count; i++)
                    if (IsGreaterOrEqual(digits[i], maxDigit))
                        maxDigit = digits[i];
                
                answer.Append(maxDigit);
                digits.Remove(maxDigit);
            }

            return answer.ToString();
        }

        /// <summary>
        /// checks if digit is greater than/equal to maxDigit or not
        /// </summary>
        /// <param name="digit"></param>
        /// <param name="maxDigit"></param>
        /// <returns></returns>
        private static bool IsGreaterOrEqual(long digit, long maxDigit)
        {
            if (maxDigit == long.MinValue)
                return true;

            string digitStr = digit.ToString();
            string maxDigitStr = maxDigit.ToString();

            string number1 = (digitStr + maxDigitStr);
            string number2 = (maxDigitStr + digitStr);
            
            return (long.Parse(number1) >= long.Parse(number2)) ? true : false;
        }


        /// <summary>
        /// Process function for MaximizeSalary6
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string ProcessMaximizeSalary6(string inStr) =>
            TestCommon.TestTools.Process(inStr, (Func<long,long[],string>)MaximizeSalary6);
    }
}
