using System.Reactive.Disposables;
using Accolades.Brann.Avalonia;
using Accolades.Brann.Core;
using ReactiveUI;
using Splat;

namespace Accolades.Brann.ViewModels;

public class SplashViewModel : ViewModelBase, IActivatableViewModel
{
    private readonly ISuggestionProvider _suggestionsProvider;

    /// <summary>
    /// Initialize a new <see cref="SplashViewModel"/>.
    /// </summary>
    /// <param name="suggestionsProvider">The plugin manager.</param>
    public SplashViewModel(ISuggestionProvider? suggestionsProvider = null)
    {
        Activator = new ViewModelActivator();

        _suggestionsProvider = suggestionsProvider ?? Locator.Current.GetRequiredService<ISuggestionProvider>();

        this.WhenActivated(Initialize);
    }

    /// <summary>
    /// Occurs when the initialization process finished.
    /// </summary>
    public event EventHandler? Initialized;

    /// <summary>
    /// Gets the view model activator.
    /// </summary>
    public ViewModelActivator Activator { get; }

    /// <summary>
    /// Initialize the application requirements.
    /// </summary>
    /// <param name="disposables"></param>
    private async void Initialize(CompositeDisposable disposables)
    {
        var delayTask = Task.Delay(TimeSpan.FromSeconds(3));
        var initializeTask = _suggestionsProvider.Initialize();

        await Task.WhenAll(delayTask, initializeTask);

        Initialized?.Invoke(this, EventArgs.Empty);
    }
}