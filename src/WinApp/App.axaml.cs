using Accolades.Brann.Core;
using Accolades.Brann.Core.Internals;
using Accolades.Brann.Models;
using Accolades.Brann.Plugins;
using Accolades.Brann.Plugins.ChatGpt;
using Accolades.Brann.Plugins.Windows;
using Accolades.Brann.ViewModels;
using Accolades.Brann.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Splat;

namespace Accolades.Brann;

public partial class App : Application
{
    /// <summary>
    /// Initialize application.
    /// </summary>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Occurs when the framework initialization completed.
    /// </summary>
    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            SplashView splashView;
            desktop.MainWindow = splashView = new SplashView();
            await splashView.ShowDialog();

            desktop.MainWindow = new PaletteView { DataContext = new PaletteViewModel() };
            desktop.MainWindow.Show();

            splashView.Close();
        }

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Register application services.
    /// </summary>
    public override void RegisterServices()
    {
        base.RegisterServices();

        Locator.CurrentMutable.RegisterLazySingleton<IDialogService>(() => new DialogService());
        Locator.CurrentMutable.RegisterLazySingleton<ISuggestionProvider>(
            () => new SuggestionProvider(new List<IPlugin> { new WindowsPlugin(), new ChatGptPlugin() }));
    }
}