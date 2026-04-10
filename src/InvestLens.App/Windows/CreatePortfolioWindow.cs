using System.Windows;
using InvestLens.ViewModel;

namespace InvestLens.App.Windows
{
    public class CreatePortfolioWindow : CreateEditPortfolioWindow
    {
        public CreatePortfolioWindow(ICreatePortfolioWindowViewModel viewModel)
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(CreatePortfolioWindow), new FrameworkPropertyMetadata(typeof(CreatePortfolioWindow)));
            DataContext = viewModel;
        }
    }
}
