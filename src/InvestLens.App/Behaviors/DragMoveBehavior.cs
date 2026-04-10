using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace InvestLens.App.Behaviors;

public class DragMoveBehavior : Behavior<Window>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.MouseLeftButtonDown += AssociatedObjectOnMouseLeftButtonDown;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.MouseLeftButtonDown -= AssociatedObjectOnMouseLeftButtonDown;
    }

    private void AssociatedObjectOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount != 1) return;
        AssociatedObject.DragMove();
    }
}