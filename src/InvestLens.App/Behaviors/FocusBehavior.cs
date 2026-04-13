using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace InvestLens.App.Behaviors;

public class FocusBehavior : Behavior<FrameworkElement>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += AssociatedObjectOnLoaded;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
        base.OnDetaching();
    }

    private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs e)
    {
        AssociatedObject.Focus();
    }
}