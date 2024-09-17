namespace Accolades.Brann.Plugins;

public interface ISuggestion
{
    /// <summary>
    /// Gets the suggestion type.
    /// </summary>
    SuggestionType Type { get; }
    
    /// <summary>
    /// Gets the suggestion name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets value indicating if the suggestion is enabled or not.
    /// </summary>
    bool IsEnabled { get; }
    
    public Task Execute();
}