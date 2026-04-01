using System.Windows;
using Autofac;
using InvestLens.App.Startup;
using InvestLens.ViewModel.Services;

namespace InvestLens.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Error: {e.Exception.Message}\n{e.Exception.StackTrace}",
                "Critical Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            e.Handled = false;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            MessageBox.Show($"Unhandled exception: {exception?.Message}\n{exception?.StackTrace}");
        }

        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            MessageBox.Show($"Error in Task: {e.Exception.Message}\n{e.Exception.StackTrace}");
            e.SetObserved();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = Bootstrapper.BootStrap();

            var userManager = container.Resolve<IUserManager>();
            var userTask = userManager.LoadAsync();
            userTask.Await();

            var mainWindow = container.Resolve<MainWindow>();

            mainWindow.Show();
        }
    }
}
