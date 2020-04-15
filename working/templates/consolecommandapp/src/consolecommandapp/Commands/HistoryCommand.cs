using System;
using System.Collections.Immutable;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TemplateCommandLineApp.Commands
{
    public class HistoryCommand : ICommand
    {
        private readonly CommandLineApplication _app;
        private readonly string[] _history;

        public HistoryCommand(CommandLineApplication app)
        {
            _app = app;
        }

        /// <summary>
        /// Runs the command after instantiation
        /// </summary>
        public void Run()
        {
            Console.WriteLine($"Command history");
            foreach (var historyItem in ReadLine.GetHistory().ToImmutableSortedSet())
            {
                Console.WriteLine(historyItem);
            }

            ReadLine.GetHistory();
        }


        /// <summary>
        /// Configuration of the command
        /// </summary>
        /// <param name="command">Parent command</param>
        public static void Configure(CommandLineApplication command)
        {
            command.Description = "Retrieve input history.";
            command.HelpOption("-?|-h|--help");

            command.OnExecute(() =>

            {
                new HistoryCommand(command).Run();
                return 0;
            });
        }
    }
}