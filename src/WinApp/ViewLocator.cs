using Accolades.Brann.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ReactiveUI;

namespace Accolades.Brann;

public class ViewLocator : IDataTemplate, IViewLocator
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        return GetViewFromViewModel(data);
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }

    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        var view = GetViewFromViewModel(viewModel) as IViewFor;
        return view;
    }

    private Control GetViewFromViewModel(object? o)
    {
        if (o is null)
        {
            throw new ArgumentNullException(nameof(o));
        }
        
        var name = o.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type == null) return new TextBlock { Text = "Not Found: " + name };
        
        var control = (Control)Activator.CreateInstance(type)!;
        control.DataContext = o;
        return control;
    }
}