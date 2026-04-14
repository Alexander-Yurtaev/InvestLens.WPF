using InvestLens.ViewModel;
using InvestLens.ViewModel.NavigationTree;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace InvestLens.App.Views
{
    /// <summary>
    /// Interaction logic for NavigationView.xaml
    /// </summary>
    public partial class NavigationView : UserControl
    {
        public NavigationView()
        {
            this.Loaded += OnLoaded;
            InitializeComponent();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is NavigationViewModel viewModel)
            {
                await viewModel.LoadAsync();
            }
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var vm = ((ToggleButton)sender).DataContext as NavigationTreeItem;
            vm?.NavigateCommand?.Execute(vm);
        }
    }
}
