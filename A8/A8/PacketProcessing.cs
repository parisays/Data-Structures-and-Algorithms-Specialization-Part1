using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class PacketProcessing : Processor
    {
        public PacketProcessing(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long[]>)Solve);

        public long[] Solve(long bufferSize, 
                            long[] arrivalTimes, 
                            long[] processingTimes)
        {
            if(!arrivalTimes.Any()) //there are no packets
                return new long[] { };

            if(bufferSize == 0) 
            {
                List<long> result = new List<long>(arrivalTimes.Length) { -1 };
                return result.ToArray();
            }


            List<Packet> packets = GetPackets(arrivalTimes, processingTimes).ToList();
            Buffer buffer = new Buffer(bufferSize);
            List<long> responses = new List<long>();
            foreach(Packet p in packets)
                responses.Add(buffer.SinglePacketProcess(p));

            return responses.ToArray();
        }

        private IEnumerable<Packet> GetPackets(long[] arrivalTimes, long[] processingTimes)
        {
            for (int i = 0; i < arrivalTimes.Length; i++)
                yield return new Packet(arrivalTimes[i], processingTimes[i]);
        }
    }

    public class Buffer
    {
        public long Size;
        public List<long> FinishTimes;
        
        public Buffer(long size)
        {
            this.Size = size;
            this.FinishTimes = new List<long>();
        }

        public long SinglePacketProcess(Packet p)
        {
            // other packets are already processed by the time p arrives
            while (FinishTimes.Any() && FinishTimes.ElementAt(0) <= p.ArrivalTime) 
                FinishTimes.RemoveAt(0);

            long processTime;

            if (FinishTimes.Count < this.Size)
            {
                if (FinishTimes.Count == 0)
                {
                    FinishTimes.Add(p.ArrivalTime + p.ProcessTime);
                    processTime = p.ArrivalTime;
                }
                else
                {
                    long startTime = p.ArrivalTime;
                    long lastTime = FinishTimes.Last();

                    if (lastTime > startTime)
                        startTime = lastTime;
                    
                    FinishTimes.Add(startTime + p.ProcessTime);
                    processTime = startTime;
                }
            }

            else
                processTime = -1;

            return processTime;
            
        }
    }

    public class Packet 
    {
        public long ArrivalTime;
        public long ProcessTime;

        public Packet(long arrivalTime, long processTime)
        {
            this.ArrivalTime = arrivalTime;
            this.ProcessTime = processTime;
        }

    }

    //public class Response
    //{
    //    public bool Dropped;
    //    public long StartTime;

    //    public Response(bool dropped, long startTime)
    //    {
    //        this.Dropped = dropped;
    //        this.StartTime = startTime;
    //    }
    //}
}
