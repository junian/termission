using System;
using System.IO;
using System.Reflection;
using Eto.Drawing;

namespace Juniansoft.Termission.EtoForms.Resources
{
    public static class DesktopAppResources
    {
        public static string AceEditorHtml => GetString("ace-editor.html");
        public static string AceEditorJS => GetString("ace-editor.js");
        public static string AceJS => GetString("ace.js");
        public static string AceLanguageToolsJS => GetString("ace-ext-language_tools.js");
        public static string AceModeJavascriptJs => GetString("ace-mode-javascript.js");
        public static string AceModeCSharpJs => GetString("ace-mode-csharp.js");

        public static Image DocumentNew => FromResource(nameof(DocumentNew));
        public static Image DocumentOpen => FromResource(nameof(DocumentOpen));
        public static Image DocumentSave => FromResource(nameof(DocumentSave));
        public static Image DocumentSaveAs => FromResource(nameof(DocumentSaveAs));
        public static Image MediaStart => FromResource(nameof(MediaStart));
        public static Image MediaRecord => FromResource(nameof(MediaRecord));
        public static Image Preferences => FromResource(nameof(Preferences));
        public static Image Refresh => FromResource(nameof(Refresh));
        public static Image Terminal => FromResource(nameof(Terminal));

        private static Bitmap FromResource(string name)
        {
            return Bitmap.FromResource($"{typeof(DesktopAppResources).Namespace}.{name}.png");
        }

        private static string GetString(string name)
        {
            var type = typeof(DesktopAppResources);
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
