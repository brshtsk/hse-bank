namespace HseBank.Domain.Interfaces;

public interface IBankAccount : IEntity
{
    int Id { get; }
    string Name { get; set; }
    float Balance { get; }

    /// <summary>
    /// Увеличивает баланс счета на указанную сумму
    /// </summary>
    /// <param name="amount">Сумма пополнения</param>
    void Deposit(float amount);

    /// <summary>
    /// Уменьшает баланс счета на указанную сумму
    /// </summary>
    /// <param name="amount">Сумма списания</param>
    void Withdraw(float amount);
}