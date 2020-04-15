using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TemplateCommandLineApp.Commands
{
    public class ClearScreenCommand : ICommand
    {
        private CommandLineApplication _app;

        public ClearScreenCommand(CommandLineApplication app)
        {
            _app = app;
        }

        public void Run()
        {
            Console.Clear();
        }

        public static void Configure(CommandLineApplication command)
        {
            command.Description = "Clears the screen";
            command.HelpOption("-?|-h|--help");

            command.OnExecute(() =>

            {
                new ClearScreenCommand(command).Run();
                return 0;
            });
        }
    }
}