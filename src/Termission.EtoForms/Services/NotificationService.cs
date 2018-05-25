using System;
using Eto.Forms;
using Juniansoft.Termission.Core.Services;

namespace Juniansoft.Termission.EtoForms.Services
{
    public class NotificationService: INotificationService
    {
        public void Show(string title, string message)
        {
            var notification = new Notification
            {
                ID = Guid.NewGuid().ToString(),
                Title = title,
                Message = message
            };

            notification.Show(MainApplication.TrayIndicator);
        }
    }
}
