using System.ComponentModel;

namespace InvestLens.ViewModel;

public interface IHeaderViewModel
{
    string Title { get; set; }
    string Description { get; set; }
    event PropertyChangedEventHandler? PropertyChanged;
}