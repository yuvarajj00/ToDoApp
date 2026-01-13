using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTOs;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDosController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        // GET: api/ToDos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetToDos([FromQuery] int? taskId = null)
        {
            if (taskId.HasValue)
            {
                var todosByTask = await _toDoService.GetToDosByTaskIdAsync(taskId.Value);
                return Ok(todosByTask);
            }

            var todos = await _toDoService.GetAllToDosAsync();
            return Ok(todos);
        }

        // GET: api/ToDos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemDto>> GetToDo(int id)
        {
            var todo = await _toDoService.GetToDoByIdAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // POST: api/ToDos
        [HttpPost]
        public async Task<ActionResult<ToDoItemDto>> CreateToDo(CreateToDoDto toDoDto)
        {
            var todo = await _toDoService.CreateToDoAsync(toDoDto);

            if (todo == null)
            {
                return BadRequest("Task not found");
            }

            return CreatedAtAction(nameof(GetToDo), new { id = todo.Id }, todo);
        }

        // PUT: api/ToDos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDo(int id, UpdateToDoDto toDoDto)
        {
            var todo = await _toDoService.UpdateToDoAsync(id, toDoDto);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // DELETE: api/ToDos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo(int id)
        {
            var result = await _toDoService.DeleteToDoAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
