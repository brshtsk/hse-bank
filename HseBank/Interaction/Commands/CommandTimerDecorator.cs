using System.Diagnostics;

namespace HseBank.Interaction.Commands;

/// <summary>
/// Декоратор для измерения времени выполнения команды.
/// </summary>
public class CommandTimerDecorator : ICommand
{
    private readonly ICommand _command;

    public CommandTimerDecorator(ICommand command)
    {
        _command = command ?? throw new ArgumentNullException(nameof(command));
    }

    public void Execute()
    {
        var sw = Stopwatch.StartNew();
        _command.Execute();
        sw.Stop();
        Console.WriteLine($"Время выполнения команды: {sw.ElapsedMilliseconds} мс");
    }
}