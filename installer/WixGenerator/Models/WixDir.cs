using System.Xml.Serialization;

namespace Accolades.Brann.WixGenerator;

[Serializable]
[XmlRoot("Directory")]
public class WixDir
{
    public WixDir()
    {
    }
    
    public WixDir(string installDirectoryRef, string? binariesDirectoryVariable, string? name = null)
    {
        _directories = new List<WixDir>();
        
        Id = installDirectoryRef;
        FileSource = binariesDirectoryVariable is null ? null : $"$(var.{binariesDirectoryVariable})";
        Name = name;
    }
    
    [XmlAttribute]
    public string Id { get; init; } = null!;
    
    [XmlAttribute]
    public string? FileSource { get; init; }


    private readonly List<WixDir> _directories = null!;
    /// <summary>
    /// Gets the subdirectories.
    /// </summary>
    [XmlElement("Directory")]
    public WixDir[] Directories
    {
        get => _directories.ToArray();
        init => _directories = new List<WixDir>(value);
    }

    private WixComponent? _component;

    /// <summary>
    /// Gets the wix component link to this directory.
    /// </summary>
    public WixComponent? Component
    {
        get => _component;
        init => _component = value;
    }

    [XmlAttribute] 
    public string? Name { get; init; }

    public void AddFile(string file)
    {
        var folders = file.Split(Path.DirectorySeparatorChar);

        if (folders.Length == 1)
        {
            _component ??= new WixComponent("cmp_" + Id);
            _component.AddFile(folders[0]);
            
            return;
        }
        
        var existingDirectory = _directories.SingleOrDefault(d => d.Name == folders[0]);

        if (existingDirectory is null)
        {
            existingDirectory = new WixDir(Id + "_" + folders[0], null, folders[0]);
            _directories.Add(existingDirectory);
        }
        
        existingDirectory.AddFile(Path.Join(folders.Skip(1).ToArray()));
    }
}