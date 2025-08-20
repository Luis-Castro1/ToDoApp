using CommunityToolkit.Mvvm.ComponentModel;
using ToDoApp.Mobile.Core.Interfaces.ViewModels;
using ToDoApp.Mobile.Services;

namespace ToDoApp.Mobile.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject, IBaseViewModel
    {
        [ObservableProperty]
        private bool _isBusy;
        [ObservableProperty]
        private string _title;

        public event EventHandler<Exception>? ExceptionError;
        //protected void OnExceptionError(Exception e)
        //{
        //    ExceptionError?.Invoke(this, e);
        //}
    }
}
