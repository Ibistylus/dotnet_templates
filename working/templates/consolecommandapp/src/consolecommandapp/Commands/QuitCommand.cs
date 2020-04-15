using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TemplateCommandLineApp.Commands
{
    public interface IQuitCommand : ICommand    
    {
        
    }
    
    public class QuitCommand : IQuitCommand, ICommand
    {
        private readonly CommandLineApplication _app;
        private ILogger<QuitCommand> _logger;

        public QuitCommand(CommandLineApplication app, ILogger<QuitCommand> logger)
        {
            _app = app;
            _logger = logger;
        }

        /// <summary>
        /// Runs the command after instantiation
        /// </summary>
        public void Run()
        {
            Console.WriteLine($"Exiting...");
            Environment.ExitCode = 1;
        }


        /// <summary>
        /// Configuration of the command
        /// </summary>
        /// <param name="command">Parent command</param>
        public static void Configure(CommandLineApplication command)
        {
            command.AddName("exit");
            command.Description = "Quit the application.";
            command.HelpOption("-?|-h|--help");

            command.OnExecute(() =>

            {
                new QuitCommand(command, command.GetService<ILogger<QuitCommand>>()).Run();
                return 0;
            });
        }
    }
}