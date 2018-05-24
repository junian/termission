using System;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;
using Juniansoft.Termission.Core;
using Juniansoft.Termission.EtoForms.Resources;

namespace Juniansoft.Termission.EtoForms.Forms
{
    public class AboutForm : Dialog
    {
        public AboutForm()
        {
            var title = CoreApp.AssemblyProduct;

            /* dialog attributes */

            this.Title = $"About {title}";
            this.MinimumSize = new Size(300, 0);
            this.Resizable = false;

            /* dialog controls */

            var imageView = new ImageView
            {
                Image = DesktopAppResources.DevAppLogo,
                Size = new Size(128, 128)
            };

            var labelTitle = new Label
            {
                Text = title,
                Font = new Font(FontFamilies.Sans, 16),
                TextAlignment = TextAlignment.Center
            };

            var version = CoreApp.AssemblyVersion;
            var labelVersion = new Label
            {
                Text = string.Format("Version {0}", version),
                TextAlignment = TextAlignment.Center
            };

            var copyright = CoreApp.AssemblyCopyright;
            var company = CoreApp.AssemblyCompany;
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
