using System.Reactive;
using System.Windows.Input;
using Accolades.Brann.Avalonia;
using Accolades.Brann.Core;
using Accolades.Brann.Plugins;
using ReactiveUI;
using Splat;

namespace Accolades.Brann.ViewModels;

public class SuggestionsViewModel : ViewModelBase, IRoutableViewModel
{
    /// <summary>
    /// Initialize a new <see cref="SuggestionsViewModel"/>.
    /// </summary>
    /// <param name="screen">The screen hosting the view.</param>
    public SuggestionsViewModel(IScreen screen)
    {
        HostScreen = screen;
        UrlPathSegment = "Suggestions";
        
        SuggestionProvider = Locator.Current.GetRequiredService<ISuggestionProvider>();

        _executeCommand = ReactiveCommand.CreateFromTask<ISuggestion>((suggestion, token) =>
        {
            return Task.FromResult(Unit.Default);
        });
    }
    
    /// <summary>
    /// Gets the suggestion provider.
    /// </summary>
    public ISuggestionProvider SuggestionProvider { get; }

    private ReactiveCommand<ISuggestion, Unit> _executeCommand;
    /// <summary>
    /// Gets the command to execute the suggestion.
    /// </summary>
    public ICommand ExecuteCommand => _executeCommand;
    
    /// <summary>
    /// Gets the path segment to identify a url.
    /// </summary>
    public string? UrlPathSegment { get; }
    
    /// <summary>
    /// The screen hosting the view.
    /// </summary>
    public IScreen HostScreen { get; }
}