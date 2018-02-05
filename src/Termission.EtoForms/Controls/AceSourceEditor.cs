using System;
using System.Text;
using Eto.Forms;
using Juniansoft.Termission.Core.Resources;
using Juniansoft.Termission.EtoForms.Resources;

namespace Juniansoft.Termission.EtoForms.Controls
{
    public class AceSourceEditor: WebView
    {
        public AceSourceEditor()
        {
            var js = new StringBuilder();
            //js.AppendLine(DesktopAppResources.AceJS);
            //js.AppendLine(DesktopAppResources.AceModeJavascriptJs);
            //js.AppendLine(DesktopAppResources.AceModeCSharpJs);
            //js.AppendLine(DesktopAppResources.AceLanguageToolsJS);
            //js.AppendLine(DesktopAppResources.AceEditorJS);

            var html = new StringBuilder();
            //html.AppendLine(DesktopAppResources.AceEditorHtml);
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
