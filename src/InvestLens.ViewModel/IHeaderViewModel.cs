using System.ComponentModel;

namespace InvestLens.ViewModel;

public interface IHeaderViewModel : INotifyPropertyChanged
{
    string Title { get; }
    string Description { get; }
}