namespace Accolades.Brann.Plugins.Windows;

public class AppSuggestion : ISuggestion
{
    public AppSuggestion(string name)
    {
        Name = name;
    }
    
    /// <summary>
    /// Gets the app. suggestion name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets value indicating if the suggestion is enabled or not.
    /// </summary>
    public bool IsEnabled => true;

    /// <summary>
    /// Gets the suggestion type.
    /// </summary>
    public SuggestionType Type => SuggestionType.Application;
}