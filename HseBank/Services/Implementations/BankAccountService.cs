using HseBank.Infrastructure.Data;
using HseBank.Domain.Interfaces;
using HseBank.Infrastructure.Data;
using HseBank.Services.Interfaces;

namespace HseBank.Services.Implementations;

public class BankAccountService : IBankAccountService
{
    private readonly IDomainFactory _domainFactory;
    private readonly IRepository<IBankAccount> _repository;

    public BankAccountService(IDomainFactory domainFactory, IRepository<IBankAccount> repository)
    {
        _domainFactory = domainFactory;
        _repository = repository;
    }

    public IBankAccount CreateAccount(string name, float initialBalance = 0)
    {
        var account = _domainFactory.CreateBankAccount(name, initialBalance);
        _repository.Add(account);
        return account;
    }

    public IBankAccount GetAccount(int id)
    {
        return _repository.Get(id);
    }

    public void Deposit(int accountId, float amount)
    {
        var account = GetAccount(accountId) ?? throw new ArgumentException("Account not found");
        account.Deposit(amount);
        _repository.Update(account);
    }

    public void Withdraw(int accountId, float amount)
    {
        var account = GetAccount(accountId) ?? throw new ArgumentException("Account not found");
        account.Withdraw(amount);
        _repository.Update(account);
    }

    public IEnumerable<IBankAccount> GetAccounts()
    {
        return _repository.GetAll();
    }
}