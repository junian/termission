using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace Juniansoft.Termission.Mobile.Resources
{
    public static class MobileAppResources
    {
        public static string CodeMirrorCss => GetString("codemirror.css");
        public static string CodeMirrorJs => GetString("codemirror.js");
        public static string CodeMirrorModeJavascriptJs => GetString("codemirror-mode-javascript.js");
        public static string CodeMirrorEditorHtml => GetString("codemirror-editor.html");

        private static string GetString(string name)
        {
            var type = typeof(MobileAppResources);
            var assembly = type.GetTypeInfo().Assembly;
            var content = "";
            using (var s = assembly.GetManifestResourceStream($"{type.Namespace}.{name}"))
            using (var r = new StreamReader(s))
            {
                content = r.ReadToEnd();
            }
            return content;
        }

        public static ImageSource DevAppLogo => GetImage(nameof(DevAppLogo));

        private static ImageSource GetImage(string name)
        {
            var type = typeof(MobileAppResources);
            var assembly = type.GetTypeInfo().Assembly;
            return ImageSource.FromResource($"{type.Namespace}.{name}.png", assembly);
        }
    }
}
