using HseBank.Infrastructure.Data;
using HseBank.Domain.Interfaces;
using HseBank.Services.Interfaces;

namespace HseBank.Services.Implementations;

public class OperationService : IOperationService
{
    private readonly IDomainFactory _domainFactory;
    private readonly IRepository<IOperation> _operationRepository;

    public OperationService(IDomainFactory domainFactory, IRepository<IOperation> operationRepository)
    {
        _domainFactory = domainFactory;
        _operationRepository = operationRepository;
    }

    public IOperation CreateOperation(OperationType type, int bankAccountId, float amount, DateTime date,
        int categoryId, string description = "")
    {
        var operation = _domainFactory.CreateOperation(type, bankAccountId, amount, date, categoryId, description);
        _operationRepository.Add(operation);
        return operation;
    }

    public IOperation GetOperation(int id)
    {
        return _operationRepository.Get(id);
    }

    public IEnumerable<IOperation> GetOperationsByAccount(int bankAccountId)
    {
        // Получаем все операции и фильтруем по Id банковского счета
        return _operationRepository.GetAll().Where(op => op.BankAccountId == bankAccountId);
    }

    public IEnumerable<IOperation> GetOperations()
    {
        return _operationRepository.GetAll();
    }
}