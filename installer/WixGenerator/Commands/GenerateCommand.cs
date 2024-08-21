using Spectre.Console.Cli;

namespace Accolades.Brann.WixGenerator.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
internal class GenerateCommand : AsyncCommand<GenerateSettings>
{
    public override Task<int> ExecuteAsync(CommandContext context, GenerateSettings settings)
    {
        var root = new WixRoot(settings.BinariesFolder, settings.Includes);
        root.Fragment.ComponentGroup.Id = settings.ComponentGroup;

        var rootDirRef = root.Fragment.DirectoryRef;

        rootDirRef.Id = settings.InstallDirectoryRef;
        rootDirRef.Name = null;
        rootDirRef.FileSource = $"$(var.{settings.FileSourceVariable})";
        rootDirRef.DiskId = "1";

        root.Serialize(settings.Output);

        return Task.FromResult(0);
    }
}