namespace ToDoApp.Mobile.Core.Dtos
{
    public class AuthResponseDto
    {
        public AuthResponseDto()
        {

        }
        public bool IsAuthenticated { get; set; }
        public string? Token { get; set; }
        public string? UserName { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
