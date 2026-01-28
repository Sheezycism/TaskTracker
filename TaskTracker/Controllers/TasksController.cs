using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {

        private readonly TaskDbContext _context;
        public TasksController(TaskDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] string? q, [FromQuery] string sort = "dueDate:asc")
        {
            var query = _context.Tasks.AsQueryable(); ;

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.ToLower();
                query = query.Where(t => t.Title.ToLower().Contains(q) || t.Description.ToLower().Contains(q));
            }

            query = sort.ToLower() switch
            {
                "duedate:desc" => query.OrderByDescending(t => t.DueDate),
                _ => query.OrderBy(t => t.DueDate)
            };

            return Ok(await query.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoTasks>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task != null ? Ok(task) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ToDoTasks>> Create(ToDoTasks task)
        {

            if (string.IsNullOrWhiteSpace(task.Title))
                return BadRequest(new ProblemDetails { Detail = "Title is required." });

            task.CreatedAt = DateTime.UtcNow;
            if (task.DueDate.HasValue) task.DueDate = task.DueDate.Value.ToUniversalTime();

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ToDoTasks task)
        {
            if (id != task.Id) return BadRequest();

            // Ensure Kind remains UTC
            if (task.DueDate.HasValue) task.DueDate = task.DueDate.Value.ToUniversalTime();

            _context.Entry(task).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) { if (!TaskExists(id)) return NotFound(); throw; }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TaskExists(int id) => _context.Tasks.Any(e => e.Id == id);

    }
}
