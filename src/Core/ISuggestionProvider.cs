namespace Accolades.Brann.Core;

public interface ISuggestionProvider
{
    /// <summary>
    /// Gets or sets the search term.
    /// </summary>
    string SearchTerm { get; set; }
    
    /// <summary>
    /// Gets the suggestions.
    /// </summary>
    ISuggestions Suggestions { get; }

    Task Initialize();
}