using Microsoft.Extensions.Logging;
using HseBank.Domain.Interfaces;
using HseBank.Services.Interfaces;

namespace HseBank.Services.Facades;

public class OperationFacade
{
    private readonly IOperationService _operationService;
    private readonly IBankAccountService _bankAccountService;
    private readonly ILogger<OperationFacade> _logger;

    public OperationFacade(IOperationService operationService, IBankAccountService bankAccountService,
        ILogger<OperationFacade> logger)
    {
        _operationService = operationService;
        _bankAccountService = bankAccountService;
        _logger = logger;
    }

    public int CreateOperation(OperationType type, int bankAccountId, float amount, DateTime date, int categoryId,
        string description = "")
    {
        // Создание операции
        var operation = _operationService.CreateOperation(type, bankAccountId, amount, date, categoryId, description);

        // Обновляем баланс счета: если доход – пополнение, если расход – списание
        if (type == OperationType.Income)
        {
            try
            {
                _bankAccountService.Deposit(bankAccountId, amount);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "Ошибка при пополнении счета {BankAccountId} на сумму {Amount}", bankAccountId,
                    amount);
                return -1;
            }
        }
        else if (type == OperationType.Expense)
        {
            try
            {
                _bankAccountService.Withdraw(bankAccountId, amount);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "Ошибка при списании средств со счета {BankAccountId} на сумму {Amount}",
                    bankAccountId, amount);
                return -1;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogWarning(e,
                    "Ошибка при списании со счета {BankAccountId} на сумму {Amount}. Сообщение об ошибке: {Message}",
                    bankAccountId, amount, e.Message);
                return -1;
            }
        }

        return operation.Id;
    }

    public IOperation GetOperation(int id)
    {
        return _operationService.GetOperation(id);
    }

    public IEnumerable<IOperation> GetOperationsByAccount(int bankAccountId)
    {
        return _operationService.GetOperationsByAccount(bankAccountId);
    }
}