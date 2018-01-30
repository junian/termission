using System;
using Eto.Forms;
using Juniansoft.Samariterm.EtoForms.Resources;

namespace Juniansoft.Samariterm.EtoForms.MenuCommands
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
