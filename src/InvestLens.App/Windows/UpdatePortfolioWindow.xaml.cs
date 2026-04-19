using InvestLens.ViewModel.Windows;
using System.Windows;

namespace InvestLens.App.Windows
{
    /// <summary>
    /// Interaction logic for UpdatePortfolioWindow.xaml
    /// </summary>
    public partial class UpdatePortfolioWindow : Window
    {
        public UpdatePortfolioWindow(IUpdatePortfolioWindowViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
