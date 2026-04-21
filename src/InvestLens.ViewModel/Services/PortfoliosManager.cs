using AutoMapper;
using InvestLens.DataAccess.Repositories;
using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.Model.NavigationTree;
using InvestLens.Model.Services;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Helpers;
using InvestLens.ViewModel.NavigationTree;

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

    public List<INavigationTreeItem> GetPortfoliosMenuItems()
    {
        var result = _portfolioCache.Values.Select(p =>
            new NavigationTreeItem(
                new PortfolioNavigationTreeModel(p.Id, "", p.Name, p.PortfolioType) { Title = p.Name, Description = p.Description ?? "" },
                _eventAggregator)).Cast<INavigationTreeItem>().ToList();

        return result;
    }

    public async Task<PortfolioDetails?> GetPortfolioDetiails(int id)
    {
        var portfolio = GetPortfolioByIdFromCache(id);
        if (portfolio is null)
        {
            _windowManager.ShowErrorDialog($"Не найден портфель с Id={id}.");
            return null;
        }

        var details = new PortfolioDetails(portfolio.Id, portfolio.Name, portfolio.PortfolioType)
        {
            Description = portfolio.Description ?? ""
        };

        if (details.PortfolioType == PortfolioType.Invest)
        {
            await FillInvestPortfolio(portfolio.Id, details);
        }
        else
        {
            await FillComplexPortfolio(portfolio, details);
        }

        return details;
    }

    private async Task FillInvestPortfolio(int portfolioId, PortfolioDetails details)
    {
        var transactions = await _portfolioRepository.GetTransactions(portfolioId);
        var securityInfos = transactions
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

        var operations = _mapper
            .Map<List<SecurityOperation>>(transactions)
            .OrderBy(o => o.Date);
        details.Operations.Clear();
        details.Operations.AddRange(operations);
    }

    private async Task FillComplexPortfolio(PortfolioModel portfolio, PortfolioDetails details)
    {
        foreach (var childPortfolio in portfolio.ChildrenPortfolios)
        {
            var childDetails = new PortfolioDetails(0, "", PortfolioType.Invest);
            await FillInvestPortfolio(childPortfolio.Id, childDetails);

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
                details.Operations.AddRange(childDetails.Operations);
            }
        }

        details.Portfolios.AddRange(portfolio.ChildrenPortfolios.Select(c => c.Id));
    }

    public List<LookupModel> GetLookupModels(int ownerId, int? portfolioId = null)
    {
        var portfolios = _portfolioCache.Values
            .Where(p => (portfolioId is null || p.Id != portfolioId.Value) && 
                        p.PortfolioType != PortfolioType.Complex);

        return portfolios.Select(p => new Model.Crud.Portfolio
                                    .LookupModel(p.Id, p.Name, p.PortfolioType)).ToList();
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
                        var childPortfolio = GetPortfolioByIdFromCache(childId);
                        if (childPortfolio is null)
                        {
                            _windowManager.ShowErrorDialog($"Не найден портфель с Id={childId}.");
                            return;
                        }
                        childPortfolio!.ParentPortfolioId = portfolio.Id;
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

    public bool CheckNameUnique(int portfolioId, int ownerId, string name)
    {
        var isExists = _portfolioCache.Values
                            .Any(p => p.OwnerId == ownerId
                                      && (portfolioId == 0 || p.Id != portfolioId)
                                      && p.Name == name);

        return !isExists;
    }

    public async Task<int> Merge(List<Transaction> transactions)
    {
        return await _portfolioRepository.Merge(transactions);
    }

    public async Task<int> Recreate(List<Transaction> transactions)
    {
        return await _portfolioRepository.Recreate(transactions);
    }

    public async Task ReloadPortfolio(int id)
    {
        try
        {
            var portfolio = await _portfolioRepository.GetPortfolioById(id);

            if (portfolio is null)
            {
                _windowManager.ShowErrorDialog($"Портфель с Id={id} не найден.");
                return;
            }

            _portfolioCache[portfolio.Id] = _mapper.Map<PortfolioModel>(portfolio);

            await RefreshCardsAsync();
        }
        finally
        {
            _loadSemaphoreSlim.Release();
        }

        _eventAggregator.GetEvent<PortfoliosLoadedEvent>().Publish();
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
            var portfolios = await _portfolioRepository.GetAllPortfolios();

            _portfolioCache.Clear();
            foreach (var portfolio in portfolios)
            {
                _portfolioCache[portfolio.Id] = _mapper.Map<PortfolioModel>(portfolio);
            }

            await RefreshCardsAsync();
        }
        finally
        {
            _loadSemaphoreSlim.Release();
        }

        _eventAggregator.GetEvent<PortfoliosLoadedEvent>().Publish();
    }

    private PortfolioModel? GetPortfolioByIdFromCache(int id)
    {
        if (_portfolioCache.TryGetValue(id, out var portfolio))
        {
            return portfolio;
        }

        return null;
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
            CardType = PortfoliosCardHelper.PortfolioTypeToStringConverter(model.PortfolioType),
            CardTypeForeground = PortfoliosCardHelper.PortfolioTypeToForegroundConverter(model.PortfolioType),
            CardTypeBackground = PortfoliosCardHelper.PortfolioTypeToBackgroundConverter(model.PortfolioType),
            LastDateUpdate = "сегодня"
        };
        var details = await GetPortfolioDetiails(model.Id);
        if (details is null)
        {
            _windowManager.ShowErrorDialog($"Ошибка при получении данных для портфеля '{model.Name}'");
            return null;
        }

        return card;
    }

    public List<PortfolioModel> GetAllPortfolios(PortfolioType portfolioType)
    {
        return _portfolioCache.Values
            .Where(p => p.PortfolioType == portfolioType)
            .ToList();
    }

    public async Task<List<Transaction>> GetLastTtransactions(int count)
    {
        var transactions = await _portfolioRepository.GetLastTtransactions(count);
        return transactions;
    }
}