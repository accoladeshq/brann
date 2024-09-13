using Accolades.Brann.ViewModels;
using Avalonia.ReactiveUI;

namespace Accolades.Brann.Views;

public partial class PaletteView : ReactiveWindow<PaletteViewModel>
{
    /// <summary>
    /// Initialize a new <see cref="PaletteView"/>
    /// </summary>
    public PaletteView()
    {
        InitializeComponent();
    }
}