using System;
using System.IO;
using System.Reflection;
using Eto.Drawing;

namespace Juniansoft.Termission.EtoForms.Resources
{
    public static class DesktopAppResources
    {
        //public static string AceEditorHtml => GetString("ace-editor.html");
        //public static string AceEditorJS => GetString("ace-editor.js");
        //public static string AceJS => GetString("ace.js");
        //public static string AceLanguageToolsJS => GetString("ace-ext-language_tools.js");
        //public static string AceModeJavascriptJs => GetString("ace-mode-javascript.js");
        //public static string AceModeCSharpJs => GetString("ace-mode-csharp.js");

        public static Image DocumentNew => ImageFromResource(nameof(DocumentNew));
        public static Image DocumentOpen => ImageFromResource(nameof(DocumentOpen));
        public static Image DocumentSave => ImageFromResource(nameof(DocumentSave));
        public static Image DocumentSaveAs => ImageFromResource(nameof(DocumentSaveAs));
        public static Image MediaStart => ImageFromResource(nameof(MediaStart));
        public static Image MediaRecord => ImageFromResource(nameof(MediaRecord));
        public static Image Preferences => ImageFromResource(nameof(Preferences));
        public static Image Refresh => ImageFromResource(nameof(Refresh));
        public static Image Terminal => ImageFromResource(nameof(Terminal));
        public static Image DevAppLogo => ImageFromResource(nameof(DevAppLogo));
        public static Icon DevAppIcon => IconFromResource(nameof(DevAppIcon));

        private static Bitmap ImageFromResource(string name)
        {
            return Bitmap.FromResource($"{typeof(DesktopAppResources).Namespace}.{name}.png");
        }

        private static Icon IconFromResource(string name)
        {
            return Icon.FromResource($"{typeof(DesktopAppResources).Namespace}.{name}.ico");
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
