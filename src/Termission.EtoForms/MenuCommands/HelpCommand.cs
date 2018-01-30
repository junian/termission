using System;
using Eto;
using Eto.Forms;

namespace Juniansoft.Termission.EtoForms.MenuCommands
{
    public class HelpCommand: Command
    {
        public HelpCommand()
        {
            this.MenuText = "&Documentation";
            if (!Platform.Instance.IsWinForms)
                this.ToolBarText = "Documentation";
            this.ToolTip = "Documentation";
            //this.Image = DesktopAppResources.DocumentNew;
            this.Shortcut = Application.Instance.CommonModifier | Keys.Semicolon;
        }
    }
}
