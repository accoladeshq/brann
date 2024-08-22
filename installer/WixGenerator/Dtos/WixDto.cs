using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator.Dtos;

[Serializable]
[XmlRoot("Wix", Namespace = "http://schemas.microsoft.com/wix/2006/wi")]
public class WixDto
{
    /// <summary>
    /// Gets the wix fragment.
    /// </summary>
    [XmlElement(Order = 2)] 
    public required WixFragmentDto Fragment { get; set; }
    
    /// <summary>
    /// Gets the include for variables.
    /// </summary>
    [XmlAnyElement(Order = 1)]
    public System.Xml.XmlProcessingInstruction? Include { get; set; }
}