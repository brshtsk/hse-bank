using HseBank.Services.Facades;

namespace HseBank.Interaction.Commands;

/// <summary>
/// Команда перевода средств между счетами в банке.
/// </summary>
public class TransferCommand : ICommand
{
    private readonly BankAccountFacade _facade;
    private readonly int _sourceId;
    private readonly int _targetId;
    private readonly float _amount;

    public TransferCommand(BankAccountFacade facade, int sourceId, int targetId, float amount)
    {
        _facade = facade;
        _sourceId = sourceId;
        _targetId = targetId;
        _amount = amount;
    }

    public void Execute()
    {
        int getMoney = _facade.Withdraw(_sourceId, _amount);
        if (getMoney != -1)
        {
            int putMoney = _facade.Deposit(_targetId, _amount);
            if (putMoney != -1)
            {
                Console.WriteLine($"Со счета {_sourceId} на счет {_targetId} успешно переведено {_amount}\u20bd");
            }
            else
            {
                Console.WriteLine(
                    "Операция перевода прервана. Не удалось зачислить средства на счет. Деньги возвращены отправителю.");
                _facade.Deposit(_sourceId, _amount);
            }
        }
        else
        {
            Console.WriteLine("Операция перевода прервана. Не удалось списать средства со счета.");
        }
    }
}