using HseBank.Domain.Entities;
using HseBank.Domain.Interfaces;

namespace HseBank.Domain.Factories;

/// <summary>
/// Фабрика для централизованного создания доменных объектов
/// и валидации перед созданием.
/// </summary>
public class DomainFactory : IDomainFactory
{
    public IBankAccount CreateBankAccount(string name, float initialBalance = 0)
    {
        // Валидация
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название счета не может быть пустым.");

        if (initialBalance < 0)
            throw new ArgumentException("Начальный баланс не может быть отрицательным.");

        return new BankAccount(name, initialBalance);
    }

    public ICategory CreateCategory(CategoryType type, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название категории не может быть пустым.");

        return new Category(type, name);
    }

    public IOperation CreateOperation(OperationType type, int bankAccountId, float amount, DateTime date,
        int categoryId, string description = "")
    {
        // Пример валидации, запрещающей отрицательные суммы
        if (amount < 0)
            throw new ArgumentException("Сумма операции не может быть отрицательной.");

        return new Operation(type, bankAccountId, amount, date, categoryId, description);
    }
}