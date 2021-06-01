using System.Threading;
using System.Collections.Concurrent;

namespace System
{
    public static class LogFormatter
    {
        public static string Format(int logLevel, String information)
        {
            return $"{logLevel.ToString()}#Information#{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}#"
                                                   + $"{DateTime.Now.Millisecond.ToString()}#"
                                                   + $"{information}";  
        }

        public static string Format(int logLevel, Exception exception, string information = null)
        {
            return $"{logLevel.ToString()}" + "#Exception#" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                                         + "#" +    DateTime.Now.Millisecond.ToString()
                                                                         + "#" +    exception.Message
                                                                         + "\r\n" + exception.Source
                                                                         + "\r\n" + exception.StackTrace
                                                                         + ((information != null) ? "\r\n"
                                                                         + "#Information#" + information : "");
        }
    }
}
