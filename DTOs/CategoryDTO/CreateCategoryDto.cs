using System.ComponentModel.DataAnnotations; 

namespace Zulfikar.Solar.API.DTOs.CategoryDTO
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "اسم التصنيف مطلوب.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "يجب أن يكون اسم التصنيف بين 3 و 100 حرف.")]
        public string Name { get; set; } = string.Empty;
    }
}