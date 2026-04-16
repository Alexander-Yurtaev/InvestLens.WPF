using Autofac.Features.OwnedInstances;
using AutoMapper;
using InvestLens.DataAccess.Repositories;
using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Enums;
using InvestLens.Model.NavigationTree;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.NavigationTree;

namespace InvestLens.ViewModel.Services;

public class PortfoliosManager : IPortfoliosManager
{
    private readonly IMapper _mapper;
    private readonly IAuthManager _authManager;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IEventAggregator _eventAggregator;
    private readonly SemaphoreSlim _loadSemaphoreSlim = new SemaphoreSlim(1);

    private readonly Dictionary<int, PortfolioDetail> _portfolios = new Dictionary<int, PortfolioDetail>
    {
        { 1, new PortfolioDetail(1, "Составной", PortfolioType.Complex)
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
        { 2, new PortfolioDetail(2, "Портфель №1", PortfolioType.Invest)
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
        { 3, new PortfolioDetail(3, "Портфель №2", PortfolioType.Invest)
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

    private readonly Dictionary<int, Model.Crud.Portfolio.PortfolioModel> _portfolioCache = [];

    public PortfoliosManager(
        IMapper mapper,
        IAuthManager authManager,
        IPortfolioRepository portfolioRepository, 
        IEventAggregator eventAggregator)
    {
        _mapper = mapper;
        _authManager = authManager;
        _portfolioRepository = portfolioRepository;
        _eventAggregator = eventAggregator;

        _eventAggregator.GetEvent<LoginEvent>().Subscribe(OnLogin);
    }

    public List<Card> Cards { get; } = [];

    public List<INavigationTreeItem> GetPortfoliosMenuItems(int ownerId)
    {
        var result = _portfolioCache.Values.Select(p =>
            new NavigationTreeItem(
                new PortfolioNavigationTreeModel(p.Id, "", p.Name, p.PortfolioType) { Title = p.Name, Description = p.Description ?? "" },
                _eventAggregator)).Cast<INavigationTreeItem>().ToList();

        return result;
    }

    public async Task<PortfolioDetail?> GetPortfolio(int id)
    {
        var portfolio = await _portfolioRepository.GetPortfolioById(id);
        if (portfolio is null) return null;

        var detail = new PortfolioDetail(portfolio.Id, portfolio.Name, portfolio.PortfolioType);
        return detail;
    }

    public async Task<List<LookupModel>> GetLookupModels(int ownerId, int? portfolioId = null)
    {
        var portfolios = (await _portfolioRepository.GetAllPortfolios(ownerId))
            .Where(p => portfolioId is null || p.Id != portfolioId.Value && p.PortfolioType != PortfolioType.Complex);

        return portfolios.Select(p => new Model.Crud.Portfolio.LookupModel(p.Id, p.Name, p.PortfolioType)).ToList();
    }

    private PortfolioType TitleToPortfolioType(string title)
    {
        return title switch
        {
            "Составной" => PortfolioType.Complex,
            "Портфель №1" => PortfolioType.Invest,
            "Портфель №2" => PortfolioType.Invest,
            _ => throw new ArgumentOutOfRangeException(title)
        };
    }

    private string PortfolioTypeToStringConverter(PortfolioType portfolioType)
    {
        return portfolioType switch
        {
            PortfolioType.Complex => "Составной",
            PortfolioType.Invest => "Инвест",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private string PortfolioTypeToForegroundConverter(PortfolioType portfolioType)
    {
        return portfolioType switch
        {
            PortfolioType.Complex => "#FFC8102E",
            PortfolioType.Invest => "#FF2C8C6E",
            _ => "0xFFFF4500"
        };
    }

    private string PortfolioTypeToBackgroundConverter(PortfolioType portfolioType)
    {
        return portfolioType switch
        {
            PortfolioType.Complex => "#1AC8102E",
            PortfolioType.Invest => "#1A2C8C6E",
            _ => "0xFFFFA500"
        };
    }

    private async void OnLogin(UserInfo info)
    {
        await Refresh(info.Id);
    }

    private async Task Refresh(int ownerId)
    {
        await _loadSemaphoreSlim.WaitAsync();

        try
        {
            var portfolios = await _portfolioRepository.GetAllPortfolios(ownerId);

            _portfolioCache.Clear();
            foreach (var portfolio in portfolios)
            {
                _portfolioCache[portfolio.Id] = _mapper.Map<PortfolioModel>(portfolio);
            }

            RefreshCards();
        }
        finally
        {
            _loadSemaphoreSlim.Release();
        }

        _eventAggregator.GetEvent<PortfoliosRefreshedEvent>().Publish();
    }

    private void RefreshCards()
    {
        var result = _portfolioCache.Values.Select(p =>
        {
            var card = new Card(p.Id, p.Name, true)
            {
                CardType = PortfolioTypeToStringConverter(p.PortfolioType),
                CardTypeForeground = PortfolioTypeToForegroundConverter(p.PortfolioType),
                CardTypeBackground = PortfolioTypeToBackgroundConverter(p.PortfolioType),
                LastDateUpdate = "сегодня"
            };
            //card.Stats.AddRange(p.PortfolioStats);
            return card;
        }).ToList();

        Cards.Clear();
        Cards.AddRange(result);
    }

    public async Task Create(CreateModel model)
    {
        var portfolio = _mapper.Map<InvestLens.Model.Entities.Portfolio>(model);
        await _portfolioRepository.CreatePortfolio(portfolio);
        await Refresh(model.OwnerId);
        _eventAggregator.GetEvent<PortfoliosRefreshedEvent>().Publish();
    }

    public async Task Delete(int id)
    {
        await _portfolioRepository.Delete(id);
        _portfolioCache.Remove(id);

        var userId = _authManager.CurrentUser!.Id;
        await Refresh(userId);

        _eventAggregator.GetEvent<PortfoliosRefreshedEvent>().Publish();
    }

    public async Task<bool> CheckNameUniqueAsync(int ownerId, string name)
    {
        return await _portfolioRepository.CheckNameUniqueAsync(ownerId, name);
    }
}