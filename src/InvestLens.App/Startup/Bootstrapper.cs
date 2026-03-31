using Autofac;
using InvestLens.ViewModel;

namespace InvestLens.App.Startup
{
    public static class Bootstrapper
    {
        public static IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<HeaderViewModel>().As<IHeaderViewModel>();

            return builder.Build();
        }
    }
}
