namespace Accolades.Brann.WixGenerator.Dtos;

[Serializable]
public class WixFragmentDto
{
    /// <summary>
    /// Gets the wix component group which reference all components.
    /// </summary>
    public required WixComponentGroupDto ComponentGroup { get; set; }

    /// <summary>
    /// Gets the wix directory reference (basically the installation folder).
    /// </summary>
    public required WixDirDto DirectoryRef { get; set; }
}