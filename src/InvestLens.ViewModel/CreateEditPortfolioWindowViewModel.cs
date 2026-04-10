using System.Collections;
using System.ComponentModel;

namespace InvestLens.ViewModel;

public interface ICreateEditPortfolioWindowViewModel
{
    string Title { get; set; }
    bool HasErrors { get; }
    event PropertyChangedEventHandler? PropertyChanged;
    IEnumerable GetErrors(string? propertyName);
    event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}

public class CreateEditPortfolioWindowViewModel : ValidationViewModelBase, ICreateEditPortfolioWindowViewModel
{
    public string Title { get; set; } = string.Empty;
}