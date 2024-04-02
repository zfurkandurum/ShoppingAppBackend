using ShoppingAppBackend.Data;
using ShoppingAppBackend.Interfaces;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Repository;

public class ProductRepository : IProductRepository
{
    private DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }
    
    public ICollection<Product> GetProducts()
    {
        return _context.Products.ToList();
    }

    public List<List<Product>> GetProductByCategory(int categoryId)
    {
        return _context.Categories.Where(c => c.CategoryId == categoryId).Select(c => c.Products).ToList();
    }

    public Product GetProduct(int productId)
    {
        return _context.Products.Where(p => p.ProductId == productId).FirstOrDefault();
    }

    public bool ProductExists(int productId)
    {
        return _context.Products.Any(p => p.ProductId == productId);
    }

    public bool CreateProduct(Product product)
    {
        _context.Add(product);
        return Save();
    }

    public bool DeleteProduct(Product product)
    {
        _context.Remove(product);
        return Save();
    }

    public bool UpdateProduct(Product product)
    {
        _context.Update(product);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}