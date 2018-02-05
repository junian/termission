using System;
using System.IO;
using System.Reflection;

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
    }
}
