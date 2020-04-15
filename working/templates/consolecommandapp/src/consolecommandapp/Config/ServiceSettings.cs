namespace TemplateCommandLineApp.Config
{
    internal interface IServiceSettings
    {
    }

    public class ServiceSettings : IServiceSettings
    {
        public int Port { get; set; }
    }
}