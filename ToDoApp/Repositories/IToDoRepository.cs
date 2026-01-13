using ToDoApp.Models;

namespace ToDoApp.Repositories
{
    public interface IToDoRepository : IRepository<ToDoItem>
    {
        Task<IEnumerable<ToDoItem>> GetToDosByTaskIdAsync(int taskId);
    }
}
