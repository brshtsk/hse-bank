namespace HseBank.Domain.Interfaces;

/// <summary>
/// Тип операции: доход или расход
/// </summary>
public enum OperationType
{
    Income,
    Expense
}

public interface IOperation : IEntity
{
    int Id { get; }
    OperationType Type { get; set; }
    int BankAccountId { get; set; }
    float Amount { get; set; }
    DateTime Date { get; set; }
    string Description { get; set; }
    int CategoryId { get; set; }
}