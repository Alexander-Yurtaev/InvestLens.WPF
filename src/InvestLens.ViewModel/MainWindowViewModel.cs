namespace InvestLens.ViewModel;

public class MainWindowViewModel : BindableBase
{
    public MainWindowViewModel(NavigationViewModel navigationVm)
    {
        NavigationVm = navigationVm;
    }

    public NavigationViewModel NavigationVm { get; set; }
}