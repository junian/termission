using Eto;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juniansoft.Termission.EtoForms.Resources;

namespace Juniansoft.Termission.EtoForms.MenuCommands
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
