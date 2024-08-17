using Accolades.Brann.Updater.Exceptions;
using Octokit;
using FileMode = System.IO.FileMode;

namespace Accolades.Brann.Updater.Models;

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
            a.ContentType == Constants.GitHubAppContentType &&
            a.Name.Contains(architecture.ToString(), StringComparison.InvariantCultureIgnoreCase));

        if (a is null)
        {
            throw new UpdaterException($"There is no new release for the architecture {architecture.ToString()}");
        }

        return new GithubRelease(
            release.Id,
            release.TagName,
            new GitHubReleaseAsset(a.Id, a.Name, a.BrowserDownloadUrl));
    }

    /// <inheritdoc cref="DownloadInstaller"/>
    public async Task<Uri> DownloadInstaller(GithubRelease release)
    {
        var tempFolder = Directory.CreateTempSubdirectory();
        var installerPath = Path.Join(tempFolder.FullName, release.Asset.Name);
        
        await using var fileStream = new FileStream(installerPath, FileMode.CreateNew);

        var webStream = await _httpClient.GetStreamAsync(release.Asset.DownloadUrl);
        await webStream.CopyToAsync(fileStream);

        return installerPath.ToUri();
    }
}