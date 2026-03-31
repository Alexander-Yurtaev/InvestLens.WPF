using Autofac;
using InvestLens.ViewModel;

namespace InvestLens.App.Startup
{
    public class Bootstrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();

            return builder.Build();
        }
    }
}
