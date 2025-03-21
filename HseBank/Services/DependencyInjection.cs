using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HseBank.Domain.Factories;
using HseBank.Domain.Interfaces;
using HseBank.Infrastructure.Data;
using HseBank.Services.Facades;
using HseBank.Services.Implementations;
using HseBank.Services.Interfaces;
using HseBank.Interaction;

namespace HseBank;

public static class DependencyInjection
{
    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddLogging(config =>
        {
            config.AddConsole();
        });

        // 1) Регистрация фабрики (DomainFactory) как Singleton.
        services.AddSingleton<IDomainFactory, DomainFactory>();

        // 2) Сначала регистрирую репозитории без привязки к интерфейсам.
        services.AddSingleton<Repository<IBankAccount>>();
        services.AddSingleton<Repository<ICategory>>();
        services.AddSingleton<Repository<IOperation>>();
        
        // 3) Потом регистирирую прокси с привязкой к интерфейсам.
        // Для конструкции прокси использую ранее зарегистрированные репозитории.
        services.AddSingleton<IRepository<IBankAccount>>(sp =>
            new RepositoryProxy<IBankAccount>(
                sp.GetRequiredService<Repository<IBankAccount>>()
            )
        );
        services.AddSingleton<IRepository<ICategory>>(sp =>
            new RepositoryProxy<ICategory>(
                sp.GetRequiredService<Repository<ICategory>>()
            )
        );
        services.AddSingleton<IRepository<IOperation>>(sp =>
            new RepositoryProxy<IOperation>(
                sp.GetRequiredService<Repository<IOperation>>()
            )
        );

        // 4) Регистрация сервисов.
        services.AddTransient<IBankAccountService, BankAccountService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IOperationService, OperationService>();

        // 5) Регистрация фасадов.
        services.AddTransient<BankAccountFacade>();
        services.AddTransient<CategoryFacade>();
        services.AddTransient<OperationFacade>();
        services.AddTransient<AnalyticsFacade>();
        
        // 6) Регистрация UserInterface.
        services.AddTransient<UserInterface>();
        
        // 7) Регистрация сервиса для работы с консолью.
        services.AddTransient<ConsoleApp>();

        // Собираем.
        return services.BuildServiceProvider();
    }
}