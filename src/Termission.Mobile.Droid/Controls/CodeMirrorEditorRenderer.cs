using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Webkit;
using Juniansoft.Termission.Mobile.Controls;
using Juniansoft.Termission.Mobile.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CodeMirrorEditor), typeof(CodeMirrorEditorRenderer))]
namespace Juniansoft.Termission.Mobile.Droid.Controls
{
    public class CodeMirrorEditorRenderer: WebViewRenderer
    {
        public CodeMirrorEditorRenderer(Context ctx): base(ctx)
        {
        }

        internal class JavascriptCallback : Java.Lang.Object, IValueCallback
        {
            public JavascriptCallback(Action<string> callback)
            {
                _callback = callback;
            }

            private Action<string> _callback;
            public void OnReceiveValue(Java.Lang.Object value)
            {
                _callback?.Invoke(Convert.ToString(value));
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is CodeMirrorEditor webView)
            {
                webView.EvaluateJavascript = async (js) =>
                {
                    var reset = new ManualResetEvent(false);
                    var response = string.Empty;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Control?.EvaluateJavascript(js, new JavascriptCallback((r) => { response = r; reset.Set(); }));
                    });
                    await Task.Run(() => { reset.WaitOne(); });
                    return response;
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);


        }

    }
}
