using HseBank.Services.Facades;

namespace HseBank.Interaction.Commands;

/// <summary>
/// Команда получения статистики по счетам в банке.
/// </summary>
public class GetStatisticsCommand : ICommand
{
    private readonly AnalyticsFacade _analyticsFacade;

    public GetStatisticsCommand(AnalyticsFacade analyticsFacade)
    {
        _analyticsFacade = analyticsFacade;
    }

    public void Execute()
    {
        int accountsCount = _analyticsFacade.GetAccountsCount();
        Console.WriteLine("Количество счетов в банке: {0}", accountsCount);

        int emptyAccounts = _analyticsFacade.GetEmptyAccountsCount();
        Console.WriteLine("Количество пустых счетов: {0}", emptyAccounts);

        Console.WriteLine();
        Console.WriteLine("Расходы по названиям категорий:");

        var expenses = _analyticsFacade.GetExpensesByCategories();
        foreach (var expense in expenses)
        {
            Console.WriteLine("{0}: {1} руб", expense.Key, expense.Value);
        }

        Console.WriteLine();
        Console.WriteLine("Доходы по названиям категорий:");

        var incomes = _analyticsFacade.GetIncomesByCategories();
        foreach (var income in incomes)
        {
            Console.WriteLine("{0}: {1} руб", income.Key, income.Value);
        }
    }
}