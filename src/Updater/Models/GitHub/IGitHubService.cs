namespace Accolades.Brann.Updater.Models;

internal interface IGitHubService
{
    /// <summary>
    /// Gets the latest github release for the product.
    /// </summary>
    /// <returns></returns>
    Task<GithubRelease> GetLatestRelease();
}