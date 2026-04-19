using InvestLens.ViewModel.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InvestLens.ViewModel.Windows.Dialogs;

public class WarningDialogViewModel : BaseDialogViewModel, IWarningDialogViewModel
{
    public WarningDialogViewModel(IWindowManager windowManager, string message, string actionContext) 
        : base(windowManager, message)
    {
        Icon = "⚠️";
        Header = "Предупреждение";
    }

    protected override void CloseWindow()
    {
        WindowManager.CloseWindow<WarningDialogViewModel>();
    }
}