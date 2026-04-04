using Autofac;
using InvestLens.ViewModel;
using InvestLens.ViewModel.Pages;
using InvestLens.ViewModel.Services;

namespace InvestLens.App.Startup
{
    public static class Bootstrapper
    {
        public static IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<ViewModelFactory>().As<IViewModelFactory>().SingleInstance();
            builder.RegisterType<NotificationsManager>().As<INotificationsManager>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserManager>().SingleInstance();
            builder.RegisterType<MetricsManager>().As<IMetricsManager>().SingleInstance();
            builder.RegisterType<ActivityManager>().As<IActivityManager>().SingleInstance();
            builder.RegisterType<PortfolioDynamicsService>().As<IPortfolioDynamicsService>().SingleInstance();
            builder.RegisterType<PortfoliosManager>().As<IPortfoliosManager>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<HeaderViewModel>().As<IHeaderViewModel>();

            //Pages
            builder.RegisterType<DashboardViewModel>().As<IDashboardViewModel>();
            
            builder.RegisterType<PortfoliosViewModel>().As<IPortfoliosViewModel>();
            builder.RegisterType<PortfolioDetailViewModel>().As<IPortfolioDetailViewModel>();
            
            builder.RegisterType<DictionariesViewModel>().As<IDictionariesViewModel>();
            builder.RegisterType<DictionariesMoexViewModel>().As<IDictionariesMoexViewModel>();
            builder.RegisterType<DictionariesMoexSecuritiesViewModel>().As<IDictionariesMoexSecuritiesViewModel>();
            builder.RegisterType<DictionariesMoexBondsViewModel>().As<IDictionariesMoexBondsViewModel>();
            
            builder.RegisterType<DictionariesDohodViewModel>().As<IDictionariesDohodViewModel>();
            builder.RegisterType<DictionariesDohodBondsViewModel>().As<IDictionariesDohodBondsViewModel>();
            
            builder.RegisterType<DownloaderViewModel>().As<IDownloaderViewModel>();
            builder.RegisterType<SchedulerViewModel>().As<ISchedulerViewModel>();
            
            builder.RegisterType<SettingsViewModel>().As<ISettingsViewModel>();
            builder.RegisterType<SettingsCommonViewModel>().As<ISettingsCommonViewModel>();
            builder.RegisterType<SettingsPluginsViewModel>().As<ISettingsPluginsViewModel>();

            return builder.Build();
        }
    }
}
