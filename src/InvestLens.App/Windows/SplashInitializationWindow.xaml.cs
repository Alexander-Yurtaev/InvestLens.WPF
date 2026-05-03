using InvestLens.ViewModel.Windows;
using System.Windows;

namespace InvestLens.App.Windows
{
    /// <summary>
    /// Interaction logic for SplashInitializationWindow.xaml
    /// </summary>
    public partial class SplashInitializationWindow : Window
    {
        public SplashInitializationWindow(ISplashInitializationWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
