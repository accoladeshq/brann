using System.Text;
using System.Xml.Serialization;
using Accolades.Brann.WixGenerator.Dtos;
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
        var wixBuilder = new WixBuilder(settings.BinariesFolder)
            .WithInclude("Common.wxi")
            .WithComponentGroupName(settings.ComponentGroup);

        var wix = wixBuilder.Build();
        var wixDto = _mapper.Map<WixDto>(wix);
        
        using var stream = new StreamWriter(settings.Output, false, new UTF8Encoding());

        var serializer = new XmlSerializer(typeof(WixDto));

        var ns = new XmlSerializerNamespaces();
        ns.Add("", "http://schemas.microsoft.com/wix/2006/wi");

        serializer.Serialize(stream, wixDto, ns);

        return Task.FromResult(0);
    }
}