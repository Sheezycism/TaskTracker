using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Controllers;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Tests
{
       public class TasksControllerTests
{
    private TaskDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<TaskDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new TaskDbContext(options);
    }

    [Fact]
    public async Task GetTasks_ReturnsSeededData_HappyPath()
    {
        // Arrange
        var context = GetDbContext();
        context.Tasks.Add(new ToDoTasks { Id = 1, Title = "Test Task", Description = "Desc" });
        await context.SaveChangesAsync();
        var controller = new TasksController(context);

        // Act
        var result = await controller.GetTasks(null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var tasks = Assert.IsAssignableFrom<IEnumerable<ToDoTasks>>(okResult.Value);
        Assert.Single(tasks);
    }

    [Fact]
    public async Task Create_InvalidPost_Returns400ProblemDetails()
    {
        // Arrange
        var context = GetDbContext();
        var controller = new TasksController(context);
        var invalidTask = new ToDoTasks { Title = "" }; 
        // Act
        var result = await controller.Create(invalidTask);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var problem = Assert.IsType<ProblemDetails>(badRequestResult.Value);
        Assert.Equal("Title is required.", problem.Detail);
    }
}
}
