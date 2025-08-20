using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.DTOs
{
    public class TaskDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string UserId { get; set; }
    }
}
