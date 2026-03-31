namespace InvestLens.ViewModel;

public class HeaderViewModel : BindableBase, IHeaderViewModel
{
    private string _title = string.Empty;
    private string _description = string.Empty;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }
}
