using InvestLens.Model;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class EditPortfolioWindowViewModel : CreateEditPortfolioWindowViewModel, IEditPortfolioWindowViewModel
{
    private readonly PortfolioDetail _model;

    public EditPortfolioWindowViewModel(PortfolioDetail model, IWindowManager windowManager) : base(windowManager)
    {
        _model = model;
        Title = $"Редактирование: {_model.Title}";
        ActionTitle = $"Редактирование портфеля:  {_model.Title}";
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