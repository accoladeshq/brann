// ReSharper disable UnusedMember.Global
using Serilog;

namespace Accolades.Brann.WixGenerator;

public class WixBuilder
{
    private readonly string _binariesFolder;
    private string _binariesFolderVariable;
    private string _installDirectoryRef;
    private string? _include;
    private string _componentGroupName;
    
    public WixBuilder(string binariesFolder)
    {
        _include = null;
        _binariesFolder = binariesFolder;
        _installDirectoryRef = Constants.InstallDirectoryRef;
        _binariesFolderVariable = Constants.BinariesDirectoryVariable;
        _componentGroupName = Constants.ComponentGroupName;
    }

    public WixBuilder WithInstallDirectoryRef(string installDirectoryRef)
    {
        _installDirectoryRef = installDirectoryRef;
        return this;
    }

    public WixBuilder WithInclude(string include)
    {
        _include = include;
        return this;
    }

    public WixBuilder WithBinariesDirectoryVariable(string binariesFolderVariable)
    {
        _binariesFolderVariable = binariesFolderVariable;
        return this;
    }

    public WixBuilder WithComponentGroupName(string componentGroupName)
    {
        _componentGroupName = componentGroupName;
        return this;
    }

    public Wix Build()
    {
        var wix = new Wix(_installDirectoryRef, _binariesFolderVariable, _componentGroupName, _include);
        
        var files = Directory.GetFiles(_binariesFolder, "*", SearchOption.AllDirectories)
            .Where(f => !f.Contains(".exe"))
            .Select(p => p.Remove(0, _binariesFolder.Length + 1)) // We use +1 to remove the first slash
            .ToList();

        Log.Information($"Found {files.Count} files in directory {_binariesFolder}");
        
        foreach (var file in files)
        {
            Log.Information($"Adding file {file} to wix file.");
            wix.AddFile(file);
        }
        
        return wix;
    }
}