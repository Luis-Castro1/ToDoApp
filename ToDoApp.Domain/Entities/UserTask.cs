namespace ToDoApp.Domain.Entities
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string UserId { get; set; }
    }
}
