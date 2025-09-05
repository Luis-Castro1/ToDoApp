using MauiIcons.Core;
using ToDoApp.Mobile.Core.Interfaces.ViewModels;

namespace ToDoApp.Mobile.Views;

public partial class ForgotPasswordPage : ContentPage
{
	public ForgotPasswordPage(IForgotPasswordViewModel forgotPasswordViewModel)
	{
		InitializeComponent();
        BindingContext = forgotPasswordViewModel;
		_ = new MauiIcon();
    }
}