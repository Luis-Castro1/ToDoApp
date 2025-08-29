using ToDoApp.Mobile.Core.Interfaces.Services;
using ToDoApp.Mobile.Core.Common.Enums;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;

namespace ToDoApp.Mobile.Services
{
    public class NotificationService : INotificationService
    {
        public async Task ShowAlertAsync(string message, TypeNotification type)
        {
            await App.Current.MainPage.DisplayAlert("Error", message, "OK");
        }

        public async Task ShowNotificationAsync(string message, TypeNotification type)
        {
            CancellationTokenSource cancellationTokenSource = new();

            SnackbarOptions snackbarOptions = new()
            {
                BackgroundColor = type switch
                {
                    TypeNotification.OK => Colors.Green,
                    TypeNotification.Error => Colors.Red,
                    TypeNotification.Warning => Colors.Orange,
                    TypeNotification.Info => Colors.DeepSkyBlue,
                    _ => Colors.Gray
                },
                TextColor = Colors.White,
                CornerRadius = new(10),
                Font = Microsoft.Maui.Font.SystemFontOfSize(15, FontWeight.Bold),
                ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14, Microsoft.Maui.FontWeight.Bold)
            };

            var snackbar = Snackbar.Make(message, null, "", TimeSpan.FromSeconds(3), snackbarOptions);
            await snackbar.Show(cancellationTokenSource.Token);
        }
    }
}
