using InvestLens.Model.Enums;
using InvestLens.ViewModel.Services;
using System.Runtime.CompilerServices;

namespace InvestLens.ViewModel;

public sealed class CreatePortfolioWindowViewModel : CreateUpdatePortfolioWindowViewModel, ICreatePortfolioWindowViewModel, ISupportPortfolioType
{
    public CreatePortfolioWindowViewModel(
        Model.Portfolio.CreateModel model, 
        IWindowManager windowManager, 
        IPortfoliosManager portfoliosManager) : base(model, windowManager, portfoliosManager)
    {
        Header = "Создание";
        ActionTitle = "Создать портфель";

        InvalidateCommands();
    }

    public bool IsPortfolioSimpleType
    {
        get => Model.PortfolioType == PortfolioType.Invest;
        set
        {
            ((Model.Portfolio.CreateModel)Model).SetPortfolioType(value ? PortfolioType.Invest : PortfolioType.Complex);

            ValidateProperty(IsPortfolioSimpleType);
            RaisePropertyChanged();
            RaisePropertyChanged(nameof(IsPortfolioComplexType));
        }
    }

    public bool IsPortfolioComplexType
    {
        get => Model.PortfolioType == PortfolioType.Complex;
        set
        {
            ((Model.Portfolio.CreateModel)Model).SetPortfolioType(value ? PortfolioType.Complex : PortfolioType.Invest);

            ValidateProperty(IsPortfolioComplexType);
            RaisePropertyChanged();
            RaisePropertyChanged(nameof(IsPortfolioSimpleType));
        }
    }

    #region Overrides of CreateUpdatePortfolioWindowViewModel

    protected override void ExecuteAction()
    {
        // Create portfolio
        // ...
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