using System.Diagnostics;

namespace Accolades.Brann.Updater.Models;

internal class FileSystemService : IFileSystemService
{
    public Uri CopySelfToTempDir()
    {
        var tempDirectory = Directory.CreateTempSubdirectory();
        var destinationPath = Path.Join(tempDirectory.FullName, "Brann.Update.exe").ToUri();
        var sourcePath = GetExecutablePath();
        
        File.Copy(sourcePath.AbsolutePath, destinationPath.AbsolutePath);
        
        return destinationPath;
    }

    public void StartProcess(string absolutePath, params string[] parameters)
    {
        var process = Process.Start(absolutePath, parameters);
        process.WaitForExit();
    }

    private Uri GetExecutablePath()
    {
        var module = Process.GetCurrentProcess().MainModule;

        if (module is null)
        {
            throw new Exception("You try to call the updater from the dll.");
        }

        return module.FileName.ToUri();
    }
}