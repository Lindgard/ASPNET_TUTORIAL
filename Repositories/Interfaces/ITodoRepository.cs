using TodoApi.Models;
public interface ITodoRepository
{
    Task<List<Todo>> GetAllAsync();
    Task<List<Todo>> GetCompletedAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task<Todo> AddAsync(Todo todo);
    Task UpdateAsync(Todo todo);
    Task DeleteAsync(Todo todo);
}