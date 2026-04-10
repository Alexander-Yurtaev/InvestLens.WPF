using InvestLens.Model;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class EditPortfolioWindowViewModel : CreateEditPortfolioWindowViewModel, IEditPortfolioWindowViewModel
{
    public EditPortfolioWindowViewModel(CreateEditPortfolioModel model, IWindowManager windowManager) : base(model, windowManager)
    {
        Title = $"Редактирование: {Model.Title}";
        ActionTitle = $"Редактирование портфеля:  {Model.Title}";
    }

    #region Overrides of CreateEditPortfolioWindowViewModel

    protected override void OnClose()
    {
        WindowManager.CloseWindow<EditPortfolioWindowViewModel>();
    }

    protected override void OnAction()
    {
        // Save changes
        // ...
        WindowManager.CloseWindow<EditPortfolioWindowViewModel>();
    }

    #endregion
}