using System.Reactive;
using System.Windows.Input;
using Accolades.Brann.Avalonia;
using Accolades.Brann.Models;
using ReactiveUI;
using Splat;

namespace Accolades.Brann.ViewModels;

public class PaletteViewModel : ViewModelBase, IScreen, IActivatableViewModel
{
    private readonly IDialogService _dialogService;
    
    /// <summary>
    /// Initialize a new <see cref="PaletteViewModel"/>.
    /// </summary>
    public PaletteViewModel()
    {
        
        _dialogService = Locator.Current.GetRequiredService<IDialogService>();
        _displaySettingsCommand = ReactiveCommand.CreateFromTask(DisplaySettings);

        Router = new RoutingState();
        Activator = new ViewModelActivator();
        
        this.WhenActivated(Activate);
    }

    /// <summary>
    /// Get the view model activator.
    /// </summary>
    public ViewModelActivator Activator { get; }
    
    /// <summary>
    /// Gets the router.
    /// </summary>
    public RoutingState Router { get; }
    
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
    
    /// <summary>
    /// Occurs when the view is activated.
    /// </summary>
    /// <param name="disposable">The disposable action.</param>
    private void Activate(Action<IDisposable> disposable)
    {
        Router.Navigate.Execute(new SuggestionsViewModel(this));
    }
}