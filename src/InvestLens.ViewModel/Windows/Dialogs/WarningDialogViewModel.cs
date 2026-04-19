using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Windows.Dialogs;

public class WarningDialogViewModel : BaseDialogViewModel, IWarningDialogViewModel
{
    public WarningDialogViewModel(IWindowManager windowManager, string message, string actionContext) 
        : base(windowManager, message)
    {
    }

    public override string Icon => "⚠️";
    public override string Header => "Предупреждение";

    protected override void CloseWindow()
    {
        WindowManager.CloseWindow<WarningDialogViewModel>();
    }
}