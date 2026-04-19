using InvestLens.ViewModel.Windows.Dialogs;
using System.Windows;

namespace InvestLens.App.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for PortfolioImportDialog.xaml
    /// </summary>
    public partial class PortfolioImportDialog : Window
    {
        public PortfolioImportDialog(IPortfolioImportDialogViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
