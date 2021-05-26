namespace System
{
    public interface ILogs
    { 
        void Write(int logLevel, String information);
             
        void Write(int logLevel, Exception exception, string information = null);
    }
}