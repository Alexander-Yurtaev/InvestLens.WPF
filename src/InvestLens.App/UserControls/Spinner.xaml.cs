using System.Windows;
using System.Windows.Controls;

namespace InvestLens.App.UserControls
{
    /// <summary>
    /// Interaction logic for Spinner.xaml
    /// </summary>
    public partial class Spinner : UserControl
    {
        public Spinner()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(
                nameof(Size),
                typeof(double),
                typeof(Spinner),
                new PropertyMetadata(48.0, OnSizeChanged));

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(
                nameof(Radius),
                typeof(CornerRadius),
                typeof(Spinner),
                new PropertyMetadata(new CornerRadius(24)));

        public double Size
        {
            get { return (double)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public CornerRadius Radius
        {
            get { return (CornerRadius)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Spinner;
            if (control != null && e.NewValue is double newSize)
            {
                var newRadius = newSize / 2;
                control.Radius = new CornerRadius(newRadius);
            }
        }
    }
}
