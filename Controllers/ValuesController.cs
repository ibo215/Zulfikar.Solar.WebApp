using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zulfikar.Solar.API.DTOs.AuthDTOs;
using Zulfikar.Solar.API.Models; // لـ ApplicationUser

using System.IdentityModel.Tokens.Jwt; // لـ JwtSecurityToken
using System.Security.Claims; // لـ Claims
using Microsoft.IdentityModel.Tokens; // لـ SymmetricSecurityKey
using System.Text; // لـ Encoding

// لخاصيات JWT
using Microsoft.Extensions.Configuration;


namespace Zulfikar.Solar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = string.Join(" ", errors) });
            }

            // يمكنك هنا إضافة أدوار للمستخدمين الجدد تلقائيًا إذا أردت
            // await _userManager.AddToRoleAsync(user, "User");

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, UserId = user.Id, Username = user.UserName, Email = user.Email });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser? user = await _userManager.FindByEmailAsync(model.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.UsernameOrEmail);
            }

            if (user == null)
            {
                return Unauthorized(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = "بيانات الاعتماد غير صحيحة." });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = "بيانات الاعتماد غير صحيحة." });
            }

            var token = await GenerateJwtToken(user);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token, UserId = user.Id, Username = user.UserName, Email = user.Email });
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var validIssuer = jwtSettings["ValidIssuer"];
            var validAudience = jwtSettings["ValidAudience"];

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            // يمكنك إضافة الأدوار هنا إذا كنت تستخدم نظام الأدوار
            // var roles = await _userManager.GetRolesAsync(user);
            // foreach (var role in roles)
            // {
            //     claims.Add(new Claim(ClaimTypes.Role, role));
            // }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSettings["ExpiryDays"] ?? "7")); // يمكن إضافة مدة صلاحية في الـ appsettings

            var token = new JwtSecurityToken(
                issuer: validIssuer,
                audience: validAudience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}