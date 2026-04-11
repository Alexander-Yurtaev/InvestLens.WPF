using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using InvestLens.ViewModel.Services;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public abstract class CreateUpdatePortfolioWindowViewModel : ValidationViewModelBase, ICreateEditPortfolioWindowViewModel
{
    protected readonly Model.Portfolio.BaseModel Model;
    protected readonly IWindowManager WindowManager;
    private string _header = string.Empty;
    private string _actionTitle = string.Empty;

    protected CreateUpdatePortfolioWindowViewModel(
        Model.Portfolio.BaseModel model, 
        IWindowManager windowManager,
        IPortfoliosManager portfoliosManager)
    {
        Model = model;
        WindowManager = windowManager;
        CloseCommand = new DelegateCommand(OnClose);
        ActionCommand = new DelegateCommand(OnAction, CanAction);
        LookupModels =
            new ObservableCollection<LookupViewModel>(portfoliosManager.GetLookupModels()
                .Select(m => new LookupViewModel(m)).ToList());
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

    [Required(ErrorMessage = "Укажите название портфеля")]
    public string Name
    {
        get => Model.Name;
        set
        {
            if (string.Equals(Model.Name, value, StringComparison.InvariantCulture)) return;
            Model.Name = value;
            ValidateProperty(value);
            RaisePropertyChanged();
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
                ValidateProperty(value);
                RaisePropertyChanged();
            }
        }
    }

    public ObservableCollection<LookupViewModel> LookupModels { get; }

    public ICommand CloseCommand { get; set; }
    public ICommand ActionCommand { get; set; }

    private void OnAction()
    {
        if (!Validate()) return;
        ExecuteAction();
    }

    protected abstract void ExecuteAction();
    
    protected virtual bool CanAction()
    {
        return !HasErrors;
    }

    protected abstract void OnClose();

    #region Overrides of ValidationViewModelBase

    protected override void InvalidateCommands()
    {
        ((DelegateCommand)ActionCommand).RaiseCanExecuteChanged();
    }

    #endregion
}