using HseBank;
using HseBank.Domain.Interfaces;
using HseBank.Interaction.Commands;
using HseBank.Services.Facades;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        // Инициализируем DI-контейнер
        var serviceProvider = DependencyInjection.ConfigureServices();

        // Получаем фасад, сервис или любой другой зарегистрированный класс
        var bankAccountFacade = serviceProvider.GetRequiredService<BankAccountFacade>();
        var categoryFacade = serviceProvider.GetRequiredService<CategoryFacade>();
        var operationFacade = serviceProvider.GetRequiredService<OperationFacade>();
        var statisticsFacade = serviceProvider.GetRequiredService<AnalyticsFacade>();

        var createAccountCommand = new CreateAccountCommand(bankAccountFacade, "Alice", 1000);
        createAccountCommand.Execute();

        var createAccountCommand2 = new CreateAccountCommand(bankAccountFacade, "Bob", 500);
        createAccountCommand2.Execute();

        var transferCommand = new TransferCommand(bankAccountFacade, 1, 2, 500);
        transferCommand.Execute();

        var newCategoryCommand = new AddCategoryCommand(categoryFacade, "Еда", OperationType.Expense);
        newCategoryCommand.Execute();

        var newCategoryCommand2 = new AddCategoryCommand(categoryFacade, "Зарплата", OperationType.Income);
        newCategoryCommand2.Execute();

        var makeOperationCommand = new MakeOperationCommand(operationFacade, OperationType.Expense, 1, 100, 1);
        makeOperationCommand.Execute();

        var statisticsCommand = new GetStatisticsCommand(statisticsFacade);
        statisticsCommand.Execute();

        // Намеренно добавляю невозможные операции
        var makeOperationCommand2 = new MakeOperationCommand(operationFacade, OperationType.Expense, 1, 100, 999);
        makeOperationCommand2.Execute();

        var transferCommand2 = new TransferCommand(bankAccountFacade, 1, 2, 9999);
        transferCommand2.Execute();

        // Под конец адекватная операция: пополнение на 100
        var makeOperationCommand3 = new MakeOperationCommand(operationFacade, OperationType.Income, 1, 100, 2);
        makeOperationCommand3.Execute();
    }
}