using InvestLens.Common.Helpers;
using InvestLens.ViewModel;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace InvestLens.App.Behaviors;

public class PasswordBoxBehavior : Behavior<PasswordBox>
{
    public required string Propertyname { get; set; }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PasswordChanged += AssociatedObjectOnPasswordChanged;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.PasswordChanged -= AssociatedObjectOnPasswordChanged;
        base.OnDetaching();
    }

    private void AssociatedObjectOnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (AssociatedObject.DataContext is not IConfirmPasswordSupport vm) return;

        var prop = vm.GetType().GetProperty(Propertyname);
        if (prop is null) return;
        prop.SetValue(vm, PasswordHelper.HashPassword(AssociatedObject.Password));
    }
}