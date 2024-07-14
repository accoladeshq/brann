namespace Accolades.Brann.Plugins;

public interface IPlugin
{
    /// <summary>
    /// Gets the plugin name.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Initialize plugin
    /// </summary>
    /// <returns></returns>
    Task Initialize();

    /// <summary>
    /// Search for suggestions.
    /// </summary>
    /// <param name="search">The search term.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<ISuggestion>> Search(string search, CancellationToken cancellationToken);
}