using Accolades.Brann.ViewModels;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;

namespace Accolades.Brann.Views;

public partial class SuggestionsView : ReactiveUserControl<SuggestionsViewModel>
{
    public SuggestionsView()
    {
        InitializeComponent();
    }
    
    /// <summary>
    /// Occurs when the window is loaded.
    /// </summary>
    private void OnLoaded(object? _, RoutedEventArgs __) => SearchBox.Focus();
    
    
    private void OnKeyDown(object? _, KeyEventArgs e)
    {
        if (e.Key != Key.Down) return;

        if (SuggestionsBox.ContainerFromIndex(1) is not ListBoxItem firstItem) 
            return;
        
        firstItem.Focus();
    }
    
    private void OnListBoxTapped(object? sender, TappedEventArgs e)
    {
    }
}