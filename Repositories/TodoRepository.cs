using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using ITodoRepository = ITodoRepository.ITodoRepository;

namespace Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDb _db;
        public TodoRepository(TodoDb db)
        {
            _db = db;
        }

        public Task<List<Todo>> GetAllAsync() =>
            _db.Todos.ToListAsync();
        public Task<List<Todo>> GetCompletedAsync() =>
            _db.Todos.Where(t => t.IsComplete).ToListAsync();
        public Task<Todo?> GetByIdAsync(int id) =>
            _db.Todos.FindAsync(id).AsTask();
        public async Task<Todo> AddAsync(Todo todo)
        {
            _db.Todos.Add(todo);
            await _db.SaveChangesAsync();
            return todo;
        }
        public async Task UpdateAsync(Todo todo)
        {
            _db.Todos.Update(todo);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Todo todo)
        {
            _db.Todos.Remove(todo);
            await _db.SaveChangesAsync();
        }
    }
}