namespace Accolades.Brann.Plugins;

public abstract class Plugin : IPlugin
{
    /// <summary>
    /// Initialize a new <see cref="Plugin"/>.
    /// </summary>
    /// <param name="name">The plugin name.</param>
    protected Plugin(string name)
    {
        Name = name;
    }
    
    /// <summary>
    /// Gets the plugin name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Get suggestions.
    /// </summary>
    /// <param name="search">The search term.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public abstract Task<IEnumerable<ISuggestion>> Search(string search, CancellationToken cancellationToken);

    /// <summary>
    /// Initialize a new <see cref="Plugin"/>.
    /// </summary>
    /// <returns></returns>
    public abstract Task Initialize();
}