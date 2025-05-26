using Microsoft.EntityFrameworkCore;
using Zulfikar.Solar.API.Data;
using Zulfikar.Solar.API.Models;

namespace Zulfikar.Solar.API.Interfaces.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync() =>
            await _context.Categories.ToListAsync();

        public async Task<Category?> GetByIdAsync(int id) =>
            await _context.Categories.FindAsync(id);

        public async Task AddAsync(Category category) =>
            await _context.Categories.AddAsync(category);

        public void Update(Category category) =>
            _context.Categories.Update(category);

        public void Delete(Category category) =>
            _context.Categories.Remove(category);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }

}
