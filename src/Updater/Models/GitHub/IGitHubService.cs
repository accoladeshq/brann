namespace Accolades.Brann.Updater.Models;

internal interface IGitHubService
{
    /// <summary>
    /// Gets the latest github release for the product.
    /// </summary>
    /// <returns></returns>
    Task<GithubRelease> GetLatestRelease();

    /// <summary>
    /// Download the corresponding installer for a dedicated release.
    /// </summary>
    /// <param name="release">The release we want to download the installer.</param>
    /// <returns></returns>
    Task<Uri> DownloadInstaller(GithubRelease release);
}