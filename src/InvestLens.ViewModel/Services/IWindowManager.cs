namespace InvestLens.ViewModel.Services;

public interface IWindowManager
{
    void ShowWindow<TViewModel>(TViewModel? viewModel = null) where TViewModel : class;
    bool? ShowDialogWindow<TViewModel>(TViewModel? viewModel = null) where TViewModel : class;
    void CloseWindow<TViewModel>() where TViewModel : class;
    void SetMainWindow<TViewModel>() where TViewModel : class;
}