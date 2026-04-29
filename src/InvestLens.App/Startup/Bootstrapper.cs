using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using InvestLens.App.Services;
using InvestLens.App.Windows;
using InvestLens.App.Windows.Dialogs;
using InvestLens.DataAccess;
using InvestLens.DataAccess.Repositories;
using InvestLens.DataAccess.Services;
using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Crud.User;
using InvestLens.Model.Services;
using InvestLens.ViewModel;
using InvestLens.ViewModel.Pages;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Windows;
using InvestLens.ViewModel.Windows.Dialogs;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows;

namespace InvestLens.App.Startup
{
    public static class Bootstrapper
    {
        public static IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            RegisterDataContext(builder);
         
            builder.RegisterAutoMapper(typeof(App).Assembly);

            builder.RegisterType<InvestLensDataContextFactory>().AsSelf();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<ViewModelFactory>().As<IViewModelFactory>().SingleInstance();
            builder.RegisterType<NotificationsManager>().As<INotificationsManager>().SingleInstance();
            builder.RegisterType<MetricsService>().As<IMetricsService>().SingleInstance();
            builder.RegisterType<ActivityManager>().As<IActivityManager>().SingleInstance();
            builder.RegisterType<PortfoliosManager>().As<IPortfoliosManager>().SingleInstance();
            builder.RegisterType<DictionariesMoexManager>().As<IDictionariesMoexManager>().SingleInstance();
            builder.RegisterType<DictionariesManager>().As<IDictionariesManager>().SingleInstance();
            builder.RegisterType<MoexService>().As<IMoexService>().SingleInstance();
            builder.RegisterType<DohodService>().As<IDohodService>().SingleInstance();

            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<AuthManager>().As<IAuthManager>().SingleInstance();
            
            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<SecurityService>().As<ISecurityService>();

            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<PortfolioRepository>().As<IPortfolioRepository>();
            builder.RegisterType<SecurityRepository>().As<ISecurityRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();

            builder.RegisterType<ConfirmDeleteDialog>().AsSelf();
            builder.RegisterType<ConfirmDeleteDialogViewModel>().As<IConfirmDeleteDialogViewModel>();

            builder.RegisterType<ModalDialog>().AsSelf();
            builder.RegisterType<ConfirmDialogViewModel>().As<IConfirmDialogViewModel>();
            builder.RegisterType<ErrorDialogViewModel>().As<IErrorDialogViewModel>();
            builder.RegisterType<InformationDialogViewModel>().As<IInformationDialogViewModel>();
            builder.RegisterType<SuccessDialogViewModel>().As<ISuccessDialogViewModel>();
            builder.RegisterType<WarningDialogViewModel>().As<IWarningDialogViewModel>();
            
            builder.RegisterType<PortfolioImportDialog>().AsSelf();
            builder.RegisterType<PortfolioImportDialogViewModel>().As<IPortfolioImportDialogViewModel>();

            builder.RegisterType<RegistrationWindow>().AsSelf();
            builder.RegisterType<RegistrationWindowViewModel>().As<IRegistrationWindowViewModel>();
            builder.RegisterType<RegistrationModel>().AsSelf();

            builder.RegisterType<LoginWindow>().AsSelf();
            builder.RegisterType<LoginWindowViewModel>().As<ILoginWindowViewModel>();
            builder.RegisterType<LoginModel>().AsSelf();

            builder.Register(context => new Model.Crud.Portfolio.CreateModel(context.Resolve<IAuthManager>().CurrentUser!.Id))
                .As<CreateModel>()
                .InstancePerDependency();
            builder.RegisterType<UpdateModel>().AsSelf();

            builder.RegisterType<CreatePortfolioWindow>().AsSelf();
            builder.RegisterType<CreatePortfolioWindowViewModel>().As<ICreatePortfolioWindowViewModel>();

            builder.RegisterType<UpdatePortfolioWindow>().AsSelf();
            builder.RegisterType<UpdatePortfolioWindowViewModel>().As<IUpdatePortfolioWindowViewModel>();

            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<HeaderViewModel>().As<IHeaderViewModel>();
            builder.RegisterType<PortfolioDynamicsViewModel>().As<IPortfolioDynamicsViewModel>();

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

            var container = builder.Build();

            ApplyMigrations(container);

            return container;
        }

        private static void RegisterDataContext(ContainerBuilder builder)
        {
            builder.Register(context => context.Resolve<InvestLensDataContextFactory>().CreateDbContext([]))
                .As<InvestLensDataContext>()
                .InstancePerLifetimeScope();
        }

        private static void ApplyMigrations(IContainer container)
        {
            try
            {
                using var scope = container.BeginLifetimeScope();
                var context = scope.Resolve<InvestLensDataContext>();

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
