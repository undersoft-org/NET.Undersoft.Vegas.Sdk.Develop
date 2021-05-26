using System.IO;

namespace System
{
    public interface ILogWriter
    {
        void Write(string information);

        void Clear(DateTime olderThen);
    }
}