namespace Accolades.Brann.WixGenerator;

[Serializable]
public class WixFragment
{
    /// <summary>
    /// Initialize a new <see cref="WixFragment"/> used for serialization.
    /// </summary>
    public WixFragment() { }
    
    public WixFragment(string installDirectoryRef, string binariesDirectoryVariable)
    {
        ComponentGroup = new WixComponentGroup();
        DirectoryRef = new WixDir(installDirectoryRef, binariesDirectoryVariable);
    }
    
    /// <summary>
    /// Gets the wix component group which reference all components.
    /// </summary>
    public WixComponentGroup ComponentGroup { get; set; } = null!;

    /// <summary>
    /// Gets the wix directory reference (basically the installation folder).
    /// </summary>
    public WixDir DirectoryRef { get; set; } = null!;

    /// <summary>
    /// Add a file into the directory tree and reference it in the component group.
    /// </summary>
    /// <param name="fileToAdd">The file to add.</param>
    public void AddFile(string fileToAdd)
    {
        DirectoryRef.AddFile(fileToAdd);
    }
}