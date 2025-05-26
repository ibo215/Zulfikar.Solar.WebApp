using AutoMapper;
using Zulfikar.Solar.API.DTOs.ProductDTO;
using Zulfikar.Solar.API.Interfaces.Repositories;
using Zulfikar.Solar.API.Interfaces.Services;
using Zulfikar.Solar.API.Models;

namespace Zulfikar.Solar.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PreviewProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return _mapper.Map<List<PreviewProductDto>>(products);
        }

        public async Task<PreviewProductDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return _mapper.Map<PreviewProductDto>(product);
        }

        public async Task<PreviewProductDto?> CreateAsync(CreateProductDto productDto) // <--- تم تغيير نوع الإرجاع هنا
        {
            var product = _mapper.Map<Product>(productDto);
            product.DateAdded = DateTime.UtcNow;
            await _repository.AddAsync(product);
            var saved = await _repository.SaveChangesAsync();
            if (!saved) return null; // في حالة فشل الحفظ

            // هنا قد تحتاج لجلب المنتج مع التصنيف لـ mapping صحيح لـ PreviewProductDto.CategoryName
            // أو التأكد من أن الـ DTO لديه فقط CategoryId هنا، ثم جلب اسم التصنيف عند الحاجة للعرض
            // لأغراض التبسيط، سنقوم بـ mapping المنتج الذي تم حفظه مباشرة.
            // إذا كان PreviewProductDto يحتاج CategoryName، فقد تحتاج لجلب Category بالـ ID هنا.
            var createdProductWithCategory = await _repository.GetByIdAsync(product.Id);
            return _mapper.Map<PreviewProductDto>(createdProductWithCategory);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto productDto)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return false;
            }

            if (id != productDto.Id)
            {
                return false;
            }

            _mapper.Map(productDto, existingProduct);
            existingProduct.LastModified = DateTime.UtcNow;
            _repository.Update(existingProduct);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            _repository.Delete(product);
            return await _repository.SaveChangesAsync();
        }
    }
}