﻿using Microsoft.Extensions.Logging;
using HseBank.Domain.Interfaces;
using HseBank.Services.Interfaces;
using HseBank.Infrastructure.Export;

namespace HseBank.Services.Facades;

public class OperationFacade
{
    private readonly IOperationService _operationService;
    private readonly IBankAccountService _bankAccountService;
    private readonly ICategoryService _categoryService;
    private readonly ILogger<OperationFacade> _logger;

    public OperationFacade(IOperationService operationService, IBankAccountService bankAccountService,
        ICategoryService categoryService, ILogger<OperationFacade> logger)
    {
        _operationService = operationService;
        _bankAccountService = bankAccountService;
        _categoryService = categoryService;
        _logger = logger;
    }

    public int CreateOperation(OperationType type, int bankAccountId, float amount, DateTime date, int categoryId,
        string description = "")
    {
        // Проверка наличия такой категории
        if (_categoryService.GetCategory(categoryId) == null)
        {
            _logger.LogWarning("Категория {CategoryId} не найдена", categoryId);
            return -1;
        }

        // Проверка соответствия типа операции
        if (_categoryService.GetCategory(categoryId).Type != type)
        {
            _logger.LogWarning("Категория {CategoryId} не соответствует типу операции {Type}", categoryId, type);
            return -1;
        }

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

    public object GetData()
    {
        try
        {
            IEnumerable<IOperation> operations = _operationService.GetOperations();
            return operations;
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