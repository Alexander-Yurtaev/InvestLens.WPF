using InvestLens.ViewModel.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InvestLens.ViewModel.Dialogs;

public class ConfirmDialogViewModel : BaseDialogViewModel, IConfirmDialogViewModel
{
    public ConfirmDialogViewModel(IWindowManager windowManager, string message, string? actionContext = null)
        : base(windowManager, message)
    {
        Icon = "ℹ️";
        Header = "Информация";
        ActionContext = actionContext ?? "OK";
    }

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