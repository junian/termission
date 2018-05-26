using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eto;
using Eto.Forms;
using Eto.WinForms.Forms;
using Eto.WinForms.Forms.Controls;
using Juniansoft.MvvmReady;
using Juniansoft.Termission.Core;
using Juniansoft.Termission.Core.Constants;
using Juniansoft.Termission.Core.Services;
using Juniansoft.Termission.EtoForms;
using Juniansoft.Termission.EtoForms.Controls;
using Juniansoft.Termission.EtoForms.Forms;
using Juniansoft.Termission.WinForms.Controls;
using Juniansoft.Termission.WinForms.Services;

namespace Juniansoft.Termission.WinForms
{
	class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
            var platform = Eto.Platform.Get(Eto.Platforms.WinForms);

            RegisterUIHandlers(platform);

            ConfigureStyles();

            var app = new MainApplication(platform);

            RegisterServices();

            app.Run(args);
        }

        private static void RegisterServices()
        {
        }

        private static void RegisterUIHandlers(Platform platform)
        {
            platform.Add<Notification.IHandler>(() => new Controls.NotificationHandler());
            platform.Add<SyntaxHightlightTextArea.ISyntaxHightlightTextArea>(() => new SyntaxHightlightTextAreaHandler());
        }

        private static void ConfigureStyles()
        {
            Style.Add<FormHandler>(EtoStyles.FormMain, handler =>
            {
                var control = handler.Control;
                control.StartPosition = FormStartPosition.CenterScreen;
            });

            Style.Add<DialogHandler>(EtoStyles.DeviceBotDialog, handler =>
            {
                var control = handler.Control;
                control.StartPosition = FormStartPosition.CenterScreen;
            });

            Style.Add<SyntaxHightlightTextAreaHandler>(EtoStyles.SourceEditor, handler =>
            {
                var txtBotEditor = handler.Control;
                txtBotEditor.AutoCompleteBracketsList = new char[] {
                        '(',
                        ')',
                        '{',
                        '}',
                        '[',
                        ']',
                        '\"',
                        '\"',
                        '\'',
                        '\''};
                txtBotEditor.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
                "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
                txtBotEditor.AutoScrollMinSize = new System.Drawing.Size(227, 70);
                txtBotEditor.BackBrush = null;
                txtBotEditor.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
                txtBotEditor.CharHeight = 14;
                txtBotEditor.CharWidth = 8;
                txtBotEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
                txtBotEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
                txtBotEditor.Font = new System.Drawing.Font("Consolas", 9.75F);
                txtBotEditor.IsReplaceMode = false;
                txtBotEditor.LeftBracket = '(';
                txtBotEditor.LeftBracket2 = '{';
                txtBotEditor.Location = new System.Drawing.Point(12, 33);
                txtBotEditor.Name = "txtBotEditor";
                txtBotEditor.Paddings = new System.Windows.Forms.Padding(0);
                txtBotEditor.RightBracket = ')';
                txtBotEditor.RightBracket2 = '}';
                txtBotEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
                txtBotEditor.Size = new System.Drawing.Size(529, 345);
                txtBotEditor.TabIndex = 4;
                txtBotEditor.Zoom = 100;
            });

            Style.Add<TextAreaHandler>(EtoStyles.SendCommandText, handler =>
            {
                var control = handler.Control;
                control.Font = new Font(FontFamily.GenericMonospace, control.Font.Size);
            });
        }
    }
}
