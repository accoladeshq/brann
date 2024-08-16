using System.Diagnostics;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;

namespace Accolades.Brann.Models;

internal class DialogService : IDialogService
{
    /// <inheritdoc cref="IDialogService.Open{TViewModel,TResult}"/>
    public async Task<TResult> Open<TViewModel, TResult>() where TViewModel: class, new()
    {
        IDisposable? disposable = null;

        try
        {
            var interaction = new Interaction<TViewModel, TResult>();
            var viewModel = new TViewModel();

            disposable = interaction.RegisterHandler(RunViewInteraction);

            var result = await interaction.Handle(viewModel);

            return result;
        }
        finally
        {
            disposable?.Dispose();
        }
    }

    /// <summary>
    /// Run the view interaction.
    /// </summary>
    /// <param name="interactionContext">The interaction context.</param>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    /// <typeparam name="TResult">The result.</typeparam>
    /// <exception cref="PlatformNotSupportedException">Occurs when the platform is not a IClassicDesktopStyleApplicationLifetime.</exception>
    private async Task RunViewInteraction<TViewModel, TResult>(IInteractionContext<TViewModel, TResult> interactionContext) where TViewModel : class
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime c)
        {
            throw new PlatformNotSupportedException();
        }
        
        var viewLocator = new ViewLocator();
        var view = viewLocator.Build(interactionContext.Input) as Window;
        
        Debug.Assert(view is not null && c.MainWindow is not null, "Every dialog should be a Window and main window set.");
        
        var result = await view.ShowDialog<TResult>(c.MainWindow);
        interactionContext.SetOutput(result);
    }
}