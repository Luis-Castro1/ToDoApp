namespace ToDoApp.Mobile.Core.DTOs
{
    public class GeneralResponseDto
    {
        public GeneralResponseDto()
        {
            ErrorMessage = string.Empty;
            Data = string.Empty;
        }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string Data { get; set; }
    }
}
