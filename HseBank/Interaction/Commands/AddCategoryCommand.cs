using HseBank.Services.Facades;
using HseBank.Domain.Interfaces;

namespace HseBank.Interaction.Commands;

/// <summary>
/// Команда для добавления категории.
/// </summary>
public class AddCategoryCommand : ICommand
{
    private readonly CategoryFacade _facade;
    private readonly string _name;
    private readonly OperationType _operationType;
    
    public AddCategoryCommand(CategoryFacade facade, string name, OperationType operationType)
    {
        _facade = facade;
        _name = name;
        _operationType = operationType;
    }
    
    public void Execute()
    {
        int id = _facade.CreateCategory(_operationType, _name);
        if (id != -1)
        {
            Console.WriteLine($"Категория {_name} успешно добавлена. ID категории: {id}");
        }
    }
}