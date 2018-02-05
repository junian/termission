using System;
using System.Text;
using Juniansoft.Termission.Core.Resources;
using Juniansoft.Termission.Mobile.Resources;
using Xamarin.Forms;

namespace Juniansoft.Termission.Mobile.Controls
{
    public class CodeMirrorEditor: WebView
    {
        public CodeMirrorEditor()
        {
            var css = MobileAppResources.CodeMirrorCss;

            var js = new StringBuilder();
            js.AppendLine(MobileAppResources.CodeMirrorJs);
            //js.AppendLine(AppResources.CodeMirrorModeCLikeJs);
            js.AppendLine(MobileAppResources.CodeMirrorModeJavascriptJs);

            var html = new StringBuilder();
            html.AppendLine(MobileAppResources.CodeMirrorEditorHtml);
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
