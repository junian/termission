using System;
using System.Text;
using System.Threading.Tasks;
using Juniansoft.Termission.Core.Resources;
using Juniansoft.Termission.Mobile.Resources;
using Xamarin.Forms;

namespace Juniansoft.Termission.Mobile.Controls
{
    public class CodeMirrorEditor: WebView
    {
        public static BindableProperty EvaluateJavascriptProperty =
            BindableProperty.Create(
                nameof(EvaluateJavascript), 
                typeof(Func<string, Task<string>>), 
                typeof(CodeMirrorEditor), 
                null, 
                BindingMode.OneWayToSource);

        public Func<string, Task<string>> EvaluateJavascript
        {
            get { return (Func<string, Task<string>>)GetValue(EvaluateJavascriptProperty); }
            set { SetValue(EvaluateJavascriptProperty, value); }
        }

        public static BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(CodeMirrorEditor));
        
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

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
