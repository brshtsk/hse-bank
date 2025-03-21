namespace HseBank.Interaction;

/// <summary>
/// Запускает консольное приложение и передаёт управление UserInterface.
/// </summary>
public class ConsoleApp
{
    private readonly UserInterface _ui;

    public ConsoleApp(UserInterface ui)
    {
        _ui = ui;
    }

    public void Run()
    {
        // Можно добавить приветственное сообщение
        Console.WriteLine("Добро пожаловать в HSE-Банк!");

        // Основной цикл работы приложения
        _ui.ShowMainMenu();
    }
}