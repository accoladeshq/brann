using Accolades.Brann.Plugins;

namespace Accolades.Brann.Core;

public interface ISuggestionCategory : IEnumerable<ISuggestion>
{
    /// <summary>
    /// Gets the suggestion type.
    /// </summary>
    public SuggestionType Type { get; }

    /// <summary>
    /// Add a suggestion to this category.
    /// </summary>
    /// <param name="suggestion"></param>
    internal void Add(ISuggestion suggestion);
}