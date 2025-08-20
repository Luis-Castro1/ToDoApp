using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Application.DTOs
{
    public class CreateTasksDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
