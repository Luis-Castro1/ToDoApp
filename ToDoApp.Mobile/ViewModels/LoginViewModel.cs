using FluentValidation.Results;
using MauiIcons.Material;
using ToDoApp.Mobile.Core.Common.Enums;
using ToDoApp.Mobile.Core.Interfaces.Services;
using ToDoApp.Mobile.Core.Interfaces.ViewModels;
using ToDoApp.Mobile.Core.Models;
using ToDoApp.Mobile.Helpers;

namespace ToDoApp.Mobile.ViewModels
{
    public partial class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        private readonly IAuthService _authService;
        private readonly INotificationService _notificationService;
        private readonly INavigateService _navigateService;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
            _notificationService = ServiceHelper.GetTService<INotificationService>();
            _navigateService = ServiceHelper.GetTService<INavigateService>();
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string _user;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string _password;

        [ObservableProperty]
        private bool _showPassword = true;

        [ObservableProperty]
        private MaterialIcons _iconPassword = MaterialIcons.Visibility;

        [RelayCommand(CanExecute = nameof(CanLogin))]
        public async Task LoginAsync()
        {
            try
            {
                if (ModelValidate())
                {
                    IsBusy = true;

                    var response = await _authService.LoginAsync(User, Password);

                    if (response == null || !response.IsAuthenticated)
                    {
                        throw new Exception("Login failed. Please check your credentials.");
                    }

                    await _navigateService.NavigateToAsync("HomePage");
                    await _notificationService.ShowNotificationAsync($"Bienvenido {response.UserName}", TypeNotification.Info);
                }
            }
            catch (Exception ex)
            {
                await _notificationService.ShowNotificationAsync(ex.Message, TypeNotification.Error);
            }
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        private bool ModelValidate()
        {
            var messageError = string.Empty;
            var loginModel = new LoginModel
            {
                User = User,
                Password = Password
            };

            ValidationResult validationResult = loginModel.Validate();

            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    messageError += item.ErrorMessage + "\n";
                }

                _notificationService.ShowAlertAsync(messageError, TypeNotification.Error);
                return false;
            }

            return true;
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(User) &&
                   !string.IsNullOrWhiteSpace(Password);
        }

        [RelayCommand]
        public void TogglePasswordVisibility()
        {
            ShowPassword = !ShowPassword;
            IconPassword = ShowPassword ? MaterialIcons.Visibility : MaterialIcons.VisibilityOff;
        }
    }
}
