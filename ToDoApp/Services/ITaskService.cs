using ToDoApp.DTOs;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDto>> GetAllTasksAsync();
        Task<TaskItemDto?> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskItemDto>> GetTasksByStatusAsync(string status);
        Task<TaskItemDto> CreateTaskAsync(CreateTaskDto taskDto);
        Task<TaskItemDto?> UpdateTaskAsync(int id, UpdateTaskDto taskDto);
        Task<bool> DeleteTaskAsync(int id);
    }
}
