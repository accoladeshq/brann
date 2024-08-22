using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator.Dtos;

[Serializable]
[XmlRoot("Directory")]
public class WixDirDto
{
    /// <summary>
    /// Gets the directory identifier.
    /// </summary>
    [XmlAttribute]
    public required string Id { get; set; }

    /// <summary>
    /// Gets the directory name.
    /// </summary>
    [XmlAttribute] 
    public string? Name { get; set; }
    
    /// <summary>
    /// Gets the directory where files are.
    /// </summary>
    [XmlAttribute]
    public string? FileSource { get; set; }
    
    /// <summary>
    /// Gets the subdirectories.
    /// </summary>
    [XmlElement("Directory")]
    public required WixDirDto[] Directories { get; set; }

    /// <summary>
    /// Gets the directory component.
    /// </summary>
    public WixComponentDto? Component { get; set; }
}