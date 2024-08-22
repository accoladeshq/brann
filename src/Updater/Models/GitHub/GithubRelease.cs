namespace Accolades.Brann.Updater.Models;

internal class GitHubReleaseAsset
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

internal class GithubRelease
{

    public GithubRelease(long id, string tagName, GitHubReleaseAsset asset)
    {
        Id = id;
        TagName = tagName;
        Asset = asset;
    }
    
    /// <summary>
    /// Gets the release identifier.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Gets the tag's name.
    /// </summary>
    public string TagName { get; }

    /// <summary>
    /// Gets the release assets.
    /// </summary>
    public GitHubReleaseAsset Asset { get; }
}