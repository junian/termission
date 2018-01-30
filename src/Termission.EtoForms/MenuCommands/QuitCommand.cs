using Eto;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juniansoft.Termission.EtoForms.MenuCommands
{
    public class QuitCommand: Command
    {
        public QuitCommand()
        {
            this.MenuText = "&Quit";
            if (!Platform.Instance.IsWinForms)
                this.ToolBarText = "Quit";
            this.ToolTip = "Quit";
            this.Shortcut = Application.Instance.CommonModifier | Keys.Q;
        }
    }
}
