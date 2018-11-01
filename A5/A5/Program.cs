using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Program
    {
        static void Main(string[] args) { }

        /// <summary>
        /// search a sorted data for a key
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long[] BinarySearch1(long[] a , long [] b)
        {
            List<long> result = new List<long>();
            foreach(long num in b)
            {
                result.Add(BinarySearch(a, num, 0, a.Length-1));
            }
            return result.ToArray();
        }

        private static long BinarySearch(long[] numList, long number,int start, int end)
        {
            if (start > end)
                return -1;
            int mid = (start + end) / 2;
            if (number == numList[mid])
                return mid;
            else if (number > numList[mid])
                return BinarySearch(numList, number, mid + 1, end);
            else 
                return BinarySearch(numList, number, start, mid - 1);
            
        }

        /// <summary>
        /// process function for BinarySearch1
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string ProcessBinarySearch1(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[]>)BinarySearch1);


        /// <summary>
        /// find a majority element in a data
        /// </summary>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static long MajorityElement2(long n, long[] a)
        {
            //    //non divide and conquer approach
            //    Dictionary<long, int> elements = new Dictionary<long, int>();
            //    foreach (long num in a)
            //    {
            //        if (!elements.ContainsKey(num))
            //            elements.Add(num, 1);
            //        else
            //            elements[num]++;
            //    }

            //    return (elements.Where(m => m.Value > a.Length / 2).Count() == 0) ? 0 : 1;

            // divide and conquer approach 
            return GetMajorityElement(a, 0, a.Length - 1).Item1.Count()> (n/2)?1:0;

        }

        private static (long[], long[]) GetMajorityElement(long[] items, int left, int right)
        {
            if (left == right)
                return (new long[] { items[left] }, new long[] {-1});

            int mid = (left + right) / 2;
            var leftHalf = GetMajorityElement(items, left, mid);
            var rightHalf = GetMajorityElement(items, mid + 1, right);

            return CountMerge(leftHalf, rightHalf);
        }

        private static (long[], long[]) CountMerge((long[], long[]) leftHalf, (long[], long[]) rightHalf)
        {
            (var leftMajors, var rightOthers) = ChunkProcess(leftHalf.Item1.ToList(), rightHalf.Item2);
            (var rightMajors,var  leftOthers) = ChunkProcess(rightHalf.Item1.ToList(), leftHalf.Item2);

            var majors = new List<long>();
            var others = new List<long>();

            if (leftMajors[0] == rightMajors[0])
            {
                majors.AddRange(leftMajors);
                majors.AddRange(rightMajors);
                others.AddRange(leftOthers);
                others.AddRange(rightOthers);
                

            }
            else if (leftMajors.Length > rightMajors.Length)
            {
                majors.AddRange(leftMajors);
                others.AddRange(rightMajors);
                others.AddRange(rightOthers);
                others.AddRange(leftOthers);
            }
            else
            {
                majors.AddRange(rightMajors);
                others.AddRange(leftMajors);
                others.AddRange(rightOthers);
                others.AddRange(leftOthers);
            }

            return (majors.ToArray(), others.ToArray());
        }

        private static (long[] leftMajors, long[] rightOthers) ChunkProcess(List<long> majors, long[] elements)
        {
            List<long> others = new List<long>();
            foreach(long element in elements)
            {
                if (majors[0] == element)
                    majors.Add(element);
                else
                    others.Add(element);
            }

            return (majors.ToArray(), others.ToArray());
        }



        /// <summary>
        /// process function for MajorityElement2
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string ProcessMajorityElement2(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)MajorityElement2);

        /// <summary>
        /// improve the quick sort algorithm
        /// </summary>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static long[] ImprovingQuickSort3(long n, long[] a)
        {
            QuickSort3WayPartition(a, 0, (int)n-1);

            return a;
        }

        private static void QuickSort3WayPartition(long[] items, int left, int right)
        {
            if (left >= right)
                return;
            
            int lt = left;
            int gt = right;
            int i = left + 1;

            int pivotIndex = left;
            long pivotValue = items[pivotIndex];


            while (i <= gt)
            {
                if (items[i]< pivotValue)
                {
                    (items[i], items[lt]) = (items[lt], items[i]);
                    i++;
                    lt++;
                }
                else if (pivotValue< items[i])
                {
                    (items[i], items[gt]) = (items[gt], items[i]);
                    gt--;
                }
                else
                    i++;

            }

            QuickSort3WayPartition(items, left, lt - 1);
            QuickSort3WayPartition(items, gt + 1, right);

        }

        /// <summary>
        /// process function for ImprovingQuickSort3
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string ProcessImprovingQuickSort3(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[]>)ImprovingQuickSort3);

        /// <summary>
        /// check how close a data is to being sorted
        /// </summary>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static long NumberofInversions4(long n, long[] a)
        {
            return MergeSort(a.ToList()).Item2;
            //return MergeSort2(a.ToList(), 0, a.Length - 1);
        }

        private static (List<long>, long) MergeSort(List<long> items)
        {
            if (items.Count == 1)
                return (items, 0);

            var lefthalf = MergeSort(items.Take(items.Count / 2).ToList());
            var righthalf = MergeSort(items.Skip(items.Count / 2).ToList());

            var sortedItems = Merge(lefthalf.Item1, righthalf.Item1);

            return (sortedItems.Item1, sortedItems.Item2 +
                                          righthalf.Item2 +
                                            lefthalf.Item2);
        }

        private static (List<long>, long) Merge(List<long> lefthalf, List<long> righthalf)
        {
            int countOfInversions = 0;
            List<long> sortedList = new List<long>();
            int i = 0, j = 0;
            while (i < lefthalf.Count && j < righthalf.Count)
            {
                if(lefthalf[i] <= righthalf[j])
                {
                    sortedList.Add(lefthalf[i++]);
                }
                else
                {
                    sortedList.Add(righthalf[j++]);
                    countOfInversions += lefthalf.Count - i;
                }
            }

            while (i < lefthalf.Count)
                sortedList.Add(lefthalf[i++]);
            while (j < righthalf.Count)
                sortedList.Add(righthalf[j++]);

            return (sortedList, countOfInversions);
        }

        
       /// <summary>
       /// process function for NumberofInversions4
       /// </summary>
       /// <param name="inStr"></param>
       /// <returns></returns>
        public static string ProcessNumberofInversions4(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)NumberofInversions4);

        /// <summary>
        /// compute the number of segments that contain each point
        /// </summary>
        /// <param name="points"></param>
        /// <param name="startSegments"></param>
        /// <param name="endSegment"></param>
        /// <returns></returns>
        public static long[] OrganizingLottery5(long[] points, long[] startSegments,
            long[] endSegment)
        {
            Dictionary<long, long> pointCoverage = new Dictionary<long, long>();
            List<Tuple<long, string>> pairs = new List<Tuple<long, string>>();

            for (int i = 0; i < startSegments.Length; i++)
                pairs.Add(Tuple.Create(startSegments[i], "Left"));
            foreach (long point in points)
            {
                if (!pointCoverage.ContainsKey(point))
                {
                    pointCoverage.Add(point, 0);
                    pairs.Add(Tuple.Create(point, "Point"));
                }
                
            }
            for(int i=0; i<endSegment.Length; i++)
                pairs.Add(Tuple.Create(endSegment[i], "Right"));

            pairs = pairs.OrderBy(p => p.Item1).ToList();

            long leftCount = 0;
            for(int i=0; i<pairs.Count; i++)
            {
                if (pairs[i].Item2 == "Left")
                    leftCount++;

                else if (pairs[i].Item2 == "Right")
                    leftCount--;

                else if (pairs[i].Item2 == "Point")
                    pointCoverage[pairs[i].Item1] += leftCount;
            }

            List<long> result = new List<long>();
            foreach(long point in points)
            {
                result.Add(pointCoverage[point]);
            }
            return result.ToArray();
        }

        /// <summary>
        /// process function for OrganizingLottery5
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string ProcessOrganizingLottery5(string inStr) =>
            TestTools.Process(inStr,(Func<long[], long[], long[], long[]>)OrganizingLottery5);

        /// <summary>
        /// find the closest pair of points
        /// </summary>
        /// <param name="n"></param>
        /// <param name="xPoints"></param>
        /// <param name="yPoints"></param>
        /// <returns></returns>
        public static double ClosestPoints6(long n, long[] xPoints, long[] yPoints)
        {
            List<(long, long)> points = new List<(long, long)>();
            for (int i = 0; i < n; i++)
                points.Add((xPoints[i], yPoints[i]));
            
            return LargeSizeMinimumDistance(points.OrderBy(p => p.Item1).ToList());
        }

        
        private static double LargeSizeMinimumDistance(List<(long, long)> points)
        {
            if (points.Count <= 3)
                return SmallSizeMinimumDistance(points);

            List<(long, long)> leftPoints = points.Take(points.Count / 2).ToList();
            List <(long, long)> rightPoints = points.Skip(points.Count / 2).ToList();

            double leftMinimum = LargeSizeMinimumDistance(leftPoints);
            double rightMinimum = LargeSizeMinimumDistance(rightPoints);

            double seperatedMinimum = Math.Min(leftMinimum, rightMinimum);
            double middleLine = (leftPoints.Last().Item1 + rightPoints.First().Item1) / 2;

            double hybridMinimum = ComputeHybridMinimum(leftPoints, rightPoints,
                                                        middleLine, seperatedMinimum);

            int result = (int)Math.Round(Math.Min(hybridMinimum, seperatedMinimum) * 10_000);

            return (double)result / 10_000;
        }

        private static double ComputeHybridMinimum(List<(long, long)> leftPoints, List<(long, long)> rightPoints,
                            double line, double sigma)
        {
            List<(long, long)> total = new List<(long, long)>();
            
            for (int i = 0; i < leftPoints.Count; i++)
                if (Math.Abs(line - leftPoints[i].Item1) <= sigma)
                    total.Add(leftPoints[i]);
            
            for (int i = 0; i < rightPoints.Count; i++)
                if (Math.Abs(rightPoints[i].Item1 - line) <= sigma)
                    total.Add(rightPoints[i]);

            total = total.OrderBy(p => p.Item2).ToList();

            double result = sigma;

            for (int i = 0; i < total.Count; i++)
                for (int j = i + 1; j < Math.Min(i + 8, total.Count); j++)
                    if (Math.Abs(total[i].Item2 - total[j].Item2) <= sigma)
                        result = Math.Min(result, Math.Sqrt(Math.Pow(total[i].Item1 - total[j].Item1, 2)
                                            + Math.Pow(total[i].Item2 - total[j].Item2, 2)) );

            return result;
        }

        private static double SmallSizeMinimumDistance(List<(long, long)> points)
        {
            double result = double.MaxValue;
            for (int i = 0; i < points.Count; i++)
                for (int j = i + 1; j < points.Count; j++)
                    result = Math.Min(result, Math.Sqrt(Math.Pow(points[i].Item1 - points[j].Item1, 2)
                                            + Math.Pow(points[i].Item2 - points[j].Item2, 2)) );
            return result;

        }

        /// <summary>
        /// process function for ClosestPoints6
        /// </summary>
        /// <param name="inStr"></param>
        /// <returns></returns>
        public static string ProcessClosestPoints6(string inStr) =>
           TestTools.Process(inStr, (Func<long, long[], long[], double>)
               ClosestPoints6);

    }
}
