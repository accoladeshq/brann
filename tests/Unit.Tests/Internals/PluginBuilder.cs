using Accolades.Brann.Plugins;

namespace Accolades.Brann.Unit.Tests.Internals;

internal class PluginBuilder
{
    private string _name;

    /// <summary>
    /// Initialize a new <see cref="PluginBuilder"/>.
    /// </summary>
    public PluginBuilder()
    {
        _name = string.Empty;
    }

    /// <summary>
    /// Update plugin name.
    /// </summary>
    /// <param name="name">The plugin name.</param>
    /// <returns></returns>
    public PluginBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    /// <summary>
    /// Build the suggestion provider.
    /// </summary>
    /// <returns></returns>
    private Plugin Build()
    {
        return new MockPlugin(_name);
    }

    /// <summary>
    /// Convert a builder to it's corresponding provider.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>The suggestion provider.</returns>
    public static implicit operator Plugin(PluginBuilder builder) => builder.Build();
}