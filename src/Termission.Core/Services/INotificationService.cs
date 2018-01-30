using System;
namespace Juniansoft.Termission.Core.Services
{
    public interface INotificationService
    {
        void Show(string title, string message);
    }
}
