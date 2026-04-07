using InvestLens.Model.Enums;
using InvestLens.Model.Menu;
using InvestLens.ViewModel;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers.Menu;
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
            var userManagerMock = new Mock<IUserManager>();
            var notificationsManagerMock = new Mock<INotificationsManager>();
            
            var portfoliosManager = new Mock<IPortfoliosManager>();
            portfoliosManager.Setup(pm => pm.GetPortfoliosMenuItems()).Returns(GetPortfoliosMenuItems());

            var dohodService = new Mock<IDohodService>();
            dohodService.Setup(pm => pm.GetDohodBondsMenuItems()).Returns(GetDohodBondsMenuItems);

            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock.Setup(ea => ea.GetEvent<SelectMenuNodeEvent>()).Returns(new SelectMenuNodeEvent());
            var navigationVm = new NavigationViewModel(portfoliosManager.Object, dohodService.Object, eventAggregatorMock.Object);

            var vm = new MainWindowViewModel(navigationVm, 
                headerVmMock.Object, 
                userManagerMock.Object,
                notificationsManagerMock.Object,
                viewModelFactoryMock.Object, 
                eventAggregatorMock.Object);

            Assert.NotNull(vm.NavigationVm.MenuItems);
            Assert.Equal(10, vm.NavigationVm.MenuItems.Count);

            var dashboard = vm.NavigationVm.MenuItems[0] as MenuItemWrapper;
            Assert.NotNull(dashboard);
            Assert.NotNull(dashboard.Model);
            Assert.Equal("Главная", dashboard.Model.Header);
            Assert.NotNull(dashboard.Children);
            Assert.Empty(dashboard.Children);

            var divider = vm.NavigationVm.MenuItems[1] as IMenuNode;
            Assert.NotNull(divider);
            
            var portfolios = vm.NavigationVm.MenuItems[2] as MenuItemWrapper;
            Assert.NotNull(portfolios);
            Assert.NotNull(portfolios.Model);
            Assert.Equal("Портфели", portfolios.Model.Header);
            Assert.NotNull(portfolios.Children);
            Assert.Equal(3, portfolios.Children.Count);

            var dictionaries = vm.NavigationVm.MenuItems[3] as MenuItemWrapper;
            Assert.NotNull(dictionaries);
            Assert.NotNull(dictionaries.Model);
            Assert.Equal("Справочники", dictionaries.Model.Header);
            Assert.NotNull(dictionaries.Children);
            Assert.Equal(2, dictionaries.Children.Count);

            var moex = dictionaries.Children[0];
            Assert.NotNull(moex);
            Assert.NotNull(moex.Model);
            Assert.Equal("MOEX", moex.Model.Header);
            Assert.NotNull(moex.Children);
            Assert.Equal(2, moex.Children.Count);

            var dohod = dictionaries.Children[1];
            Assert.NotNull(dohod);
            Assert.NotNull(dohod.Model);
            Assert.Equal("Dohod.ru", dohod.Model.Header);
            Assert.NotNull(dohod.Children);
            Assert.Equal(3, dohod.Children.Count);

            divider = vm.NavigationVm.MenuItems[4] as IMenuNode;
            Assert.NotNull(divider);

            var downloader = vm.NavigationVm.MenuItems[5] as MenuItemWrapper;
            Assert.NotNull(downloader);
            Assert.NotNull(downloader.Model);
            Assert.Equal("Менеджер закачек", downloader.Model.Header);
            Assert.NotNull(downloader.Children);
            Assert.Empty(downloader.Children);

            divider = vm.NavigationVm.MenuItems[6] as IMenuNode;
            Assert.NotNull(divider);

            var scheduler = vm.NavigationVm.MenuItems[7] as MenuItemWrapper;
            Assert.NotNull(scheduler);
            Assert.NotNull(scheduler.Model);
            Assert.Equal("Планировщик", scheduler.Model.Header);
            Assert.NotNull(scheduler.Children);
            Assert.Empty(scheduler.Children);

            divider = vm.NavigationVm.MenuItems[8] as IMenuNode;
            Assert.NotNull(divider);

            var settings = vm.NavigationVm.MenuItems[9] as MenuItemWrapper;
            Assert.NotNull(settings);
            Assert.NotNull(settings.Model);
            Assert.Equal("Настройки", settings.Model.Header);
            Assert.NotNull(settings.Children);
            Assert.Equal(2, settings.Children.Count);
        }

        private List<MenuItemModel> GetPortfoliosMenuItems()
        {
            var result = new List<MenuItemModel>
            {
                new MenuItemModel(NodeType.PortfoliosComplex, "📊", "Составной инвестиционный"),
                new MenuItemModel(NodeType.PortfoliosFirst, "💰", "Портфель №1"),
                new MenuItemModel(NodeType.PortfoliosSecond, "💎", "Портфель №2")
            };

            return result;
        }

        private List<MenuItemModel> GetDohodBondsMenuItems()
        {
            var result = new List<MenuItemModel>
            {
                new MenuItemModel(NodeType.DictionariesDohodBondsAAA, "", "AAA"),
                new MenuItemModel(NodeType.DictionariesDohodBondsAA, "", "AA"),
                new MenuItemModel(NodeType.DictionariesDohodBondsAplus, "", "A+")
            };

            return result;
        }
    }
}
