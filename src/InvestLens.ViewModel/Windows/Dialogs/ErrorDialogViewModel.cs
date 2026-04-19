using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Windows.Dialogs;

public class ErrorDialogViewModel : BaseDialogViewModel, IErrorDialogViewModel
{
    public ErrorDialogViewModel(IWindowManager windowManager, string message) 
        : base(windowManager, message)
    {
    }

    public override string Icon => "❌";
    public override string Header => "Ошибка";

    protected override void CloseWindow()
    {
        WindowManager.CloseWindow<ErrorDialogViewModel>();
    }
}