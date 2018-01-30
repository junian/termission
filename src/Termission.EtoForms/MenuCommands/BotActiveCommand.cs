using System;
using Eto.Forms;
using Juniansoft.Termission.EtoForms.Resources;

namespace Juniansoft.Termission.EtoForms.MenuCommands
{
    public class BotActiveCommand: CheckCommand
    {
        public BotActiveCommand()
        {
            this.MenuText = "Activate";
            this.ToolBarText = "Activate";
            this.ToolTip = "Activate";
            this.Image = DesktopAppResources.MediaRecord;
        }
    }
}
