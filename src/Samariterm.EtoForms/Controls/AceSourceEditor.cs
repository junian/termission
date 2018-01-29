using System;
using Eto.Forms;
using Juniansoft.Samariterm.Core.Resources;

namespace Juniansoft.Samariterm.EtoForms.Controls
{
    public class AceSourceEditor: WebView
    {
        public AceSourceEditor()
        {
            this.LoadHtml(AppResources.AceEditorHtml);
        }
    }
}
