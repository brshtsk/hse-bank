using HseBank.Domain.Interfaces;
using HseBank.Services.Facades;

namespace HseBank.Interaction.Commands;

/// <summary>
/// Команда создания операции в банке.
/// </summary>
public class MakeOperationCommand : ICommand
{
    private readonly OperationFacade _facade;
    private readonly OperationType _type;
    private readonly int _bankAccountId;
    private readonly float _amount;
    private readonly DateTime _date;
    private readonly int _categoryId;
    private readonly string _description;

    public MakeOperationCommand(OperationFacade facade, OperationType type, int bankAccountId, float amount,
        int categoryId, DateTime? date = null, string description = "")
    {
        _facade = facade;
        _type = type;
        _bankAccountId = bankAccountId;
        _amount = amount;
        _date = date ?? DateTime.Now;
        _categoryId = categoryId;
        _description = description;
    }

    public void Execute()
    {
        int res = _facade.CreateOperation(_type, _bankAccountId, _amount, _date, _categoryId, _description);
        if (res != -1)
        {
            Console.WriteLine($"Операция успешно выполнена. ID операции: {res}");
            return;
        }
        
        Console.WriteLine("Операция прервана. Не удалось выполнить операцию.");
    }
}