using System.Windows.Input;

namespace InvestLens.ViewModel.Windows.Dialogs
{
    public interface IBaseDialogViewModel : IViewableViewModel
    {
        ICommand AcceptCommand { get; set; }
        string ActionContext { get; init; }
        ICommand CancelCommand { get; set; }
        string Header { get; init; }
        string Icon { get; init; }
        string Message { get; }
        bool ShowCancelButton { get; }
    }
}