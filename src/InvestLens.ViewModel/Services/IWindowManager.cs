namespace InvestLens.ViewModel.Services;

public interface IWindowManager
{
    void ShowWindow<TViewModel>(bool asDialog = false) where TViewModel : class;
    void CloseWindow<TViewModel>() where TViewModel : class;
    void SetMainWindow<TViewModel>() where TViewModel : class;
}