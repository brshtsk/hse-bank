using HseBank.Services.Facades;
using HseBank.Interaction.Commands;
using HseBank.Domain.Interfaces;

namespace HseBank.Interaction;

/// <summary>
/// Отвечает за взаимодействие с пользователем через консоль:
/// показывает меню, обрабатывает ввод, вызывает соответствующие команды.
/// </summary>
public class UserInterface
{
    private readonly BankAccountFacade _bankAccountFacade;
    private readonly CategoryFacade _categoryFacade;
    private readonly OperationFacade _operationFacade;
    private readonly AnalyticsFacade _analyticsFacade;

    public UserInterface(
        BankAccountFacade bankAccountFacade,
        CategoryFacade categoryFacade,
        OperationFacade operationFacade,
        AnalyticsFacade analyticsFacade)
    {
        _bankAccountFacade = bankAccountFacade;
        _categoryFacade = categoryFacade;
        _operationFacade = operationFacade;
        _analyticsFacade = analyticsFacade;
    }

    /// <summary>
    /// Основное меню приложения. 
    /// Показывает пункты, считывает выбор пользователя и вызывает соответствующие методы.
    /// </summary>
    public void ShowMainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n==== Главное меню ====");
            Console.WriteLine("1. Создать счёт");
            Console.WriteLine("2. Добавить категорию");
            Console.WriteLine("3. Пополнить счёт");
            Console.WriteLine("4. Снять со счёта");
            Console.WriteLine("5. Перевести между счетами");
            Console.WriteLine("6. Показать все категории");
            Console.WriteLine("7. Совершить операцию (доход/расход)");
            Console.WriteLine("8. Показать статистику по банку");
            Console.WriteLine("9. Показать информацию о счёте");
            Console.WriteLine("0. Выход");

            Console.Write("Выберите пункт меню: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateAccountFlow();
                    break;
                case "2":
                    AddCategoryFlow();
                    break;
                case "3":
                    DepositFlow();
                    break;
                case "4":
                    WithdrawFlow();
                    break;
                case "5":
                    TransferFlow();
                    break;
                case "6":
                    ShowCategoriesFlow();
                    break;
                case "7":
                    MakeOperationFlow();
                    break;
                case "8":
                    ShowStatisticsFlow();
                    break;
                case "9":
                    ShowAccountInfoFlow();
                    break;
                case "0":
                    Console.WriteLine("Выход из приложения...");
                    return;
                default:
                    Console.WriteLine("Неизвестный пункт меню. Повторите ввод.");
                    break;
            }
        }
    }

    #region Сценарии взаимодействия

    private void CreateAccountFlow()
    {
        Console.Write("Введите название счёта: ");
        var name = Console.ReadLine();

        Console.Write("Введите начальный баланс (число): ");
        var balanceInput = Console.ReadLine();
        float initialBalance = 0;

        float.TryParse(balanceInput, out initialBalance);

        // Создаём команду
        var command = new CreateAccountCommand(_bankAccountFacade, name, initialBalance);
        // Оборачиваем её в декоратор для измерения времени
        var timedCommand = new CommandTimerDecorator(command);
        // Выполняем
        timedCommand.Execute();
    }

    private void AddCategoryFlow()
    {
        Console.Write("Введите название категории: ");
        var name = Console.ReadLine();

        Console.Write("Выберите тип категории (1 - Доход, 2 - Расход): ");
        var typeInput = Console.ReadLine();
        OperationType operationType = typeInput == "1" ? OperationType.Income : OperationType.Expense;

        var command = new AddCategoryCommand(_categoryFacade, name, operationType);
        var timedCommand = new CommandTimerDecorator(command);
        timedCommand.Execute();
    }

    private void DepositFlow()
    {
        Console.Write("Введите ID счёта: ");
        var accountIdInput = Console.ReadLine();
        int.TryParse(accountIdInput, out var accountId);

        Console.Write("Введите сумму для пополнения: ");
        var amountInput = Console.ReadLine();
        float.TryParse(amountInput, out var amount);

        var command = new DepositCommand(_bankAccountFacade, accountId, amount);
        var timedCommand = new CommandTimerDecorator(command);
        timedCommand.Execute();
    }

    private void WithdrawFlow()
    {
        Console.Write("Введите ID счёта: ");
        var accountIdInput = Console.ReadLine();
        int.TryParse(accountIdInput, out var accountId);

        Console.Write("Введите сумму для снятия: ");
        var amountInput = Console.ReadLine();
        float.TryParse(amountInput, out var amount);

        var command = new WithdrawCommand(_bankAccountFacade, accountId, amount);
        var timedCommand = new CommandTimerDecorator(command);
        timedCommand.Execute();
    }

    private void TransferFlow()
    {
        Console.Write("Введите ID счёта-отправителя: ");
        var sourceIdInput = Console.ReadLine();
        int.TryParse(sourceIdInput, out var sourceId);

        Console.Write("Введите ID счёта-получателя: ");
        var targetIdInput = Console.ReadLine();
        int.TryParse(targetIdInput, out var targetId);

        Console.Write("Введите сумму для перевода: ");
        var amountInput = Console.ReadLine();
        float.TryParse(amountInput, out var amount);

        var command = new TransferCommand(_bankAccountFacade, sourceId, targetId, amount);
        var timedCommand = new CommandTimerDecorator(command);
        timedCommand.Execute();
    }

    private void ShowCategoriesFlow()
    {
        var command = new ShowCategoriesCommand(_categoryFacade);
        var timedCommand = new CommandTimerDecorator(command);
        timedCommand.Execute();
    }

    private void MakeOperationFlow()
    {
        Console.Write("Введите тип операции (1 - Доход, 2 - Расход): ");
        var typeInput = Console.ReadLine();
        OperationType type = typeInput == "1" ? OperationType.Income : OperationType.Expense;

        Console.Write("Введите ID счёта: ");
        var accountIdInput = Console.ReadLine();
        int.TryParse(accountIdInput, out var accountId);

        Console.Write("Введите сумму: ");
        var amountInput = Console.ReadLine();
        float.TryParse(amountInput, out var amount);

        Console.Write("Введите ID категории: ");
        var categoryIdInput = Console.ReadLine();
        int.TryParse(categoryIdInput, out var categoryId);

        // Для простоты дата и описание берутся по умолчанию
        var command = new MakeOperationCommand(_operationFacade, type, accountId, amount, categoryId);
        var timedCommand = new CommandTimerDecorator(command);
        timedCommand.Execute();
    }

    private void ShowStatisticsFlow()
    {
        var command = new GetStatisticsCommand(_analyticsFacade);
        var timedCommand = new CommandTimerDecorator(command);
        timedCommand.Execute();
    }

    private void ShowAccountInfoFlow()
    {
        Console.Write("Введите ID счёта: ");
        var accountIdInput = Console.ReadLine();
        int.TryParse(accountIdInput, out var accountId);

        var command = new GetAccountInfoCommand(_bankAccountFacade, accountId);
        var timedCommand = new CommandTimerDecorator(command);
        timedCommand.Execute();
    }

    #endregion
}