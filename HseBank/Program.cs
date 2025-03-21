using HseBank;
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

        var createAccountCommand = new CreateAccountCommand(bankAccountFacade, "Alice", 1000);
        createAccountCommand.Execute();

        var createAccountCommand2 = new CreateAccountCommand(bankAccountFacade, "Bob", 500);
        createAccountCommand2.Execute();

        var transferCommand = new TransferCommand(bankAccountFacade, 1, 2, 500);
        transferCommand.Execute();
    }
}