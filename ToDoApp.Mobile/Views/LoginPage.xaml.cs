using MauiIcons.Core;
using ToDoApp.Mobile.Core.Interfaces.ViewModels;

namespace ToDoApp.Mobile.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(ILoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
        _ = new MauiIcon();
    }
}