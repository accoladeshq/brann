using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator.Dtos;

public class WixComponentRefDto
{
    [XmlAttribute]
    public required string Id { get; set; }
}