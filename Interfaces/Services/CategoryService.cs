using AutoMapper;
using Zulfikar.API.DTOs;
using Zulfikar.Solar.API.DTOs.CategoryDTO;
using Zulfikar.Solar.API.Interfaces.Repositories;
using Zulfikar.Solar.API.Models;

namespace Zulfikar.Solar.API.Interfaces.Services
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
            return category == null ? null : _mapper.Map<UpdateCategoryDto>(category);
        }

        public async Task AddAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(dto, existing);
            _repository.Update(existing);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return false;

            _repository.Delete(category);
            await _repository.SaveChangesAsync();
            return true;
        }
    }

}
