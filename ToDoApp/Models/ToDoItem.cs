using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.New;

        // Navigation property
        [ForeignKey("TaskId")]
        public TaskItem? Task { get; set; }
    }
}
