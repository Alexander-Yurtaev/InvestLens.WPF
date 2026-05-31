using AutoMapper;
using InvestLens.DataAccess.Repositories;
using InvestLens.DataAccess.Repositories.Settings;
using InvestLens.Model;
using InvestLens.Model.Entities.Settings;
using InvestLens.Model.Helpers;
using InvestLens.Model.MoexApi.Responses;
using InvestLens.Model.MoexApi.Responses.ResponseItems;
using InvestLens.Model.MoexApi.Settings;
using System.Collections.Concurrent;
using System.Net.Http.Json;

namespace InvestLens.DataAccess.Services;

public class MoexCache
{
    public ConcurrentBag<EngineModel> Engines { get; } = [];
    public ConcurrentBag<MarketModel> Markets { get; } = [];
    public ConcurrentBag<BoardModel> Boards { get; } = [];
    public ConcurrentBag<BoardGroupModel> BoardGroups { get; } = [];
    public ConcurrentBag<DurationModel> Durations { get; } = [];
    public ConcurrentBag<SecurityTypeModel> SecurityTypes { get; } = [];
    public ConcurrentBag<SecurityGroupModel> SecurityGroups { get; } = [];
    public ConcurrentBag<SecurityCollectionModel> SecurityCollections { get; } = [];
}

public class MoexService : IMoexService
{
    private readonly IMapper _mapper;
    private readonly IDatabaseService _databaseService;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IEngineRepository _engineRepository;
    private readonly IMarketRepository _marketRepository;
    private readonly IBoardRepository _boardRepository;
    private readonly IBoardGroupRepository _boardGroupRepository;
    private readonly IDurationRepository _durationRepository;
    private readonly ISecurityTypeRepository _securityTypeRepository;
    private readonly ISecurityGroupRepository _securityGroupRepository;
    private readonly ISecurityCollectionRepository _securityCollectionRepository;
    private readonly IHistoryRepository _historyRepository;
    private HttpClient _httpClient;
    private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

    public MoexService(
        IMapper mapper, 
        IHttpClientFactory factory,
        IDatabaseService databaseService,
        ITransactionRepository transactionRepository,
        IEngineRepository engineRepository,
        IMarketRepository marketRepository,
        IBoardRepository boardRepository,
        IBoardGroupRepository boardGroupRepository,
        IDurationRepository durationRepository,
        ISecurityTypeRepository securityTypeRepository,
        ISecurityGroupRepository securityGroupRepository,
        ISecurityCollectionRepository securityCollectionRepository,
        IHistoryRepository historyRepository
        )
    {
        _httpClient = factory.CreateClient("moex");
        _mapper = mapper;
        _databaseService = databaseService;
        _transactionRepository = transactionRepository;
        _engineRepository = engineRepository;
        _marketRepository = marketRepository;
        _boardRepository = boardRepository;
        _boardGroupRepository = boardGroupRepository;
        _durationRepository = durationRepository;
        _securityTypeRepository = securityTypeRepository;
        _securityGroupRepository = securityGroupRepository;
        _securityCollectionRepository = securityCollectionRepository;
        _historyRepository = historyRepository;
        MoexDictionaries = new();
    }

    public MoexCache MoexDictionaries { get; }

    public async Task LoadHistory(Model.Entities.Security security, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(security.MarketpriceBoardid))
        {
            throw new ArgumentException($"Аргумент {nameof(security.MarketpriceBoardid)} не задан");
        }

        DateTime firstDate = DateTime.UtcNow;
        await _semaphoreSlim.WaitAsync(ct);
        try
        {
            firstDate = await _transactionRepository.GetFirstDate(security.SecId);
        }
        finally
        {
            _semaphoreSlim.Release();
        }

        var historyList = new List<InvestLens.Model.Entities.Moex.History>();

        var currentHistoryCursor = new InvestLens.Model.MoexApi.HistoryCursor();

        var board = MoexDictionaries.Boards.FirstOrDefault(b => b.BoardId == security.MarketpriceBoardid);

        if (board?.Market is null)
        {
            throw new ArgumentException($"Не найдена доска Id={security.MarketpriceBoardid} либо у доски не указан рынок");
        }

