using System;
using System.IO;
using System.Reflection;

namespace Juniansoft.Termission.Core.Resources
{
    public static class AppResources
    {
        public static string AceEditorHtml => GetString("ace-editor.html");
        public static string AceEditorJS => GetString("ace-editor.js");
        public static string AceJS => GetString("ace.js");
        public static string AceLanguageToolsJS => GetString("ace-ext-language_tools.js");
        public static string AceModeJavascriptJs => GetString("ace-mode-javascript.js");
        public static string AceModeCSharpJs => GetString("ace-mode-csharp.js");

        public static string CodeMirrorEditorHtml => GetString("codemirror-editor.html");
        public static string CodeMirrorCss => GetString("codemirror.css");
        public static string CodeMirrorJs => GetString("codemirror.js");
        public static string CodeMirrorModeCLikeJs => GetString("codemirror-mode-clike.js");
        public static string CodeMirrorModeJavascriptJs => GetString("codemirror-mode-javascript.js");

        private static string GetString(string name)
        {
            var type = typeof(AppResources);
            var assembly = type.GetTypeInfo().Assembly;
            var content = "";
            using (var s = assembly.GetManifestResourceStream($"{type.Namespace}.{name}"))
            using(var r = new StreamReader(s))
            {
                content = r.ReadToEnd();
            }
            return content;
        }
    }
}
