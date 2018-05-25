using Juniansoft.Termission.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juniansoft.Termission.Wpf.Services
{
    [Obsolete("Use EtoForms NotificationService instead")]
    public class NotificationService : INotificationService
    {
        public void Show(string title, string message)
        {

        }
    }
}
