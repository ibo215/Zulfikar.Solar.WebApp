using System.ComponentModel.DataAnnotations;

namespace Zulfikar.Solar.API.DTOs.AuthDTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "اسم المستخدم أو البريد الإلكتروني مطلوب.")]
        public string UsernameOrEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}