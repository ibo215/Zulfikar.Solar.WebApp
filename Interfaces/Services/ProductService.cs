using AutoMapper;
using Zulfikar.Solar.API.DTOs.ProductDTO;
using Zulfikar.Solar.API.Interfaces.Repositories;
using Zulfikar.Solar.API.Models;

namespace Zulfikar.Solar.API.Interfaces.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PreviewProductDto>> GetAllAsync()
        {
            var products = await _repo.GetAllAsync();
            return _mapper.Map<List<PreviewProductDto>>(products);
        }

        public async Task<PreviewProductDto?> GetByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            return _mapper.Map<PreviewProductDto>(product);
        }

        public async Task<bool> CreateAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _repo.AddAsync(product);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            _repo.Update(existing);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            _repo.Delete(existing);
            return await _repo.SaveChangesAsync();
        }
    }

}
