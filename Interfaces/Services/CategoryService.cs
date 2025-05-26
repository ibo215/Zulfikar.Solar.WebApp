using AutoMapper;
using Zulfikar.Solar.API.DTOs.CategoryDTO;
using Zulfikar.Solar.API.Interfaces.Repositories;
using Zulfikar.Solar.API.Interfaces.Services;
using Zulfikar.Solar.API.Models;
using Zulfikar.API.DTOs; // لـ PreviewCategoryDto

namespace Zulfikar.Solar.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PreviewCategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<List<PreviewCategoryDto>>(categories);
        }

        public async Task<UpdateCategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return _mapper.Map<UpdateCategoryDto>(category);
        }

        public async Task<PreviewCategoryDto> AddAsync(CreateCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
            return _mapper.Map<PreviewCategoryDto>(category);
        }

        public async Task<bool> UpdateAsync(int id, UpdateCategoryDto categoryDto)
        {
            var existingCategory = await _repository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return false;
            }

            _mapper.Map(categoryDto, existingCategory);
            _repository.Update(existingCategory);
            // قم بتحويل نتيجة SaveChangesAsync() إلى bool
            return await _repository.SaveChangesAsync(); // <--- هذا السطر تم اصلاحه مسبقاً، لا مشكلة فيه
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }
            _repository.Delete(category);
            // قم بتحويل نتيجة SaveChangesAsync() إلى bool
            return await _repository.SaveChangesAsync(); // <--- هذا السطر تم اصلاحه مسبقاً، لا مشكلة فيه
        }
    }
}