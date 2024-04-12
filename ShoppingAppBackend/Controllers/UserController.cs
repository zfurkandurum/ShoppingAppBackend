using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppBackend.Dto;
using ShoppingAppBackend.Interfaces;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ApplicationUser>))]
    public IActionResult GetUsers()
    {
        var users = _mapper.Map<List<UserDto>>(_userRepository.GetAllUsers());
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(users);
    }
    
    [HttpGet("{email}")]
    [ProducesResponseType(200, Type = typeof(Category))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user =await _userManager.FindByNameAsync(email);
        
        if (user == null) 
            return BadRequest(ModelState);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(user);
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
    {
        var user = await _userManager.FindByNameAsync(userDto.Email);

        if (user == null)
            return BadRequest("User not found");

        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.Phone = userDto.Phone;
        user.Address = userDto.Address;

        // Adres güncelleme işlemi için ek kontrol veya işlemler eklenebilir

        var updateUserResult = await _userManager.UpdateAsync(user);

        if (!updateUserResult.Succeeded)
            return BadRequest(updateUserResult.Errors);

        return Ok("User updated successfully");
    }
    
    [HttpDelete("{email}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteUser(string email)
    {
        if (!_userRepository.UserExists(email))
            return BadRequest(ModelState);

        var userDelete = _userRepository.GetUser(email);
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_userRepository.DeleteUser(userDelete))
        {
            ModelState.AddModelError("","error");
        }

        return Ok();
    }
}