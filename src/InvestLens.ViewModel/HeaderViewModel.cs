using InvestLens.Model.Menu;

namespace InvestLens.ViewModel;

public class HeaderViewModel : BindableBase, IHeaderViewModel
{
    private MenuItemModel? _model;

    public string Title => _model?.Title ?? string.Empty;

    public string Description => _model?.Description ?? string.Empty;

    public void SetModel(MenuItemModel model)
    {
        _model = model;
        RaisePropertyChanged(nameof(Title));
        RaisePropertyChanged(nameof(Description));
    }
}
