namespace ToDoApp.Mobile.Core.Interfaces.ViewModels
{
    public interface ILoginViewModel : IBaseViewModel
    {
        string User { get; set; }
        string Password { get; set; }
        bool ShowPassword { get; set; }
        enum IconPassword;
    }
}
