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
                    new Stat("Стоимость", 84320, "$", false),
                    new Stat("Доходность", 15.2, "%"),
                    new Stat("Активов", 12)
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
                new Stat("Стоимость", 24150, "$", false),
                new Stat("Доходность", -8.4, "%"),
                new Stat("Активов", 8)
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
                new Stat("Стоимость", 16062, "$", false),
                new Stat("Доходность", 22.1, "%"),
                new Stat("Активов", 6)
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
        LoadPortfolioInfos();
    }

    public List<Card> Cards { get; } = [];

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
        {
            var portfolioType = TitleToPortfolioType(p.Value.Title);
            var card = new Card(p.Value.Title)
            {
                CardType = PortfolioTypeToStringConverter(portfolioType),
                CardTypeForeground = PortfolioTypeToForegroundConverter(portfolioType),
                CardTypeBackground = PortfolioTypeToBackgroundConverter(portfolioType),
                LastDateUpdate = "сегодня"
            };
            card.Stats.AddRange(p.Value.PortfolioStats);
            return card;
        }).ToList();

        Cards.Clear();
        Cards.AddRange(result);
    }

    private PortfolioType TitleToPortfolioType(string title)
    {
        return title switch
        {
            "Составной инвестиционный" => PortfolioType.Primary,
            "Портфель №1" => PortfolioType.Dividend,
            "Портфель №2" => PortfolioType.Agressive,
            _ => PortfolioType.Primary
        };
    }

    private string PortfolioTypeToStringConverter(PortfolioType portfolioType)
    {
        return portfolioType switch
        {
            PortfolioType.Primary => "Основной",
            PortfolioType.Agressive => "Агрессивный",
            PortfolioType.Dividend => "Дивидендный",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private string PortfolioTypeToForegroundConverter(PortfolioType portfolioType)
    {
        return portfolioType switch
        {
            PortfolioType.Primary => "#FFC8102E",
            PortfolioType.Agressive => "#FF2C8C6E",
            PortfolioType.Dividend => "#FF2C8C6E",
            _ => "0xFFFF4500"
        };
    }

    private string PortfolioTypeToBackgroundConverter(PortfolioType portfolioType)
    {
        return portfolioType switch
        {
            PortfolioType.Primary => "#1AC8102E",
            PortfolioType.Agressive => "#1A2C8C6E",
            PortfolioType.Dividend => "#1A2C8C6E",
            _ => "0xFFFFA500"
        };
    }
}