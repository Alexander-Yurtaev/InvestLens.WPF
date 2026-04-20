namespace InvestLens.ViewModel.Services;

public interface IWindowManager
{
    void ShowErrorDialog(string message);
    void ShowWarningDialog(string message, string actionContext);
    void ShowInformationDialog(string message);
    bool? ShowConfirmDialog(string message, string actionContext);
    void ShowSuccessDialog(string message);
    void ShowWindow<TViewModel>(TViewModel? viewModel = null) where TViewModel : class;
    bool? ShowDialogWindow<TViewModel>(TViewModel? viewModel = null) where TViewModel : class;
    TViewModel? ShowModalDialog<TViewModel>(TViewModel? viewModel = null) where TViewModel : class;
    void CloseWindow<TViewModel>() where TViewModel : class;
    void SetMainWindow<TViewModel>() where TViewModel : class;
    string ShowSelectFileDialog(string title, string? filter = "");
}