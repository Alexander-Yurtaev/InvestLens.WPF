using InvestLens.Model;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.Tests;

public class PortfolioStatsWrapperTests
{
    [Theory]
    [InlineData(84320, "$", false, "$84,320")]
    [InlineData(15.2, "%", true, "+15.2%")]
    [InlineData(-15.2, "%", true, "-15.2%")]
    [InlineData(12, "", false, "12")]
    public void ValueDisplayShouldReturnCorrectFormat(double value, string unit, bool suffix, string expected)
    {
        var model = new PortfolioStats("", value, unit, suffix);
        var vm = new PortfolioStatsWrapper(model);

        Assert.Equal(expected, vm.ValueDisplay);
    }
}
