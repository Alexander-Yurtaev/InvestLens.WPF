using Microsoft.Xaml.Behaviors;
using System.Windows;
using InvestLens.ViewModel;

namespace InvestLens.App.Behaviors;

public class LoadBehavior : Behavior<FrameworkElement>
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

    private async void AssociatedObjectOnLoaded(object sender, RoutedEventArgs e)
    {
        if (AssociatedObject.DataContext is ILoadableViewModel vm)
        {
            await vm.Load();
        }
    }
}