using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Interfaces;

public interface ICartRepository
{
    ICollection<Cart> GetCarts();
    Cart GetCartByUserId(int userId);
    bool CartExists(int userId);
    bool AddItemToCart(int userId,int productId, int quantity);
    bool RemoveItemFromCart(int userId, int productId);
    bool ClearCart(int userId);
    bool Save(int userId);
}