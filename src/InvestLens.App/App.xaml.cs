using InvestLens.App.Services;
using InvestLens.App.Windows;
using InvestLens.App.Windows.Dialogs;
using InvestLens.DataAccess;
using InvestLens.DataAccess.Repositories;
using InvestLens.DataAccess.Repositories.Settings;
using InvestLens.DataAccess.Services;
using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Crud.User;
using InvestLens.Model.Services;
using InvestLens.ViewModel;
using InvestLens.ViewModel.Factories;
using InvestLens.ViewModel.Pages;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Windows;
using InvestLens.ViewModel.Windows.Dialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace InvestLens.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

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

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            ApplyMigrations();

            var provider = ServiceProvider.GetRequiredService<IMoexProvider>();
            try
            {
                await provider.LoadMoexIndex();
            }
            catch (Exception ex)
            {

                throw;
            }

            var windowManager = ServiceProvider.GetRequiredService<IWindowManager>();
            windowManager.ShowWindow<LoginWindowViewModel>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(_ => { }, typeof(App).Assembly);
            RegisterDataContext(services);

            services.AddHttpClient("moex", client =>
            {
                client.BaseAddress = new Uri("https://iss.moex.com/");
                client.Timeout = TimeSpan.FromSeconds(10);
            });

            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            services.AddSingleton<INotificationsManager, NotificationsManager>();
            services.AddSingleton<IActivityManager, ActivityManager>();
            services.AddSingleton<IPortfoliosManager, PortfoliosManager>();
            services.AddSingleton<IDictionariesMoexManager, DictionariesMoexManager>();
            services.AddSingleton<IDictionariesManager, DictionariesManager>();
            services.AddSingleton<IMetricsService, MetricsService>();
            services.AddSingleton<IDohodService, DohodService>();
            services.AddSingleton<IMoexProvider, MoexProvider>();

            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<IAuthManager, AuthManager>();

            services.AddScoped<ICreatePortfolioViewModelFactory, CreatePortfolioViewModelFactory>();
            services.AddScoped<ICreateDictionariesDohodBondsViewModelFactory, CreateDictionariesDohodBondsViewModelFactory>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISecurityService, SecurityService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<ISecurityRepository, SecurityRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IEngineRepository, EngineRepository>();
            services.AddScoped<IMarketRepository, MarketRepository>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<IBoardGroupRepository, BoardGroupRepository>();
            services.AddScoped<IDurationRepository, DurationRepository>();
            services.AddScoped<ISecurityTypeRepository, SecurityTypeRepository>();
            services.AddScoped<ISecurityGroupRepository, SecurityGroupRepository>();
            services.AddScoped<ISecurityCollectionRepository, SecurityCollectionRepository>();

            services.AddScoped<MainWindow>();
            services.AddScoped<IMainWindowViewModel, MainWindowViewModel>();

            services.AddScoped<ConfirmDeleteDialog>();
            services.AddScoped<IConfirmDeleteDialogViewModel, ConfirmDeleteDialogViewModel>();

            services.AddScoped<ModalDialog>();
            services.AddScoped<IConfirmDialogViewModel, ConfirmDialogViewModel>();
            services.AddScoped<IErrorDialogViewModel, ErrorDialogViewModel>();
            services.AddScoped<IInformationDialogViewModel, InformationDialogViewModel>();
            services.AddScoped<ISuccessDialogViewModel, SuccessDialogViewModel>();
            services.AddScoped<IWarningDialogViewModel, WarningDialogViewModel>();

            services.AddScoped<PortfolioImportDialog>();
            services.AddScoped<IPortfolioImportDialogViewModel, PortfolioImportDialogViewModel>();

            services.AddScoped<RegistrationWindow>();
            services.AddScoped<IRegistrationWindowViewModel, RegistrationWindowViewModel>();
            services.AddScoped<RegistrationModel>();

            services.AddScoped<LoginWindow>();
            services.AddScoped<ILoginWindowViewModel, LoginWindowViewModel>();
            services.AddScoped<LoginModel>();

            services.AddTransient(sp =>
            {
                var authManager = sp.GetRequiredService<IAuthManager>();
                if (authManager.CurrentUser is null)
                {
                    throw new InvalidOperationException("Пользователь не авторизован!");
                }

                var model = new Model.Crud.Portfolio.CreateModel(authManager.CurrentUser!.Id);
                return model;
            });
            services.AddScoped<UpdateModel>();

            services.AddScoped<CreatePortfolioWindow>();
            services.AddScoped<ICreatePortfolioWindowViewModel, CreatePortfolioWindowViewModel>();

            services.AddScoped<UpdatePortfolioWindow>();
            services.AddScoped<IUpdatePortfolioWindowViewModel, UpdatePortfolioWindowViewModel>();

            services.AddScoped<INavigationViewModel, NavigationViewModel>();
            services.AddScoped<IHeaderViewModel, HeaderViewModel>();
            services.AddScoped<IPortfolioDynamicsViewModel, PortfolioDynamicsViewModel>();

            //Pages
            services.AddScoped<IDashboardViewModel, DashboardViewModel>();

            services.AddScoped<IPortfoliosViewModel, PortfoliosViewModel>();
            services.AddScoped<IPortfolioDetailViewModel, PortfolioDetailViewModel>();

            services.AddScoped<IDictionariesViewModel, DictionariesViewModel>();
            services.AddScoped<IDictionariesMoexViewModel, DictionariesMoexViewModel>();
            services.AddScoped<IDictionariesMoexSecuritiesViewModel, DictionariesMoexSecuritiesViewModel>();
            services.AddScoped<IDictionariesMoexBondsViewModel, DictionariesMoexBondsViewModel>();

            services.AddScoped<IDictionariesDohodViewModel, DictionariesDohodViewModel>();
            services.AddScoped<IDictionariesDohodBondsViewModel, DictionariesDohodBondsViewModel>();

            services.AddScoped<IDownloaderViewModel, DownloaderViewModel>();
            services.AddScoped<ISchedulerViewModel, SchedulerViewModel>();

            services.AddScoped<ISettingsViewModel, SettingsViewModel>();
            services.AddScoped<ISettingsCommonViewModel, SettingsCommonViewModel>();
            services.AddScoped<ISettingsPluginsViewModel, SettingsPluginsViewModel>();
        }

        private IServiceCollection RegisterDataContext(IServiceCollection services)
        {
            services.AddScoped<InvestLensDataContextFactory>();
            services.AddScoped(sp =>
            {
                var factory = sp.GetRequiredService<InvestLensDataContextFactory>();
                return factory.CreateDbContext([]);
            });

            return services;
        }

        private void ApplyMigrations()
        {
            try
            {
                var context = ServiceProvider.GetRequiredService<InvestLensDataContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
