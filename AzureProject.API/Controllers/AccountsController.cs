using AzureProject.DataAccess.Abstract;
using AzureProject.Entity.Concrete;
using AzureProject.Entity.DTOs; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; 
namespace AzureProject.API.Controllers
{
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService; 
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IJwtService jwtService, SignInManager<AppUser> signInManager) // IJwtService inject edirik
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) // FromBody atributu əlavə olundu
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return NotFound(new { message = "User not found" }); // Daha aydın cavab

            // Şifrə yoxlanışı
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized(new { message = "Incorrect password" }); // Unauthorized cavabı istifadə olundu

            // İstifadəçi rolları
            var roles = await _userManager.GetRolesAsync(user);

            // Token yaradılması
            string tokenStr = _jwtService.Genereate(user, roles, _configuration);

            // JSON cavabı
            return Ok(new { token = tokenStr });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user != null) return StatusCode(409); // İstifadəçi artıq mövcuddur

            user = new AppUser
            {
                UserName = registerDto.UserName,
                FullName = registerDto.FullName,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded)
                return BadRequest(result.Errors);

            // İstifadəçinin rollarını alırıq
            var roles = await _userManager.GetRolesAsync(user);

            // Token yaradılır
            string tokenStr = _jwtService.Genereate(user,roles, _configuration);

            // JSON cavabı qaytarılır
            return Ok(new { token = tokenStr });
        }

       
    }

}
#region MyRegion
//[HttpGet("Create Role")]
//public async Task<IActionResult> CreateRole()
//{
//    foreach (var item in Enum.GetValues(typeof(Roless)))
//    {
//        if (!await _roleManager.RoleExistsAsync(item.ToString()))
//        {
//            await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
//        }
//    }

//    return Ok();
//}    //public enum Roless
//{
//    Admin, Member, SuperAdmin
//} 
#endregion