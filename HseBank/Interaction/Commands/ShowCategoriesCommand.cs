using HseBank.Services.Facades;

namespace HseBank.Interaction.Commands;

/// <summary>
/// Команда показа категорий.
/// </summary>
public class ShowCategoriesCommand : ICommand
{
    private readonly CategoryFacade _facade;
    
    public ShowCategoriesCommand(CategoryFacade facade)
    {
        _facade = facade;
    }
    
    public void Execute()
    {
        var categories = _facade.GetAllCategories();
        if (categories.Count() == 0)
        {
            Console.WriteLine("Категорий пока нет.");
        }
        else
        {
            Console.WriteLine("Список категорий:");
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Id}. {category.Name}");
            }
        }
    }
}