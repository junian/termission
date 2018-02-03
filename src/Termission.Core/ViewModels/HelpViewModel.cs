using System;
using Juniansoft.Termission.Core.Resources;

namespace Juniansoft.Termission.Core.ViewModels
{
    public class HelpViewModel: BaseViewModel
    {
        public HelpViewModel()
        {
        }

        public string Content
        {
            get => LoadHelpContent(AppResources.DocumentationMd);
        }

        private string LoadHelpContent(string md)
        {
            var result = CommonMark.CommonMarkConverter.Convert(md);
            return result;
        }
    }
}
