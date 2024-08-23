using Accolades.Brann.Commons.Extensions;
using Accolades.Brann.Updater.GitHub.Exceptions;
using Octokit;
using FileMode = System.IO.FileMode;

namespace Accolades.Brann.Updater.GitHub;

internal class GitHubService : IGitHubService
{
    private readonly GitHubClient _gitHubClient;
    private readonly HttpClient _httpClient;
    
    public GitHubService()
    {
        var productHeader = new ProductHeaderValue(Constants.GitHubProject);
        _gitHubClient = new GitHubClient(productHeader);
        _httpClient = new HttpClient();
    }
    
    public async Task<GithubRelease> GetLatestRelease()
    {
        var release = await _gitHubClient.Repository.Release.GetLatest(Constants.GitHubOrganization, Constants.GitHubProject);
        var architecture = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;
        
        var a = release.Assets.FirstOrDefault(a => 
            a.Name.Contains(architecture.ToString(), StringComparison.InvariantCultureIgnoreCase));

        if (a is null)
        {
            throw new UpdaterGitHubException($"There is no new release for the architecture {architecture.ToString()}");
        }

        return new GithubRelease(
            release.Id,
            release.TagName,
            new GitHubReleaseAsset(a.Id, a.Name, a.BrowserDownloadUrl));
    }

    /// <inheritdoc cref="DownloadInstaller"/>
    public async Task<Uri> DownloadInstaller(GithubRelease release, Uri tempDirectory)
    {
        var installerPath = Path.Join(tempDirectory.AbsolutePath, release.Asset.Name);
        
        await using var fileStream = new FileStream(installerPath, FileMode.CreateNew);

        var webStream = await _httpClient.GetStreamAsync(release.Asset.DownloadUrl);
        await webStream.CopyToAsync(fileStream);

        return installerPath.ToUri();
    }
}