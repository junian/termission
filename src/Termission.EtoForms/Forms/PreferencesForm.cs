using Eto.Forms;
using Juniansoft.Termission.Core.ViewModels;
using Juniansoft.Termission.EtoForms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juniansoft.MvvmReady;

namespace Juniansoft.Termission.EtoForms.Forms
{
    public class PreferencesForm : Dialog
    {
        public PreferencesForm()
        {
            var vm = ServiceLocator.Current.Get<PreferencesViewModel>();
            var view = ServiceLocator.Current.Get<PreferencesView>();

            this.Content = view;
            this.Title = "Preferences";
            this.DataContext = vm;
        }
    }
}
