using InvestLens.ViewModel.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InvestLens.ViewModel.Dialogs;

public class ErrorDialogViewModel : BaseDialogViewModel, IErrorDialogViewModel
{
    public ErrorDialogViewModel(IWindowManager windowManager, string message) 
        : base(windowManager, message)
    {
        Icon = "❌";
        Header = "Ошибка";
    }

    protected override void CloseWindow()
    {
        WindowManager.CloseWindow<ErrorDialogViewModel>();
    }
}