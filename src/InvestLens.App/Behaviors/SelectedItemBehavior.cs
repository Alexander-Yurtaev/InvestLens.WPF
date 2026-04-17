using InvestLens.ViewModel.NavigationTree;
using Microsoft.Xaml.Behaviors;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace InvestLens.App.Behaviors;

public class SelectedItemBehavior : Behavior<TreeView>
{
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(INavigationTreeItem), typeof(SelectedItemBehavior), new PropertyMetadata(null, SelectedItemChangedCallback));

    public INavigationTreeItem SelectedItem
    {
        get { return (INavigationTreeItem)GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.SelectedItemChanged += AssociatedObject_SelectedItemChanged;
    }

    private void AssociatedObject_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        
    }

    private static void SelectedItemChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var targetItem = e.NewValue as INavigationTreeItem;
        var behavior = (SelectedItemBehavior)d;

        foreach (INavigationTreeItem item in behavior.AssociatedObject.Items)
        {
            var treeItem = (TreeViewItem)behavior.AssociatedObject.ItemContainerGenerator.ContainerFromItem(item);
            if (item == targetItem)
            {
                treeItem.IsSelected = true;
                break;
            }

            var result = SelecteItem(treeItem, targetItem, item.Children);
            if (result) break;
        }
    }

    private static bool SelecteItem(TreeViewItem parentTreeItem, INavigationTreeItem? targetItem, ObservableCollection<INavigationTreeItem> children)
    {
        foreach (INavigationTreeItem item in children)
        {
            var treeItem = (TreeViewItem)parentTreeItem.ItemContainerGenerator.ContainerFromItem(item);
            if (item == targetItem)
            {
                treeItem.IsSelected = true;
                return true;
            }

            var result = SelecteItem(treeItem, targetItem, item.Children);
            if (result) return true;
        }
        return false;
    }
}