using Accolades.Brann.Updater;
using Accolades.Brann.Updater.DependencyInjection;
using Accolades.Brann.Updater.Models;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

var registrations = new ServiceCollection();
var registrar = new TypeRegistrar(registrations);
registrations.AddSingleton<IGitHubService, GitHubService>();
registrations.AddSingleton<IFileSystemService, FileSystemService>();

var app = new CommandApp<UpdateCommand>(registrar);
return app.Run(args);