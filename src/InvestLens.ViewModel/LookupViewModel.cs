using InvestLens.Model.Portfolio;

namespace InvestLens.ViewModel;

public class LookupViewModel(LookupModel model) : BindableBase
{
    private bool _isChecked;

    public string Name => model.Name;

    public string Description => model.Description;

    public bool IsChecked
    {
        get => _isChecked;
        set => SetProperty(ref _isChecked, value);
    }
}