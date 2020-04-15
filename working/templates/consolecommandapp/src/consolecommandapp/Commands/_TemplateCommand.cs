using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TemplateCommandLineApp.Commands
{
    public class TemplateCommand : ICommand
    {
        private readonly CommandLineApplication _app;
        private ILogger<TemplateCommand> _logger;
        private readonly string _yourNameArgument;

        public TemplateCommand(CommandLineApplication app, string yourNameArgument)
        {
            _yourNameArgument = yourNameArgument;
            _app = app;
            _logger = _app.GetRequiredService<ILogger<TemplateCommand>>();
        }

        /// <summary>
        /// Runs the command after instantiation
        /// </summary>
        public void Run()
        {
            var nameArgument = _yourNameArgument != null
                ? _yourNameArgument
                : "boss";
            Console.WriteLine($"Hello there {nameArgument}");
        }


        /// <summary>
        /// Configuration command
        /// </summary>
        /// <param name="command">Parent command</param>
        public static void Configure(CommandLineApplication command)
        {
            command.Description = "A template command.";
            command.HelpOption("-?|-h|--help");

            var yourNameArgument = command.Argument("[YourName]",
                "A name to be excited about.").IsRequired();

            command.OnExecute(() =>

            {
                new TemplateCommand(command, yourNameArgument.Value).Run();
                return 0;
            });
        }
    }
}