using HseBank.Domain.Interfaces;

namespace HseBank.Domain.Entities;

public class Operation : IOperation
{
    private static int _operationsCount = 0;

    public int Id { get; private set; }
    public OperationType Type { get; set; }
    public int BankAccountId { get; set; }
    public float Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }

    internal Operation(OperationType type, int bankAccountId, float amount, DateTime date, int categoryId,
        string description = "")
    {
        Id = ++_operationsCount;
        Type = type;
        BankAccountId = bankAccountId;
        Amount = amount;
        Date = date;
        CategoryId = categoryId;
        Description = description;
    }
}