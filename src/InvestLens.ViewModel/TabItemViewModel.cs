using InvestLens.Model;

namespace InvestLens.ViewModel;

public class TabItemViewModel : BindableBase
{
    private string _header;
    private List<Security> _content;

    public TabItemViewModel(string header)
    {
        Header = header;
    }

    public string Header
    {
        get => _header;
        set => SetProperty(ref _header, value);
    }

    public List<Security> Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }
}