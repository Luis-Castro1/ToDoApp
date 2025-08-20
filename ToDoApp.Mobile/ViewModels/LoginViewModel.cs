using CommunityToolkit.Maui.Alerts;
using MauiIcons.Material;
using ToDoApp.Mobile.Core.Common.Enums;
using ToDoApp.Mobile.Core.Interfaces.Services;
using ToDoApp.Mobile.Core.Interfaces.ViewModels;
using ToDoApp.Mobile.Helpers;
using ToDoApp.Mobile.Services;

namespace ToDoApp.Mobile.ViewModels
{
    public partial class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        private readonly IAuthService _authService;
        private readonly INotificationService _notificationService;
        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
            _notificationService = ServiceHelper.GetTService<INotificationService>();
        }

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private bool _showPassword = true;

        [ObservableProperty]
        private MaterialIcons _iconPassword = MaterialIcons.Visibility;


        [RelayCommand]
        public async Task LoginAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_email) || string.IsNullOrWhiteSpace(_password))
                {
                    throw new ArgumentException("Email and password cannot be empty.");
                }

                IsBusy = true;
                var response = await _authService.LoginAsync(_email, _password);

                if (response == null || !response.IsAuthenticated)
                {
                    throw new Exception("Login failed. Please check your credentials.");
                }

                await _notificationService.ShowNotificationAsync($"Bienvenido {response.UserName}", TypeNotification.Info);
                Console.WriteLine($"Login successful for user: {response.UserName}");

            }
            catch (Exception ex)
            {
                await _notificationService.ShowNotificationAsync(ex.Message, TypeNotification.Error);
            }
        }

        [RelayCommand]
        public void TogglePasswordVisibility()
        {
            ShowPassword = !ShowPassword;
            IconPassword = ShowPassword ? MaterialIcons.Visibility : MaterialIcons.VisibilityOff;
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
