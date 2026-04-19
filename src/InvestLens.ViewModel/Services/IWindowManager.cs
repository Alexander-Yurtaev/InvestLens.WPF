namespace InvestLens.ViewModel.Services;

public interface IWindowManager
{
    void ShowErrorDialog(string message);
    void ShowWarningDialog(string message, string actionContext);
    void ShowInformationDialog(string message, string actionContext);
    bool? ShowConfirmDialog(string message, string actionContext);
    void ShowSuccessDialog(string message, string actionContext);
    void ShowWindow<TViewModel>(TViewModel? viewModel = null) where TViewModel : class;
    bool? ShowDialogWindow<TViewModel>(TViewModel? viewModel = null) where TViewModel : class;
    void CloseWindow<TViewModel>() where TViewModel : class;
    void SetMainWindow<TViewModel>() where TViewModel : class;
}