using System.Windows.Input;

namespace InvestLens.ViewModel.Windows.Dialogs;

public interface IConfirmDeleteDialogViewModel : IConfirmable
{
    ICommand CloseCommand { get; set; }
    ICommand DeleteCommand { get; set; }
    bool IsAgree { get; set; }
}