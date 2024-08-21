using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator;

public class WixComponentRef
{
    [XmlAttribute]
    public string Id { get; set; }
}
