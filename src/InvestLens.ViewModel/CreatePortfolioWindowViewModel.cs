using InvestLens.Model;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class CreatePortfolioWindowViewModel : CreateEditPortfolioWindowViewModel, ICreatePortfolioWindowViewModel
{
    public CreatePortfolioWindowViewModel(CreateEditPortfolioModel model, IWindowManager windowManager) : base(model, windowManager)
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