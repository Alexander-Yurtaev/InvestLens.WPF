using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class CreatePortfolioWindowViewModel : CreateEditPortfolioWindowViewModel, ICreatePortfolioWindowViewModel
{
    public CreatePortfolioWindowViewModel(IWindowManager windowManager) : base(windowManager)
    {
        Title = "Создание";
        ActionTitle = "Создать портфель";
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