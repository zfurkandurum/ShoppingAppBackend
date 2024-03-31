using ShoppingAppBackend.Dto;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Interfaces;

public interface ICategoryRepository
{
    ICollection<Category> GetCategories();

    Category GetCategory(int categoryId);

    bool CategoryExist(int categoryId);
    
    bool CreateCategory(Category category);
    
    bool UpdateCategory(Category category);
    
    bool DeleteCategory(Category category);
    
    bool Save();

}