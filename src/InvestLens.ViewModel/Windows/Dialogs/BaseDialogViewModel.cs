using InvestLens.ViewModel.Services;
using System.Windows.Input;

namespace InvestLens.ViewModel.Windows.Dialogs;

public abstract class BaseDialogViewModel : IBaseDialogViewModel
{
    protected readonly IWindowManager WindowManager;

    protected BaseDialogViewModel(IWindowManager windowManager, string message)
    {
        Message = message;
        WindowManager = windowManager;
        CancelCommand = new DelegateCommand(OnCancel);
        AcceptCommand = new DelegateCommand(OnAccept);
        CloseCommand = new DelegateCommand(OnClose);
    }

    public string Icon { get; init; } = string.Empty;
    public string Header { get; init; } = string.Empty;
    public string Message { get; }
    public string ActionContext { get; init; } = "OK";
    public virtual bool ShowCancelButton => false;
    public string ViewName => "ModalDialog";

    public ICommand CancelCommand { get; set; }
    public ICommand AcceptCommand { get; set; }
    public ICommand CloseCommand { get; set; }

    protected virtual void OnCancel()
    {
        CloseWindow();
    }

    protected virtual void OnAccept()
    {
        CloseWindow();
    }

    protected virtual void OnClose()
    {
        CloseWindow();
    }

    protected abstract void CloseWindow();
}
