using InvestLens.Model.Enums;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public sealed class UpdatePortfolioWindowViewModel : CreateUpdatePortfolioWindowViewModel, IUpdatePortfolioWindowViewModel
{
    public UpdatePortfolioWindowViewModel(
        Model.Portfolio.UpdateModel model,
        IWindowManager windowManager,
        IPortfoliosManager portfoliosManager) : base(model, windowManager, portfoliosManager)
    {
        Title = $"Редактирование: {Model.Title}";
        ActionTitle = $"Редактирование портфеля:  {Model.Title}";
        InvalidateCommands();
    }

    public bool IsPortfolioSimpleType => Model.PortfolioType == PortfolioType.Invest;

    public bool IsPortfolioComplexType => Model.PortfolioType == PortfolioType.Complex;

    #region Overrides of CreateUpdatePortfolioWindowViewModel

    protected override void OnClose()
    {
        WindowManager.CloseWindow<UpdatePortfolioWindowViewModel>();
    }

    protected override void OnAction()
    {
        // Save changes
        // ...
        WindowManager.CloseWindow<UpdatePortfolioWindowViewModel>();
    }

    #endregion
}