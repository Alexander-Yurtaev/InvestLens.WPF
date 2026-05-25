using System.Windows;
using System.Windows.Controls;

namespace InvestLens.App.UserControls
{
    /// <summary>
    /// Interaction logic for BusyIndicator.xaml
    /// </summary>
    public partial class BusyIndicator : UserControl
    {
        public BusyIndicator()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty BusyContentProperty =
            DependencyProperty.Register(
                nameof(BusyContent),
                typeof(string),
                typeof(BusyIndicator),
                new PropertyMetadata(null));

        public string BusyContent
        {
            get => (string)GetValue(BusyContentProperty);
            set => SetValue(BusyContentProperty, value);
        }
    }
}
