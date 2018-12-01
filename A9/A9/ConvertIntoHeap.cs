using TestCommon;
using System;
using System.Collections.Generic;

namespace A9
{
    public class ConvertIntoHeap : Processor
    {
        public ConvertIntoHeap(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], Tuple<long, long>[]>)Solve);

        public List<Tuple<long, long>> Swaps;

        public Tuple<long, long>[] Solve(
            long[] array)
        {
            double size = array.Length;
            Swaps = new List<Tuple<long, long>>();

            for(int i = (int)Math.Floor(size /2) - 1; i>=0; i--)
            {
                SiftDown(i, array);
            }

            return Swaps.ToArray();
        }

        private void SiftDown(int i, long[] array)
        {
            int leftChild = 2 * i + 1;
            int rightChild = 2 * i + 2;
            int size = array.Length;
            int minIndex = i;
            
            if( leftChild < size && array[leftChild] < array[minIndex] )
                minIndex = leftChild;

            if (rightChild < size && array[rightChild] < array[minIndex])
                minIndex = rightChild;

            if (i != minIndex)
            {
                Swaps.Add(Tuple.Create<long, long>(i, minIndex));
                (array[i], array[minIndex]) = (array[minIndex], array[i]);
                SiftDown(minIndex, array);
            }
            
        }
    }

}