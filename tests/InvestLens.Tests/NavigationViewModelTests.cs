using InvestLens.Model.Menu;
using InvestLens.ViewModel;

namespace InvestLens.Tests
{
    public class NavigationViewModelTests
    {
        [Fact]
        public void MenuItemsShouldBeInitializedCorrect()
        {
            var navigationVm = new NavigationViewModel();
            var vm = new MainWindowViewModel(navigationVm);

            Assert.NotNull(vm.NavigationVm.MenuItems);
            Assert.Equal(10, vm.NavigationVm.MenuItems.Count);

            var dashboard = vm.NavigationVm.MenuItems[0] as MenuNode;
            Assert.NotNull(dashboard);
            Assert.Equal("Главная", dashboard.Header);
            Assert.NotNull(dashboard.Children);
            Assert.Empty(dashboard.Children);

            var divider = vm.NavigationVm.MenuItems[1] as MenuDivider;
            Assert.NotNull(divider);

            var portfolios = vm.NavigationVm.MenuItems[2] as MenuNode;
            Assert.NotNull(portfolios);
            Assert.Equal("Портфели", portfolios.Header);
            Assert.NotNull(portfolios.Children);
            Assert.Equal(3, portfolios.Children.Count);

            var dictionaries = vm.NavigationVm.MenuItems[3] as MenuNode;
            Assert.NotNull(dictionaries);
            Assert.Equal("Справочники", dictionaries.Header);
            Assert.NotNull(dictionaries.Children);
            Assert.Equal(2, dictionaries.Children.Count);

            var moex = dictionaries.Children[0];
            Assert.NotNull(moex);
            Assert.Equal("MOEX", moex.Header);
            Assert.NotNull(moex.Children);
            Assert.Equal(2, moex.Children.Count);

            var dohod = dictionaries.Children[1];
            Assert.NotNull(dohod);
            Assert.Equal("Dohod.ru", dohod.Header);
            Assert.NotNull(dohod.Children);
            Assert.Single(dohod.Children);

            divider = vm.NavigationVm.MenuItems[4] as MenuDivider;
            Assert.NotNull(divider);

            var downloader = vm.NavigationVm.MenuItems[5] as MenuNode;
            Assert.NotNull(downloader);
            Assert.Equal("Менеджер закачек", downloader.Header);
            Assert.NotNull(downloader.Children);
            Assert.Empty(downloader.Children);

            divider = vm.NavigationVm.MenuItems[6] as MenuDivider;
            Assert.NotNull(divider);

            var scheduler = vm.NavigationVm.MenuItems[7] as MenuNode;
            Assert.NotNull(scheduler);
            Assert.Equal("Планировщик", scheduler.Header);
            Assert.NotNull(scheduler.Children);
            Assert.Empty(scheduler.Children);

            divider = vm.NavigationVm.MenuItems[8] as MenuDivider;
            Assert.NotNull(divider);

            var settings = vm.NavigationVm.MenuItems[9] as MenuNode;
            Assert.NotNull(settings);
            Assert.Equal("Настройки", settings.Header);
            Assert.NotNull(settings.Children);
            Assert.Equal(2, settings.Children.Count);
        }
    }
}
