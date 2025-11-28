using TodoApi.DTO;
namespace TodoApi
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDto>> GetAllAsync();
        Task<IEnumerable<TodoItemDto>> GetCompletedAsync();
        Task<TodoItemDto?> GetByIdAsync(int id);
        Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto);
        Task<bool> UpdateAsync(int id, UpdateTodoItemDto dto);
        Task<bool> DeleteAsync(int id);
    }
}