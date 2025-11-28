namespace TodoApi.DTO;

public class TodoItemDto
{
    public int id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
}