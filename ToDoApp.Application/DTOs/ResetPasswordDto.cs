namespace ToDoApp.Application.DTOs
{
    public class ResetPasswordDto
    {
        public string UserOrEmail { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }

        public ResetPasswordDto()
        {
            UserOrEmail = string.Empty;
            Code = string.Empty;
            NewPassword = string.Empty;
        }
    }
}
