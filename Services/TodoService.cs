using TodoApi;
using TodoApi.DTO;
using TodoApi.Models;

namespace TodoApi.Services
{

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
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;

            existing.Name = dto.Name;
            existing.IsComplete = dto.IsComplete;

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todo = _repo.GetByIdAsync(id);
            if (todo is null) return false;

            await _repo.DeleteAsync(todo);
            return true;
        }

        public static TodoItemDto MapToDto(Todo todo) =>
            new()
            {
                id = todo.Id,
                Name = todo.Name,
                IsComplete = todo.IsComplete
            };
    }
}