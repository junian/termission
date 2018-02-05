using System;
using System.IO;
using System.Reflection;

namespace Juniansoft.Termission.Core.Resources
{
    public static class AppResources
    {
        public static string DocumentationMd => GetString("DOCUMENTATION.md");
        public static string AcknowledgementsMd => GetString("ACKNOWLEDGEMENTS.md");

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
