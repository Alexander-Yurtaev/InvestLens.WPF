using InvestLens.ViewModel.Windows.Dialogs;
using System.Windows;

namespace InvestLens.App.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteDialog.xaml
    /// </summary>
    public partial class ConfirmDeleteDialog : Window
    {
        public ConfirmDeleteDialog(IConfirmDeleteDialogViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
