using System.Xml.Serialization;
using Serilog;

namespace Accolades.Brann.WixGenerator;

[Serializable]
[XmlRoot("Wix", Namespace = "http://schemas.microsoft.com/wix/2006/wi")]
public class Wix
{
    public Wix()
    {
    }
    
    public Wix(string installDirectoryRef, string binariesDirectoryVariable)
    {
        Fragment = new WixFragment(installDirectoryRef, binariesDirectoryVariable);
    }
    
    /// <summary>
    /// Gets the wix fragment.
    /// </summary>
    public WixFragment Fragment { get; set; } = null!;

    public void AddFile(string file)
    {
        var uri = new Uri(file, UriKind.RelativeOrAbsolute);

        if (uri.IsAbsoluteUri)
        {
            throw new Exception($"{file} is an absolute uri. Wix Generator cannot work with this kind of path");
        }

        var filePathParts = file.Split(Path.DirectorySeparatorChar);

        if (filePathParts.Length > 1)
        {
            Log.Information($"Found nested file {file}");
        }
        
        Fragment.AddFile(file);
    }
}