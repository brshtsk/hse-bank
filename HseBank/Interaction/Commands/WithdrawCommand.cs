using HseBank.Services.Facades;

namespace HseBank.Interaction.Commands;

/// <summary>
/// Команда списания средств со счета в банке.
/// </summary>
public class WithdrawCommand : ICommand
{
    private readonly BankAccountFacade _facade;
    private readonly int _targetId;
    private readonly float _amount;

    public WithdrawCommand(BankAccountFacade facade, int targetId, float amount)
    {
        _facade = facade;
        _targetId = targetId;
        _amount = amount;
    }

    public void Execute()
    {
        int res = _facade.Withdraw(_targetId, _amount);
        if (res != -1)
        {
            Console.WriteLine($"Со счета {_targetId} успешно списано {_amount}");
        }
    }
}