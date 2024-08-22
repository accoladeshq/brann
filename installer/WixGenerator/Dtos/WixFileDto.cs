using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator.Dtos;

public class WixFileDto
{
    /// <summary>
    /// Gets the file identifier.
    /// </summary>
    [XmlAttribute]
    public required string Id { get; set; }

    /// <summary>
    /// Gets the file name.
    /// </summary>
    [XmlAttribute]
    public required string Name { get; set; }
}