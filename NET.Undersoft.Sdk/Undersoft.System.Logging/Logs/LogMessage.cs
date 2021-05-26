using System.IO;
using System.Threading;

namespace System
{
    public class LogMessage
    {
        private static long autoId = DateTime.Now.Ticks;

        public LogMessage()
        {
            Id = Interlocked.Increment(ref autoId);
        }

        public long Id { get; set; }

        public int Level { get; set; }

        public string Type { get; set; }

        public DateTime Time { get; set; }

        public int Millis { get; set; }

        public string Message { get; set; }
    }
}