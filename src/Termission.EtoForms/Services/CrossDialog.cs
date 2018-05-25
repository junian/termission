using System;
using System.Threading.Tasks;
using Eto.Forms;
using Juniansoft.Termission.Core.Services;

namespace Juniansoft.Termission.EtoForms.Services
{
    public class CrossDialog: ICrossDialog, INotificationService
    {
        public Task<string> ShowOpenDialogAsync(string extension = "json", string typename = "JSON Files")
        {
            return Task.FromResult<string>(ShowOpenDialog(extension, typename));
        }

        public string ShowOpenDialog(string extension = "json", string typename = "JSON Files")
        {
            var openFileDialog = new OpenFileDialog
            {
                MultiSelect = false,
                Filters =
                {
                    new FileDialogFilter(typename, $".{extension}"),
                },
            };
            if (openFileDialog.ShowDialog(null) == DialogResult.Ok)
            {
                return openFileDialog.FileName;
            }
            return string.Empty;
        }

        public string ShowSaveDialog(string extension = "json", string typename = "JSON Files")
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filters =
                {
                    new FileDialogFilter(typename, $".{extension}"),
                },
            };
            if (saveFileDialog.ShowDialog(null) == DialogResult.Ok)
            {
                return saveFileDialog.FileName;
            }
            return string.Empty;
        }

        public Task<string> ShowSaveDialogAsync(string extension = "json", string typename = "JSON Files")
        {
            return Task.FromResult<string>(ShowSaveDialog(extension, typename));
        }

        public async Task ShowMessageBoxAsync(string message)
        {
            await Task.Run(() => MessageBox.Show(message));
        }

        public void Show(string title, string message)
        {
            MessageBox.Show(message, title);
        }
    }
}
