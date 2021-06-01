using System.IO;

namespace System
{
    public interface ILogHandler
    {
        bool Write(string information);

        bool Clean(DateTime olderThen);
    }
}