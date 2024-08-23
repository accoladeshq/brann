namespace Accolades.Brann.Updater.GitHub;

public class GitHubReleaseAsset
{

    public GitHubReleaseAsset(int id, string name, string downloadUrl)
    {
        Id = id;
        Name = name;
        DownloadUrl = downloadUrl;
    }
    
    /// <summary>
    /// Gets the release identifier.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the release name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the release download url.
    /// </summary>
    public string DownloadUrl { get; }
}