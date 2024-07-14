using Accolades.Brann.Plugins;

namespace Accolades.Brann.Core;

public class PluginInitializedEventArgs : EventArgs
{
    /// <summary>
    /// Initialize a new <see cref="PluginInitializedEventArgs"/>.
    /// </summary>
    /// <param name="plugin">The initialized plugin.</param>
    public PluginInitializedEventArgs(IPlugin plugin)
    {
        Plugin = plugin;
    }
    
    /// <summary>
    /// Gets the plugin.
    /// </summary>
    public IPlugin Plugin { get; }
}