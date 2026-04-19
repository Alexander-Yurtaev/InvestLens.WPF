using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Windows.Dialogs;

public class InformationDialogViewModel : BaseDialogViewModel, IInformationDialogViewModel
{
    public InformationDialogViewModel(IWindowManager windowManager, string message) 
        : base(windowManager, message)
    {
    }

    public override string Icon => "ℹ️";
    public override string Header => "Информация";

    protected override void CloseWindow()
    {
        WindowManager.CloseWindow<InformationDialogViewModel>();
    }
}