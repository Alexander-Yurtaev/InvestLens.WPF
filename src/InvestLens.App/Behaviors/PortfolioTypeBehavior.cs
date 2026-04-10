using System.Windows;
using InvestLens.ViewModel;
using Microsoft.Xaml.Behaviors;

namespace InvestLens.App.Behaviors;

public class PortfolioTypeBehavior : Behavior<Window>
{
    public static readonly DependencyProperty IsPortfolioComplexTypeProperty = DependencyProperty.Register(
        nameof(IsPortfolioComplexType), typeof(bool), typeof(PortfolioTypeBehavior), new PropertyMetadata(false, PropertyChangedCallback));

    public bool IsPortfolioComplexType
    {
        get => (bool)GetValue(IsPortfolioComplexTypeProperty);
        set => SetValue(IsPortfolioComplexTypeProperty, value);
    }

    public double WindowHeight { get; set; }

    #region Overrides of Behavior

    protected override void OnAttached()
    {
        base.OnAttached();
        WindowHeight = AssociatedObject.Height;
    }

    #endregion

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var behavior = d as PortfolioTypeBehavior;
        if (behavior?.AssociatedObject is not Window window) return;
        if (window.DataContext is not CreateEditPortfolioWindowViewModel viewModel) return;

        window.Height = viewModel.IsPortfolioComplexType ? behavior.WindowHeight + 240 : behavior.WindowHeight;
    }
}