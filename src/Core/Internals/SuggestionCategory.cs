using System.Collections.ObjectModel;
using Accolades.Brann.Plugins;

namespace Accolades.Brann.Core.Internals;

internal class SuggestionCategory : ObservableCollection<ISuggestion>, ISuggestionCategory
{
    /// <summary>
    /// Initialize a new <see cref="SuggestionCategory"/>.
    /// </summary>
    /// <param name="type">The category type.</param>
    /// <param name="name">The category name.</param>
    public SuggestionCategory(SuggestionType type, string name)
    {
        Type = type;
        Name = name;
    }

    /// <summary>
    /// Gets the suggestion type.
    /// </summary>
    public SuggestionType Type { get; }

    /// <summary>
    /// Gets the suggestion name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets value indicating if the suggestion is enabled or not.
    /// </summary>
    public bool IsEnabled => false;

    /// <summary>
    /// Insert a suggestion into the list.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="item">The item to add.</param>
    /// <exception cref="InvalidOperationException">If you try to add a suggestion with a different type.</exception>
    protected override void InsertItem(int index, ISuggestion item)
    {
        if (item.Type != Type)
        {
            throw new InvalidOperationException($"You cannot an item with type {item.Type} to this category");
        }
        
        base.InsertItem(index, item);
    }
}