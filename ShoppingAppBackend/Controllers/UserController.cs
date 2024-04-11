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
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
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

    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(Category))]
    [ProducesResponseType(400)]
    public IActionResult GetUserById(int userId)
    {
        if (!_userRepository.UserExists(userId))
            return BadRequest(ModelState);

        var user = _mapper.Map<ApplicationUser>(_userRepository.GetUser(userId));
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(user);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult UpdateUser([FromBody] UserDto createUser)
    {
        if (createUser == null)
            return BadRequest(ModelState);

        var user = _userRepository.GetAllUsers()
            .Where(u => u.Id == createUser.UserId).FirstOrDefault();

        if (user != null)
        {
            ModelState.AddModelError("","Already Exists");
            return StatusCode(422, ModelState);
        }
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userMap = _mapper.Map<ApplicationUser>(createUser);
        if (!_userRepository.CreateUser(userMap))
        {
            ModelState.AddModelError("","error");
            return StatusCode(500, ModelState);
        }
        return Ok("User Created");
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult UpdateUser(int userId, [FromBody] UserDto updateUser)
    {
        if (updateUser == null)
            return BadRequest(ModelState);

        if (userId != updateUser.UserId)
            return BadRequest(ModelState);

        if (!_userRepository.UserExists(userId))
            return NotFound();
        
        if (!ModelState.IsValid)
            return BadRequest();

        var userMap = _mapper.Map<ApplicationUser>(updateUser);
        
        if (!_userRepository.UpdateUser(userMap))
        {
            ModelState.AddModelError("", "error");
            return StatusCode(500, ModelState);
        }
        return Ok();
    }

    [HttpDelete("{userId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteUser(int userId)
    {
        if (!_userRepository.UserExists(userId))
            return BadRequest(ModelState);

        var userDelete = _userRepository.GetUser(userId);
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_userRepository.DeleteUser(userDelete))
        {
            ModelState.AddModelError("","error");
        }

        return Ok();
    }
}