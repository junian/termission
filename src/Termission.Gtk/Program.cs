using System;
using Eto;
using Eto.GtkSharp.Forms;
using Eto.GtkSharp.Forms.Controls;
using Juniansoft.Termission.Core;
using Juniansoft.Termission.Core.Constants;
using Juniansoft.Termission.Core.Engines.Scripts;
using Juniansoft.Termission.Core.Services;
using Juniansoft.Termission.EtoForms;
using Juniansoft.Termission.EtoForms.Controls;
using Juniansoft.Termission.EtoForms.Forms;
using Juniansoft.Termission.GtkSharp.Controls;
using Juniansoft.Termission.GtkSharp.Services;

namespace Juniansoft.Termission.GtkSharp
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Application.Init();
            //MainWindow win = new MainWindow();
            //win.Show();
            //Application.Run();

            var platform = new Eto.GtkSharp.Platform();

            platform.Add<SyntaxHightlightTextArea.ISyntaxHightlightTextArea>(() => new SyntaxHightlightTextAreaHandler());

            ConfigureStyles();

            var app = new MainApplication(platform);

            ServiceLocator.Current.Register<INotificationService, NotificationService>();
            ServiceLocator.Current.Register<ICSharpBotEngine, CSharpMcsScriptEngine>();

            app.Run(new MainForm());
        }

        private static void ConfigureStyles()
        {
            Style.Add<FormHandler>(EtoStyles.FormMain, handler =>
            {
                var control = handler.Control;
                control.WindowPosition = Gtk.WindowPosition.Center;
            });

            Style.Add<DialogHandler>(EtoStyles.DeviceBotDialog, handler =>
            {
                var control = handler.Control;
                control.WindowPosition = Gtk.WindowPosition.Center;
            });

            Style.Add<SyntaxHightlightTextAreaHandler>(EtoStyles.SourceEditor, handler =>
            {
                var control = handler.Control;
                control.ModifyFont(Pango.FontDescription.FromString($"Courier {handler.Font.Size}"));
            });

            Style.Add<TextAreaHandler>(EtoStyles.SendCommandText, handler =>
            {
                var control = handler.Control;
                control.ModifyFont(Pango.FontDescription.FromString($"Courier {handler.Font.Size}"));
            });
        }
    }
}
