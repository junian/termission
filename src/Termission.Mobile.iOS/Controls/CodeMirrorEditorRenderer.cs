using System;
using System.Threading.Tasks;
using Juniansoft.Termission.Mobile.Controls;
using Juniansoft.Termission.Mobile.iOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CodeMirrorEditor), typeof(CodeMirrorEditorRenderer))]
namespace Juniansoft.Termission.Mobile.iOS.Controls
{
    public class CodeMirrorEditorRenderer: WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is CodeMirrorEditor webView)
            {
                webView.EvaluateJavascript = (js) =>
                {
                    return Task.FromResult(this.EvaluateJavascript(js));
                };
            }
        }
    }
}
