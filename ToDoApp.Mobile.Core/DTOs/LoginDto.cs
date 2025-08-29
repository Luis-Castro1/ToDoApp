namespace ToDoApp.Mobile.Core.Dtos
{
    public class LoginDto
    {
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public LoginDto() { }
        public LoginDto(string user, string password)
        {
            User = user;
            Password = password;
        }
    }
}
