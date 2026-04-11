using InvestLens.Model;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public sealed class CreatePortfolioWindowViewModel : CreateEditPortfolioWindowViewModel, ICreatePortfolioWindowViewModel
{
    public CreatePortfolioWindowViewModel(
        CreateEditPortfolioModel model, 
        IWindowManager windowManager, 
        IPortfoliosManager portfoliosManager) : base(model, windowManager, portfoliosManager)
    {
        Title = "Создание";
        ActionTitle = "Создать портфель";
        InvalidateCommands();
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