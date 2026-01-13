using ToDoApp.DTOs;

namespace ToDoApp.Services
{
    public interface IToDoService
    {
        Task<IEnumerable<ToDoItemDto>> GetAllToDosAsync();
        Task<ToDoItemDto?> GetToDoByIdAsync(int id);
        Task<IEnumerable<ToDoItemDto>> GetToDosByTaskIdAsync(int taskId);
        Task<ToDoItemDto?> CreateToDoAsync(CreateToDoDto toDoDto);
        Task<ToDoItemDto?> UpdateToDoAsync(int id, UpdateToDoDto toDoDto);
        Task<bool> DeleteToDoAsync(int id);
    }
}
