namespace InvestLens.ViewModel;

public class HeaderViewModel : BindableBase, IHeaderViewModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
