using HseBank.Domain.Interfaces;
using HseBank.Services.Interfaces;

namespace HseBank.Services.Facades;

public class AnalyticsFacade
{
    private readonly IOperationService _operationService;
    private readonly IBankAccountService _bankAccountService;
    private readonly ICategoryService _categoryService;

    public AnalyticsFacade(IOperationService operationService, IBankAccountService bankAccountService,
        ICategoryService categoryService)
    {
        _operationService = operationService;
        _bankAccountService = bankAccountService;
        _categoryService = categoryService;
    }

    /// <summary>
    /// Подсчитывает чистую сумму (доходы минус расходы) за выбранный период для заданного счета.
    /// </summary>
    public float GetNetAmount(int bankAccountId, DateTime from, DateTime to)
    {
        var accountExists = _bankAccountService.GetAccounts().Any(account => account.Id == bankAccountId);
        if (!accountExists)
        {
            return -1;
        }

        var operations = _operationService.GetOperationsByAccount(bankAccountId)
            .Where(op => op.Date >= from && op.Date <= to);

        float totalIncome = operations
            .Where(op => op.Type == OperationType.Income)
            .Sum(op => op.Amount);

        float totalExpense = operations
            .Where(op => op.Type == OperationType.Expense)
            .Sum(op => op.Amount);

        return totalIncome - totalExpense;
    }

    /// <summary>
    /// Показывает общее количество счетов.
    /// </summary>
    public int GetAccountsCount()
    {
        return _bankAccountService.GetAccounts().Count();
    }

    /// <summary>
    /// Показывает количество пустых счетов.
    /// </summary>
    public int GetEmptyAccountsCount()
    {
        return _bankAccountService.GetAccounts().Count(ac => ac.Balance == 0);
    }

    /// <summary>
    /// Группировка сумм расходов по названиям категорий.
    /// </summary>
    public Dictionary<string, float> GetExpensesByCategories()
    {
        var expenseOperations = _operationService.GetOperations()
            .Where(op => op.Type == OperationType.Expense);

        var groupedExpenses = expenseOperations
            .GroupBy(op => op.CategoryId)
            .Select(group => new
            {
                CategoryId = group.Key,
                TotalExpense = group.Sum(op => op.Amount)
            });

        var categories = _categoryService.GetAllCategories();

        var result = (from expenseGroup in groupedExpenses
            join category in categories on expenseGroup.CategoryId equals category.Id
            select new
            {
                CategoryName = category.Name,
                expenseGroup.TotalExpense
            }).ToDictionary(x => x.CategoryName, x => x.TotalExpense);

        return result;
    }

    /// <summary>
    /// Группировка сумм доходов по названиям категорий.
    /// </summary>
    public Dictionary<string, float> GetIncomesByCategories()
    {
        var incomeOperations = _operationService.GetOperations()
            .Where(op => op.Type == OperationType.Income);

        var groupedIncomes = incomeOperations
            .GroupBy(op => op.CategoryId)
            .Select(group => new
            {
                CategoryId = group.Key,
                TotalIncome = group.Sum(op => op.Amount)
            });

        var categories = _categoryService.GetAllCategories();

        var result = (from incomeGroup in groupedIncomes
            join category in categories on incomeGroup.CategoryId equals category.Id
            select new
            {
                CategoryName = category.Name,
                incomeGroup.TotalIncome
            }).ToDictionary(x => x.CategoryName, x => x.TotalIncome);

        return result;
    }
}