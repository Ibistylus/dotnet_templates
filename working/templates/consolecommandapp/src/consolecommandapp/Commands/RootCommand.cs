using System.Reflection;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace TemplateCommandLineApp.Commands
{
    public class RootCommand : ICommand
    {
        private readonly CommandLineApplication _app;
        private readonly ILogger _logger;

        /// <summary>
        ///     Constructor for root command. This receives the CommandLineApplication, which contains a method
        ///     to get required services from the IoC Container passed at creation of the app.
        /// </summary>
        /// <param name="app">Commandline application that contains available services.</param>
        private RootCommand(CommandLineApplication app)
        {
            _app = app;
//            _logger = _app.GetRequiredService<ILogger<RootCommand>>();
//            _logger = logger.ForContext<RootCommand>();
        }

        public void Run()
        {
            _app.ShowHelp();
        }

        /// <summary>
        ///     Static configuration method that creates a new instance of the root command.
        /// </summary>
        /// <param name="app">CommandLineApplication</param>
        public static void Configure(CommandLineApplication app)
        {
            app.FullName = "BasicConsole Application";
            app.Name = "BasicConsole";
            var thisApp = Assembly.GetExecutingAssembly().FullName;
            app.VersionOption("-v|-ver|--version", thisApp);
            app.HelpOption("-?|-h|--help");

            // NOTE: Register and configure commands here.
            app.Command("clear", ClearScreenCommand.Configure);
            app.Command("history", HistoryCommand.Configure);
            app.Command("tests", TestCommand.Configure);
            app.Command("quit", QuitCommand.Configure);

            app.OnExecute(() =>
            {
                new RootCommand(app).Run();
                return 0;
            });
        }
    }
}