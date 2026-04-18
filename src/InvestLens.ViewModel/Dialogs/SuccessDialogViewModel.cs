using InvestLens.ViewModel.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InvestLens.ViewModel.Dialogs;

public class SuccessDialogViewModel : BaseDialogViewModel, ISuccessDialogViewModel
{
    public SuccessDialogViewModel(IWindowManager windowManager, string message) 
        : base(windowManager, message)
    {
        Icon = "✅";
        Header = "Успех";
    }

    protected override void CloseWindow()
    {
        WindowManager.CloseWindow<SuccessDialogViewModel>();
    }
}