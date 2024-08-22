namespace Accolades.Brann.Models;

internal interface IDialogService
{
    /// <summary>
    /// Open a dialog.
    /// </summary>
    /// <typeparam name="TViewModel">The view model associated to this dialog.</typeparam>
    /// <typeparam name="TResult">The dialog result.</typeparam>
    /// <exception cref="PlatformNotSupportedException">Occurs when the platform is not a IClassicDesktopStyleApplicationLifetime.</exception>
    /// <returns></returns>
    Task<TResult> Open<TViewModel, TResult>() where TViewModel: class, new();
}