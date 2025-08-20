namespace ToDoApp.Mobile.Core.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public LoginDto() { }
        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
