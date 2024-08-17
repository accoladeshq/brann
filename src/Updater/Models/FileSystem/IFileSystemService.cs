namespace Accolades.Brann.Updater.Models;

internal interface IFileSystemService
{   
    Uri CopySelfToTempDir();
    
    void StartProcess(string absolutePath, params string[] parameters);
}