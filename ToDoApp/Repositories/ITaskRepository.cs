using ToDoApp.Models;

namespace ToDoApp.Repositories
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<TaskItem?> GetTaskWithToDosAsync(int id);
        Task<IEnumerable<TaskItem>> GetAllTasksWithToDosAsync();
        Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(Models.TaskStatus status);
    }
}
