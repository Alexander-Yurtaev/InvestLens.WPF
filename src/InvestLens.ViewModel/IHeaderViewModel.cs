using InvestLens.Model.Menu;
using System.ComponentModel;

namespace InvestLens.ViewModel;

public interface IHeaderViewModel
{
    string Title { get; }
    string Description { get; }
    void SetModel(MenuItemModel model);
    event PropertyChangedEventHandler? PropertyChanged;
}