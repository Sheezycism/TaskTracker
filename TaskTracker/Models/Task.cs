using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models
{

    public enum TaskStatus
    {
        New = 0,
        InProgress = 1,
        Completed = 2
    }

    public enum TaskPriority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }
    public class ToDoTasks
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title is required and cannot be empty.")]
        [MinLength(1, ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; }

        [EnumDataType(typeof(TaskStatus), ErrorMessage = "Status must be: New, InProgress, or Done.")]
        public TaskStatus Status { get; set; } = TaskStatus.New;

        [EnumDataType(typeof(TaskPriority), ErrorMessage = "Priority must be: Low, Medium, or High.")]
        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
