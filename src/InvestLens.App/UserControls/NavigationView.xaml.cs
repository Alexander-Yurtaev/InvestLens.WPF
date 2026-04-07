using InvestLens.ViewModel.Wrappers.Menu;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace InvestLens.App.UserControls
{
    /// <summary>
    /// Interaction logic for NavigationView.xaml
    /// </summary>
    public partial class NavigationView : UserControl
    {
        public NavigationView()
        {
            InitializeComponent();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var vm = ((ToggleButton)sender).DataContext as MenuItemWrapper;
            vm?.NavigateCommand?.Execute(vm);
        }
    }
}
