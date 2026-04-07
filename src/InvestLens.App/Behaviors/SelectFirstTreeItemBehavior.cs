using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace InvestLens.App.Behaviors;

public class SelectFirstTreeItemBehavior : Behavior<TreeView>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        if (AssociatedObject.IsLoaded)
        {
            SelectFirstItem(AssociatedObject);
        }
        else
        {
            AssociatedObject.Loaded += AssociatedObjectOnLoaded;
        }
    }

    private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs e)
    {
        AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
        SelectFirstItem(AssociatedObject);
    }

    private void SelectFirstItem(TreeView treeView)
    {
        if (treeView.Items.Count == 0) return;
        
        var firstItem = treeView.Items[0];
        if (treeView.ItemContainerGenerator.ContainerFromItem(firstItem) is TreeViewItem treeViewItem)
        {
            treeViewItem.IsSelected = true;
        }
    }
}