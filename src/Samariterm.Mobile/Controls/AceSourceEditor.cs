using System;
using System.Text;
using Juniansoft.Samariterm.Core.Resources;
using Xamarin.Forms;

namespace Juniansoft.Samariterm.Mobile.Controls
{
    public class AceSourceEditor: WebView
    {
        public AceSourceEditor()
        {
            var js = new StringBuilder();
            js.AppendLine(AppResources.AceJS);
            js.AppendLine(AppResources.AceModeJavascriptJs);
            js.AppendLine(AppResources.AceModeCSharpJs);
            js.AppendLine(AppResources.AceLanguageToolsJS);
            js.AppendLine(AppResources.AceEditorJS);

            var html = new StringBuilder();
            html.AppendLine(AppResources.AceEditorHtml);
            html.Replace("//ace.js", js.ToString());

            var htmlSource = new HtmlWebViewSource
            {
                Html = html.ToString()
            };

            this.Source = htmlSource;
            this.Eval("document.getElementById('editor').style.fontSize='10px';");
        }
    }
}
