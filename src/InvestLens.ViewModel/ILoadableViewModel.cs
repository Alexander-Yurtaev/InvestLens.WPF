namespace InvestLens.ViewModel;

public interface ILoadableViewModel
{
    Task Load(bool? force=false);
}