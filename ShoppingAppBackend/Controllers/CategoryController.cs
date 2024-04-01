using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppBackend.Dto;
using ShoppingAppBackend.Interfaces;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
    public IActionResult GetCategories()
    {
        var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(categories);
    }

    [HttpGet("{categoryId}")]
    [ProducesResponseType(200, Type = typeof(Category))]
    [ProducesResponseType(400)]
    public IActionResult GetCategory(int categoryId)
    {
        if (!_categoryRepository.CategoryExist(categoryId))
            return NotFound();

        var category = _mapper.Map<Category>(_categoryRepository.GetCategory(categoryId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
    {
        if (categoryCreate == null)
            return BadRequest(ModelState);

        var category = _categoryRepository.GetCategories()
            .Where(c => c.CategoryId == categoryCreate.CategoryId).FirstOrDefault();
        
        if (category != null)
        {
            ModelState.AddModelError("","Already Exists");
            return StatusCode(422, ModelState);
        }
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var categoryMap = _mapper.Map<Category>(categoryCreate);
        if (!_categoryRepository.CreateCategory(categoryMap))
        {
            ModelState.AddModelError("","error");
            return StatusCode(500, ModelState);
        }
        
        return Ok("Done");
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
    {
        if (updatedCategory == null)
            return BadRequest(ModelState);
        
        if (categoryId != updatedCategory.CategoryId)
            return BadRequest(ModelState);
        
        if (!_categoryRepository.CategoryExist(categoryId))
            return NotFound();
        
        if (!ModelState.IsValid)
            return BadRequest();
        
        var categoryMap = _mapper.Map<Category>(updatedCategory);

        if (!_categoryRepository.UpdateCategory(categoryMap))
        {
            ModelState.AddModelError("", "error");
            return StatusCode(500, ModelState);
        }

        return Ok();
    }
    
    [HttpDelete("{categoryId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteCategory(int categoryId)
    {
        if (!_categoryRepository.CategoryExist(categoryId))
            return NotFound();

        var categoryDelete = _categoryRepository.GetCategory(categoryId);
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_categoryRepository.DeleteCategory(categoryDelete))
        {
            ModelState.AddModelError("","error");
        }

        return Ok();
    }
}