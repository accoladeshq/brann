using Spectre.Console;
using Spectre.Console.Cli;

namespace Accolades.Brann.Updater;

// ReSharper disable once ClassNeverInstantiated.Global Used and instantiate by Spectre.Console
internal class UpdaterSettings : CommandSettings
{
    /// <summary>
    /// Gets the update stage.
    /// </summary>
    [CommandOption("-s|--stage")]
    public int Stage { get; init; }
    
    [CommandOption("-i|--installer")]
    public string Installer { get; init; } = default!;

    /// <summary>
    /// Validate the updater settings.
    /// </summary>
    /// <returns></returns>
    public override ValidationResult Validate()
    {
        var valid = Stage is 1 or 2;

        if (valid && Stage is 2)
        {
            valid = Stage is 2 && !string.IsNullOrEmpty(Installer);
        }
        
        return valid ? ValidationResult.Success() : ValidationResult.Error("Stage must be 1 or 2");
    }
}