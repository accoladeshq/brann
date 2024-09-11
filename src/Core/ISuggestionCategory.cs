using Accolades.Brann.Plugins;

namespace Accolades.Brann.Core;

public interface ISuggestionCategory : IEnumerable<ISuggestion>, ISuggestion
{
    /// <summary>
    /// Add a suggestion to this category.
    /// </summary>
    /// <param name="suggestion"></param>
    internal void Add(ISuggestion suggestion);
}