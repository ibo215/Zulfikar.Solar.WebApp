using System.ComponentModel.DataAnnotations;

namespace Zulfikar.Solar.API.DTOs.ProductDTO
{
    public class UpdateProductDto
    {
        [Required(ErrorMessage = "معرف المنتج مطلوب للتحديث.")]
        [Range(1, int.MaxValue, ErrorMessage = "معرف المنتج يجب أن يكون رقمًا صحيحًا موجبًا.")]
        public int Id { get; set; }

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

        // الخصائص الجديدة التي يمكن تحديثها
        public bool IsActive { get; set; }
        // LastModified لا يتم إدخاله من العميل، يتم تعيينه تلقائيًا في الـ Service
    }
}