        while (currentHistoryCursor.Total == 0 || currentHistoryCursor.Total > currentHistoryCursor.Index * currentHistoryCursor.PageSize)
        {
            ct.ThrowIfCancellationRequested();

            var url = $"https://iss.moex.com/iss/history/engines/{board.Market.TradeEngineName}/markets/{board.Market.MarketName}/securities/{security.SecId}.json" +
                $"?from={firstDate.ToString("yyyy-MM-dd")}&tradingsession=3&marketprice_board=1" +
                $"&start={currentHistoryCursor.Index * currentHistoryCursor.PageSize}";

            var response = await _httpClient.GetFromJsonAsync<HistoryResponse>(url, ct);
            if (response is null)
            {
                throw new InvalidOperationException("Пришел пустой или не корректный ответ от Moex");
            }

            if (response.History is not null)
            {
                var history = MoexResponseHelper
                    .GetModels<Model.MoexApi.Responses.ResponseItems.History, InvestLens.Model.Entities.Moex.History>(response.History)
                    .ToList();

                historyList.AddRange(history);
            }

            if (response.HistoryCursor is not null)
            {
                var historyCursor = MoexResponseHelper
                    .GetModels<Model.MoexApi.Responses.ResponseItems.HistoryCursor, Model.MoexApi.HistoryCursor>(response.HistoryCursor)
                    .SingleOrDefault();

                if (historyCursor is null)
                {
                    throw new InvalidOperationException("Ошибка при обработке курсора данных от Moex");
                }

                currentHistoryCursor.PageSize = historyCursor.PageSize;
                currentHistoryCursor.Total = historyCursor.Total;
                currentHistoryCursor.Index++;
            }
            else
            {
                break;
            }
        }

        if (historyList.Any())
        {
            foreach (var history in historyList)
            {
                ct.ThrowIfCancellationRequested();
                await _historyRepository.AddOrUpdate(history);
            }
        }

