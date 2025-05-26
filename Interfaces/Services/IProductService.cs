using Zulfikar.Solar.API.DTOs.ProductDTO;

namespace Zulfikar.Solar.API.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<PreviewProductDto>> GetAllAsync();
        Task<PreviewProductDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(int id, UpdateProductDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
