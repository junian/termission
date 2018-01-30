using System;
using Juniansoft.Termission.Core.Events;

namespace Juniansoft.Termission.Core.ViewModels
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
