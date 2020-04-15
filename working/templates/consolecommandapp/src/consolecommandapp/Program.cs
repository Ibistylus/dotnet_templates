// ** Simple CLI Program that uses a McMaster CommandLineApplication framework. 
// Command application derived from article creating Console Apps Neatly CommandLineApplication: https://gist.github.com/iamarcel/9bdc3f40d95c13f80d259b7eb2bbcabb 
// Added Generic Host using this tutorial: https://www.stevejgordon.co.uk/using-generic-host-in-dotnet-core-console-based-microservices
// Cross project dependency injection: https://asp.net-hacker.rocks/2017/03/06/using-dependency-injection-in-multiple-projects.html


using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TemplateCommandLineApp.Commands;
using TemplateCommandLineApp.Config;
using TemplateCommandLineApp.Extensions;
using TemplateCommandLineApp.Handlers;

namespace ComonsoleCommandLineApp
{
    class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables("MyConsole")
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.development.json", optional: true);

            var config = configBuilder.Build();

            var services = new ServiceCollection()
                .AddSingleton(PhysicalConsole.Singleton)
                .Configure<ServiceSettings>(config)
                .AddSerilogServices(new LoggerConfiguration().ReadFrom.Configuration(config))
                .AddAppServices()
                .BuildServiceProvider();

//            var logger = services.GetService<ILogger>().ForContext<Program>();

            if (args.Length > 0 && args[0].ToLower() == "inter")
            {
                var input = "";
                InteractiveMode(input);
            }
            else
            {
                var app = new CommandLineApplication<RootCommand>();
                app.Conventions
                    .UseDefaultConventions()
                    .UseConstructorInjection(services);
                ArgumentExecutor(app, args);
            }

            return 0;
        }

        private static void InteractiveMode(string input)
        {
            ReadLineConfiguration();
            while (Environment.ExitCode != 1)
            {
                input = ReadLine.Read("BasicConsole>");

                switch (input)
                {
                    case "random":
                        Console.WriteLine("We random");
                        break;
                    default:
                        var app = new CommandLineApplication<RootCommand>();
                        var command = CommandLineExtensions.SplitCommandLine(input);
                        ArgumentExecutor(app, command.ToArray());

                        break;
                }
            }
        }

        private static void ReadLineConfiguration()
        {
            ReadLine.HistoryEnabled = true;
            var app = new CommandLineApplication<RootCommand>();
            var initialCommands = ArgumentExecutor(app, new string[] {"--version"});
            ReadLine.AutoCompletionHandler = new AutoCompletionHandler(initialCommands.ToArray());
            ReadLine.AddHistory(initialCommands.ToArray());
        }

        private static string[] ArgumentExecutor(CommandLineApplication app, string[] args)
        {
            //Configure root command here. This creates a complete command line application instance.
            RootCommand.Configure(app);

            try
            {
                app.Execute(args);
                return app.GetCommandNames();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}