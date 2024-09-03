using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using Accolades.Brann.Avalonia;
using Accolades.Brann.Core;
using Accolades.Brann.Models;
using Accolades.Brann.Plugins;
using DynamicData.Binding;
using ReactiveUI;
using Splat;

namespace Accolades.Brann.ViewModels;

public class PaletteViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    
    /// <summary>
    /// Initialize a new <see cref="PaletteViewModel"/>.
    /// </summary>
    public PaletteViewModel()
    {
        SuggestionProvider = Locator.Current.GetRequiredService<ISuggestionProvider>();
        _dialogService = Locator.Current.GetRequiredService<IDialogService>();
        _displaySettingsCommand = ReactiveCommand.CreateFromTask(DisplaySettings);
        
        this.WhenValueChanged(vm => vm.SuggestionProvider.Suggestions)
            .Where(s => s is not null)
            .Select(FlattenSuggestionsFromProvider!)
            .ToProperty(this, vm => vm.Suggestions, out _suggestions);
    }

    /// <summary>
    /// Gets or sets the search term.
    /// </summary>
    public string SearchTerm
    {
        get => SuggestionProvider.SearchTerm;
        set => SuggestionProvider.SearchTerm = value;
    }
    
    private readonly ObservableAsPropertyHelper<IEnumerable<ISuggestion>> _suggestions;
    /// <summary>
    /// Gets the suggestions.
    /// </summary>
    public IEnumerable<ISuggestion> Suggestions => _suggestions.Value;
    
    private readonly ReactiveCommand<Unit, Unit> _displaySettingsCommand;
    /// <summary>
    /// Gets the command to display settings view.
    /// </summary>
    public ICommand DisplaySettingsCommand => _displaySettingsCommand;
    
    private ISuggestionProvider SuggestionProvider { get; }
    
    /// <summary>
    /// Display settings view.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<Unit> DisplaySettings(CancellationToken cancellationToken)
    {
        var r = await _dialogService.Open<SettingsViewModel, Unit>();
        return r;
    }

    private static IEnumerable<ISuggestion> FlattenSuggestionsFromProvider(ISuggestions suggestions)
    {
        var list = new List<ISuggestion>();
        
        foreach (var suggestionCategory in suggestions)
        {
            list.Add(suggestionCategory);
            list.AddRange(suggestionCategory);
        }

        return list;
    }
}