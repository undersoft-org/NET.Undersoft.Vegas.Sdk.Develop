
namespace System
{
    public interface ISerialBlock
    {
        ServiceSite Site { get; }

        long BlockSize { get; set; }
        int  BlockOffset { get; set; }

        byte[] SerialBlock { get; set; }
        IntPtr SerialBlockPtr { get; }
        int    SerialBlockId { get; set; }

        byte[] DeserialBlock { get; }
        IntPtr DeserialBlockPtr { get; }      
        int    DeserialBlockId { get; set; }
    }
}



