using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Interfaces;

public interface IProductRepository
{
    ICollection<Product> GetProducts();

    List<List<Product>> GetProductByCategory(int categoryId);
    Product GetProduct(int productId);

    bool ProductExists(int productId);

    bool CreateProduct(Product product);

    bool DeleteProduct(Product product);

    bool UpdateProduct(Product product);

    bool Save();
}