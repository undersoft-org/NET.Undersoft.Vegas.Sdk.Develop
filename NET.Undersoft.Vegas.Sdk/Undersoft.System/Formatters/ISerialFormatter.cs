using System.IO;

namespace System.Instant
{
    public interface ISerialFormatter
    {
        int SerialCount { get; set; }
        int DeserialCount { get; set; }
        int ProgressCount { get; set; }
        int ItemsCount { get; }

        int Serialize(Stream stream, int offset, int batchSize, SerialFormat serialFormat = SerialFormat.Binary);
        int Serialize(ISerialBlock buffor, int offset, int batchSize, SerialFormat serialFormat = SerialFormat.Binary);

        object Deserialize(Stream stream, SerialFormat serialFormat = SerialFormat.Binary);
        object Deserialize(ref object source, SerialFormat serialFormat = SerialFormat.Binary);

        object[] GetMessage();
        object GetHeader();
    }

    public interface IDealSource
    {
        object Emulate(object source, string name = null);
        object Impact(object source, string name = null);
        object Locate(object path = null);
    }

   
}