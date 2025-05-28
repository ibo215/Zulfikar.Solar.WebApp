using System.ComponentModel.DataAnnotations;

namespace Zulfikar.Solar.API.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "اسم المستخدم مطلوب.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "اسم المستخدم يجب أن يكون بين 3 و 50 حرفًا.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "كلمة المرور يجب أن تكون 6 أحرف على الأقل.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}