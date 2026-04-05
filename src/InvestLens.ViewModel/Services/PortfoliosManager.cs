using InvestLens.Model;
using InvestLens.Model.Enums;
using InvestLens.Model.Menu;

namespace InvestLens.ViewModel.Services;

public class PortfoliosManager : IPortfoliosManager
{
    private readonly Dictionary<NodeType, PortfolioDetail> _portfolios = new Dictionary<NodeType, PortfolioDetail>
    {
        { NodeType.PortfoliosComplex, new PortfolioDetail("Составной инвестиционный")
            {
                PortfolioStats = {
                    new PortfolioStats("Стоимость", 84320, "$", false),
                    new PortfolioStats("Доходность", 15.2, "%"),
                    new PortfolioStats("Активов", 12)
                },
                Securities =
                {
                    new SecurityInfo("AAPL", "Apple Inc"){Count = 125, AveragePrice = 168.2, CurrentPrice = 175.32, TotalPrice = 21915, Profit = 890}
                },
                Operations =
                {
                    new SecurityOperation("AAPL", SecurityOperationType.Sell){Date = new DateTime(2025, 03, 15), Count = 50, Price = 172.5, TotalPrice = 8625},
                    new SecurityOperation("AAPL", SecurityOperationType.Buy){Date = new DateTime(2025, 02, 15), Count = 75, Price = 165.8, TotalPrice = 12435},
                }
            }
        },
        { NodeType.PortfoliosFirst, new PortfolioDetail("Портфель №1")
        {
            PortfolioStats = {
                new PortfolioStats("Стоимость", 24150, "$", false),
                new PortfolioStats("Доходность", -8.4, "%"),
                new PortfolioStats("Активов", 8)
            },
            Securities =
            {
                new SecurityInfo("MSFT", "Microsoft"){Count = 48, AveragePrice = 398.5, CurrentPrice = 420.75, TotalPrice = 20196, Profit = 1068}
            },
            Operations =
            {
                new SecurityOperation("MSFT", SecurityOperationType.Sell){Date = new DateTime(2025, 03, 10), Count = 20, Price = 405.3, TotalPrice = 8106},
                new SecurityOperation("MSFT", SecurityOperationType.Buy){Date = new DateTime(2025, 02, 10), Count = 28, Price = 393.2, TotalPrice = 11010},
            }
        } },
        { NodeType.PortfoliosSecond, new PortfolioDetail("Портфель №2")
        {
            PortfolioStats = {
                new PortfolioStats("Стоимость", 16062, "$", false),
                new PortfolioStats("Доходность", 22.1, "%"),
                new PortfolioStats("Активов", 6)
            },
            Securities =
            {
                new SecurityInfo("SPY", "S&P 500 ETF"){Count = 32, AveragePrice = 465.1, CurrentPrice = 478.20, TotalPrice = 15302, Profit = 419}
            },
            Operations =
            {
                new SecurityOperation("SPY", SecurityOperationType.Buy){Date = new DateTime(2025, 03, 01), Count = 15, Price = 470.2, TotalPrice = 7053}
            }
        } },
    };

    public PortfoliosManager()
    {
        PortfolioInfos = [];
        LoadPortfolioInfos();
    }

    public List<PortfolioInfo> PortfolioInfos { get; set; }

    public List<MenuNode> GetPortfoliosMenuItems()
    {
        var result = new List<MenuNode>
        {
            new MenuNode(NodeType.PortfoliosComplex, "📊", "Составной инвестиционный"){Title = "Составной инвестиционный", Description = "Детальная информация о портфеле"},
            new MenuNode(NodeType.PortfoliosFirst, "💰", "Портфель №1"){Title = "Портфель №1", Description = "Детальная информация о портфеле"},
            new MenuNode(NodeType.PortfoliosSecond, "💎", "Портфель №2"){Title = "Портфель №2", Description = "Детальная информация о портфеле"}
        };

        return result;
    }

    public PortfolioDetail GetPortfolio(NodeType nodeType)
    {
        return _portfolios[nodeType];
    }

    private void LoadPortfolioInfos()
    {
        var result = _portfolios.Select(p =>
            new PortfolioInfo(p.Value.Title, PortfolioType.Primary, "сегодня", p.Value.PortfolioStats.ToList())).ToList();

        //var result = new List<PortfolioInfo>
        //{
        //    new PortfolioInfo("Составной инвестиционный", PortfolioType.Primary, "сегодня",
        //    [
        //        new PortfolioStats("Стоимость", 84320, "$", false),
        //        new PortfolioStats("Доходность", 15.2, "%"),
        //        new PortfolioStats("Активов", 12)
        //    ]),
        //    new PortfolioInfo("Портфель №1", PortfolioType.Dividend, "вчера",
        //    [
        //        new PortfolioStats("Стоимость", 24150, "$", false),
        //        new PortfolioStats("Доходность", -8.4, "%"),
        //        new PortfolioStats("Активов", 8)
        //    ]),
        //    new PortfolioInfo("Портфель №2", PortfolioType.Agressive, "2 дня назад",
        //    [
        //        new PortfolioStats("Стоимость", 16062, "$", false),
        //        new PortfolioStats("Доходность", 22.1, "%"),
        //        new PortfolioStats("Активов", 6)
        //    ]),
        //};

        PortfolioInfos.Clear();
        PortfolioInfos.AddRange(result);
    }
}