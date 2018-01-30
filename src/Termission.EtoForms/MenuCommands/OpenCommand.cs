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
    public class OpenCommand: Command
    {
        public OpenCommand()
        {
            this.MenuText = "&Open";
            if(!Platform.Instance.IsWinForms)
                this.ToolBarText = "Open";
            this.ToolTip = "Open";
            this.Shortcut = Application.Instance.CommonModifier | Keys.O;
            this.Image = DesktopAppResources.DocumentOpen;
        }
    }
}
