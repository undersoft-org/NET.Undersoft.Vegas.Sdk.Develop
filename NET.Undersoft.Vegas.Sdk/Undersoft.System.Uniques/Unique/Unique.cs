/*************************************************
   Copyright (c) 2021 Undersoft

   System.Uniques.Unique.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Uniques
{
    using System.Collections.Concurrent;
    using System.Threading;

    public static class Unique
    {
        #region Fields

        private static readonly int CAPACITY = 100 * 1000;
        private static readonly int LOW_LIMIT = 50 * 1000;
        private static readonly int NEXT_KEY_VECTOR = PRIMES_ARRAY.Get(4);
        private static readonly int WAIT_LOOPS = 500;
        private static bool generating;
        private static Thread generator;
        private static object holder = new object();
        private static Unique32 key32;
        private static Unique64 key64;
        private static long keyNumber = DateTime.Now.Ticks;
        private static ConcurrentQueue<long> keys = new ConcurrentQueue<long>();
        private static Random randomSeed = new Random(DateTime.Now.Ticks.UniqueKey32());

        #endregion

        #region Constructors

        static Unique()
        {
            key32 = new Unique32();
            key64 = new Unique64();
            generator = startup();
        }

        #endregion

        #region Properties

        public static Unique32 Key32 { get => key32; }

        public static Unique64 Key64 { get => key64; }

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

        #endregion

        #region Methods

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

        private unsafe static void keyGeneration()
        {
            uint seed = nextSeed();
            int count = CAPACITY - keys.Count;
            for (int i = 0; i < count; i++)
            {
                long keyNo = nextKeyNumber();
                keys.Enqueue((long)UniqueCode64.ComputeUniqueKey(((byte*)&keyNo), 8, seed));
            }
            Stop();
        }

        private static unsafe long nextKeyNumber()
        {
            return Interlocked.Add(ref keyNumber, NEXT_KEY_VECTOR);
        }

        private static uint nextSeed()
        {
            return (uint)randomSeed.Next();
        }

        private static Thread startup()
        {
            generating = true;
            Thread _reffiler = new Thread(new ThreadStart(keyGeneration));
            _reffiler.Start();
            return _reffiler;
        }

        #endregion
    }
}
