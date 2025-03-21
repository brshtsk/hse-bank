using Microsoft.Extensions.Logging;
using HseBank.Services.Interfaces;

namespace HseBank.Services.Facades;

public class BankAccountFacade
{
    private readonly IBankAccountService _bankAccountService;
    private readonly ILogger<BankAccountFacade> _logger;

    public BankAccountFacade(IBankAccountService bankAccountService, ILogger<BankAccountFacade> logger)
    {
        _bankAccountService = bankAccountService;
        _logger = logger;
    }

    public int CreateAccount(string name, float initialBalance = 0)
    {
        try
        {
            var account = _bankAccountService.CreateAccount(name, initialBalance);
            return account.Id;
        }
        catch (ArgumentException e)
        {
            _logger.LogWarning(e, "Ошибка при создании счета {Name} с начальным балансом {InitialBalance}. Сообщение об ошибке: {Message}",
                name, initialBalance, e.Message);
            return -1;
        }
    }

    public int Deposit(int accountId, float amount)
    {
        try
        {
            _bankAccountService.Deposit(accountId, amount);
            return 0;
        }
        catch (ArgumentException e)
        {
            _logger.LogWarning(e, "Ошибка при пополнении счета {AccountId} на сумму {Amount}", accountId, amount);
            return -1;
        }
    }

    public int Withdraw(int accountId, float amount)
    {
        try
        {
            _bankAccountService.Withdraw(accountId, amount);
            return 0;
        }
        catch (ArgumentException e)
        {
            _logger.LogWarning(e, "Ошибка при списании со счета {AccountId} на сумму {Amount}.", accountId, amount);
            return -1;
        }
        catch (InvalidOperationException e)
        {
            _logger.LogWarning(e,
                "Ошибка при списании со счета {AccountId} на сумму {Amount}.",
                accountId, amount);
            return -1;
        }
    }
}