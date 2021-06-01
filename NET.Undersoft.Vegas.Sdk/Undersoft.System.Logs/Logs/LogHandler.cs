using System.IO;
using System.Instant;

namespace System
{
    public class LogHandler : ILogHandler
    {
        IDeputy writer { get; set; }
        IDeputy cleaner { get; set; }

        public LogHandler(IDeputy writeMethod, IDeputy cleanMethod = null)
        {
            writer = writeMethod;
            cleaner = cleanMethod;
        }

        public bool Write(string information)
        {
            return (bool)writer.Execute(information);
        }
        public bool Clean(DateTime olderThen)
        {
            return (bool)cleaner.Execute(olderThen);
        }
    }
}