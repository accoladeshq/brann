using System.Reactive.Linq;
using Accolades.Brann.Plugins;
using DynamicData.Binding;
using ReactiveUI;

namespace Accolades.Brann.Core.Internals;

public class SuggestionProvider : ReactiveObject, ISuggestionProvider
{
    private static readonly object Locker = new();

    private readonly List<IPlugin> _plugins;

    /// <summary>
    /// Initialize new <see cref="SuggestionProvider"/>.
    /// </summary>
    /// <param name="plugins">The plugin list.</param>
    public SuggestionProvider(IEnumerable<IPlugin> plugins)
    {
        PluginInitialized = null;
        
        _plugins = plugins.ToList();
        _isInitialized = false;
        _searchTerm = string.Empty;
        
        _suggestions = this
            .WhenAnyPropertyChanged(nameof(SearchTerm), nameof(IsInitialized))
            .Throttle(TimeSpan.FromMilliseconds(250))
            .Where(p => p is not null && p._isInitialized)
            .SelectMany(p => Search(p!._searchTerm))
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, x => x.Suggestions, out _suggestions, initialValue: new Suggestions());
    }

    /// <summary>
    /// Occurs when a plugin is initialized.
    /// </summary>
    public event EventHandler<PluginInitializedEventArgs>? PluginInitialized;
    
    private readonly ObservableAsPropertyHelper<ISuggestions> _suggestions;
    /// <summary>
    /// Gets the suggestions.
    /// </summary>
    public ISuggestions Suggestions => _suggestions.Value;
    
    private string _searchTerm;
    /// <summary>
    /// Gets or sets the search term.
    /// </summary>
    public string SearchTerm
    {
        get => _searchTerm;
        set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
    }
    
    private bool _isInitialized;
    /// <summary>
    /// Gets or sets the value indicating if initialized.
    /// </summary>
    private bool IsInitialized
    {
        get => _isInitialized;
        set => this.RaiseAndSetIfChanged(ref _isInitialized, value);
    }

    /// <summary>
    /// Initialize the suggestion provider.
    /// </summary>
    /// <returns>The initialization task.</returns>
    public Task Initialize()
    {
        lock (Locker)
        {
            EnsureNotInitialized();

            var tasks = _plugins.Select(InitializePlugin).ToArray();
            Task.WaitAll(tasks);
            
            IsInitialized = true;
            
            return Task.CompletedTask;   
        }
    }

    /// <summary>
    /// Ensure that the provider was not already initialized.
    /// </summary>
    /// <exception cref="InvalidOperationException">Occurs when the provider was already initialized.</exception>
    private void EnsureNotInitialized()
    {
        if (!IsInitialized)
        {
            return;
        }
        
        throw new InvalidOperationException("You cannot initialize the provider twice.");
    }
    
    private async Task<Suggestions> Search(string search)
    {
        var suggestionItems = new List<ISuggestion>();

        foreach (var plugin in _plugins)
        {
            var commands = await plugin.Search(search.Trim(), CancellationToken.None);
            suggestionItems.AddRange(commands);
        }

        return new Suggestions(suggestionItems);
    }

    private async Task InitializePlugin(IPlugin plugin)
    {
        await plugin.Initialize();
        PluginInitialized?.Invoke(this,new PluginInitializedEventArgs(plugin));
    }
}