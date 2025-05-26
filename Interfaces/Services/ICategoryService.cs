using Zulfikar.Solar.API.DTOs.CategoryDTO;
using Zulfikar.API.DTOs; // لـ PreviewCategoryDto

namespace Zulfikar.Solar.API.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<PreviewCategoryDto>> GetAllAsync();
        Task<UpdateCategoryDto?> GetByIdAsync(int id);
        Task<PreviewCategoryDto> AddAsync(CreateCategoryDto categoryDto); // <--- تم تغيير نوع الإرجاع هنا
        Task<bool> UpdateAsync(int id, UpdateCategoryDto categoryDto);
        Task<bool> DeleteAsync(int id);
    }
}