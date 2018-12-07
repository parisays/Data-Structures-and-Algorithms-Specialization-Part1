using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A9
{
    public class ParallelProcessing : Processor
    {
        public ParallelProcessing(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], Tuple<long, long>[]>)Solve);

        public Tuple<long, long>[] Solve(long threadCount, long[] jobDuration)
        {
            PriorityQueue queue = new PriorityQueue((int)threadCount);

            List<Tuple<long, long>> process = new List<Tuple<long, long>>(); // (thread index, start time)

            foreach(long job in jobDuration)
            {
                
                process.Add(Tuple.Create<long, long>(queue.Threads.ElementAt(0).Index
                    , queue.Threads.ElementAt(0).NextFreeTime));
                                

                queue.ChangePriority(0,
                                    queue.Threads.ElementAt(0).NextFreeTime + job,
                                            (int)threadCount);

            }

            return process.ToArray();
        }
    }

    public class PriorityQueue
    {
        public List<Thread> Threads;

        public PriorityQueue(int threadCount)
        {
            this.Threads = new List<Thread>();
            for (int i = 0; i < threadCount; i++)
                this.Threads.Add(new Thread(i, 0));
        }

        public void ChangePriority(int index, long priority, int size)
        {
            long oldPriority = Threads.ElementAt(index).NextFreeTime;
            Threads[index].NextFreeTime = priority;

            if (priority < oldPriority)
                SiftUp(index);
            else
                SiftDown(index, size);

        }

        private void SiftDown(int index, int size)
        {
            int minIndex = index;
            int leftIndex = LeftChild(index);
            int rightIndex = RightChild(index);

            if (leftIndex < size && CompareThreads(
                                    Threads.ElementAt(leftIndex), Threads.ElementAt(minIndex)))
                minIndex = leftIndex;

            if (rightIndex < size && CompareThreads(
                                    Threads.ElementAt(rightIndex), Threads.ElementAt(minIndex)))
                minIndex = rightIndex;

            if(minIndex != index)
            {
                (Threads[index], Threads[minIndex]) =
                                        (Threads[minIndex], Threads[index]);
                SiftDown(minIndex, size);
            }

        }


        private void SiftUp(int index)
        {
            while(index>0 && CompareThreads(  Threads.ElementAt(index),
                                            Threads.ElementAt(Parent(index)) ))
            {
                (Threads[index], Threads[Parent(index)]) =
                                        (Threads[Parent(index)], Threads[index]);
                index = Parent(index);
            }
        }

        private int Parent(int i) => (int)Math.Floor((double)i / 2);

        private int RightChild(int index) => 2 * index + 2;

        private int LeftChild(int index) => 2 * index + 1;

        private bool CompareThreads(Thread thread1, Thread thread2)
        {
            if (thread1.NextFreeTime == thread2.NextFreeTime)
                return thread1.Index < thread2.Index;
            else
                return thread1.NextFreeTime < thread2.NextFreeTime;
        }
    }


    public class Thread
    {
        public int Index;
        public long NextFreeTime;

        public Thread(int index,long freeTime)
        {
            this.Index = index;
            this.NextFreeTime = freeTime;
        }
    }
}
