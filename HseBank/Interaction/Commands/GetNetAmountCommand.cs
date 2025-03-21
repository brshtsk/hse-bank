using HseBank.Services.Facades;

namespace HseBank.Interaction.Commands;

/// <summary>
/// Команда получения суммы операций по счету (пополнения - списания).
/// </summary>
public class GetNetAmountCommand : ICommand
{
    private readonly AnalyticsFacade _facade;
    private readonly int _accountId;
    private readonly DateTime _fromDate;
    private readonly DateTime _toDate;

    public GetNetAmountCommand(AnalyticsFacade facade, int accountId, DateTime? fromDate = null,
        DateTime? toDate = null)
    {
        _facade = facade;
        _accountId = accountId;
        _fromDate = fromDate ?? DateTime.MinValue;
        _toDate = toDate ?? DateTime.MaxValue;
    }

    public void Execute()
    {
        float netAmount = _facade.GetNetAmount(_accountId, _fromDate, _toDate);
        if (netAmount < 0)
        {
            Console.WriteLine("Не удалось получить информацию о сумме операций по счету.");
            return;
        }

        Console.WriteLine(
            $"Сумма операций по счету {_accountId} с {_fromDate:dd.MM.yyyy} по {_toDate:dd.MM.yyyy}: {netAmount}\u20bd");
    }
}