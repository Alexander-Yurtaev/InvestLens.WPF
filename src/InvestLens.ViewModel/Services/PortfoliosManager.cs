using Autofac.Features.OwnedInstances;
using AutoMapper;
using InvestLens.DataAccess.Repositories;
using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.Model.NavigationTree;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.NavigationTree;
using System.Collections.Generic;

namespace InvestLens.ViewModel.Services;

public class PortfoliosManager : IPortfoliosManager
{
    private readonly IMapper _mapper;
    private readonly IAuthManager _authManager;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IWindowManager _windowManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly SemaphoreSlim _loadSemaphoreSlim = new SemaphoreSlim(1);

    private readonly Dictionary<int, Model.Crud.Portfolio.PortfolioModel> _portfolioCache = [];

    public PortfoliosManager(
        IMapper mapper,
        IAuthManager authManager,
        IPortfolioRepository portfolioRepository,
        IWindowManager windowManager,
        IEventAggregator eventAggregator)
    {
        _mapper = mapper;
        _authManager = authManager;
        _portfolioRepository = portfolioRepository;
        _windowManager = windowManager;
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

    public async Task<PortfolioDetails?> GetPortfolioDetiails(int id)
    {
        var portfolio = await _portfolioRepository.GetPortfolioById(id);
        if (portfolio is null) return null;

        var details = new PortfolioDetails(portfolio.Id, portfolio.Name, portfolio.PortfolioType)
        {
            Description = portfolio.Description ?? ""
        };

        if (details.PortfolioType == PortfolioType.Invest)
        {
            FillInvestPortfolio(portfolio, details);
        }
        else
        {
            FillComplexPortfolio(portfolio, details);
        }

        var portfolioStats = CreateStat(details);
        details.PortfolioStats.AddRange(portfolioStats);

        var operations = _mapper
            .Map<List<SecurityOperation>>(portfolio.Transactions)
            .OrderBy(o => o.Date);

        details.Operations.AddRange(operations);

        return details;
    }

    private void FillInvestPortfolio(Portfolio portfolio, PortfolioDetails details)
    {
        var securityInfos = portfolio.Transactions
                .GroupBy(t => t.Symbol)
                .Select(g =>
                {
                    var count = g.Where(t => t.Event == TransactionEvents.Buy).Sum(t => t.Quantity)
                                -
                                g.Where(t => t.Event == TransactionEvents.Sell).Sum(t => t.Quantity);
                    var totalPrice = g.Where(t => t.Event == TransactionEvents.Buy).Sum(t => t.Price)
                                     -
                                     g.Where(t => t.Event == TransactionEvents.Sell).Sum(t => t.Price);

                    var dividendCount = g.Where(t => t.Event == TransactionEvents.Dividend)
                                     .Sum(t => t.Quantity);

                    return new SecurityInfo(g.Key, g.Key)
                    {
                        Count = count,
                        DividendCount = dividendCount,
                        TotalPrice = totalPrice
                    };
                });

        details.Securities.AddRange(securityInfos);
    }

    private List<Stat> CreateStat(PortfolioDetails details)
    {
        var portfolioStats = new List<Stat>
        {
            new Stat("Количество", details.Securities.Sum(s => s.Count)),
            new Stat("Стоимость", details.Securities.Sum(s => s.TotalPrice), "₽"),
            new Stat("Дивиденды", details.Securities.Sum(s => s.DividendCount), "₽"),
        };
        return portfolioStats;
    }

    private void FillComplexPortfolio(Portfolio portfolio, PortfolioDetails details)
    {
        foreach (var childPortfolio in portfolio.ChildrenPortfolios)
        {
            var childDetails = new PortfolioDetails(0, "", PortfolioType.Invest);
            FillInvestPortfolio(childPortfolio, childDetails);

            foreach (var childSecurityInfo in childDetails.Securities)
            {
                var info = details.Securities.FirstOrDefault(s => s.SecId == childSecurityInfo.SecId);
                if (info is null)
                {
                    details.Securities.Add((SecurityInfo)childSecurityInfo.Clone());
                }
                else
                {
                    info.Count += childSecurityInfo.Count;
                    info.DividendCount += childSecurityInfo.DividendCount;
                    info.TotalPrice += childSecurityInfo.TotalPrice;
                }
            }

            
        }

        details.Portfolios.AddRange(portfolio.ChildrenPortfolios.Select(c => c.Id));
    }

    public async Task<List<LookupModel>> GetLookupModels(int ownerId, int? portfolioId = null)
    {
        var portfolios = (await _portfolioRepository.GetAllPortfolios(ownerId))
            .Where(p => (portfolioId is null || p.Id != portfolioId.Value) && p.PortfolioType != PortfolioType.Complex);

        return portfolios.Select(p => new Model.Crud.Portfolio.LookupModel(p.Id, p.Name, p.PortfolioType)).ToList();
    }

    public async Task Create(CreateModel model)
    {
        using (var transaction = await _portfolioRepository.BeginTransactionAsync())
        {
            try
            {
                var portfolio = await _portfolioRepository.CreatePortfolio(model);
                await _portfolioRepository.Save();

                if (model.Portfolios.Any())
                {
                    foreach (var childId in model.Portfolios)
                    {
                        var childPortfolio = await _portfolioRepository.GetPortfolioById(childId);
                        childPortfolio!.ParentPortfolioId = portfolio!.Id;
                    }
                    await _portfolioRepository.Save();
                }

                await _portfolioRepository.CommitTransactionAsync();

                _portfolioCache[portfolio.Id] = _mapper.Map<PortfolioModel>(portfolio);
                var card = await CreateCard(_portfolioCache[portfolio.Id]);
                if (card is not null)
                {
                    Cards.Add(card);
                }

                _eventAggregator.GetEvent<PortfolioCreatedEvent>().Publish(portfolio.Id);
            }
            catch (Exception ex)
            {
                await _portfolioRepository.RollbackTransactionAsync();
                _windowManager.ShowErrorDialog(ex.Message);
            }
        }
    }

    public async Task Update(UpdateModel model)
    {
        await _portfolioRepository.Update(model);
        await _portfolioRepository.Save();
        var portfolio = await _portfolioRepository.GetPortfolioById(model.Id);
        if (portfolio is null)
        {
            await Task.FromException(new KeyNotFoundException($"Портфель с {model.Id} не найден."));
            return;
        }

        _portfolioCache[portfolio.Id] = _mapper.Map<PortfolioModel>(portfolio);

        var index = Cards.FindIndex(card => card.Id == portfolio.Id);
        var card = await CreateCard(_portfolioCache[portfolio.Id]);
        if (card is not null)
        {
            Cards[index] = card;
        }

        _eventAggregator.GetEvent<PortfolioUpdatedEvent>().Publish(model.Id);
    }

    public async Task<bool> Delete(int portfolioId)
    {
        await _portfolioRepository.Delete(portfolioId);
        var count = await _portfolioRepository.Save();
        if (count == 0) return false;

        _portfolioCache.Remove(portfolioId);

        var card = Cards.First(card => card.Id == portfolioId);
        Cards.Remove(card);

        _eventAggregator.GetEvent<PortfolioDeletedEvent>().Publish(portfolioId);

        return true;
    }

    public async Task<bool> CheckNameUniqueAsync(int portfolioId, int ownerId, string name)
    {
        return await _portfolioRepository.CheckNameUniqueAsync(portfolioId, ownerId, name);
    }

    private string PortfolioTypeToStringConverter(PortfolioType portfolioType)
    {
        switch (portfolioType)
        {
            case PortfolioType.Invest:
                return "Инвест";
            case PortfolioType.Complex:
                return "Составной";
            default:
                _windowManager.ShowWarningDialog($"Неизвестный тип портфеля {portfolioType}", "OK");
                return "";
        }
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
        await LoadPortfolios(info.Id);
    }

    private async Task LoadPortfolios(int ownerId)
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

            RefreshCardsAsync();
        }
        finally
        {
            _loadSemaphoreSlim.Release();
        }

