using System.Diagnostics;
using Accolades.Brann.Updater.Models;
using Spectre.Console.Cli;

namespace Accolades.Brann.Updater;

// ReSharper disable once ClassNeverInstantiated.Global Used by Spectre.
internal class UpdateCommand : AsyncCommand<UpdaterSettings>
{
    private readonly IGitHubService _githubService;
    private readonly IFileSystemService _fileSystemService;
    
    /// <summary>
    /// Initialize a new <see cref="UpdateCommand"/>
    /// </summary>
    /// <param name="githubService">The gitHub service.</param>
    /// <param name="fileSystemService">The file system service.</param>
    public UpdateCommand(IGitHubService githubService, IFileSystemService fileSystemService)
    {
        _githubService = githubService;
        _fileSystemService = fileSystemService;
    }

    /// <summary>
    /// Handle the command.
    /// </summary>
    /// <param name="context">The command context.</param>
    /// <param name="settings">The update settings.</param>
    /// <returns></returns>
    public override async Task<int> ExecuteAsync(CommandContext context, UpdaterSettings settings)
    {
        Debugger.Launch();
        
        switch (settings.Stage)
        {
            case 1:
                await HandleStage1();
                break;
            case 2:
                break;
        }

        return 0;
    }

    private async Task HandleStage1()
    {
        var release = await _githubService.GetLatestRelease();
        var installerPath = await _githubService.DownloadInstaller(release);
        
        var updatedTempUri = _fileSystemService.CopySelfToTempDir();
        _fileSystemService.StartProcess(
            updatedTempUri.AbsolutePath,
            "--stage", "2",
            "--installer", $"{installerPath.AbsolutePath}"
        );
    }
}