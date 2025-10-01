using Serilog;

namespace DemoAppBE.Infrastructure.Extensions
{
    public static class LoggerExtensions
    {
        public static LoggerConfiguration ConfigureSerilog(this LoggerConfiguration loggerConfig, IConfiguration configuration)
        {
            return loggerConfig
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console();
        }
    }
}
