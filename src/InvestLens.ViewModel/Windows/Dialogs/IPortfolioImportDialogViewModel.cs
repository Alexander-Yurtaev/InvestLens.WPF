using System.Windows.Input;

namespace InvestLens.ViewModel.Windows.Dialogs
{
    public interface IPortfolioImportDialogViewModel
    {
        string FileName { get; }
        bool IsSelected { get; }
        ICommand SelectFileCommand { get; set; }
    }
}