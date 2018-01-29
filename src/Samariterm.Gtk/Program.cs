using System;
using Eto;
using Eto.GtkSharp.Forms;
using Eto.GtkSharp.Forms.Controls;
using Juniansoft.Samariterm.Core;
using Juniansoft.Samariterm.Core.Constants;
using Juniansoft.Samariterm.Core.Engines.Scripts;
using Juniansoft.Samariterm.Core.Services;
using Juniansoft.Samariterm.EtoForms;
using Juniansoft.Samariterm.EtoForms.Controls;
using Juniansoft.Samariterm.EtoForms.Forms;
using Juniansoft.Samariterm.GtkSharp.Controls;
using Juniansoft.Samariterm.GtkSharp.Services;

namespace Juniansoft.Samariterm.GtkSharp
{
    class MainClass
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

            ServiceLocator.Instance.Register<INotificationService, NotificationService>();
            ServiceLocator.Instance.Register<ICSharpBotEngine, CSharpMcsScriptEngine>();

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
