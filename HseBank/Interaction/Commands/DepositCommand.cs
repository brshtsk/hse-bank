using HseBank.Services.Facades;

namespace HseBank.Interaction.Commands;

/// <summary>
/// Команда пополнения счета в банке.
/// </summary>
public class DepositCommand : ICommand
{
    private readonly BankAccountFacade _facade;
    private readonly int _targetId;
    private readonly float _amount;
    
    public DepositCommand(BankAccountFacade facade, int targetId, float amount)
    {
        _facade = facade;
        _targetId = targetId;
        _amount = amount;
    }
    
    public void Execute()
    {
        float res = _facade.Deposit(_targetId, _amount);
        if (res >= 0)
        {
            Console.WriteLine($"Счет {_targetId} успешно пополнен на сумму {_amount}");
        }
    }
}