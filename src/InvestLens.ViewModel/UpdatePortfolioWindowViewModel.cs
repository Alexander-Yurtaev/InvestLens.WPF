using InvestLens.DataAccess.Repositories;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;
using System.Runtime.CompilerServices;

namespace InvestLens.ViewModel;

public sealed class UpdatePortfolioWindowViewModel : CreateUpdatePortfolioWindowViewModel, IUpdatePortfolioWindowViewModel, ISupportPortfolioType
{
    public UpdatePortfolioWindowViewModel(
        UpdateModel model,
        IWindowManager windowManager,
        IAuthManager authManager,
        IPortfoliosManager portfoliosManager) : base(model, windowManager, authManager, portfoliosManager)
    {
        Header = "Редактирование";
        ActionTitle = "Сохранить";

        InvalidateCommands();
    }

    public bool IsPortfolioSimpleType => Model.PortfolioType == PortfolioType.Invest;

    public bool IsPortfolioComplexType => Model.PortfolioType == PortfolioType.Complex;

    #region Overrides of CreateUpdatePortfolioWindowViewModel

    public override async Task Load(bool? force = false)
    {
        await base.Load(force);

        foreach (var id in Model.Portfolios)
        {
            var lm = LookupModels.FirstOrDefault(lm => lm.Id == id);
            if (lm is null) continue;
            lm.IsChecked = true;
        }
    }

    protected override async Task ExecuteAction()
    {
        var updateModel = (UpdateModel)Model;

        updateModel.Portfolios.Clear();
        updateModel.Portfolios.AddRange(LookupModels.Where(lm => lm.IsChecked).Select(lm => lm.Id));

        await PortfoliosManager.Update(updateModel);
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