using TodoApi;
using TodoApi.DTO;
using TodoApi.Models;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repo;
    public TodoService(ITodoRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<TodoItemDto>> GetAllAsync() =>
        (await _repo.GetAllAsync()).Select(MapToDto);

    public async Task<IEnumerable<TodoItemDto>> GetCompletedAsync() =>
        (await _repo.GetCompletedAsync()).Select(MapToDto);
    public async Task<TodoItemDto?> GetByIdAsync(int id)
    {
        var todo = await _repo.GetByIdAsync(id);
        return todo is null ? null : MapToDto(todo);
    }

    public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto)
    {
        var todo = new Todo { Name = dto.Name };
        var created = await _repo.AddAsync(todo);
        return MapToDto(created);
    }
    public async Task<bool> UpdateAsync(int id, UpdateTodoItemDto dto)
    {

    }
}