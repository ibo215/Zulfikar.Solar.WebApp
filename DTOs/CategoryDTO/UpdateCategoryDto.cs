using System.ComponentModel.DataAnnotations; // تأكد من إضافة هذا الـ using

namespace Zulfikar.Solar.API.DTOs.CategoryDTO
{
    public class UpdateCategoryDto
    {
        // يجب أن يكون الـ ID موجودًا هنا إذا كان يتم إرساله في جسم الطلب لتحديد الكائن المراد تحديثه
        // ولكن بما أنك تمرر الـ ID في الـ URL، يمكننا الاحتفاظ به كما هو أو إضافته هنا إذا كنت تفضل.
        // حاليًا، بما أن الـ Controller يأخذ الـ ID من الـ URL، لسنا بحاجة لـ [Required] على الـ ID في الـ DTO.

        [Required(ErrorMessage = "اسم التصنيف مطلوب.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "يجب أن يكون اسم التصنيف بين 3 و 100 حرف.")]
        public string Name { get; set; } = string.Empty;
    }
}