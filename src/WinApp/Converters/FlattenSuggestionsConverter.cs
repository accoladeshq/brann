using System.Globalization;
using Accolades.Brann.Core;
using Accolades.Brann.Plugins;
using Avalonia.Data.Converters;

namespace Accolades.Brann.Converters;

public class FlattenSuggestionsConverter : IValueConverter
{
    /// <summary>
    /// Flatten a <see cref="ISuggestions"/> into a <see cref="IEnumerable{ISuggestion}"/>
    /// </summary>
    /// <param name="value">The list to flatten.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="parameter">The parameter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If the value is not a <see cref="ISuggestions"/></exception>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not ISuggestions suggestions)
        {
            throw new ArgumentException();
        }
        
        var list = new List<ISuggestion>();
        
        foreach (var suggestionCategory in suggestions)
        {
            list.Add(suggestionCategory);
            list.AddRange(suggestionCategory);
        }

        return list;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}