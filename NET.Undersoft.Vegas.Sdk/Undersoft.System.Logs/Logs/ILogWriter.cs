using System.IO;

namespace System
{
    public interface ILogWriter
    {
        bool Write(string information);

        bool Clear(DateTime olderThen);
    }
}