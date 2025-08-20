namespace ToDoApp.Application.DTOs
{
    public class CompleteTaskDto
    {
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
    }
}
