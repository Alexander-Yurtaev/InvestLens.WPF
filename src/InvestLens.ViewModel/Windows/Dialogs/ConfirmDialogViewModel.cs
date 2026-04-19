using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Windows.Dialogs;

public class ConfirmDialogViewModel : BaseDialogViewModel, IConfirmDialogViewModel
{
    private string _actionContext;

    public ConfirmDialogViewModel(IWindowManager windowManager, string message, string? actionContext = null)
        : base(windowManager, message)
    {
        _actionContext = actionContext ?? base.ActionContext;
    }

    public override string Icon => "ℹ️";

    public override string Header => "Информация";
    public override string ActionContext => _actionContext;

    public override bool ShowCancelButton => true;

    public bool IsConfirmed { get; private set; }

    protected override void OnCancel()
    {
        IsConfirmed = false;
        base.OnCancel();
    }

    protected override void OnAccept()
    {
        IsConfirmed = true;
        base.OnAccept();
    }

    protected override void CloseWindow()
    {
        WindowManager.CloseWindow<ConfirmDialogViewModel>();
    }
}