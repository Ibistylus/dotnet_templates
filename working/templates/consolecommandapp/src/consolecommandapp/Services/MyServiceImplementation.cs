using McMaster.Extensions.CommandLineUtils;

namespace TemplateCommandLineApp
{
    class MyServiceImplementation : IMyService
    {
        private readonly IConsole _console;

        public MyServiceImplementation(IConsole console)
        {
            _console = console;
        }

        public void Invoke()
        {
            _console.WriteLine("Hello dependency injection!");
        }
    }
}