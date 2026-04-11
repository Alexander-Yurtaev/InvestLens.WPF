using Autofac;
using InvestLens.App.Services;
using InvestLens.App.Windows;
using InvestLens.Model;
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
            builder.RegisterType<MetricsManager>().As<IMetricsManager>().SingleInstance();
            builder.RegisterType<ActivityManager>().As<IActivityManager>().SingleInstance();
            builder.RegisterType<PortfolioDynamicsService>().As<IPortfolioDynamicsService>().SingleInstance();
            builder.RegisterType<PortfoliosManager>().As<IPortfoliosManager>().SingleInstance();
            builder.RegisterType<DictionariesMoexManager>().As<IDictionariesMoexManager>().SingleInstance();
            builder.RegisterType<DictionariesManager>().As<IDictionariesManager>().SingleInstance();
            builder.RegisterType<MoexService>().As<IMoexService>().SingleInstance();
            builder.RegisterType<DohodService>().As<IDohodService>().SingleInstance();
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
            
            builder.RegisterType<SecurityService>().As<ISecurityService>();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();

            builder.RegisterType<RegistrationWindow>().AsSelf();
            builder.RegisterType<RegistrationWindowViewModel>().As<IRegistrationWindowViewModel>();
            builder.RegisterType<RegistrationModel>().AsSelf();

            builder.RegisterType<LoginWindow>().AsSelf();
            builder.RegisterType<LoginWindowViewModel>().As<ILoginWindowViewModel>();
            builder.RegisterType<LoginModel>().AsSelf();

            builder.RegisterType<Model.Portfolio.CreateModel>().AsSelf();
            builder.RegisterType<Model.Portfolio.UpdateModel>().AsSelf();

            builder.RegisterType<CreatePortfolioWindow>().AsSelf();
            builder.RegisterType<CreatePortfolioWindowViewModel>().As<ICreatePortfolioWindowViewModel>();

            builder.RegisterType<UpdatePortfolioWindow>().AsSelf();
            builder.RegisterType<UpdatePortfolioWindowViewModel>().As<IUpdatePortfolioWindowViewModel>();

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
