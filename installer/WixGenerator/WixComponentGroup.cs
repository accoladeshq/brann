using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator;

public class WixComponentGroup
{
    [XmlAttribute]
    public string Id { get; set; }

    [XmlElement("ComponentRef")]
    public WixComponentRef[] Components { get; set; }

    List<WixComponentRef> _Components;

    internal void Add(WixComponentRef comp)
    {
        if (_Components == null)
            _Components = new List<WixComponentRef>();

        _Components.Add(comp);
    }

    internal void PrepareToSerialization()
    {
        if (_Components != null)
            Components = _Components.ToArray();
    }
}
