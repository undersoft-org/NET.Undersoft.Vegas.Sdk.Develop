using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using System.Extract;
using System.Runtime;
using System.Threading;

namespace System.Uniques
{
    public static class Unique
    {
        private static object holder = new object();

        private static int Capacity = 100000;

        private static int LowLimit = 50000;

        private static Thread refiller = refillStartup();

        private static ConcurrentQueue<long> keyQueue = new ConcurrentQueue<long>();

        private static Random random = new Random(DateTime.Now.Ticks.GetHashKey32());

        private static long autoKey = DateTime.Now.Ticks;

        private static unsafe long keyIncrement()
        {
           return Interlocked.Add(ref autoKey, 389);
        }

        private static uint nextSeed()
        {
           return (uint)random.Next();
        }

        public static bool refilling;

        private static Thread refillStartup()
        {
            refilling = true;
            Thread _reffiler = new Thread(new ThreadStart(refillment));
            _reffiler.Start();
            return _reffiler;
        }

        public static void Start()
        {
            lock (holder)
            {
                if (!refilling)
                {
                    refilling = true;
                    refiller.Start();
                }
            }
        }

        public static void Stop()
        {
            if (refilling)
            {               
                refiller.Join();
                refilling = false;
            }
        }

        private unsafe static void refillment()
        {
            uint seed = nextSeed();
            int count = Capacity - keyQueue.Count;           
            for (int i = 0; i < count; i++)
            {
                long autokey = keyIncrement();
                keyQueue.Enqueue((long)HashHandle64.ComputeHashKey(((byte*)&autokey), 8, seed));
            }
            Stop();
        }

        public static long NewKey
        {
            get
                {
                long key = 0;
                int counter = 0;
                bool loop = false;
                while (counter < 100)
                {
                    if (!(loop = keyQueue.TryDequeue(out key)))
                    {
                        if (!refilling)
                            Start();

                        counter++;
                        Thread.Sleep(20);
                    }
                    else
                    {
                        int count = keyQueue.Count;
                        if (keyQueue.Count < LowLimit)
                            Start();
                        break;
                    }
                }
                return key;
            }
        }             
    }
}
