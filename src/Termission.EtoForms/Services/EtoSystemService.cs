using System;
using Eto.Forms;

namespace Juniansoft.Samariterm.EtoForms.Services
{
    public class EtoSystemService: Juniansoft.Samariterm.Core.Services.SystemService
    {
        public EtoSystemService()
        {
        }

        public override void Quit()
        {
            Application.Instance.Quit();
        }
    }
}
