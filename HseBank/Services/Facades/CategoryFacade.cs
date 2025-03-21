﻿using Microsoft.Extensions.Logging;
using HseBank.Domain.Interfaces;
using HseBank.Services.Interfaces;

namespace HseBank.Services.Facades;

public class CategoryFacade
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryFacade> _logger;

    public CategoryFacade(ICategoryService categoryService, ILogger<CategoryFacade> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public int CreateCategory(CategoryType type, string name)
    {
        var category = _categoryService.CreateCategory(type, name);
        return category.Id;
    }

    public IEnumerable<ICategory> GetAllCategories()
    {
        return _categoryService.GetAllCategories();
    }

    public void UpdateCategory(ICategory category)
    {
        try
        {
            _categoryService.UpdateCategory(category);
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, "Ошибка при обновлении категории {category}", category.Id);
            throw;
        }
    }

    public void DeleteCategory(int id)
    {
        _categoryService.DeleteCategory(id);
    }
}