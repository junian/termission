using Eto.Forms;
using Juniansoft.Samariterm.Core.ViewModels;
using Juniansoft.Samariterm.EtoForms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juniansoft.Samariterm.EtoForms.Forms
{
    public class PreferencesForm : Dialog
    {
        public PreferencesForm()
        {
            var vm = Core.ServiceLocator.Instance.Get<PreferencesViewModel>();
            var view = Core.ServiceLocator.Instance.Get<PreferencesView>();

            this.Content = view;
            this.Title = "Preferences";
            this.DataContext = vm;
        }
    }
}
