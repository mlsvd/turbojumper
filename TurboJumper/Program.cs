using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TurboJumper.Decorators;
using TurboJumper.Factories;
using TurboJumper.Handlers;
using TurboJumper.Listeners;
using TurboJumper.Managers;
using TurboJumper.Presenters;
using TurboJumper.Providers;

namespace TurboJumper;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Console.WriteLine("Start");
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false) // Add configuration sources
            .Build();
        
        var serviceProvider = new ServiceCollection()
            // Handlers
            .AddTransient<SwitchToProcessHandler>()
            // Providers
            .AddTransient<ProcessProvider>()
            .AddTransient<IProcessProvider, DecoratedProcessProvider>()
            .AddTransient<IProcessListPresenter, ProcessListPresenter>()
            .AddTransient<ProcessKeyboardShortcutProvider>()
            // Decorators
            .AddTransient<ProcessListMainProcessFilterDecorator>()
            .AddTransient<ProcessListIconDecorator>()
            .AddTransient<ProcessListKeyCombinationDecorator>()
            // Factories
            .AddTransient<FormViewCoordinatesFactory>()
            .AddTransient<ProcessWrapperElementPresenterFactory>()
            .AddTransient<ProcessWrapperFactory>()
            // Listeners
            .AddTransient<KeyboardListener>()
            .AddTransient<ProcessFormButtonListener>()
            // Managers
            // .AddTransient<KeyboardManager>()
            .AddSingleton<KeyboardManager>()
            // 
            .AddTransient<IAppConfigurationProvider, AppConfigurationProvider>()
            .AddTransient<MainForm>()
            .AddSingleton<IConfiguration>(configuration)
            .BuildServiceProvider();


        ApplicationConfiguration.Initialize();

        var mainForm = serviceProvider.GetRequiredService<MainForm>();
        Application.Run(mainForm);
    }
}
