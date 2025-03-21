using HseBank.Domain.Interfaces;

namespace HseBank.Domain.Entities;

public class Category : ICategory
{
    private static int _categoriesCount = 0;
    
    public int Id { get; private set; }
    public OperationType Type { get; set; }
    public string Name { get; set; }

    internal Category(OperationType type, string name)
    {
        Id = ++_categoriesCount;
        Type = type;
        Name = name;
    }
}