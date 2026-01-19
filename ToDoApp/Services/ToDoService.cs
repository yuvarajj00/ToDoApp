using ToDoApp.DTOs;
using ToDoApp.Models;
using ToDoApp.Repositories;

namespace ToDoApp.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly ITaskRepository _taskRepository;

        public ToDoService(IToDoRepository toDoRepository, ITaskRepository taskRepository)
        {
            _toDoRepository = toDoRepository;
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<ToDoItemDto>> GetAllToDosAsync()
        {
            var todos = await _toDoRepository.GetAllAsync();
            return todos.Select(MapToDto);
        }

        public async Task<ToDoItemDto?> GetToDoByIdAsync(int id)
        {
            var todo = await _toDoRepository.GetByIdAsync(id);
            return todo == null ? null : MapToDto(todo);
        }

        public async Task<IEnumerable<ToDoItemDto>> GetToDosByTaskIdAsync(int taskId)
        {
            var todos = await _toDoRepository.GetToDosByTaskIdAsync(taskId);
            return todos.Select(MapToDto);
        }

        public async Task<ToDoItemDto?> CreateToDoAsync(CreateToDoDto toDoDto)
        {
            // Verify that the task exists
            var taskExists = await _taskRepository.ExistsAsync(toDoDto.TaskId);
            if (!taskExists) return null;

            var todo = new ToDoItem
            {
                TaskId = toDoDto.TaskId,
                Description = toDoDto.Description,
                DueDate = toDoDto.DueDate?.ToUniversalTime(),
                Status = Enum.TryParse<Models.TaskStatus>(toDoDto.Status, true, out var status) 
                    ? status 
                    : Models.TaskStatus.New
            };

            var createdToDo = await _toDoRepository.AddAsync(todo);
            return MapToDto(createdToDo);
        }

        public async Task<ToDoItemDto?> UpdateToDoAsync(int id, UpdateToDoDto toDoDto)
        {
            var todo = await _toDoRepository.GetByIdAsync(id);
            if (todo == null) return null;

            todo.Description = toDoDto.Description;
            todo.DueDate = toDoDto.DueDate?.ToUniversalTime();
            todo.Status = Enum.TryParse<Models.TaskStatus>(toDoDto.Status, true, out var status) 
                ? status 
                : todo.Status;

            await _toDoRepository.UpdateAsync(todo);
            return MapToDto(todo);
        }

        public async Task<bool> DeleteToDoAsync(int id)
        {
            var todo = await _toDoRepository.GetByIdAsync(id);
            if (todo == null) return false;

            await _toDoRepository.DeleteAsync(todo);
            return true;
        }

        private static ToDoItemDto MapToDto(ToDoItem todo)
        {
            return new ToDoItemDto
            {
                Id = todo.Id,
                TaskId = todo.TaskId,
                Description = todo.Description,
                DueDate = todo.DueDate,
                Status = todo.Status.ToString()
            };
        }
    }
}
