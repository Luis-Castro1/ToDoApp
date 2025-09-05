namespace ToDoApp.Mobile.Core.DTOs
{
    public class ResetPasswordDto
    {
        public string UserOrEmail { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }

        public ResetPasswordDto()
        {
            UserOrEmail = string.Empty;
            Token = string.Empty;
            NewPassword = string.Empty;
        }
    }
}
