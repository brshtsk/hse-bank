using HseBank.Domain.Interfaces;

namespace HseBank.Domain.Entities;

/// <summary>
/// Реализация банковского счета
/// </summary>
public class BankAccount : IBankAccount
{
    private static int _accountsCount = 0;

    public int Id { get; private set; }
    public string Name { get; set; }
    public float Balance { get; private set; }

    // Конструктор доступен только внутри сборки или через фабрику. Поэтому отрицательный баланс передан не будет.
    internal BankAccount(string name, float initialBalance = 0)
    {
        Id = ++_accountsCount;
        Name = name;
        Balance = initialBalance;
    }

    public void Deposit(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("Сумма пополнения не может быть отрицательной.");

        Balance += amount;
    }

    public void Withdraw(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("Сумма списания не может быть отрицательной.");

        if (Balance - amount < 0)
            throw new InvalidOperationException("Недостаточно средств на счете.");

        Balance -= amount;
    }
}