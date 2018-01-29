using AppKit;
using Eto;
using Eto.Mac.Forms;
using Eto.Mac.Forms.Controls;
using Eto.Mac.Forms.ToolBar;
using Juniansoft.Samariterm.Core;
using Juniansoft.Samariterm.Core.Constants;
using Juniansoft.Samariterm.Core.Engines.Scripts;
using Juniansoft.Samariterm.Core.Services;
using Juniansoft.Samariterm.EtoForms;
using Juniansoft.Samariterm.EtoForms.Controls;
using Juniansoft.Samariterm.EtoForms.Forms;
using Juniansoft.Samariterm.Mac.Controls;
using Juniansoft.Samariterm.Mac.Services;

namespace Samariterm.Mac
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            //NSApplication.Init();
            //NSApplication.Main(args);

            var platform = new Eto.Mac.Platform();

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
