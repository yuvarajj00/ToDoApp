using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.New;

        // Navigation property for ToDo items (subtasks)
        public ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    }
}
