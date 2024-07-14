using Accolades.Brann.Avalonia;
using Accolades.Brann.Core;
using Splat;

namespace Accolades.Brann.ViewModels;

public class PaletteViewModel : ViewModelBase
{
    /// <summary>
    /// Initialize a new <see cref="PaletteViewModel"/>.
    /// </summary>
    public PaletteViewModel()
    {   
        SuggestionProvider = Locator.Current.GetRequiredService<ISuggestionProvider>();
    }

    /// <summary>
    /// Gets the <see cref="ISuggestionProvider"/>
    /// </summary>
    public ISuggestionProvider SuggestionProvider { get; }
}