        return;
    }

    public async Task LoadMoexIndex(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var response = await _httpClient.GetFromJsonAsync<IndexResponse>("iss/index.json", ct);
        if (response is null)
        {
            throw new InvalidOperationException("Пришел пустой или не корректный ответ от Moex");
        }

        await _databaseService.BeginTransactionAsync();

        try
        {
            MoexDictionaries.Engines.Clear();
            if (response.Engines is not null)
            {
                var engines = MoexResponseHelper.GetModels<Engines, Engine>(response.Engines);
                foreach (var engineEntity in engines)
                {
                    ct.ThrowIfCancellationRequested();
                    await _engineRepository.AddOrUpdate(engineEntity);
                    var engineModel = _mapper.Map<EngineModel>(engineEntity);
                    MoexDictionaries.Engines.Add(engineModel);
                }
            }

            MoexDictionaries.Markets.Clear();
            if (response.Markets is not null)
            {
                var markets = MoexResponseHelper.GetModels<Markets, Market>(response.Markets);
                foreach (var marketEntity in markets)
                {
                    ct.ThrowIfCancellationRequested();
                    await _marketRepository.AddOrUpdate(marketEntity);
                    var marketModel = _mapper.Map<MarketModel>(marketEntity);
                    MoexDictionaries.Markets.Add(marketModel);
                }
            }

            MoexDictionaries.Boards.Clear();
            if (response.Boards is not null)
            {
                var boards = MoexResponseHelper.GetModels<Boards, Board>(response.Boards);
                foreach (var boardEntity in boards)
                {
                    ct.ThrowIfCancellationRequested();
                    await _boardRepository.AddOrUpdate(boardEntity);
                    var boardModel = _mapper.Map<BoardModel>(boardEntity);
                    MoexDictionaries.Boards.Add(boardModel);
                }
            }

            MoexDictionaries.BoardGroups.Clear();
            if (response.BoardGroups is not null)
            {
                var boardGroups = MoexResponseHelper.GetModels<BoardGroups, BoardGroup>(response.BoardGroups);
                foreach (var boardGroupEntity in boardGroups)
                {
                    ct.ThrowIfCancellationRequested();
                    await _boardGroupRepository.AddOrUpdate(boardGroupEntity);
                    var boardGroupModel = _mapper.Map<BoardGroupModel>(boardGroupEntity);
                    MoexDictionaries.BoardGroups.Add(boardGroupModel);
                }
            }

            MoexDictionaries.Durations.Clear();
            if (response.Durations is not null)
            {
                var durations = MoexResponseHelper.GetModels<Durations, Duration>(response.Durations);
                foreach (var durationEntity in durations)
                {
                    ct.ThrowIfCancellationRequested();
                    await _durationRepository.AddOrUpdate(durationEntity);
                    var durationModel = _mapper.Map<DurationModel>(durationEntity);
                    MoexDictionaries.Durations.Add(durationModel);
                }
            }

            MoexDictionaries.SecurityTypes.Clear();
            if (response.SecurityTypes is not null)
            {
                var securityTypes = MoexResponseHelper.GetModels<SecurityTypes, Model.Entities.Settings.SecurityType>(response.SecurityTypes);
                foreach (var securityTypeEntity in securityTypes)
                {
                    ct.ThrowIfCancellationRequested();
                    await _securityTypeRepository.AddOrUpdate(securityTypeEntity);
                    var securityTypeModel = _mapper.Map<SecurityTypeModel>(securityTypeEntity);
                    MoexDictionaries.SecurityTypes.Add(securityTypeModel);
                }
            }

            MoexDictionaries.SecurityGroups.Clear();
            if (response.SecurityGroups is not null)
            {
                var securityGroups = MoexResponseHelper.GetModels<SecurityGroups, Model.Entities.Settings.SecurityGroup>(response.SecurityGroups);
                foreach (var securityGroupEntity in securityGroups)
                {
                    ct.ThrowIfCancellationRequested();
                    await _securityGroupRepository.AddOrUpdate(securityGroupEntity);
                    var securityGroupModel = _mapper.Map<SecurityGroupModel>(securityGroupEntity);
                    MoexDictionaries.SecurityGroups.Add(securityGroupModel);
                }
            }

            MoexDictionaries.SecurityCollections.Clear();
            if (response.SecurityCollections is not null)
            {
                var securityCollections = MoexResponseHelper.GetModels<SecurityCollections, Model.Entities.Settings.SecurityCollection>(response.SecurityCollections);
                foreach (var securityCollectionEntity in securityCollections)
                {
                    ct.ThrowIfCancellationRequested();
                    await _securityCollectionRepository.AddOrUpdate(securityCollectionEntity);
                    var securityCollectionModel = _mapper.Map<SecurityCollectionModel>(securityCollectionEntity);
                    MoexDictionaries.SecurityCollections.Add(securityCollectionModel);
                }
            }

            await _databaseService.SaveAsync();
            await _databaseService.CommitTransactionAsync();
        }
        catch
        {
            await _databaseService.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<List<SecurityModel>> GetSecurityList(IEnumerable<string> secIdNewList, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var securityModelBag = new ConcurrentBag<SecurityModel>();
        using var semaphorSlim = new SemaphoreSlim(5);
        var tasks = new List<Task>();

        foreach (var secId in secIdNewList)
        {
            await semaphorSlim.WaitAsync(ct);

            tasks.Add(Task.Run(async () => {
                try
                {
                    ct.ThrowIfCancellationRequested();
                    var response = await _httpClient.GetFromJsonAsync<SecuritiesResponse>($"iss/securities.json?q={secId}", ct);
                    if (response?.Securities == null)
                    {
                        return;
                    }

                    var securities = MoexResponseHelper.GetModels<Securities, Security>(response.Securities);
                    var security = securities.FirstOrDefault(s => s.SecId == secId);

                    var model = security is not null
                            ? _mapper.Map<SecurityModel>(security)
                            : new SecurityModel { SecId = secId };

                    securityModelBag.Add(model);
                }
                finally
                {
                    semaphorSlim.Release();
                }
            }, ct));
        }

        await Task.WhenAll(tasks);

        return securityModelBag.ToList();
    }
}