using Microsoft.Extensions.DependencyInjection;

namespace Accolades.Brann.Updater.GitHub;

public static class GitHubInitializer
{
    /// <summary>
    /// Register GitHub classes.
    /// </summary>
    /// <param name="services">The services.</param>
    public static void AddGitHub(this ServiceCollection services)
    {
        services.AddSingleton<IGitHubService, GitHubService>();
    }

    /// <summary>
    /// Create a new GitHub service.
    /// </summary>
    /// <returns></returns>
    public static IGitHubService CreateService()
    {
        return new GitHubService();
    }
}