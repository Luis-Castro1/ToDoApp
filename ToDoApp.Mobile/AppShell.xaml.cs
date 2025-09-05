using ToDoApp.Mobile.Views;

namespace ToDoApp.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("HomePage", typeof(HomePage));
            Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("ForgotPasswordPage", typeof(ForgotPasswordPage));
        }
    }
}
