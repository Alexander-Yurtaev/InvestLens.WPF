using System.ComponentModel;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public interface ICreateEditPortfolioWindowViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
{
    string Title { get; set; }
    ICommand CloseCommand { get; set; }
}