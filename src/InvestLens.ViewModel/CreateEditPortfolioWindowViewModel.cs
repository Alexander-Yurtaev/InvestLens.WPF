using InvestLens.ViewModel.Services;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public abstract class CreateEditPortfolioWindowViewModel : ValidationViewModelBase, ICreateEditPortfolioWindowViewModel
{
    protected readonly Model.Portfolio.BaseModel Model;
    protected readonly IWindowManager WindowManager;
    private string _header = string.Empty;
    private string _actionTitle = string.Empty;

    protected CreateEditPortfolioWindowViewModel(
        Model.Portfolio.BaseModel model, 
        IWindowManager windowManager,
        IPortfoliosManager portfoliosManager)
    {
        Model = model;
        WindowManager = windowManager;
        CloseCommand = new DelegateCommand(OnClose);
        ActionCommand = new DelegateCommand(OnAction);
        LookupModels = portfoliosManager.GetLookupModels().ToList();
    }

    public string Header
    {
        get => _header;
        set => SetProperty(ref _header, value);
    }

    public string ActionTitle
    {
        get => _actionTitle;
        set => SetProperty(ref _actionTitle, value);
    }

    public string Title
    {
        get => Model.Title;
        set
        {
            if (!string.Equals(Model.Title, value, StringComparison.InvariantCulture))
            {
                Model.Title = value;
                RaisePropertyChanged();
            }
        }
    }

    public string Description
    {
        get => Model.Description;
        set
        {
            if (!string.Equals(Model.Description, value, StringComparison.InvariantCulture))
            {
                Model.Description = value;
                RaisePropertyChanged();
            }
        }
    }

    public List<Model.Portfolio.LookupModel> LookupModels { get; }

    public ICommand CloseCommand { get; set; }
    public ICommand ActionCommand { get; set; }

    protected abstract void OnClose();
    protected abstract void OnAction();

    #region Overrides of ValidationViewModelBase

    protected override void InvalidateCommands()
    {
        ((DelegateCommand)ActionCommand).RaiseCanExecuteChanged();
    }

    #endregion
}