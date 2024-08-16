using System.Reactive;
using System.Windows.Input;
using Accolades.Brann.Avalonia;
using Accolades.Brann.Core;
using Accolades.Brann.Models;
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
    }

    /// <summary>
    /// Gets the <see cref="ISuggestionProvider"/>
    /// </summary>
    public ISuggestionProvider SuggestionProvider { get; }
    
    private readonly ReactiveCommand<Unit, Unit> _displaySettingsCommand;
    /// <summary>
    /// Gets the command to display settings view.
    /// </summary>
    public ICommand DisplaySettingsCommand => _displaySettingsCommand;
    
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
}