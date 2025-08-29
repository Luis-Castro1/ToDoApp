using MauiIcons.FontAwesome;
using MauiIcons.Material;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Platform;
using ToDoApp.Mobile.Core.Interfaces.Services;
using ToDoApp.Mobile.Core.Interfaces.ViewModels;
using ToDoApp.Mobile.Services;
using ToDoApp.Mobile.ViewModels;
using ToDoApp.Mobile.Views;

namespace ToDoApp.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            try
            {
                MauiAppBuilder builder = MauiApp.CreateBuilder();
                builder.UseMauiApp<App>();
                builder.UseFontAwesomeMauiIcons();
                builder.UseMaterialMauiIcons();
                builder.UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldEnableSnackbarOnWindows(true);
                })
                    .UseMauiCommunityToolkit();

                builder.ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemiBold");
                    fonts.AddFont("Icon-Solid-900.ttf", "IconSolid");
                    fonts.AddFont("Icon-Regular-400.ttf", "IconRegular");
                    fonts.AddFont("Icon-Brands-Regular-400.ttf", "IconBrandsRegular");
                });

#if ANDROID
                Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
                {
                    h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
                });
#endif

#if WINDOWS
                Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
                {
                    if (h.PlatformView is Microsoft.UI.Xaml.Controls.TextBox nativeEntry)
                    {
                        nativeEntry.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
                        nativeEntry.BorderBrush = null;
                        nativeEntry.Background = null;
                    }
                    //h.PlatformView.Background = null;
                    //h.PlatformView.BackgroundTintList = Window.Current?.Resources?.GetColorStateList("transparent_background");
                    //h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
                    //h.PlatformView.BorderBrush = null;
                });
#endif


#if DEBUG
                builder.Logging.AddDebug();
#endif
                builder.Services.AddSingleton<HttpClient>(sp =>
                {
                    var httpClient = new HttpClient
                    {
                        BaseAddress = new Uri("http://192.168.0.122:5054/api/") // Cambia la URL según tu API
                    };
                    // Configura el cliente HTTP según sea necesario, por ejemplo, agregar encabezados
                    //httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    return httpClient;
                });
                builder.Services.AddHttpClient();
                builder.Services.AddSingleton<IAuthService, AuthService>();
                builder.Services.AddSingleton<ILoginViewModel, LoginViewModel>();
                builder.Services.AddSingleton<LoginPage>();
                builder.Services.AddSingleton<INotificationService, NotificationService>();
                builder.Services.AddSingleton<INavigateService, NavigateService>();
                return builder.Build();
            }
            catch (Exception ex)
            {
                // Maneja la excepción aquí, por ejemplo, registrándola o mostrando un mensaje
                Console.WriteLine($"Error al crear la aplicación: {ex.Message}");
                // Puedes lanzar una excepción personalizada o manejarla de otra manera
                throw new ApplicationException("Error al inicializar la aplicación MAUI", ex);
            }
        }
    }
}