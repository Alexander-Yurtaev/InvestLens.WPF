using InvestLens.DataAccess.Repositories;
using InvestLens.Model.Enums;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;
using System.Runtime.CompilerServices;

namespace InvestLens.ViewModel;

public sealed class CreatePortfolioWindowViewModel : CreateUpdatePortfolioWindowViewModel, ICreatePortfolioWindowViewModel, ISupportPortfolioType
{
    private readonly IAuthManager _authManager;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IEventAggregator _eventAggregator;

    private bool _isPortfolioSimpleType;
    private bool _isPortfolioComplexType;

    public CreatePortfolioWindowViewModel(
        Model.Portfolio.CreateModel model, 
        IWindowManager windowManager,
        IAuthManager authManager,
        IPortfolioRepository portfolioRepository,
        IPortfoliosManager portfoliosManager,
        IEventAggregator eventAggregator) : base(model, windowManager, authManager, portfoliosManager, portfolioRepository)
    {
        _authManager = authManager;
        _portfolioRepository = portfolioRepository;
        _eventAggregator = eventAggregator;

        Header = "Создание";
        ActionTitle = "Создать портфель";
        IsPortfolioSimpleType = Model.PortfolioType == PortfolioType.Invest;
        IsPortfolioComplexType = Model.PortfolioType == PortfolioType.Complex;

        InvalidateCommands();
    }

    public bool IsPortfolioSimpleType
    {
        get => _isPortfolioSimpleType;
        set
        {
            if(!SetProperty(ref _isPortfolioSimpleType, value)) return;

            ValidateProperty(IsPortfolioSimpleType);
            RaisePropertyChanged();
        }
    }

    public bool IsPortfolioComplexType
    {
        get => _isPortfolioComplexType;
        set
        {
            if (!SetProperty(ref _isPortfolioComplexType, value)) return;

            ValidateProperty(IsPortfolioComplexType);
            RaisePropertyChanged();
        }
    }
    
    #region Overrides of CreateUpdatePortfolioWindowViewModel

    protected override async Task ExecuteAction()
    {
        // ToDo make DialogService
        if (_authManager.CurrentUser is null) throw new SystemException("Вы не авторизованы!");

        var model = (Model.Portfolio.CreateModel)Model;

        model.SetPortfolioType(IsPortfolioSimpleType
            ? PortfolioType.Invest
            : PortfolioType.Complex);

        model.Portfolios.Clear();
        model.Portfolios.AddRange(LookupModels.Where(lm => lm.IsChecked).Select(lm => lm.Id));

        await _portfolioRepository.CreatePortfolio(model);

        _eventAggregator.GetEvent<PortfolioCreatedEvent>().Publish();

        WindowManager.CloseWindow<CreatePortfolioWindowViewModel>();
    }

    protected override void OnClose()
    {
        WindowManager.CloseWindow<CreatePortfolioWindowViewModel>();
    }

    #endregion

    #region Overrides of ValidationViewModelBase

    protected override bool Validate()
    {
        base.Validate();
        ValidateProperty(LookupModels, nameof(LookupModels));
        return !HasErrors;
    }

    protected override void ValidateProperty(object? newValue, [CallerMemberName] string? propertyName = null)
    {
        base.ValidateProperty(newValue, propertyName);

        if (propertyName == nameof(LookupModels) || propertyName == nameof(IsPortfolioSimpleType) || propertyName == nameof(IsPortfolioComplexType))
        {
            if (IsPortfolioComplexType && !LookupModels.Any(m => m.IsChecked))
            {
                AddError("Укажите портфели из которых состоит составной портфель", nameof(LookupModels));
            }
            else
            {
                ClearErrors(nameof(LookupModels));
            }
        }
    }

    #endregion Overrides of ValidationViewModelBase
}