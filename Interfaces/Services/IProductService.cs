using Zulfikar.Solar.API.DTOs.ProductDTO;

namespace Zulfikar.Solar.API.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<PreviewProductDto>> GetAllAsync();
        Task<PreviewProductDto?> GetByIdAsync(int id);
        Task<PreviewProductDto?> CreateAsync(CreateProductDto productDto); // <--- تم تغيير نوع الإرجاع هنا
        Task<bool> UpdateAsync(int id, UpdateProductDto productDto);
        Task<bool> DeleteAsync(int id);
    }
}   