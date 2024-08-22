using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator.Dtos;

[Serializable]
public class WixComponentDto
{
    /// <summary>
    /// Gets the component identifier.
    /// </summary>
    [XmlAttribute]
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the wix files.
    /// </summary>
    [XmlElement("File")]
    public required WixFileDto[] Files { get; set; }
    
    [XmlAttribute]
    public required string Guid { get; set; }
}