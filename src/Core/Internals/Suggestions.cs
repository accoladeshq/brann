using System.Collections.ObjectModel;
using Accolades.Brann.Plugins;

namespace Accolades.Brann.Core.Internals;

internal class Suggestions : ObservableCollection<ISuggestionCategory>, ISuggestions
{
    /// <summary>
    /// Initialize a new <see cref="Suggestions"/>.
    /// </summary>
    /// <param name="suggestions">The existing suggestion list.</param>
    public Suggestions(IEnumerable<ISuggestion> suggestions)
    {
        foreach (var suggestion in suggestions.ToList())
        {
            AddSuggestion(suggestion);
        }
    }

    /// <summary>
    /// Initialize a new <see cref="Suggestions"/>
    /// </summary>
    internal Suggestions(): this(new List<ISuggestion>())
    {
    }

    /// <summary>
    /// Add a suggestion to the list.
    /// </summary>
    /// <param name="suggestion">The suggestion to add.</param>
    private void AddSuggestion(ISuggestion suggestion)
    {
        var existingCategory = this.SingleOrDefault(c => c.Type == suggestion.Type);

        if (existingCategory is null)
        {
            existingCategory = new SuggestionCategory(suggestion.Type, suggestion.Type.ToString());
            Add(existingCategory);
        }
            
        existingCategory.Add(suggestion);
    }
}