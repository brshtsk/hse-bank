namespace HseBank.Domain.Interfaces;

public interface ICategory : IEntity
{
    int Id { get; }
    OperationType Type { get; set; }
    string Name { get; set; }
}