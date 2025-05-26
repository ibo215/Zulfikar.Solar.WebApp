using Zulfikar.API.DTOs;
using Zulfikar.Solar.API.DTOs.CategoryDTO;

namespace Zulfikar.Solar.API.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<PreviewCategoryDto>> GetAllAsync();
        Task<UpdateCategoryDto?> GetByIdAsync(int id);
        Task AddAsync(CreateCategoryDto dto);
        Task<bool> UpdateAsync(int id, UpdateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
