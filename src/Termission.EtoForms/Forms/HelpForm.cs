using System;
using Eto.Forms;
using Juniansoft.MvvmReady;
using Juniansoft.Termission.Core.ViewModels;

namespace Juniansoft.Termission.EtoForms.Forms
{
    public class HelpForm: Dialog
    {
        private HelpViewModel _helpVm;
        public HelpForm()
        {
            _helpVm = ServiceLocator.Current.Get<HelpViewModel>();

            var webView = new WebView();
            webView.LoadHtml(_helpVm.Content);
            Title = "Help";
            Width = 800;
            Height = 600;

            Content = webView;
        }
    }
}
