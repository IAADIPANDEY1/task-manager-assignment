using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/tasks
        [HttpGet]
        public ActionResult<List<TaskItem>> GetAllTasks()
        {
            var tasks = _taskService.GetAllTasks();
            return Ok(tasks);
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTaskById(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound(new { message = $"Task with ID {id} not found" });
            }
            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public ActionResult<TaskItem> CreateTask([FromBody] TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Description))
            {
                return BadRequest(new { message = "Description is required" });
            }

            var newTask = _taskService.AddTask(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, newTask);
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public ActionResult<TaskItem> UpdateTask(int id, [FromBody] TaskItem task)
        {
            var updatedTask = _taskService.UpdateTask(id, task);
            if (updatedTask == null)
            {
                return NotFound(new { message = $"Task with ID {id} not found" });
            }
            return Ok(updatedTask);
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var deleted = _taskService.DeleteTask(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Task with ID {id} not found" });
            }
            return NoContent();
        }
    }
}