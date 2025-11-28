namespace TodoApi.DTO;

public class UpdateTodoItemDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
}