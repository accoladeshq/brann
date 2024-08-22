using Serilog;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Accolades.Brann.WixGenerator;

public class WixComponent
{
    /// <summary>
    /// Initialize a new <see cref="WixComponent"/>.
    /// </summary>
    /// <param name="id">The component's identifier.</param>
    public WixComponent(string id)
    {
        _files = new List<WixFile>();
        Id = WixHelper.FormatId("CMP", id);
        Guid = "{" + System.Guid.NewGuid().ToString().ToUpper() + "}";
    }
    
    /// <summary>
    /// Gets the component identifier.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the component unique identifier.
    /// </summary>
    public string Guid { get; }
    
    private readonly List<WixFile> _files;
    /// <summary>
    /// Gets or sets the wix files.
    /// </summary>
    public WixFile[] Files => _files.ToArray();

    /// <summary>
    /// Add a file to the component.
    /// </summary>
    /// <param name="file"></param>
    public void AddFile(string file)
    {
        if (_files.SingleOrDefault(f => f.Name == file) != null)
        {
            Log.Warning($"A file with name {file} already exist. Skipping...");
            return;
        }

        var wixFile = new WixFile(file);
        _files.Add(wixFile);
    }
}