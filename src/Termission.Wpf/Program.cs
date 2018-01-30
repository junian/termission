using Juniansoft.Samariterm.Core;
using Juniansoft.Samariterm.Core.Services;
using Juniansoft.Samariterm.EtoForms;
using Juniansoft.Samariterm.EtoForms.Controls;
using Juniansoft.Samariterm.EtoForms.Forms;
using Juniansoft.Samariterm.Wpf.Controls;
using Juniansoft.Samariterm.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juniansoft.Samariterm.Wpf
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

            ServiceLocator.Instance.Register<INotificationService, NotificationService>();

            app.Run(new MainForm());
        }
    }
}
