using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class CreatePortfolioWindowViewModel : CreateEditPortfolioWindowViewModel, ICreatePortfolioWindowViewModel
{
    public CreatePortfolioWindowViewModel(IWindowManager windowManager) : base(windowManager)
    {
        Title = "Создание";
    }

    #region Overrides of CreateEditPortfolioWindowViewModel

    protected override void OnClose()
    {
        WindowManager.CloseWindow<CreatePortfolioWindowViewModel>();
    }

    #endregion
}