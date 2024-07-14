using System.Reactive.Linq;
using Accolades.Brann.ViewModels;
using Avalonia.ReactiveUI;

namespace Accolades.Brann.Views;

public partial class SplashView : ReactiveWindow<SplashViewModel>
{
    /// <summary>
    /// Initialize a new <see cref="SplashView"/>.
    /// </summary>
    public SplashView()
    {
        InitializeComponent();

        DataContext = new SplashViewModel();
    }

    /// <summary>
    /// Show as dialog.
    /// </summary>
    /// <returns></returns>
    public Task ShowDialog()
    {
        var tsc = new TaskCompletionSource();

        Show();

        Observable.FromEventPattern(
                x => ViewModel!.Initialized += x,
                x => ViewModel!.Initialized -= x)
            .Take(1)
            .Subscribe(_ => { tsc.SetResult(); });

        return tsc.Task;
    }
}