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
        var isPortfolioComplexType = AssociatedObject.DataContext switch
        {
            CreatePortfolioWindowViewModel createViewModel => createViewModel.IsPortfolioComplexType,
            UpdatePortfolioWindowViewModel editViewModel => editViewModel.IsPortfolioComplexType,
            _ => false
        };
        AssociatedObject.Height = isPortfolioComplexType ? this.WindowHeight + 240 : this.WindowHeight;
    }

    #endregion

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var behavior = d as PortfolioTypeBehavior;
        if (behavior?.AssociatedObject is not Window window) return;
        if (window.DataContext is CreatePortfolioWindowViewModel createViewModel)
        {
            window.Height = createViewModel.IsPortfolioComplexType ? behavior.WindowHeight + 240 : behavior.WindowHeight;
        } 
    }
}