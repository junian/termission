using System;
using Juniansoft.Termission.Core.Resources;
using Markdig;

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
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var result = Markdown.ToHtml(md, pipeline);
            return result;
        }
    }
}
