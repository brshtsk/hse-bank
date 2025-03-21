﻿using HseBank.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using HseBank.Services.Interfaces;
using HseBank.Infrastructure.Export;

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
            _logger.LogWarning(e,
                "Ошибка при создании счета {Name} с начальным балансом {InitialBalance}. Сообщение об ошибке: {Message}",
                name, initialBalance, e.Message);
            return -1;
        }
    }

    public float Deposit(int accountId, float amount)
    {
        try
        {
            _bankAccountService.Deposit(accountId, amount);
            return amount;
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

    public IBankAccount GetAccount(int id)
    {
        return _bankAccountService.GetAccount(id);
    }

    public object GetData()
    {
        try
        {
            IEnumerable<IBankAccount> accounts = _bankAccountService.GetAccounts();
            return accounts;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving bank account data.");
            return null;
        }
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}