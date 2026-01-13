using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTOs;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasks([FromQuery] string? status = null)
        {
            if (!string.IsNullOrEmpty(status))
            {
                var tasksByStatus = await _taskService.GetTasksByStatusAsync(status);
                return Ok(tasksByStatus);
            }

            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> CreateTask(CreateTaskDto taskDto)
        {
            var task = await _taskService.CreateTaskAsync(taskDto);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto taskDto)
        {
            var task = await _taskService.UpdateTaskAsync(id, taskDto);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
