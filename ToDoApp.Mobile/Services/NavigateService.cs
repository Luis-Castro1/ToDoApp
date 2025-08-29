using ToDoApp.Mobile.Core.Interfaces.Services;

namespace ToDoApp.Mobile.Services
{
    public class NavigateService : INavigateService
    {
        public async Task NavigateToAsync(string route, bool animated = true)
        {
            await Shell.Current.GoToAsync(route, animated);
        }
    }
}
