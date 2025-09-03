namespace ToDoApp.Application.DTOs
{
    public class ResetPasswordResponseDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public ResetPasswordResponseDto()
        {
            IsSuccess = false;
            ErrorMessage = string.Empty;
        }
    }
}
