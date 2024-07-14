using Avalonia;
using Serilog;
using Splat;
using Splat.Serilog;

namespace Accolades.Brann.Avalonia;

internal static class LoggingExtensions
{
    public static AppBuilder UseSerilog(this AppBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        
        Locator.CurrentMutable.UseSerilogFullLogger();
        
        return builder;
    }
}