using System.IO;
using System.Instant;

namespace System
{
    public class LogWriter : ILogWriter
    {
        IDeputy writer { get; set; }

        public LogWriter(IDeputy writeevent)
        {
            writer = writeevent;
        }

        public void Write(string information)
        {
            writer.Execute(information);
        }
        public void Clear(DateTime olderThen)
        {

        }
    }
}