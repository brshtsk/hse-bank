namespace HseBank.Infrastructure.Export;

public interface IVisitor
{
    void Visit<T>(T source);
}