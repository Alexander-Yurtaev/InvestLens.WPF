using InvestLens.Model;
using InvestLens.ViewModel.Services;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public abstract class CreateEditPortfolioWindowViewModel : ValidationViewModelBase, ICreateEditPortfolioWindowViewModel
{
    protected readonly CreateEditPortfolioModel Model;
    protected readonly IWindowManager WindowManager;
    
    protected CreateEditPortfolioWindowViewModel(CreateEditPortfolioModel model, IWindowManager windowManager)
    {
        Model = model;
        WindowManager = windowManager;
        CloseCommand = new DelegateCommand(OnClose);
        ActionCommand = new DelegateCommand(OnAction);
        IsPortfolioSimpleType = true;
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

    public string ActionTitle
    {
        get => Model.ActionTitle;
        set
        {
            if (!string.Equals(Model.ActionTitle, value, StringComparison.InvariantCulture))
            {
                Model.ActionTitle = value;
                RaisePropertyChanged();
            }
        }
    }

    public string Name
    {
        get => Model.Name;
        set
        {
            if (!string.Equals(Model.Name, value, StringComparison.InvariantCulture))
            {
                Model.Name = value;
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

    public bool IsPortfolioSimpleType
    {
        get => Model.IsPortfolioSimpleType;
        set
        {
            if (Model.IsPortfolioSimpleType != value)
            {
                Model.IsPortfolioSimpleType = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsPortfolioComplexType));
            }
        }
    }

    public bool IsPortfolioComplexType
    {
        get => !Model.IsPortfolioSimpleType;
        set
        {
            if (Model.IsPortfolioSimpleType == value)
            {
                Model.IsPortfolioSimpleType = !value;
                RaisePropertyChanged();
            }
        }
    }

    public ICommand CloseCommand { get; set; }
    public ICommand ActionCommand { get; set; }

    protected abstract void OnClose();
    protected abstract void OnAction();
}