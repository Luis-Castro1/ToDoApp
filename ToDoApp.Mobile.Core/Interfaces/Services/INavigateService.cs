namespace ToDoApp.Mobile.Core.Interfaces.Services
{
    public interface INavigateService
    {
        Task NavigateToAsync(string route, bool animated = true);
    }
}
