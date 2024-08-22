using Serilog;

namespace Accolades.Brann.WixGenerator;

public class WixBuilder
{
    private readonly string _binariesFolder;
    private string _binariesFolderVariable;
    private string _installDirectoryRef;
    
    public WixBuilder(string binariesFolder)
    {
        _binariesFolder = binariesFolder;
        _installDirectoryRef = Constants.InstallDirectoryRef;
        _binariesFolderVariable = Constants.BinariesDirectoryVariable;
    }

    public WixBuilder WithInstallDirectoryRef(string installDirectoryRef)
    {
        _installDirectoryRef = installDirectoryRef;
        return this;
    }

    public WixBuilder WithBinariesDirectoryVariable(string binariesFolderVariable)
    {
        _binariesFolderVariable = binariesFolderVariable;
        return this;
    }

    public Wix Build()
    {
        var wix = new Wix(_installDirectoryRef, _binariesFolderVariable);
        
        var files = Directory.GetFiles(_binariesFolder, "*", SearchOption.AllDirectories)
            .Select(p => p.Remove(0, _binariesFolder.Length + 1)) // We use +1 to remove the first slash
            .ToList();

        Log.Information($"Found {files.Count} files in directory {_binariesFolder}");
        
        foreach (var file in files)
        {
            wix.AddFile(file);
        }
        
        return wix;
    }
}