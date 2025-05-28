using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zulfikar.API.DTOs;
using Zulfikar.Solar.API.DTOs.CategoryDTO;
using Zulfikar.Solar.API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization; // <--- أضف هذا الـ using

namespace Zulfikar.Solar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] // <--- يمكن وضعها هنا لحماية جميع الـ actions في هذا الـ Controller
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        // [AllowAnonymous] // إذا كان Controller محميًا، تسمح بالوصول لهذه الـ action
        public async Task<ActionResult<List<PreviewCategoryDto>>> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        // [AllowAnonymous] // إذا كان Controller محميًا، تسمح بالوصول لهذه الـ action
        public async Task<ActionResult<UpdateCategoryDto>> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize] // <--- حماية هذا الـ action (إنشاء تصنيف)
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var createdCategoryDto = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = createdCategoryDto.Id }, createdCategoryDto);
        }

        [HttpPut("{id}")]
        [Authorize] // <--- حماية هذا الـ action (تحديث تصنيف)
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize] // <--- حماية هذا الـ action (حذف تصنيف)
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }
}