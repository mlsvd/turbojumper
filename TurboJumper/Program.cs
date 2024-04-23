using Microsoft.Extensions.DependencyInjection;
using TurboJumper.Decorators;
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
        
        var serviceProvider = new ServiceCollection()
            .AddTransient<ProcessProvider>()
            .AddTransient<IProcessProvider, DecoratedProcessProvider>()
            .AddTransient<IProcessListPresenter, ProcessListPresenter>()
            .AddTransient<ProcessListMainProcessFilterDecorator>()
            .AddTransient<MainForm>()
            .BuildServiceProvider();


        ApplicationConfiguration.Initialize();

        var mainForm = serviceProvider.GetRequiredService<MainForm>();
        Application.Run(mainForm);
    }
}
