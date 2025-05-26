using Microsoft.EntityFrameworkCore;
using Zulfikar.Solar.API.Data;
using Zulfikar.Solar.API.Interfaces.Repositories;
using Zulfikar.Solar.API.Models;

namespace Zulfikar.Solar.API.Repositories // تأكد من المسار الصحيح
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // هذا السطر يقوم بتحويل عدد الصفوف المتأثرة إلى bool
            return await _context.SaveChangesAsync() > 0; // <--- تأكد من وجود > 0
        }
    }
}