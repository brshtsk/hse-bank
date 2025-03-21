using HseBank;
using HseBank.Interaction;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        // Настраиваем DI.
        var serviceProvider = DependencyInjection.ConfigureServices();

        // Создаём экземпляр пользовательского интерфейса.
        var ui = serviceProvider.GetRequiredService<UserInterface>();

        // И создаём ConsoleApp.
        var app = new ConsoleApp(ui);
        app.Run();
    }
}