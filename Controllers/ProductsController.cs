using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zulfikar.Solar.API.DTOs.ProductDTO;
using Zulfikar.Solar.API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization; // <--- أضف هذا الـ using

namespace Zulfikar.Solar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize] // <--- يمكن وضعها هنا لحماية جميع الـ actions في هذا الـ Controller
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        // [AllowAnonymous] // إذا كان Controller محميًا، تسمح بالوصول لهذه الـ action
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        // [AllowAnonymous] // إذا كان Controller محميًا، تسمح بالوصول لهذه الـ action
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Authorize] // <--- حماية هذا الـ action (إنشاء منتج)
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var createdProductDto = await _service.CreateAsync(dto);
            if (createdProductDto == null)
            {
                return BadRequest("فشل في إنشاء المنتج.");
            }
            return CreatedAtAction(nameof(GetById), new { id = createdProductDto.Id }, createdProductDto);
        }

        [HttpPut("{id}")]
        [Authorize] // <--- حماية هذا الـ action (تحديث منتج)
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize] // <--- حماية هذا الـ action (حذف منتج)
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}