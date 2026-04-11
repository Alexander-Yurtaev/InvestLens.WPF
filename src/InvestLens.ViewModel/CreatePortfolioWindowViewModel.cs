using InvestLens.Model.Enums;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public sealed class CreatePortfolioWindowViewModel : CreateEditPortfolioWindowViewModel, ICreatePortfolioWindowViewModel
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
            if (value)
            {
                ((Model.Portfolio.CreateModel)Model).SetPortfolioType(PortfolioType.Invest);
            }
            else
            {
                ((Model.Portfolio.CreateModel)Model).SetPortfolioType(PortfolioType.Complex);
            }

            RaisePropertyChanged();
            RaisePropertyChanged(nameof(IsPortfolioComplexType));
        }
    }

    public bool IsPortfolioComplexType
    {
        get => Model.PortfolioType == PortfolioType.Complex;
        set
        {
            if (value)
            {
                ((Model.Portfolio.CreateModel)Model).SetPortfolioType(PortfolioType.Complex);
            }
            else
            {
                ((Model.Portfolio.CreateModel)Model).SetPortfolioType(PortfolioType.Invest);
            }

            RaisePropertyChanged();
            RaisePropertyChanged(nameof(IsPortfolioSimpleType));
        }
    }

    #region Overrides of CreateEditPortfolioWindowViewModel

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