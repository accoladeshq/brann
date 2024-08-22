using System.Xml.Serialization;
using AutoMapper;
using Spectre.Console.Cli;

namespace Accolades.Brann.WixGenerator.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
internal class GenerateCommand : AsyncCommand<GenerateSettings>
{
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Initialize a new <see cref="GenerateCommand"/>.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    public GenerateCommand(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public override Task<int> ExecuteAsync(CommandContext context, GenerateSettings settings)
    {
        var wixBuilder = new WixBuilder(settings.BinariesFolder);

        var wix = wixBuilder.Build();

        using var sw = new StreamWriter(Console.OpenStandardOutput());
        sw.AutoFlush = true;
        Console.SetOut(sw);

        var serializer = new XmlSerializer(typeof(Wix));
        // ns.Add("", "http://schemas.microsoft.com/wix/2006/wi");

        serializer.Serialize(sw, wix);
        // var root = new WixRoot(settings.BinariesFolder, settings.InstallDirectoryRef, settings.Includes);
        // root.Fragment.ComponentGroup.Id = settings.ComponentGroup;
        //
        // var rootDirRef = root.Fragment.DirectoryRef;
        //
        // rootDirRef.Id = settings.InstallDirectoryRef;
        // rootDirRef.Name = null;
        // rootDirRef.FileSource = $"$(var.{settings.FileSourceVariable})";
        // rootDirRef.DiskId = "1";

        // root.Serialize(settings.Output);

        return Task.FromResult(0);
    }
}