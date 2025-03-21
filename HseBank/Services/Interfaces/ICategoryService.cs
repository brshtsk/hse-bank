using HseBank.Domain.Interfaces;

namespace HseBank.Services.Interfaces;

public interface ICategoryService
{
    ICategory CreateCategory(CategoryType type, string name);
    ICategory GetCategory(int id);
    IEnumerable<ICategory> GetAllCategories();
    void UpdateCategory(ICategory category);
    void DeleteCategory(int id);
}