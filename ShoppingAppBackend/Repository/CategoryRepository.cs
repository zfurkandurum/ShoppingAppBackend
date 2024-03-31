using ShoppingAppBackend.Data;
using ShoppingAppBackend.Interfaces;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Repository;

public class CategoryRepository : ICategoryRepository
{
    private DataContext _context;

    public CategoryRepository(DataContext context)
    {
        _context = context;
    }
    
    public ICollection<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public Category GetCategory(int categoryId)
    {
        return _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
    }

    public bool CategoryExist(int categoryId)
    {
        return _context.Categories.Any(c => c.CategoryId == categoryId);
    }

    public bool CreateCategory(Category category)
    {
        _context.Add(category);
        return Save();
    }

    public bool UpdateCategory(Category category)
    {
        _context.Update(category);
        return Save();
    }

    public bool DeleteCategory(Category category)
    {
        _context.Remove(category);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}