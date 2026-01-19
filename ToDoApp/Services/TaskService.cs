using ToDoApp.DTOs;
using ToDoApp.Models;
using ToDoApp.Repositories;

namespace ToDoApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksWithToDosAsync();
            return tasks.Select(MapToDto);
        }

        public async Task<TaskItemDto?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetTaskWithToDosAsync(id);
            return task == null ? null : MapToDto(task);
        }

        public async Task<IEnumerable<TaskItemDto>> GetTasksByStatusAsync(string status)
        {
            if (Enum.TryParse<Models.TaskStatus>(status, true, out var taskStatus))
            {
                var tasks = await _taskRepository.GetTasksByStatusAsync(taskStatus);
                return tasks.Select(MapToDto);
            }
            return Enumerable.Empty<TaskItemDto>();
        }

        public async Task<TaskItemDto> CreateTaskAsync(CreateTaskDto taskDto)
        {
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate?.ToUniversalTime(),
                Status = Enum.TryParse<Models.TaskStatus>(taskDto.Status, true, out var status) 
                    ? status 
                    : Models.TaskStatus.New
            };

            var createdTask = await _taskRepository.AddAsync(task);
            return MapToDto(createdTask);
        }

        public async Task<TaskItemDto?> UpdateTaskAsync(int id, UpdateTaskDto taskDto)
        {
            var task = await _taskRepository.GetTaskWithToDosAsync(id);
            if (task == null) return null;

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.DueDate = taskDto.DueDate?.ToUniversalTime();
            task.Status = Enum.TryParse<Models.TaskStatus>(taskDto.Status, true, out var status) 
                ? status 
                : task.Status;

            await _taskRepository.UpdateAsync(task);
            return MapToDto(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return false;

            await _taskRepository.DeleteAsync(task);
            return true;
        }

        private static TaskItemDto MapToDto(TaskItem task)
        {
            return new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status.ToString(),
                ToDoItems = task.ToDoItems.Select(td => new ToDoItemDto
                {
                    Id = td.Id,
                    TaskId = td.TaskId,
                    Description = td.Description,
                    DueDate = td.DueDate,
                    Status = td.Status.ToString()
                }).ToList()
            };
        }
    }
}
