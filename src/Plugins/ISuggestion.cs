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
}