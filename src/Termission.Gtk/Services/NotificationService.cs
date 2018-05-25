using System;
using Juniansoft.Termission.Core.Services;

namespace Juniansoft.Termission.GtkSharp.Services
{
    [Obsolete("Use EtoForms NotificationService instead")]
    public class NotificationService : INotificationService
    {
        public NotificationService()
        {
        }

        public void Show(string title, string message)
        {

        }
    }
}
