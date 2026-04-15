using InvestLens.Model.NavigationTree;
using InvestLens.ViewModel.Events;
using System.Windows.Input;

namespace InvestLens.ViewModel.NavigationTree;

public class NavigationTreeItem : BaseNavigationTreeItem
{
    private readonly IEventAggregator _eventAggregator;
    private string _icon = string.Empty;
    private string _header = string.Empty;

    public NavigationTreeItem(
        BaseNavigationTreeModel model, 
        IEventAggregator eventAggregator,
        List<INavigationTreeItem>? children = null)
    {
        _eventAggregator = eventAggregator;
        Icon = model.Icon;
        Header = model.Title;
        Model = model;

        if (children is not null)
        {
            Children.Clear();
            foreach (var child in children)
            {
                Children.Add(child);
            }
        }

        NavigateCommand = new DelegateCommand<NavigationTreeItem>(OnNavigate);
    }

    public string Icon
    {
        get => _icon;
        set => SetProperty(ref _icon, value);
    }

    public string Header
    {
        get => _header;
        set => SetProperty(ref _header, value);
    }

    public BaseNavigationTreeModel Model { get; set; }

    public ICommand NavigateCommand { get; }

    private void OnNavigate(NavigationTreeItem item)
    {
        _eventAggregator.GetEvent<SelectNavigationItemEvent>().Publish(item.Model);
    }
}