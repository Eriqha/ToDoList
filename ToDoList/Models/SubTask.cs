namespace ToDoList.Models
{
    public class SubTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }

        public int ToDoItemId { get; set; }
        public ToDoItem? ToDoItem { get; set; }

    }
}
