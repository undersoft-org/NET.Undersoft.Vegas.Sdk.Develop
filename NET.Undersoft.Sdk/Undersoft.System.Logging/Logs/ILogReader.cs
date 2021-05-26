using System.IO;

namespace System
{
    public interface ILogReader
    {
        LogMessage[] Read(DateTime afterDate);

        void Clear(DateTime olderThen);
    }
}