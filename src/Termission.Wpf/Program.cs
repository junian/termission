using Juniansoft.Termission.Core;
using Juniansoft.Termission.Core.Services;
using Juniansoft.Termission.EtoForms;
using Juniansoft.Termission.EtoForms.Controls;
using Juniansoft.Termission.EtoForms.Forms;
using Juniansoft.Termission.Wpf.Controls;
using Juniansoft.Termission.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juniansoft.Termission.Wpf
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var platform = Eto.Platform.Get(Eto.Platforms.Wpf);

            platform.Add<SyntaxHightlightTextArea.ISyntaxHightlightTextArea>(() => new SyntaxHightlightTextAreaHandler());

            //ConfigureStyles();

            var app = new MainApplication(platform);

            ServiceLocator.Current.Register<INotificationService, NotificationService>();

            app.Run(new MainForm());
        }
    }
}
