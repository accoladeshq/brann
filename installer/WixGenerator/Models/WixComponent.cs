using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator;

[Serializable]
public class WixComponent
{
    /// <summary>
    /// Initialize a new <see cref="WixComponent"/>. Used for serialization
    /// </summary>
    public WixComponent() { }
    
    /// <summary>
    /// Initialize a new <see cref="WixComponent"/>.
    /// </summary>
    /// <param name="id">The component's identifier.</param>
    public WixComponent(string id)
    {
        _files = new List<WixFile>();
        
        Id = id;
    }
    
    /// <summary>
    /// Gets the component identifier.
    /// </summary>
    [XmlAttribute]
    public string Id { get; set; } = null!;

    private List<WixFile> _files = null!;
    /// <summary>
    /// Gets or sets the wix files.
    /// </summary>
    [XmlElement("File")]
    public WixFile[] Files
    {
        get => _files.ToArray();
        set => _files = new List<WixFile>(value);
    }
    
    /// <summary>
    /// Add a file to the component.
    /// </summary>
    /// <param name="file"></param>
    public void AddFile(string file)
    {
        _files.Add(new WixFile(file));
    }
}