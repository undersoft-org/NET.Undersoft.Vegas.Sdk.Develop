/*************************************************
   Copyright (c) 2021 Undersoft

   System.Logs.Logs.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (01.06.2021) 
   @licence MIT
 *************************************************/

namespace System
{
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using System.Instant;
    using System.Threading;

    public static class Logs
    {
        #region Fields

        private static readonly int BACK_LOG_DAYS = -1;
        private static readonly int BACK_LOG_HOURS = -1;
        private static readonly int BACK_LOG_MINUTES = -1;
        private static int _logLevel = 2;
        private static DateTime clearLogTime;
        private static Thread logger;
        private static ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();
        private static bool threadLive;

        #endregion

        #region Constructors

        static Logs()
        {
            clearLogTime = DateTime.Now.AddDays(BACK_LOG_DAYS).AddHours(BACK_LOG_HOURS).AddMinutes(BACK_LOG_MINUTES);
            threadLive = true;
            logger = new Thread(new ThreadStart(logging));
            logger.Start();
        }

        #endregion

        #region Properties

        private static ILogWriter writer { get; set; }

        #endregion

        #region Methods

        public static void Add(int logLevel, Exception exception, string information = null)
        {
            if (_logLevel >= logLevel)
            {
                logQueue.Enqueue(LogFormatter.Format(logLevel, exception, information));
            }
        }

        public static void Add(int logLevel, String information)
        {
            if (_logLevel >= logLevel)
            {
                logQueue.Enqueue(LogFormatter.Format(logLevel, information));
            }
        }

        public static void AttachWriter(IDeputy writingMethod)
        {
            writer = new LogWriter(deputy);
        }

        public static void ClearLog()
        {
            try
            {
                if (writer != null)
                {
                    writer.Clear(clearLogTime);
                    clearLogTime = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetLogLevel(int logLevel)
        {
            _logLevel = logLevel;
        }

        public static void Start()
        {
            threadLive = true;
            _logLevel = 2;
            logger.Start();
        }

        public static void Start(int logLevel)
        {
            SetLogLevel(logLevel);
            if (!threadLive)
            {
                threadLive = true;
                logger.Start();
            }
        }

        public static void Start(int logLevel, IDeputy writingMethod)
        {
            AttachWriter(writingMethod);
            SetLogLevel(logLevel);
            if (!threadLive)
            {
                threadLive = true;
                logger.Start();
            }
        }

        public static void Stop()
        {
            logger.Join();
            threadLive = false;
        }

        private static void logging()
        {
            while (threadLive)
            {
                try
                {
                    Thread.Sleep(2000);
                    int count = logQueue.Count;
                    for (int i = 0; i < count; i++)
                    {
                        string message;
                        if (logQueue.TryDequeue(out message))
                        {
                            if (writer != null)
                                writer.Write(message);
                            else
                                Debug.WriteLine(message);
                        }
                    }
                    if (DateTime.Now.Day != clearLogTime.Day)
                    {
                        if (DateTime.Now.Hour != clearLogTime.Hour)
                        {
                            if (DateTime.Now.Minute != clearLogTime.Minute)
                            {
                                ClearLog();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion
    }
}
