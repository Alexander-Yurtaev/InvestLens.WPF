using InvestLens.Model.Enums;
using InvestLens.ViewModel.Services;
using System.Runtime.CompilerServices;

namespace InvestLens.ViewModel;

public sealed class UpdatePortfolioWindowViewModel : CreateUpdatePortfolioWindowViewModel, IUpdatePortfolioWindowViewModel, ISupportPortfolioType
{
    public UpdatePortfolioWindowViewModel(
        Model.Portfolio.UpdateModel model,
        IWindowManager windowManager,
        IPortfoliosManager portfoliosManager) : base(model, windowManager, portfoliosManager)
    {
        Name = $"Редактирование: {Model.Name}";
        ActionTitle = $"Редактирование портфеля:  {Model.Name}";
        InvalidateCommands();
    }

    public bool IsPortfolioSimpleType => Model.PortfolioType == PortfolioType.Invest;

    public bool IsPortfolioComplexType => Model.PortfolioType == PortfolioType.Complex;

    #region Overrides of CreateUpdatePortfolioWindowViewModel

    protected override void ExecuteAction()
    {
        // Save changes
        // ...
        WindowManager.CloseWindow<UpdatePortfolioWindowViewModel>();
    }

    protected override void OnClose()
    {
        WindowManager.CloseWindow<UpdatePortfolioWindowViewModel>();
    }

    #endregion

    #region Overrides of ValidationViewModelBase

    protected override bool Validate()
    {
        var isValid = base.Validate();
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