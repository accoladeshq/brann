using System.ComponentModel;
using Spectre.Console.Cli;

namespace Accolades.Brann.WixGenerator.Commands;

internal class GenerateSettings : CommandSettings
{
    [CommandOption("-i|--install-folder")]
    [DefaultValue("INSTALLFOLDER")]
    public string InstallDirectoryRef { get; init; }
    
    [CommandOption("-b|--binaries-folder")]
    public string BinariesFolder { get; init; }
    
    [CommandOption("-n|--include <VALUES>")]
    [DefaultValue(new string[0])]
    public string[] Includes { get; init; }
    
    [CommandOption("-c|--component-group")]
    [DefaultValue("Generated")]
    public string ComponentGroup { get; init; }
    
    [CommandOption("-f|--file-source")]
    public string FileSourceVariable { get; init; }
    
    [CommandOption("-o|--output")]
    public string Output { get; init; }
}