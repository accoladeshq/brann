using System.Reflection;
using Accolades.Brann.DependencyInjection;
using Accolades.Brann.WixGenerator.Commands;
using Accolades.Brann.WixGenerator.Profiles;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Spectre.Console.Cli;

try
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
    
    var registrations = new ServiceCollection();
    registrations.AddAutoMapper(expression => expression.AddProfile<WixProfile>());
    
    var registrar = new TypeRegistrar(registrations);

    var app = new CommandApp<GenerateCommand>(registrar);
    return app.Run(args);
}
finally
{
    await Log.CloseAndFlushAsync();

}