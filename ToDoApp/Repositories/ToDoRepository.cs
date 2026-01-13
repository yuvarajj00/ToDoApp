using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Repositories
{
    public class ToDoRepository : Repository<ToDoItem>, IToDoRepository
    {
        public ToDoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ToDoItem>> GetToDosByTaskIdAsync(int taskId)
        {
            return await _dbSet
                .Where(td => td.TaskId == taskId)
                .ToListAsync();
        }
    }
}
