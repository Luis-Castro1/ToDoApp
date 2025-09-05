using ToDoApp.Mobile.Core.Common.Enums;
using ToDoApp.Mobile.Core.Interfaces.Services;
using ToDoApp.Mobile.Core.Interfaces.ViewModels;
using ToDoApp.Mobile.Helpers;

namespace ToDoApp.Mobile.ViewModels
{
    public partial class ForgotPasswordViewModel : BaseViewModel, IForgotPasswordViewModel
    {
        private readonly IAuthService _authService;
        private readonly INotificationService _notificationService;
        private readonly INavigateService _navigateService;
        public ForgotPasswordViewModel(IAuthService authService)
        {
            _authService = authService;
            _notificationService = ServiceHelper.GetTService<INotificationService>();
            _navigateService = ServiceHelper.GetTService<INavigateService>();
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SendResetCodeCommand))]
        private string _user;

        [ObservableProperty]
        private string _resetCode;

        [RelayCommand(CanExecute = nameof(CanSendResetCode))]
        public async Task SendResetCodeAsync()
        {
            try
            {
                IsBusy = true;

                var response = await _authService.GenerateForgotPasswordCode(User);
                if (response == null || !response.IsSuccess)
                {
                    throw new Exception(response?.ErrorMessage ?? "Error generating reset code. Please try again.");
                }

                IsBusy = false;

                await _notificationService.ShowNotificationAsync("Reset code sent to your email.", TypeNotification.Info);
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await _notificationService.ShowNotificationAsync(ex.Message, TypeNotification.Error);
            }
        }

        private bool CanSendResetCode()
        {
            return !string.IsNullOrWhiteSpace(User) && !IsBusy;
        }
    }
}
