using InvestLens.ViewModel.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using InvestLens.DataAccess.Repositories;
using InvestLens.Model.Crud.Portfolio;

namespace InvestLens.ViewModel.Windows;

public abstract class CreateUpdatePortfolioWindowViewModel : ValidationViewModelBase, ICreateUpdatePortfolioWindowViewModel, ILoadableViewModel, IDisposable
{
    protected readonly BaseModel Model;
    protected readonly IWindowManager WindowManager;
    private readonly IAuthManager _authManager;
    private string _header = string.Empty;
    private string _actionTitle = string.Empty;

    protected CreateUpdatePortfolioWindowViewModel(
        BaseModel model, 
        IWindowManager windowManager,
        IAuthManager authManager,
        IPortfoliosManager portfoliosManager)
    {
        Model = model;
        WindowManager = windowManager;
        _authManager = authManager;
        PortfoliosManager = portfoliosManager;
        CloseCommand = new DelegateCommand(OnClose);
        ActionCommand = new AsyncDelegateCommand(OnAction, CanAction);
        LookupModels = [];
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
            var _ = ValidateNameAsync();
        }
    }

    public string Description
    {
        get => Model.Description;
        set
        {
            if (string.Equals(Model.Description, value, StringComparison.InvariantCulture)) return;

            Model.Description = value;
            ValidateProperty(value);
            RaisePropertyChanged();
        }
    }

    public ObservableCollection<LookupViewModel> LookupModels { get; }

    public ICommand CloseCommand { get; set; }
    public ICommand ActionCommand { get; set; }

    public virtual async Task Load(bool? force=false)
    {
        if (_authManager.CurrentUser is null)
        {
            WindowManager.ShowErrorDialog("Вы не авторизованы!");
            WindowManager.CloseWindow<CreatePortfolioWindowViewModel>();
            WindowManager.ShowDialogWindow<LoginWindowViewModel>();
            return;
        }

        var userId = _authManager.CurrentUser.Id;
        var lookupModels = PortfoliosManager.GetLookupModels(userId, Model.Id > 0 ? Model.Id : null)
            .Select(m =>
            {
                var vm = new LookupViewModel(m);
                vm.PropertyChanged += ItemOnPropertyChanged;
                return vm;
            }).ToList();

        LookupModels.Clear();
        foreach (var lookupModel in lookupModels)
        {
            LookupModels.Add(lookupModel);
        }
    }

    protected IPortfoliosManager PortfoliosManager {  get; }

    private async Task OnAction()
    {
        if (!Validate()) return;
        
        await ValidateNameAsync();
        if (HasErrors) return;

        await ExecuteAction();
    }

    protected abstract Task ExecuteAction();
    
    protected virtual bool CanAction()
    {
        return !HasErrors;
    }

    protected abstract void OnClose();

    private void ItemOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ValidateProperty(LookupModels, nameof(LookupModels));
    }

    protected async Task ValidateNameAsync()
    {
        if (_authManager.CurrentUser is null)
        {
            WindowManager.ShowErrorDialog("Вы не авторизованы!");
            WindowManager.CloseWindow<CreatePortfolioWindowViewModel>();
            WindowManager.ShowDialogWindow<LoginWindowViewModel>();
            return;
        }

        var ownerId = _authManager.CurrentUser!.Id;

        var isUnique = await PortfoliosManager.CheckNameUniqueAsync(Model.Id, ownerId, Name);
        if (!isUnique)
        {
            AddError("Портфель с таким именем уже создан", nameof(Name));
        }
    }

    #region Overrides of ValidationViewModelBase

    protected override void InvalidateCommands()
    {
        ((AsyncDelegateCommand)ActionCommand).RaiseCanExecuteChanged();
    }

    #endregion Overrides of ValidationViewModelBase

    #region IDisposable

    private void UnsubscribeEvents()
    {
        foreach (var item in LookupModels)
        {
            item.PropertyChanged -= ItemOnPropertyChanged;
        }
        LookupModels.Clear();
    }

    public void Dispose()
    {
        UnsubscribeEvents();
    }

    #endregion
}