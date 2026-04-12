using System.Security;
using InvestLens.ViewModel;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace InvestLens.App.Behaviors;

public class PasswordBoxBehavior : Behavior<PasswordBox>
{
    public required string PropertyName { get; set; }

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
        if (AssociatedObject.DataContext is not IPasswordSupport) return;

        var prop = AssociatedObject.DataContext.GetType().GetProperty(PropertyName);

        if (prop is null) return;

        var securePassword = new SecureString();
        foreach (var c in AssociatedObject.Password)
        {
            securePassword.AppendChar(c);
        }

        prop.SetValue(AssociatedObject.DataContext, securePassword);
    }
}