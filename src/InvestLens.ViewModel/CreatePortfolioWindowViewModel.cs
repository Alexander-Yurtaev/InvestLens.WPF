using InvestLens.Model.Enums;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public sealed class CreatePortfolioWindowViewModel : CreateUpdatePortfolioWindowViewModel, ICreatePortfolioWindowViewModel
{
    public CreatePortfolioWindowViewModel(
        Model.Portfolio.CreateModel model, 
        IWindowManager windowManager, 
        IPortfoliosManager portfoliosManager) : base(model, windowManager, portfoliosManager)
    {
        Title = "Создание";
        ActionTitle = "Создать портфель";
        InvalidateCommands();
    }

    public bool IsPortfolioSimpleType
    {
        get => Model.PortfolioType == PortfolioType.Invest;
        set
        {
            ((Model.Portfolio.CreateModel)Model).SetPortfolioType(value ? PortfolioType.Invest : PortfolioType.Complex);

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

            RaisePropertyChanged();
            RaisePropertyChanged(nameof(IsPortfolioSimpleType));
        }
    }

    #region Overrides of CreateUpdatePortfolioWindowViewModel

    protected override void OnClose()
    {
        WindowManager.CloseWindow<CreatePortfolioWindowViewModel>();
    }

    protected override void OnAction()
    {
        // Create portfolio
        // ...
        WindowManager.CloseWindow<CreatePortfolioWindowViewModel>();
    }

    #endregion
}