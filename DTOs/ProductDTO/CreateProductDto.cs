using System.ComponentModel.DataAnnotations;

namespace Zulfikar.Solar.API.DTOs.ProductDTO
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "اسم المنتج مطلوب.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "يجب أن يكون اسم المنتج بين 3 و 200 حرف.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "الوصف لا يمكن أن يتجاوز 1000 حرف.")]
        public string Description { get; set; } = string.Empty;

        [Url(ErrorMessage = "صيغة رابط الصورة غير صحيحة.")]
        [StringLength(500, ErrorMessage = "رابط الصورة لا يمكن أن يتجاوز 500 حرف.")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "سعر المنتج مطلوب.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "يجب أن يكون السعر أكبر من صفر.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "معرف التصنيف مطلوب.")]
        [Range(1, int.MaxValue, ErrorMessage = "معرف التصنيف يجب أن يكون رقمًا صحيحًا موجبًا.")]
        public int CategoryId { get; set; }

        // الخصائص الجديدة التي يمكن إدخالها عند الإنشاء
        // IsActive يمكن تركها افتراضية أو السماح بتعيينها
        public bool IsActive { get; set; } = true;
        // DateAdded لا يتم إدخالها من العميل، يتم تعيينها تلقائيًا في الـ Model أو الـ Service
    }
}