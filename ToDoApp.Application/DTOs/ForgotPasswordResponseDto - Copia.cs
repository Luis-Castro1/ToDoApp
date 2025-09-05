namespace ToDoApp.Application.DTOs
{
    public class ForgotPasswordDto
    {
        public string User { get; set; }
        public string Code { get; set; }
        public ForgotPasswordDto()
        {
            User = string.Empty;
            Code = string.Empty;
        }
    }
}
