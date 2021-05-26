using System.Threading;
using System.Collections.Concurrent;

namespace System
{
    public static class Logs
    {
        private static int _logLevel = 2;

        private static ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();

        private static Thread handler = handleStartup();
        
        private static DateTime ClearLogTime = DateTime.Now.AddDays(-1).AddHours(-1).AddMinutes(-1);

        private static bool threadLive;

        private static Thread handleStartup()
        {
            threadLive = true;
            Thread _handler = new Thread(new ThreadStart(logging));
            _handler.Start();
            return _handler;
        }

        private static void logging()
        {
            while (threadLive)
            {
                try
                {
                    Thread.Sleep(2000);
                    int count = logQueue.Count;
                    if (count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            string message;
                            if (logQueue.TryDequeue(out message))
                            {
                                if (Logger != null)
                                    Logger.Write(message);
                            }
                        }
                    }
                    //if (DateTime.Now.Day != ClearLogTime.Day)
                    //{
                    //    if (DateTime.Now.Hour != ClearLogTime.Hour)
                    //    {
                    //        if (DateTime.Now.Minute != ClearLogTime.Minute)
                    //        {
                    //            ClearLog();
                    //            ClearLogTime = DateTime.Now;
                    //        }
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    Add(1, ex);
                }
            }
        }

        public static ILogWriter Logger { get; set; }

        public static void Add(int logLevel, String information)
        {
            if (_logLevel >= logLevel)
            {
                logQueue.Enqueue(LogFormatter.Format(logLevel, information));
            }
        }
        public static void Add(int logLevel, Exception exception, string information = null)
        {
            if (_logLevel >= logLevel)
            {
                logQueue.Enqueue(LogFormatter.Format(logLevel, exception, information));
            }
        }      

        public static void Start(int logLevel)
        {
            threadLive = true;
            _logLevel = logLevel;
            handler.Start();
        }
        public static void Start()
        {
            threadLive = true;
            _logLevel = 2;
            handler.Start();
        }

        public static void Stop()
        {          
            handler.Join();
            threadLive = false;
        }

        public static void ClearLog()
        {
            try
            {
                DateTime time = DateTime.Now.AddDays(-7);
            }
            catch (Exception ex)
            {
                Add(1, ex);
            }

        }
    } 
}
