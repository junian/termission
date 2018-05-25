using System;
using Eto;
using Eto.GtkSharp;
using Eto.GtkSharp.Forms;
using Eto.GtkSharp.Forms.Controls;
using Juniansoft.MvvmReady;
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
            var platform = new Eto.GtkSharp.Platform();

            RegisterUIHandlers(platform);

            ConfigureStyles();

            var app = new MainApplication(platform);

            RegisterServices();

            app.Run(args);
        }

        private static void RegisterServices()
        {
            ServiceLocator.Current.Register<ICSharpBotEngine, CSharpMcsScriptEngine>();
        }

        private static void RegisterUIHandlers(Eto.GtkSharp.Platform platform)
        {
            platform.Add<SyntaxHightlightTextArea.ISyntaxHightlightTextArea>(() => new SyntaxHightlightTextAreaHandler());
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
