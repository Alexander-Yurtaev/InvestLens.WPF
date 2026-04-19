using InvestLens.ViewModel.Services;
using System.Windows.Input;

namespace InvestLens.ViewModel.Windows.Dialogs;

public abstract class BaseDialogViewModel : ValidationViewModelBase, IBaseDialogViewModel
{
    protected readonly IWindowManager WindowManager;

    protected BaseDialogViewModel(IWindowManager windowManager, string? message = "")
    {
        Message = message ?? "";
        WindowManager = windowManager;
        CancelCommand = new DelegateCommand(OnCancel);
        AcceptCommand = new DelegateCommand(OnAccept, CanAccept);
        CloseCommand = new DelegateCommand(OnClose);
    }

    public virtual string Icon => string.Empty;
    public virtual string Header => string.Empty;
    public string Message { get; }
    public virtual string ActionContext => "OK";
    public virtual bool ShowCancelButton => false;
    public string ViewName => "ModalDialog";

    public ICommand CancelCommand { get; set; }
    public ICommand AcceptCommand { get; set; }
    public ICommand CloseCommand { get; set; }

    protected virtual bool CanAccept() => true;

    protected virtual void OnCancel()
    {
        CloseWindow();
    }

    protected virtual void OnAccept()
    {
        CloseWindow();
    }

    protected void OnClose()
    {
        CloseWindow();
    }

    protected abstract void CloseWindow();
}
