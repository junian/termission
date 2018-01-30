using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;

namespace Juniansoft.Samariterm.EtoForms.MenuCommands
{
    public class AboutCommand: Command
    {
        public AboutCommand()
        {
            this.MenuText = $"About {MainApplication.AssemblyProduct}";
            this.ToolBarText = "About";
            this.ToolTip = "About";
        }
    }
}
