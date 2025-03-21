using HseBank.Services.Facades;

namespace HseBank.Interaction.Commands;

public class GetAccountInfoCommand : ICommand
{
    private readonly BankAccountFacade _facade;
    private readonly int _accountId;

    public GetAccountInfoCommand(BankAccountFacade facade, int accountId)
    {
        _facade = facade;
        _accountId = accountId;
    }

    public void Execute()
    {
        var account = _facade.GetAccount(_accountId);
        if (account != null)
        {
            Console.WriteLine($"Информация о счете {_accountId}:");
            Console.WriteLine($"Имя: {account.Name}");
            Console.WriteLine($"Баланс: {account.Balance}");
            return;
        }

        Console.WriteLine($"Счет {_accountId} не найден.");
    }
}