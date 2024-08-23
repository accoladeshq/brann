using System.Reactive;
using System.Reactive.Disposables;
using Accolades.Brann.Avalonia;
using Accolades.Brann.Updater.GitHub;
using ReactiveUI;
using Splat;

namespace Accolades.Brann.ViewModels;

public class SettingsViewModel : ViewModelBase, IActivatableViewModel
{
    private readonly IGitHubService _gitHubService;

    public SettingsViewModel(): this(null)
    {
    }
    
    /// <summary>
    /// Initialize a new <see cref="SettingsViewModel"/>.
    /// </summary>
    /// <param name="githubService">A service who manage GitHub.</param>
    public SettingsViewModel(IGitHubService? githubService = null)
    {
        Activator = new ViewModelActivator();
        _gitHubService = githubService is null ? Locator.Current.GetRequiredService<IGitHubService>() : throw new Exception();
        
        LoadGitHubRelease = ReactiveCommand.CreateFromTask(_gitHubService.GetLatestRelease);
        _gitHubRelease = LoadGitHubRelease.ToProperty(this, x => x.GitHubRelease, scheduler: RxApp.MainThreadScheduler);
        
        this.WhenActivated(disposable =>
        {
            LoadGitHubRelease.Execute().Subscribe().DisposeWith(disposable);
        });
    }

    private readonly ObservableAsPropertyHelper<GithubRelease> _gitHubRelease;

    public GithubRelease GitHubRelease => _gitHubRelease.Value;
    
    public ReactiveCommand<Unit, GithubRelease> LoadGitHubRelease { get; }
    

    /// <summary>
    /// Gets the view model activator.
    /// </summary>
    public ViewModelActivator Activator { get; }
}