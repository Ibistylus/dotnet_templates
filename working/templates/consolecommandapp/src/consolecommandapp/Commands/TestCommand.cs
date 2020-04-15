using System;
using McMaster.Extensions.CommandLineUtils;

namespace TemplateCommandLineApp.Commands
{
    public class TestCommand : ICommand
    {
        private readonly string _yourNameArgument;

        public TestCommand(string yourNameArgument)
        {
            _yourNameArgument = yourNameArgument;
        }

        public void Run()
        {
            var nameArgument = _yourNameArgument != null
                ? _yourNameArgument
                : "boss";
            Console.WriteLine($"Hello there {nameArgument}");
        }

        public static void Configure(CommandLineApplication command)
        {
            command.Description = "A tests command on which to play around.";
            command.HelpOption("-?|-h|--help");

            var yourNameArgument = command.Argument("[YourName]",
                "A name to be excited about.").IsRequired();

            command.OnExecute(() =>

            {
                new TestCommand(yourNameArgument.Value).Run();
                return 0;
            });
        }
    }
}