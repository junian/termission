using System;
using Juniansoft.Samariterm.Core.Events;

namespace Juniansoft.Samariterm.Core.ViewModels
{
    public class CoreViewModel: BaseViewModel
    {
        public event EventHandler<LogReceivedEventArgs> LogReceived;

        protected void OnLogReceived(object message, string logLevel = "INFO")
        {
            LogReceived?.Invoke(this, new LogReceivedEventArgs(message?.ToString(), logLevel));
        }
    }
}
