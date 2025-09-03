namespace ToDoApp.Application.DTOs
{
    public class ForgotPasswordResponseDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public ForgotPasswordResponseDto()
        {
            ErrorMessage = string.Empty;
        }
    }
}
