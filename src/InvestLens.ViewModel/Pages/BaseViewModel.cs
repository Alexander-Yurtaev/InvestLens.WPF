namespace InvestLens.ViewModel.Pages;

public abstract class BaseViewModel : BindableBase
{
    protected BaseViewModel(string title, string description)
    {
        ContentHeaderVm = new ContentHeaderViewModel(title, description);
    }

    public IContentHeaderViewModel ContentHeaderVm { get; private set; }
}