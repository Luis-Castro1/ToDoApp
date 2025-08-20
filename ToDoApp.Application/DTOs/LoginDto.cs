namespace ToDoApp.Application.DTOs
{
    public class LoginDto
    {
        public LoginDto()
        {
            RememberMe = true; // Default value
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
