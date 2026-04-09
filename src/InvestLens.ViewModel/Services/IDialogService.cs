namespace InvestLens.ViewModel.Services;

public interface IDialogService
{
    bool? ShowDialog(Type viewModelType);
    void CloseDialog(bool? result);
}