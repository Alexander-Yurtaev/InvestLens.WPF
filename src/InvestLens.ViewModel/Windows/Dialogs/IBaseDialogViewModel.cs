using System.Windows.Input;

namespace InvestLens.ViewModel.Windows.Dialogs
{
    public interface IBaseDialogViewModel : IViewableViewModel
    {
        ICommand AcceptCommand { get; set; }
        string ActionContext { get; }
        ICommand CancelCommand { get; set; }
        string Header { get; }
        string Icon { get; }
        string Message { get; }
        bool ShowCancelButton { get; }
    }
}