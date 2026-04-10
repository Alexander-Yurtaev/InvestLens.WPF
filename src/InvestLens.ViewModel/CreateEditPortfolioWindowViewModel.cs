using System.Windows.Input;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public abstract class CreateEditPortfolioWindowViewModel : ValidationViewModelBase, ICreateEditPortfolioWindowViewModel
{
    protected readonly IWindowManager WindowManager;
    private string _title = string.Empty;
    private string _actionTitle;
    private string _name;
    private string _description;

    protected CreateEditPortfolioWindowViewModel(IWindowManager windowManager)
    {
        WindowManager = windowManager;
        CloseCommand = new DelegateCommand(OnClose);
        ActionCommand = new DelegateCommand(OnAction);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string ActionTitle
    {
        get => _actionTitle;
        set => SetProperty(ref _actionTitle, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public ICommand CloseCommand { get; set; }
    public ICommand ActionCommand { get; set; }

    protected abstract void OnClose();
    protected abstract void OnAction();
}