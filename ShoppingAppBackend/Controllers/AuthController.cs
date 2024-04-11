using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTWithDotNetCore.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoppingAppBackend.Dto;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }
    
    [HttpPost]
    [Route("seedRoles")]
    public async Task<IActionResult> SeedRoles()
    {
        bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.USER); 
        bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN); 
        bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);

        if (isUserRoleExists && isOwnerRoleExists && isAdminRoleExists)
            return Ok("Role seeding is done");
        
        await _roleManager.CreateAsync(new IdentityRole<int>(StaticUserRoles.USER));
        await _roleManager.CreateAsync(new IdentityRole<int>(StaticUserRoles.ADMIN));
        await _roleManager.CreateAsync(new IdentityRole<int>(StaticUserRoles.OWNER));

        return Ok("Role Seeding Done"); 
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);

        if (existingUser != null)
            return BadRequest("User already exists");


        ApplicationUser newUser = new ApplicationUser()
        {
            Email = registerDto.Email,
            UserName = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

        if (!createUserResult.Succeeded)
            return BadRequest(createUserResult.Errors);
        
        await _userManager.AddToRoleAsync(newUser, StaticUserRoles.USER);

        return Ok("user created");
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        
        if (user == null)
            return BadRequest("user not found");

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user , loginDto.Password);
        
        if(!isPasswordCorrect)
            return BadRequest("password isn't correct");

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("JWTID", Guid.NewGuid().ToString()),
        };


        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = GenerateNewJsonWebToken(authClaims);
        
        return Ok(token); 
    }
    
    private string GenerateNewJsonWebToken(List<Claim> claims)
    {
        var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

        var tokenObject = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

        return token;
    }
}