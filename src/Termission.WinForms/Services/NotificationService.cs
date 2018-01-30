using Juniansoft.Samariterm.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juniansoft.Samariterm.WinForms.Services
{
    public class NotificationService : INotificationService
    {
        private NotifyIcon _notifyIcon;

        public NotificationService()
        {
            _notifyIcon = new NotifyIcon()
            {
                BalloonTipTitle = "SDEmu",
                //Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon"))),
                Text = "SDEmu",
                Visible = true,
            };
        }

        public void Show(string title, string message)
        {
            _notifyIcon.BalloonTipTitle = title;
            _notifyIcon.BalloonTipText = message;
            _notifyIcon.ShowBalloonTip(250);
        }
    }
}
