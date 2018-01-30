using System;
using System.Threading.Tasks;

namespace Juniansoft.Termission.Core.Services
{
    public interface ICrossDialog
    {
        string ShowOpenDialog(string extension = "json", string typename = "JSON Files");
        Task<string> ShowOpenDialogAsync(string extension = "json", string typename = "JSON Files");
        string ShowSaveDialog(string extension = "json", string typename = "JSON Files");
        Task<string> ShowSaveDialogAsync(string extension = "json", string typename = "JSON Files");
        Task ShowMessageBoxAsync(string message);
    }
}
