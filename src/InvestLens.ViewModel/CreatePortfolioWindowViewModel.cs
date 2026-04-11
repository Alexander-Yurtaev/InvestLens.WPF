using InvestLens.Model.Enums;
using InvestLens.ViewModel.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InvestLens.ViewModel;

public sealed class CreatePortfolioWindowViewModel : CreateUpdatePortfolioWindowViewModel, ICreatePortfolioWindowViewModel, IDisposable
{
    public CreatePortfolioWindowViewModel(
        Model.Portfolio.CreateModel model, 
        IWindowManager windowManager, 
        IPortfoliosManager portfoliosManager) : base(model, windowManager, portfoliosManager)
    {
        Header = "Создание";
        ActionTitle = "Создать портфель";

        foreach (var item in LookupModels)
        {
            item.PropertyChanged += ItemOnPropertyChanged;
        }

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

    private void ItemOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ValidateProperty(LookupModels, nameof(LookupModels));
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

    #endregion

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