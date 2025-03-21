namespace HseBank.Domain.Interfaces;

/// <summary>
/// Фабрика для создания доменных объектов.
/// Позволяет централизовать валидацию (например, запретить отрицательные суммы).
/// </summary>
public interface IDomainFactory
{
    IBankAccount CreateBankAccount(string name, float initialBalance = 0);
    ICategory CreateCategory(OperationType type, string name);

    IOperation CreateOperation(OperationType type, int bankAccountId, float amount, DateTime date, int categoryId,
        string description = "");
}