using System;
using System.Text;
using Eto.Forms;
using Juniansoft.Samariterm.Core.Resources;

namespace Juniansoft.Samariterm.EtoForms.Controls
{
    public class AceSourceEditor: WebView
    {
        public AceSourceEditor()
        {
            var js = new StringBuilder();
            js.AppendLine(AppResources.AceJS);
            js.AppendLine(AppResources.ModeJavascriptJs);
            js.AppendLine(AppResources.ModeCSharpJs);
            js.AppendLine(AppResources.LanguageToolsJS);
            js.AppendLine(AppResources.AceEditorJS);

            var html = new StringBuilder();
            html.AppendLine(AppResources.AceEditorHtml);
            html.Replace("//ace.js", js.ToString());

            this.LoadHtml(html.ToString());

            this.BrowserContextMenuEnabled = false;

        }

        public string Text
        {
            get => this.ExecuteScript("editor.getValue();");

            set => this.ExecuteScript($"editor.setValue(\"{value}\");");
        }
    }
}
