using Accolades.Brann.Updater.Exceptions;
using Octokit;

namespace Accolades.Brann.Updater.Models;

internal class GitHubService : IGitHubService
{
    private readonly Octokit.GitHubClient _client;
    
    public GitHubService()
    {
        var productHeader = new ProductHeaderValue(Constants.GitHubProject);
        _client = new Octokit.GitHubClient(productHeader);
    }
    
    public async Task<GithubRelease> GetLatestRelease()
    {
        var latest = await _client.Repository.Release.GetLatest(Constants.GitHubOrganization, Constants.GitHubProject);
        var architecture = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;
        
        var a = latest.Assets.FirstOrDefault(a => 
            a.ContentType == Constants.GitHubAppContentType &&
            a.Name.Contains(architecture.ToString(), StringComparison.InvariantCultureIgnoreCase));

        if (a is null)
        {
            throw new UpdaterException($"There is no new release for the architecture {architecture.ToString()}");
        }
        
        return new GithubRelease();
    }  
}