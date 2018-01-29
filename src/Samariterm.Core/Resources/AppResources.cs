using System;
using System.IO;
using System.Reflection;

namespace Juniansoft.Samariterm.Core.Resources
{
    public static class AppResources
    {
        public static string AceEditorHtml => GetString("AceEditor.html");
        public static string AceJS => GetString("ace.js");

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
