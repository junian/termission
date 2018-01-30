using System;
namespace Juniansoft.Termission.Core.Events
{
    public class LogReceivedEventArgs: EventArgs
    {
        public DateTime TimeStamp { get; internal set; }
        public string Message { get; internal set; }
        public string LogLevel { get; internal set; }

        public LogReceivedEventArgs(string message, string loglevel = "INFO")
        {
            Message = message;
            LogLevel = loglevel;
            TimeStamp = DateTime.Now;
        }
    }
}
