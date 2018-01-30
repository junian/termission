using AppKit;
using Eto;
using Eto.Mac.Forms;
using Eto.Mac.Forms.Controls;
using Eto.Mac.Forms.ToolBar;
using Juniansoft.Termission.Core;
using Juniansoft.Termission.Core.Constants;
using Juniansoft.Termission.Core.Engines.Scripts;
using Juniansoft.Termission.Core.Services;
using Juniansoft.Termission.EtoForms;
using Juniansoft.Termission.EtoForms.Controls;
using Juniansoft.Termission.EtoForms.Forms;
using Juniansoft.Termission.Mac.Controls;
using Juniansoft.Termission.Mac.Services;

namespace Juniansoft.Termission.Mac
{
    static class Program
    {
        static void Main(string[] args)
        {
            //NSApplication.Init();
            //NSApplication.Main(args);

            var platform = new Eto.Mac.Platform();

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
                control.Center();
            });

            Style.Add<DialogHandler>(EtoStyles.DeviceBotDialog, handler =>
            {
                var control = handler.Control;
                control.Center();
            });

            Style.Add<SyntaxHightlightTextAreaHandler>(EtoStyles.SourceEditor, handler =>
            {
                var control = handler.Control;
                control.Font = NSFont.FromFontName("Monaco", control.Font.PointSize);
                control.AutomaticQuoteSubstitutionEnabled = false;
            });

            Style.Add<TextAreaHandler>(EtoStyles.SendCommandText, handler =>
            {
                var control = handler.Control;
                control.Font = NSFont.FromFontName("Monaco", control.Font.PointSize);
                control.AutomaticQuoteSubstitutionEnabled = false;
            });

            Style.Add<ToolBarHandler>(EtoStyles.Toolbar, handler => {
                var control = handler.Control;
                control.DisplayMode = NSToolbarDisplayMode.IconAndLabel;
                control.SizeMode = NSToolbarSizeMode.Regular;
                control.ShowsBaselineSeparator = false;
            });

            Style.Add<ButtonToolItemHandler>(EtoStyles.ButtonToolItem, handler => {
                var control = handler.Control;
                var x = control.View as NSButton;
                x.BezelStyle = NSBezelStyle.Disclosure;
            });
        }
    }
}
