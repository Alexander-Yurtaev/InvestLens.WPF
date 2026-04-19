using System.Windows;
using InvestLens.ViewModel.Windows;

namespace InvestLens.App.Windows
{
    /// <summary>
    /// Interaction logic for CreatePortfolioWindow.xaml
    /// </summary>
    public partial class CreatePortfolioWindow : Window
    {
        public CreatePortfolioWindow(ICreatePortfolioWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
