using Accolades.Brann.DependencyInjection;
using Accolades.Brann.Updater;
using Accolades.Brann.Updater.GitHub;
using Accolades.Brann.Updater.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Spectre.Console.Cli;

try
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
    
    var registrations = new ServiceCollection();
    var registrar = new TypeRegistrar(registrations);
    registrations.AddGitHub();
    registrations.AddSingleton<IFileSystemService, FileSystemService>();

    var app = new CommandApp<UpdateCommand>(registrar);
    return app.Run(args);
}
finally
{
    await Log.CloseAndFlushAsync();

}