namespace Accolades.Brann.Updater.GitHub;

public class GithubRelease
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