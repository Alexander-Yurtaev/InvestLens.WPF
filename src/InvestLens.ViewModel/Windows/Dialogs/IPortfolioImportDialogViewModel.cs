using System.Windows.Input;

namespace InvestLens.ViewModel.Windows.Dialogs
{
    public interface IPortfolioImportDialogViewModel : IConfirmable
    {
        string FileFullName { get; set; }
        string FileName { get; }
        bool IsSelected { get; }
        ICommand SelectFileCommand { get; set; }
    }
}