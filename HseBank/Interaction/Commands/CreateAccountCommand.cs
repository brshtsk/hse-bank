using HseBank.Services.Facades;

namespace HseBank.Interaction.Commands;

/// <summary>
/// Команда создания счета в банке.
/// </summary>
public class CreateAccountCommand : ICommand
{
    private readonly BankAccountFacade _facade;
    private readonly string _name;
    private readonly float _initialBalance;

    public CreateAccountCommand(BankAccountFacade facade, string name, float initialBalance = 0)
    {
        _facade = facade;
        _name = name;
        _initialBalance = initialBalance;
    }

    public void Execute()
    {
        int id = _facade.CreateAccount(_name, _initialBalance);
        if (id != -1)
        {
            Console.WriteLine($"Счет {_name} успешно создан. ID счета: {id}");
        }
    }
}