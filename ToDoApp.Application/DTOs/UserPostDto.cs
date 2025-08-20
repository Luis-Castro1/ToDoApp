using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Application.DTOs
{
    public class UserPostDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string ImgUrl { get; set; }
    }
}
