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

    /// <summary>
    /// Validate the updater settings.
    /// </summary>
    /// <returns></returns>
    public override ValidationResult Validate()
    {
        return Stage is 1 or 2 ? ValidationResult.Success() : ValidationResult.Error("Stage must be 1 or 2");
    }
}