namespace ToDoApp.DTOs
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset? DueDate { get; set; }
        public string Status { get; set; } = "New";
    }

    public class CreateToDoDto
    {
        public int TaskId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset? DueDate { get; set; }
        public string Status { get; set; } = "New";
    }

    public class UpdateToDoDto
    {
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset? DueDate { get; set; }
        public string Status { get; set; } = "New";
    }
}
