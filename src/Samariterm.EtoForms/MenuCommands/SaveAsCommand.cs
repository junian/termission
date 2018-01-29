using Eto;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juniansoft.Samariterm.EtoForms.Resources;

namespace Juniansoft.Samariterm.EtoForms.MenuCommands
{
    public class SaveAsCommand: Command
    {
        public SaveAsCommand()
        {
            this.MenuText = "Save As...";
            if (!Platform.Instance.IsWinForms)
                this.ToolBarText = "Save As";
            this.ToolTip = "Save As";
            this.Image = DesktopAppResources.DocumentSaveAs;
        }
    }
}
