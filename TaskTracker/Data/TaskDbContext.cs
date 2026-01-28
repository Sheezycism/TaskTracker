using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;
using TaskStatus = TaskTracker.Models.TaskStatus;

namespace TaskTracker.Data
{
    public class TaskDbContext : DbContext
    {

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<ToDoTasks> Tasks => Set<ToDoTasks>();
    }

    public static class DbInitializer
    {
        public static void Seed(TaskDbContext context)
        {
            if (context.Tasks.Any()) return;
            context.Tasks.AddRange(
                new ToDoTasks { Title = "Fix Bug #101", Description = "Resolve null ref", Status = TaskStatus.InProgress, Priority = TaskPriority.High, DueDate = DateTime.UtcNow.AddDays(2) },
                new ToDoTasks { Title = "Refactor Auth", Description = "Move to JWT", Status = TaskStatus.New, Priority = TaskPriority.Medium }
            );
            context.SaveChanges();
        }
    }

}
