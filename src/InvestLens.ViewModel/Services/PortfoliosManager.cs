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

namespace InvestLens.ViewModel.Services;

public class PortfoliosManager : IPortfoliosManager
{
    private readonly IMapper _mapper;
    private readonly IAuthManager _authManager;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IEventAggregator _eventAggregator;
    private readonly SemaphoreSlim _loadSemaphoreSlim = new SemaphoreSlim(1);

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

        var detail = new PortfolioDetail(portfolio.Id, portfolio.Name, portfolio.PortfolioType)
        {
            Description = portfolio.Description ?? ""
        };
        detail.Portfolios.AddRange(portfolio.ChildrenPortfolios.Select(c => c.Id));
        return detail;
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
                Cards.Add(CreateCard(_portfolioCache[portfolio.Id]));
                _eventAggregator.GetEvent<PortfolioCreatedEvent>().Publish(portfolio.Id);
            }
            catch
            {
                await _portfolioRepository.RollbackTransactionAsync();
                throw;
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
        Cards[index] = CreateCard(_portfolioCache[portfolio.Id]);

        _eventAggregator.GetEvent<PortfolioUpdatedEvent>().Publish(model.Id);
    }

    public async Task Delete(int portfolioId)
    {
        await _portfolioRepository.Delete(portfolioId);
        await _portfolioRepository.Save();

        _portfolioCache.Remove(portfolioId);

        var card = Cards.First(card => card.Id == portfolioId);
        Cards.Remove(card);

        _eventAggregator.GetEvent<PortfolioDeletedEvent>().Publish(portfolioId);
    }

    public async Task<bool> CheckNameUniqueAsync(int portfolioId, int ownerId, string name)
    {
        return await _portfolioRepository.CheckNameUniqueAsync(portfolioId, ownerId, name);
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

            RefreshCards();
        }
        finally
        {
            _loadSemaphoreSlim.Release();
        }

        _eventAggregator.GetEvent<PortfoliosLoadedEvent>().Publish();
    }

    private void RefreshCards()
    {
        var result = _portfolioCache.Values.Select(CreateCard).ToList();
        Cards.Clear();
        Cards.AddRange(result);
    }

    private Card CreateCard(PortfolioModel portfolio)
    {
        var card = new Card(portfolio.Id, portfolio.Name, true)
        {
            CardType = PortfolioTypeToStringConverter(portfolio.PortfolioType),
            CardTypeForeground = PortfolioTypeToForegroundConverter(portfolio.PortfolioType),
            CardTypeBackground = PortfolioTypeToBackgroundConverter(portfolio.PortfolioType),
            LastDateUpdate = "сегодня"
        };
        //card.Stats.AddRange(p.PortfolioStats);
        return card;
    }
}