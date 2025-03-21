namespace HseBank.Domain.Interfaces;

/// <summary>
/// Тип категории: доход или расход
/// </summary>
public enum CategoryType
{
    Income,
    Expense
}

public interface ICategory : IEntity
{
    int Id { get; }
    CategoryType Type { get; set; }
    string Name { get; set; }
}