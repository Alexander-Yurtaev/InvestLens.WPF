using InvestLens.Model.Enums;
using InvestLens.Model.NavigationTree;
using InvestLens.ViewModel;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.NavigationTree;
using InvestLens.ViewModel.Services;
using Moq;

namespace InvestLens.Tests
{
    public class NavigationViewModelTests
    {
        [Fact]
        public void MenuItemsShouldBeInitializedCorrect()
        {
            var headerVmMock = new Mock<IHeaderViewModel>();
            var viewModelFactoryMock = new Mock<IViewModelFactory>();

            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock.Setup(ea => ea.GetEvent<LoginEvent>()).Returns(new LoginEvent());
            eventAggregatorMock.Setup(ea => ea.GetEvent<SelectNavigationItemEvent>()).Returns(new SelectNavigationItemEvent());
            eventAggregatorMock.Setup(ea => ea.GetEvent<PortfolioCreatedEvent>()).Returns(new PortfolioCreatedEvent());

            var portfoliosManagerMock = new Mock<IPortfoliosManager>();
            portfoliosManagerMock.Setup(pm => pm.GetPortfoliosMenuItems(It.IsAny<int>())).ReturnsAsync(GetPortfoliosMenuItems(eventAggregatorMock.Object));

            var dohodServiceMock = new Mock<IDohodService>();
            dohodServiceMock.Setup(pm => pm.GetDohodBondsMenuItems()).Returns(GetDohodBondsMenuItems(eventAggregatorMock.Object));

            var authManagerMock = new Mock<IAuthManager>();

            var navigationVm = new NavigationViewModel(authManagerMock.Object, portfoliosManagerMock.Object, dohodServiceMock.Object, eventAggregatorMock.Object);

            var vm = new MainWindowViewModel(
                navigationVm, 
                headerVmMock.Object, 
                viewModelFactoryMock.Object, 
                eventAggregatorMock.Object);

            Assert.NotNull(vm.NavigationVm.MenuItems);
            Assert.Equal(10, vm.NavigationVm.MenuItems.Count);

            var dashboard = vm.NavigationVm.MenuItems[0] as NavigationTreeItem;
            Assert.NotNull(dashboard);
            var dashboardModel = dashboard.Model as DashboardNavigationTreeModel;
            Assert.NotNull(dashboardModel);
            Assert.Equal("Главная", dashboardModel.Header);
            Assert.NotNull(dashboard.Children);
            Assert.Empty(dashboard.Children);

            var divider = vm.NavigationVm.MenuItems[1] as NavigationTreeDivider;
            Assert.NotNull(divider);
            
            var portfolios = vm.NavigationVm.MenuItems[2] as NavigationTreeItem;
            Assert.NotNull(portfolios);
            var portfoliosModel = portfolios.Model as PortfoliosNavigationTreeModel;
            Assert.NotNull(portfoliosModel);
            Assert.Equal("Портфели", portfoliosModel.Header);
            Assert.NotNull(portfolios.Children);
            Assert.Equal(3, portfolios.Children.Count);

            var dictionaries = vm.NavigationVm.MenuItems[3] as NavigationTreeItem;
            Assert.NotNull(dictionaries);
            var dictionariesModel = dictionaries.Model as DictionariesNavigationTreeModel;
            Assert.NotNull(dictionariesModel);
            Assert.Equal("Справочники", dictionariesModel.Header);
            Assert.NotNull(dictionaries.Children);
            Assert.Equal(2, dictionaries.Children.Count);

            var moex = dictionaries.Children[0] as NavigationTreeItem;
            Assert.NotNull(moex);
            var moexModel = moex.Model as DictionariesMoexNavigationTreeModel;
            Assert.NotNull(moexModel);
            Assert.Equal("MOEX", moexModel.Header);
            Assert.NotNull(moex.Children);
            Assert.Equal(2, moex.Children.Count);

            var dohod = dictionaries.Children[1] as NavigationTreeItem;
            Assert.NotNull(dohod);
            var dohodModel = dohod.Model as DictionariesDohodNavigationTreeModel;
            Assert.NotNull(dohodModel);
            Assert.Equal("Dohod.ru", dohodModel.Header);
            Assert.NotNull(dohod.Children);
            Assert.Equal(3, dohod.Children.Count);

            divider = vm.NavigationVm.MenuItems[4] as NavigationTreeDivider;
            Assert.NotNull(divider);

            var downloader = vm.NavigationVm.MenuItems[5] as NavigationTreeItem;
            Assert.NotNull(downloader);
            var downloaderModel = downloader.Model as DownloaderNavigationTreeModel;
            Assert.NotNull(downloaderModel);
            Assert.Equal("Менеджер закачек", downloaderModel.Header);
            Assert.NotNull(downloader.Children);
            Assert.Empty(downloader.Children);

            divider = vm.NavigationVm.MenuItems[6] as NavigationTreeDivider;
            Assert.NotNull(divider);

            var scheduler = vm.NavigationVm.MenuItems[7] as NavigationTreeItem;
            Assert.NotNull(scheduler);
            var schedulerModel = scheduler.Model as SchedulerNavigationTreeModel;
            Assert.NotNull(schedulerModel);
            Assert.Equal("Планировщик", schedulerModel.Header);
            Assert.NotNull(scheduler.Children);
            Assert.Empty(scheduler.Children);

            divider = vm.NavigationVm.MenuItems[8] as NavigationTreeDivider;
            Assert.NotNull(divider);

            var settings = vm.NavigationVm.MenuItems[9] as NavigationTreeItem;
            Assert.NotNull(settings);
            var settingsModel = settings.Model as SettingsNavigationTreeModel;
            Assert.NotNull(settingsModel);
            Assert.Equal("Настройки", settingsModel.Header);
            Assert.NotNull(settings.Children);
            Assert.Equal(2, settings.Children.Count);
        }

        private List<INavigationTreeItem> GetPortfoliosMenuItems(IEventAggregator eventAggregator)
        {
            var result = new List<INavigationTreeItem>
            {
                new NavigationTreeItem("📊", "Составной", new PortfolioNavigationTreeModel(1, PortfolioType.Invest), eventAggregator),
                new NavigationTreeItem("💰", "Портфель №1", new PortfolioNavigationTreeModel(2, PortfolioType.Invest), eventAggregator),
                new NavigationTreeItem("💎", "Портфель №2", new PortfolioNavigationTreeModel(3, PortfolioType.Invest), eventAggregator)
            };

            return result;
        }

        private List<INavigationTreeItem> GetDohodBondsMenuItems(IEventAggregator eventAggregator)
        {
            var result = new List<INavigationTreeItem>
            {
                new NavigationTreeItem("", "AAA", new DictionariesDohodBondNavigationTreeModel(PeriodType.Short), eventAggregator),
                new NavigationTreeItem("", "AA", new DictionariesDohodBondNavigationTreeModel(PeriodType.Middle), eventAggregator),
                new NavigationTreeItem("", "A+", new DictionariesDohodBondNavigationTreeModel(PeriodType.Long), eventAggregator)
            };

            return result;
        }
    }
}
