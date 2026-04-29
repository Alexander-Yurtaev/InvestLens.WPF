using InvestLens.Model.Enums;

namespace InvestLens.ViewModel;

public class TabItemViewModel : BindableBase
{
    private string _header = string.Empty;
    private List<object> _content = [];

    public TabItemViewModel(string header)
    {
        Header = header;
    }

    public string Header
    {
        get => _header;
        set => SetProperty(ref _header, value);
    }

    public List<object> Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }
}