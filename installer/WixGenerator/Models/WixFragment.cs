// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Accolades.Brann.WixGenerator;

public class WixFragment
{
    /// <summary>
    /// Initialize a new <see cref="WixFragment"/>.
    /// </summary>
    /// <param name="installDirectoryRef">The install directory reference.</param>
    /// <param name="binariesDirectoryVariable">The binaries directory variable.</param>
    /// <param name="componentGroupName">The component group name.</param>
    public WixFragment(string installDirectoryRef, string binariesDirectoryVariable, string componentGroupName)
    {
        ComponentGroup = new WixComponentGroup(componentGroupName);
        DirectoryRef = new WixDir(installDirectoryRef, binariesDirectoryVariable, useIdFormatter: false);
    }
    
    /// <summary>
    /// Gets the wix component group which reference all components.
    /// </summary>
    public WixComponentGroup ComponentGroup { get; }

    /// <summary>
    /// Gets the wix directory reference (basically the installation folder).
    /// </summary>
    public WixDir DirectoryRef { get; }

    /// <summary>
    /// Add a file into the directory tree and reference it in the component group.
    /// </summary>
    /// <param name="fileToAdd">The file to add.</param>
    public void AddFile(string fileToAdd)
    {
        var addedFile = DirectoryRef.AddFile(fileToAdd);

        if (addedFile is null) return;
        
        ComponentGroup.AddReference(addedFile.Id);
    }
}