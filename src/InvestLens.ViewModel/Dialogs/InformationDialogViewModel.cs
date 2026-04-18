using InvestLens.ViewModel.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InvestLens.ViewModel.Dialogs;

public class InformationDialogViewModel : BaseDialogViewModel, IInformationDialogViewModel
{
    public InformationDialogViewModel(IWindowManager windowManager, string message) 
        : base(windowManager, message)
    {
        Icon = "ℹ️";
        Header = "Информация";
    }

    protected override void CloseWindow()
    {
        WindowManager.CloseWindow<InformationDialogViewModel>();
    }
}