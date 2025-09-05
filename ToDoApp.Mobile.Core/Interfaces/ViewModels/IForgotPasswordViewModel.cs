namespace ToDoApp.Mobile.Core.Interfaces.ViewModels
{
    public interface IForgotPasswordViewModel : IBaseViewModel
    {
        string User { get; set; }
        string ResetCode { get; set; }
    }
}
