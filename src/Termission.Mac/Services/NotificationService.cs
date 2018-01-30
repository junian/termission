using System;
using Foundation;
using Juniansoft.Samariterm.Core.Services;

namespace Juniansoft.Samariterm.Mac.Services
{
    public class NotificationService: INotificationService
    {
        public NotificationService()
        {
        }

        public void Show(string title, string message)
        {
            // Trigger a local notification after the time has elapsed
            var notification = new NSUserNotification
            {
                // Add text and sound to the notification
                Title = title,
                InformativeText = message,
                SoundName = NSUserNotification.NSUserNotificationDefaultSoundName,
                HasActionButton = false
            };
            NSUserNotificationCenter.DefaultUserNotificationCenter.DeliverNotification(notification);
        }
    }
}
