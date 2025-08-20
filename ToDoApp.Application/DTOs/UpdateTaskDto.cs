namespace ToDoApp.Application.DTOs
{
    public class UpdateTaskDto
    {
        public UpdateTaskDto()
        {
            Id = null;
            Title = string.Empty;
            Description = string.Empty;
            UserId = string.Empty;
        }
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set;}
        public string UserId { get; set; }
    }
}
