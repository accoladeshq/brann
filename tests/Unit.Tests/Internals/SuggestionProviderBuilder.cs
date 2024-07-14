using Accolades.Brann.Core;
using Accolades.Brann.Core.Internals;
using Accolades.Brann.Plugins;

namespace Accolades.Brann.Unit.Tests.Internals;

internal class SuggestionProviderBuilder
{
    private readonly List<Plugin> _plugins;

    /// <summary>
    /// Initialize a new <see cref="SuggestionProviderBuilder"/>.
    /// </summary>
    public SuggestionProviderBuilder()
    {
        _plugins = new List<Plugin>();
    }

    /// <summary>
    /// Add a plugin.
    /// </summary>
    /// <param name="configurator">The plugin configurator.</param>
    /// <returns></returns>
    public SuggestionProviderBuilder WithPlugin(Action<PluginBuilder> configurator)
    {
        var pluginBuilder = new PluginBuilder();
        configurator.Invoke(pluginBuilder);
        
        _plugins.Add(pluginBuilder);

        return this;
    }
    
    /// <summary>
    /// Build the suggestion provider.
    /// </summary>
    /// <returns></returns>
    private SuggestionProvider Build()
    {
        return new SuggestionProvider(_plugins);
    }

    /// <summary>
    /// Convert a builder to it's corresponding provider.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>The suggestion provider.</returns>
    public static implicit operator SuggestionProvider(SuggestionProviderBuilder builder) => builder.Build();
}