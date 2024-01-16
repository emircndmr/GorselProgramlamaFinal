using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Logging;

namespace gorselFinal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Logging.AddProvider(new CustomLoggerProvider()); // Custom logger ekleyerek değişiklik yaptık

            return builder.Build();
        }
    }

    public class CustomLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger();
        }

        public void Dispose()
        {
        }
    }

    public class CustomLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
           
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine($"[{logLevel}] - {formatter(state, exception)}");
        }
    }
}
