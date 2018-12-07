using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A9
{
    public class MergingTables : Processor
    {
        public MergingTables(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[], long[]>)Solve);

        public long[] Parent;
        public long[] Rank;


        public long[] Solve(long[] tableSizes, long[] sourceTables, long[] targetTables)
        {
            Parent = new long[tableSizes.Length + 1];
            for (int i = 0; i <= tableSizes.Length; i++)
                Parent[i] = i;

            Rank = tableSizes;
            List<long> maxSize = new List<long>();
            int mergeCount = sourceTables.Length;

            for (int i = 0; i < mergeCount; i++)
            {
                MergeTables(targetTables[i], sourceTables[i]);
                maxSize.Add(Rank.Max());
            }

            return maxSize.ToArray();
        }

        private void MergeTables(long destination, long source)
        {
            long destinationID = Find(destination);
            long sourceID = Find(source);

            if (destinationID != sourceID)
            {
                Parent[sourceID] = destinationID;
                Rank[destinationID - 1] += Rank[sourceID - 1];
                Rank[source - 1] = 0;
            }
            
        }

        private long Find(long i)
        {
            while (i != Parent[i])
                i = Parent[i];

            return i;
        }
        
    }
}