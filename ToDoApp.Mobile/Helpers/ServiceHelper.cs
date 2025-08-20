namespace ToDoApp.Mobile.Helpers
{
    public static class ServiceHelper
    {
        public static T GetTService<T>() =>
            Current.GetService<T>();

        public static IServiceProvider Current =>
            IPlatformApplication.Current.Services;
    }
}
