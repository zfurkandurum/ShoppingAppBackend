using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppBackend.Dto;
using ShoppingAppBackend.Interfaces;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Product>))]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(products);
    }
    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetByCategoryId(int categoryId)
    {
        var productsInCategory =  _productRepository.GetProductByCategory(categoryId);
            
        if (productsInCategory == null || productsInCategory.Count == 0)
        {
            return NotFound("Belirtilen kategoriye ait ürün bulunamadı.");
        }

        return Ok(productsInCategory);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateProduct([FromBody] ProductDto createProduct)
    {
        if (createProduct == null)
            return BadRequest(ModelState);

        var product = _productRepository.GetProducts().Where(p => p.ProductId == createProduct.ProductId).FirstOrDefault();

        if (product != null)
        {
            ModelState.AddModelError("","Already Exists");
            return StatusCode(422, ModelState);
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var productMap = _mapper.Map<Product>(createProduct);

        if (!_productRepository.CreateProduct(productMap))
        {
            ModelState.AddModelError("","error");
            return StatusCode(500, ModelState);
        }
        return Ok("Done");
    }

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult UpdateProduct(int productId,[FromBody] ProductDto updateProduct)
    {
        if (updateProduct == null)
            return BadRequest(ModelState);
        if (productId != updateProduct.ProductId)
            return BadRequest(ModelState);
        if (!_productRepository.ProductExists(productId))
            return NotFound();
        if (!ModelState.IsValid)
            return BadRequest();

        var productMap = _mapper.Map<Product>(updateProduct);

        if (!_productRepository.UpdateProduct(productMap))
        {
            ModelState.AddModelError("", "error");
            return StatusCode(500, ModelState);
        }

        return Ok();
    }
    
    [HttpDelete("{productId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult DeleteProduct(int productId)
    {
        if (!_productRepository.ProductExists(productId))
            return BadRequest(ModelState);

        var productDelete = _productRepository.GetProduct(productId);
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_productRepository.DeleteProduct(productDelete))
        {
            ModelState.AddModelError("","error");
        }

        return Ok();
    }
}