using HseBank.Domain.Interfaces;

namespace HseBank.Services.Interfaces;

public interface IBankAccountService
{
    IBankAccount CreateAccount(string name, float initialBalance = 0);
    IBankAccount GetAccount(int id);
    void Deposit(int accountId, float amount);
    void Withdraw(int accountId, float amount);
    IEnumerable<IBankAccount> GetAccounts();
}