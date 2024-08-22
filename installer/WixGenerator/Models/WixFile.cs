using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator;

public class WixFile
{
    /// <summary>
    /// Initialize a default <see cref="WixFile"/> used for serialization.
    /// </summary>
    public WixFile() { }
    
    /// <summary>
    /// Initialize a new <see cref="WixFile"/>
    /// </summary>
    /// <param name="file"></param>
    public WixFile(string file)
    {
        Name = file;
        Id = WixHelper.FormatId("File", file);
    }
    
    /// <summary>
    /// Gets the file identifier.
    /// </summary>
    [XmlAttribute]
    public string Id { get; init; } = null!;
    
    /// <summary>
    /// Gets the file name.
    /// </summary>
    [XmlAttribute]
    public string Name { get; init; } = null!;
}