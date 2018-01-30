using System;
namespace Juniansoft.Samariterm.Core.Services
{
    public interface INotificationService
    {
        void Show(string title, string message);
    }
}