        _eventAggregator.GetEvent<PortfoliosLoadedEvent>().Publish();
    }

    private async Task RefreshCardsAsync()
    {
        var result = new List<Card>();
        foreach (var model in _portfolioCache.Values)
        {
            var card = await CreateCard(model);
            if (card is not null)
            {
                result.Add(card);
            }
        }

        Cards.Clear();
        Cards.AddRange(result);
    }

    private async Task<Card?> CreateCard(PortfolioModel model)
    {
        var card = new Card(model.Id, model.Name, true)
        {
            CardType = PortfolioTypeToStringConverter(model.PortfolioType),
            CardTypeForeground = PortfolioTypeToForegroundConverter(model.PortfolioType),
            CardTypeBackground = PortfolioTypeToBackgroundConverter(model.PortfolioType),
            LastDateUpdate = "сегодня"
        };
        var details = await GetPortfolioDetiails(model.Id);
        if (details is null)
        {
            _windowManager.ShowErrorDialog($"Ошибка при получении данных для портфеля '{model.Name}'");
            return null;
        }

        card.Stats.AddRange(details.PortfolioStats);
        return card;
    }

    public async Task<int> Merge(List<Transaction> transactions)
    {
        return await _portfolioRepository.Merge(transactions);
    }

    public async Task<int> Recreate(List<Transaction> transactions)
    {
        return await _portfolioRepository.Recreate(transactions);
    }
}