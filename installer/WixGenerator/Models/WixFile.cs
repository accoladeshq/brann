// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Accolades.Brann.WixGenerator;

public class WixFile
{
    /// <summary>
    /// Initialize a new <see cref="WixFile"/>
    /// </summary>
    /// <param name="file"></param>
    public WixFile(string file)
    {
        Name = file;
        Id = WixHelper.FormatId("File", file);
    }
    
    /// <summary>
    /// Gets the file identifier.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// Gets the file name.
    /// </summary>
    public string Name { get; }
}