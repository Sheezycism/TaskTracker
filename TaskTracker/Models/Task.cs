namespace TaskTracker.Models
{

    public enum TaskStatus
    {
        New,
        InProgress,
        High
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }
    public class ToDoTasks
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.New;

        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
