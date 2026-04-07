using InvestLens.Model.Enums;
using InvestLens.Model.Menu;
using InvestLens.ViewModel.Events;
using System.Windows.Input;

namespace InvestLens.ViewModel.Wrappers.Menu;

public class MenuItemWrapper : BindableBase, IMenuNode
{
    private readonly IEventAggregator _eventAggregator;

    public MenuItemWrapper(MenuItemModel model, IEventAggregator eventAggregator)
    {
        Model = model;
        _eventAggregator = eventAggregator;

        Children.AddRange(Model.Children.Select(c => new MenuItemWrapper(c, _eventAggregator)));
        NavigateCommand = new DelegateCommand<MenuItemWrapper>(OnNavigate);
    }

    public MenuItemModel Model { get; }
    public List<MenuItemWrapper> Children { get; } = [];
    public NodeType NodeType => Model.NodeType;
    public string Icon => Model.Icon;
    public string Header => Model.Header;
    public string Title => Model.Title;
    public string Description => Model.Description;

    public ICommand NavigateCommand { get; }

    private void OnNavigate(MenuItemWrapper node)
    {
        _eventAggregator.GetEvent<SelectMenuNodeEvent>().Publish(node.Model);
    }
}