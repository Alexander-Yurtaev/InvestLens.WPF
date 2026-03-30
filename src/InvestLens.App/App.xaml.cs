using InvestLens.ViewModel;
using System.Windows;

namespace InvestLens.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var navigationVm = new NavigationViewModel();
            var mainWindowViewModel = new MainWindowViewModel(navigationVm);

            var mainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };

            mainWindow.Show();
        }
    }

}
