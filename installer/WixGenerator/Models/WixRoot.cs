// ReSharper disable MemberCanBePrivate.Global
namespace Accolades.Brann.WixGenerator;

public class Wix
{
    /// <summary>
    /// Initialize a new <see cref="Wix"/> file.
    /// </summary>
    /// <param name="installDirectoryRef">The installation directory ref.</param>
    /// <param name="binariesDirectoryVariable">The binaries directory variable from wix.</param>
    /// <param name="componentGroupName">The component group name.</param>
    /// <param name="include">The name of the file to include.</param>
    public Wix(string installDirectoryRef, string binariesDirectoryVariable, string componentGroupName, string? include)
    {
        Fragment = new WixFragment(installDirectoryRef, binariesDirectoryVariable, componentGroupName);
        Include = include;
    }
    
    /// <summary>
    /// Gets the include name for variables.
    /// </summary>
    public string? Include { get; }
    
    /// <summary>
    /// Gets the wix fragment.
    /// </summary>
    public WixFragment Fragment { get; }

    /// <summary>
    /// Add a new deployment file to wix reference file.
    /// </summary>
    /// <param name="file">The relative file path.</param>
    /// <exception cref="Exception">If the path is not relative.</exception>
    public void AddFile(string file)
    {
        var uri = new Uri(file, UriKind.RelativeOrAbsolute);

        if (uri.IsAbsoluteUri)
        {
            throw new Exception($"{file} is an absolute uri. Wix Generator cannot work with this kind of path");
        }
        
        Fragment.AddFile(file);
    }
}