namespace InvestLens.ViewModel.Pages;

public abstract class BaseViewModel : BindableBase, IBaseViewModel
{
    protected BaseViewModel(string title, string description="", List<ButtonModel>? buttons=null)
    {
        ContentHeaderVm = new ContentHeaderViewModel(title, description, buttons);
    }

    public IContentHeaderViewModel ContentHeaderVm { get; private set; }
}