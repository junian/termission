using System;
using Eto.Drawing;

namespace Juniansoft.Samariterm.EtoForms.Resources
{
    public static class AppResources
    {
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
            return Bitmap.FromResource($"{typeof(AppResources).Namespace}.{name}.png");
        }
    }
}
