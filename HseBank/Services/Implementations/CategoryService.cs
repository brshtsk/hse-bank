using HseBank.Infrastructure.Data;
using HseBank.Domain.Interfaces;
using HseBank.Services.Interfaces;

namespace HseBank.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly IDomainFactory _domainFactory;
    private readonly IRepository<ICategory> _categoryRepository;

    public CategoryService(IDomainFactory domainFactory, IRepository<ICategory> categoryRepository)
    {
        _domainFactory = domainFactory;
        _categoryRepository = categoryRepository;
    }

    public ICategory CreateCategory(OperationType type, string name)
    {
        var category = _domainFactory.CreateCategory(type, name);
        _categoryRepository.Add(category);
        return category;
    }

    public ICategory GetCategory(int id)
    {
        return _categoryRepository.Get(id);
    }

    public IEnumerable<ICategory> GetAllCategories()
    {
        return _categoryRepository.GetAll();
    }

    public void UpdateCategory(ICategory category)
    {
        var existingCategory = _categoryRepository.Get(category.Id);
        if (existingCategory == null)
            throw new ArgumentException("Category not found");

        _categoryRepository.Update(category);
    }

    public void DeleteCategory(int id)
    {
        _categoryRepository.Delete(id);
    }
}