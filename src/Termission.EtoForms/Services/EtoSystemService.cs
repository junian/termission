using System;
using Eto.Forms;

namespace Juniansoft.Termission.EtoForms.Services
{
    public class EtoSystemService: Juniansoft.Termission.Core.Services.SystemService
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
