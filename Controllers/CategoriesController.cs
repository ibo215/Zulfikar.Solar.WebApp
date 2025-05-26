using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zulfikar.API.DTOs; // لـ PreviewCategoryDto
using Zulfikar.Solar.API.DTOs.CategoryDTO;
using Zulfikar.Solar.API.Interfaces.Services;

namespace Zulfikar.Solar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PreviewCategoryDto>>> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<UpdateCategoryDto>> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            // قم بتعديل السيرفيس ليرجع الـ DTO المنشأ (مع الـ ID)
            var createdCategoryDto = await _service.AddAsync(dto);
            // إذا كانت العملية ناجحة، ارجع 201 Created مع رابط للمورد المنشأ
            return CreatedAtAction(nameof(Get), new { id = createdCategoryDto.Id }, createdCategoryDto); // <--- تم التعديل هنا
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }
}