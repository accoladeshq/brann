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
    /// Gets the suggestion type.
    /// </summary>
    public SuggestionType Type => SuggestionType.Application;
}