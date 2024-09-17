using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Accolades.Brann.Avalonia;

public class ListBoxBehavior : AvaloniaObject
{
    static ListBoxBehavior()
    {
        CommandProperty.Changed.AddClassHandler<ListBox>(HandleCommandChanged);
    }

    /// <summary>
    /// Identifies the <seealso cref="CommandProperty"/> avalonia attached property.
    /// </summary>
    /// <value>Provide an <see cref="ICommand"/> derived object or binding.</value>
    public static readonly AttachedProperty<ICommand> CommandProperty = AvaloniaProperty.RegisterAttached<ListBoxBehavior, ListBox, ICommand>(
        "Command", default, false, BindingMode.OneTime);

    /// <summary>
    /// <see cref="CommandProperty"/> changed event handler.
    /// </summary>
    private static void HandleCommandChanged(ListBox interactElem, AvaloniaPropertyChangedEventArgs args)
    {
        if (args.NewValue is ICommand)
        {
             // Add non-null value
             interactElem.AddHandler(InputElement.TappedEvent, Handler);
        }
        else
        {
             // remove prev value
             interactElem.RemoveHandler(InputElement.TappedEvent, Handler);
        }
        
        // local handler fcn
        static void Handler(object s, RoutedEventArgs e)
        {
            if (s is ListBox interactElem)
            {
                ICommand commandValue = interactElem.GetValue(CommandProperty);
                if (commandValue?.CanExecute(interactElem.SelectedItem) == true)
                {
                    commandValue.Execute(interactElem.SelectedItem);
                }
            }
        }
    }


    /// <summary>
    /// Accessor for Attached property <see cref="CommandProperty"/>.
    /// </summary>
    public static void SetCommand(AvaloniaObject element, ICommand commandValue)
    {
        element.SetValue(CommandProperty, commandValue);
    }

    /// <summary>
    /// Accessor for Attached property <see cref="CommandProperty"/>.
    /// </summary>
    public static ICommand GetCommand(AvaloniaObject element)
    {
        return element.GetValue(CommandProperty);
    }
}