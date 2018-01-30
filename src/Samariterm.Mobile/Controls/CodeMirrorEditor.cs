using System;
using System.Text;
using Juniansoft.Samariterm.Core.Resources;
using Xamarin.Forms;

namespace Juniansoft.Samariterm.Mobile.Controls
{
    public class CodeMirrorEditor: WebView
    {
        public CodeMirrorEditor()
        {
            var css = AppResources.CodeMirrorCss;

            var js = new StringBuilder();
            js.AppendLine(AppResources.CodeMirrorJs);
            //js.AppendLine(AppResources.CodeMirrorModeCLikeJs);
            js.AppendLine(AppResources.CodeMirrorModeJavascriptJs);

            var html = new StringBuilder();
            html.AppendLine(AppResources.CodeMirrorEditorHtml);
            html.Replace("/*codemirror.css*/", css);
            html.Replace("//codemirror.js", js.ToString());

            var htmlSource = new HtmlWebViewSource
            {
                Html = html.ToString()
            };

            this.Source = htmlSource;
            //this.Eval("document.getElementById('editor').style.fontSize='10px';");
        }
    }
}
