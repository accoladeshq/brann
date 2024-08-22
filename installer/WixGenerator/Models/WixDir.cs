// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Accolades.Brann.WixGenerator;

public class WixDir
{
    /// <summary>
    /// Initialize a new <see cref="WixDir"/>.
    /// </summary>
    /// <param name="installDirectoryRef">The installation directory reference.</param>
    /// <param name="binariesDirectoryVariable">The binaries directory variable.</param>
    /// <param name="name">The name of the directory.</param>
    /// <param name="useIdFormatter">If we need to apply a formatter to the id.</param>
    public WixDir(string installDirectoryRef, string? binariesDirectoryVariable, string? name = null, bool useIdFormatter = true)
    {
        _directories = new List<WixDir>();
        
        Id = useIdFormatter ? WixHelper.FormatId(null, installDirectoryRef.ToUpper()) : installDirectoryRef;
        FileSource = binariesDirectoryVariable is null ? null : $"$(var.{binariesDirectoryVariable})";
        Name = name;
    }
    
    /// <summary>
    /// Gets the directory identifier.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// Gets the directory name.
    /// </summary>
    public string? Name { get; }
    
    /// <summary>
    /// Gets the file source where binaries are stored.
    /// </summary>
    public string? FileSource { get; }


    private readonly List<WixDir> _directories;
    /// <summary>
    /// Gets the subdirectories.
    /// </summary>
    public WixDir[] Directories => _directories.ToArray();

    /// <summary>
    /// Gets the wix component link to this directory.
    /// </summary>
    public WixComponent? Component { get; private set; }

    public WixComponent? AddFile(string file)
    {
        var folders = file.Split(Path.DirectorySeparatorChar);

        if (folders.Length == 1)
        {
            Component ??= new WixComponent(Id);
            Component.AddFile(folders[0]);

            return Component;
        }
        
        var existingDirectory = _directories.SingleOrDefault(d => d.Name == folders[0]);

        if (existingDirectory is null)
        {
            existingDirectory = new WixDir(Id + "_" + folders[0], null, folders[0]);
            _directories.Add(existingDirectory);
        }
        
        return existingDirectory.AddFile(Path.Join(folders.Skip(1).ToArray()));
    }
}