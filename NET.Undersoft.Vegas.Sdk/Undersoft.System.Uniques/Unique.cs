using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using System.Extract;
using System.Runtime;
using System.Threading;

namespace System.Uniques
{
    public static class Unique
    {
        private static readonly int NEXT_KEY_VECTOR = PRIMES_ARRAY.Get(4);
        private static readonly int CAPACITY = 100 * 1000;
        private static readonly int LOW_LIMIT = 50 * 1000;
        private static readonly int WAIT_LOOPS = 500;

        private static object holder = new object();

        private static long keyNumber = DateTime.Now.Ticks;

        private static Thread generator = startup();

        private static ConcurrentQueue<long> keys = new ConcurrentQueue<long>();

        private static Random randomSeed = new Random(DateTime.Now.Ticks.GetHashKey32());

        private static bool generating;

        private static unsafe long nextKeyNumber()
        {
           return Interlocked.Add(ref keyNumber, NEXT_KEY_VECTOR);
        }

        private static uint nextSeed()
        {
           return (uint)randomSeed.Next();
        }

        private unsafe static void keyGeneration()
        {
            uint seed = nextSeed();
            int count = CAPACITY - keys.Count;
            for (int i = 0; i < count; i++)
            {
                long keyNo = nextKeyNumber();
                keys.Enqueue((long)HashHandle64.ComputeHashKey(((byte*)&keyNo), 8, seed));
            }
            Stop();
        }

        private static Thread startup()
        {
            generating = true;
            Thread _reffiler = new Thread(new ThreadStart(keyGeneration));
            _reffiler.Start();
            return _reffiler;
        }

        public static void Start()
        {
            lock (holder)
            {
                if (!generating)
                {
                    generating = true;
                    generator.Start();
                }
            }
        }

        public static void Stop()
        {
            if (generating)
            {               
                generator.Join();
                generating = false;
            }
        }

        public static long NewKey
        {
            get
                {
                long key = 0;
                int counter = 0;
                bool loop = false;
                while (counter < WAIT_LOOPS)
                {
                    if (!(loop = keys.TryDequeue(out key)))
                    {
                        if (!generating)
                            Start();

                        counter++;
                        Thread.Sleep(20);
                    }
                    else
                    {
                        int count = keys.Count;
                        if (count < LOW_LIMIT)
                            Start();
                        break;
                    }
                }
                return key;
            }
        }
    }
}
