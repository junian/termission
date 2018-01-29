using System;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using Juniansoft.Samariterm.EtoForms.Resources;

namespace Juniansoft.Samariterm.EtoForms.Forms
{
    public class AboutForm : Dialog
    {
        public AboutForm()
        {
            var title = MainApplication.AssemblyProduct;

            /* dialog attributes */

            this.Title = $"About {title}";
            this.MinimumSize = new Size(300, 0);
            this.Resizable = false;

            /* dialog controls */

            var imageView = new ImageView
            {
                Image = AppResources.Terminal,
                Size = new Size(128, 128)
            };

            var labelTitle = new Label
            {
                Text = title,
                Font = new Font(FontFamilies.Sans, 16),
                TextAlignment = TextAlignment.Center
            };

            var version = MainApplication.AssemblyVersion;
            var labelVersion = new Label
            {
                Text = string.Format("Version {0}", version),
                TextAlignment = TextAlignment.Center
            };

            var copyright = MainApplication.AssemblyCopyright;
            var company = MainApplication.AssemblyCompany;
            var labelCopyright = new Label
            {
                Text = $"{copyright} by {company}",
                TextAlignment = TextAlignment.Center
            };

            var button = new Button
            {
                Text = "Close"
            };
            button.Click += (sender, e) => Close();

            /* dialog layout */

            Content = new TableLayout
            {
                Padding = new Padding(10),
                Spacing = new Size(5, 5),
                Rows =
                {
                    imageView, labelTitle, labelVersion, labelCopyright,
                    TableLayout.AutoSized(button, centered: true)
                }
            };

            AbortButton = DefaultButton = button;
        }
    }
}
