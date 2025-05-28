using Microsoft.AspNetCore.Identity;

namespace Zulfikar.Solar.API.Models
{
    public class ApplicationUser : IdentityUser
    {
         public string? FullName { get; set; }
         public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}