using System.Collections;
using System.Windows.Input;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public abstract class CreateEditPortfolioWindowViewModel : ValidationViewModelBase, ICreateEditPortfolioWindowViewModel
{
    protected readonly IWindowManager WindowManager;
    private string _title = string.Empty;

    protected CreateEditPortfolioWindowViewModel(IWindowManager windowManager)
    {
        WindowManager = windowManager;
        CloseCommand = new DelegateCommand(OnClose);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public ICommand CloseCommand { get; set; }

    protected abstract void OnClose();
}