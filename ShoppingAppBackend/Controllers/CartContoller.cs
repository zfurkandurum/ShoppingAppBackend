using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingAppBackend.Dto;
using ShoppingAppBackend.Interfaces;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartContoller : Controller
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CartContoller(ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Cart>))]
    public IActionResult GetCartById(int userId)
    {
        var cartById = _cartRepository.GetCartByUserId(userId);

        if (cartById == null)
            return NotFound();
        
        return Ok(cartById);
    }

    [HttpPost("{userId}/addItem")]
    public IActionResult AddItemToCart(int userId, [FromBody] CartItem cartItem)
    {
        if (cartItem == null)
        {
            return BadRequest("CartItem cannot be null.");
        }

        if (!_cartRepository.AddItemToCart(userId, cartItem.ProductId, cartItem.Quantity))
        {
            return BadRequest("Failed to add item to cart.");
        }

        return Ok();
    }
}