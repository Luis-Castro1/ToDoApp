using ToDoApp.Mobile.Core.Common.Enums;

namespace ToDoApp.Mobile.Core.Interfaces.Services
{
    public interface INotificationService
    {
        Task ShowNotificationAsync(string message, TypeNotification type);
        Task ShowAlertAsync(string message, TypeNotification type);
    }
}