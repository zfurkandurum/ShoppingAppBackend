using Microsoft.AspNetCore.Http.HttpResults;
using ShoppingAppBackend.Data;
using ShoppingAppBackend.Interfaces;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Repository;

public class CartRepository : ICartRepository
{
    private DataContext _context;

    public CartRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<Cart> GetCarts()
    {
        return _context.Carts.ToList();
    }

    public Cart GetCartByUserId(int userId)
    {
        return _context.Carts.Where(c => c.UserId == userId).FirstOrDefault();
    }

    public bool CartExists(int userId)
    {
        return _context.Carts.Any(c => c.UserId == userId);
    }

    public bool AddItemToCart(int userId, int productId, int quantity)
    {
        var cart = GetCartByUserId(userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _context.Carts.Add(cart);
        }
        var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);
        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
        }
        return Save(userId);
    }

    public bool RemoveItemFromCart(int userId, int productId)
    {
        var cart = GetCartByUserId(userId);
        if (cart == null)
            return false;

        var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);
        if (cartItem != null)
        {
            cart.Items.Remove(cartItem);
            return Save(userId);
        }

        return false;
    }

    public bool ClearCart(int userId)
    {
        var cart = GetCartByUserId(userId);
        if (cart == null)
            return false;

        _context.CartItems.RemoveRange(cart.Items);
        return Save(userId);
    }

    public bool Save(int userId)
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}