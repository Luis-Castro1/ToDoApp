namespace ToDoApp.Application.DTOs
{
    public class ValidateCodeResponseDto
    {
        public bool IsSuccess { get; set; }
        public string? RecoverySessionId { get; set; }
        public string? ErrorMessage { get; set; }
    }
}