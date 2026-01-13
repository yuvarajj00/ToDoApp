using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Repositories
{
    public class TaskRepository : Repository<TaskItem>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TaskItem?> GetTaskWithToDosAsync(int id)
        {
            return await _dbSet
                .Include(t => t.ToDoItems)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksWithToDosAsync()
        {
            return await _dbSet
                .Include(t => t.ToDoItems)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(Models.TaskStatus status)
        {
            return await _dbSet
                .Include(t => t.ToDoItems)
                .Where(t => t.Status == status)
                .ToListAsync();
        }
    }
}
