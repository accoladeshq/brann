using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Accolades.Brann.Views;

public partial class PaletteView : Window
{
    /// <summary>
    /// Initialize a new <see cref="PaletteView"/>
    /// </summary>
    public PaletteView()
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
        
        var item = SuggestionsBox.Items[0];
        SuggestionsBox.SelectedItem = item;
    }
}