using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public class PortfoliosManager : IPortfoliosManager
{
    public PortfoliosManager()
    {
        PortfolioInfos = [];
        LoadPortfolioInfos();
    }

    public List<PortfolioInfo> PortfolioInfos { get; set; }

    private void LoadPortfolioInfos()
    {
        var result = new List<PortfolioInfo>
        {
            new PortfolioInfo("Составной инвестиционный", PortfolioType.Primary, "сегодня", 
            [
                new PortfolioStats("Стоимость", 84320, "$", false),
                new PortfolioStats("Доходность", 15.2, "%"),
                new PortfolioStats("Активов", 12)
            ]),
            new PortfolioInfo("Портфель №1", PortfolioType.Dividend, "вчера",
            [
                new PortfolioStats("Стоимость", 24150, "$", false),
                new PortfolioStats("Доходность", -8.4, "%"),
                new PortfolioStats("Активов", 8)
            ]),
            new PortfolioInfo("Портфель №2", PortfolioType.Agressive, "2 дня назад",
            [
                new PortfolioStats("Стоимость", 16062, "$", false),
                new PortfolioStats("Доходность", 22.1, "%"),
                new PortfolioStats("Активов", 6)
            ]),
        };

        PortfolioInfos.Clear();
        PortfolioInfos.AddRange(result);
    }
}