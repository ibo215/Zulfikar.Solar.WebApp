using Zulfikar.Solar.API.Models;

namespace Zulfikar.Solar.API.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task<bool> SaveChangesAsync(); // <--- تأكد من أن هذه هي bool
    }
}