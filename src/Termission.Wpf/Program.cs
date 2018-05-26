using Eto;
using Eto.Forms;
using Juniansoft.MvvmReady;
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
		public static void Main(string[] args)
		{
            var platform = Eto.Platform.Get(Eto.Platforms.Wpf);

            RegisterUIHandlers(platform);

            ConfigureStyles();

            var app = new MainApplication(platform);

            RegisterServices();

            app.Run(args);
        }

        private static void RegisterServices()
        {
        }

        private static void ConfigureStyles()
        {
            //throw new NotImplementedException();
        }

        private static void RegisterUIHandlers(Platform platform)
        {
            platform.Add<SyntaxHightlightTextArea.ISyntaxHightlightTextArea>(() => new SyntaxHightlightTextAreaHandler());
            platform.Add<Notification.IHandler>(() => new Controls.NotificationHandler());
        }
    }
}
