using System.ComponentModel;
using Spectre.Console.Cli;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace Accolades.Brann.WixGenerator.Commands;

internal class GenerateSettings : CommandSettings
{
    [CommandOption("-i|--install-folder")]
    public required string InstallDirectoryRef { get; init; }

    [CommandOption("-b|--binaries-folder")]
    public required string BinariesFolder { get; init; }

    [CommandOption("-n|--include <VALUES>")]
    [DefaultValue(new string[0])]
    public required string[] Includes { get; init; }

    [CommandOption("-c|--component-group")]
    [DefaultValue("Generated")]
    public required string ComponentGroup { get; init; }

    [CommandOption("-f|--file-source")]
    public required string FileSourceVariable { get; init; }
    
    [CommandOption("-o|--output")]
    public required string Output { get; init; }
}