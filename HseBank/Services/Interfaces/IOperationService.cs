using HseBank.Domain.Interfaces;

namespace HseBank.Services.Interfaces;

public interface IOperationService
{
    IOperation CreateOperation(OperationType type, int bankAccountId, float amount, DateTime date, int categoryId, string description = "");
    IOperation GetOperation(int id);
    IEnumerable<IOperation> GetOperationsByAccount(int bankAccountId);
    IEnumerable<IOperation> GetOperations();
}