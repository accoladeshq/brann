using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator.Dtos;

public class WixComponentGroupDto
{
    /// <summary>
    /// Gets the component group identifier.
    /// </summary>
    [XmlAttribute]
    public required string Id { get; set; }

    /// <summary>
    /// Gets the components references.
    /// </summary>
    [XmlElement("ComponentRef")]
    public required WixComponentRefDto[] Components { get; set; }
}