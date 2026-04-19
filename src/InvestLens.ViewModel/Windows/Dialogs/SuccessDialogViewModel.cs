using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Windows.Dialogs;

public class SuccessDialogViewModel : BaseDialogViewModel, ISuccessDialogViewModel
{
    public SuccessDialogViewModel(IWindowManager windowManager, string message) 
        : base(windowManager, message)
    {
    }

    public override string Icon => "✅";
    public override string Header => "Успех";

    protected override void CloseWindow()
    {
        WindowManager.CloseWindow<SuccessDialogViewModel>();
    }
}