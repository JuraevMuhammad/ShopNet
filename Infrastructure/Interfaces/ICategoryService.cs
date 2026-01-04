using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ICategoryService
{
    List<Category> GetAllCategories();
    Category GetCategoryById(int id);
    bool AddCategory(Category category);
    bool UpdateCategory(Category category);
    bool DeleteCategory(int id);
}