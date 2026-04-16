using System.Windows.Input;

namespace InvestLens.ViewModel.Dialogs;

public interface IConfirmDeleteDialogViewModel : IConfirmable
{
    ICommand CloseCommand { get; set; }
    ICommand DeleteCommand { get; set; }
    bool IsAgree { get; set; }
}