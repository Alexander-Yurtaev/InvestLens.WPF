using AutoMapper;
using InvestLens.DataAccess.Repositories;
using InvestLens.DataAccess.Repositories.Settings;
using InvestLens.Model;
using InvestLens.Model.Entities.Settings;
using InvestLens.Model.Helpers;
using InvestLens.Model.MoexApi.Responses;
using InvestLens.Model.MoexApi.Responses.ResponseItems;
using System.Net.Http.Json;

namespace InvestLens.DataAccess.Services;

public class MoexProvider : IMoexProvider
{
    private readonly IMapper _mapper;
    private readonly IDatabaseService _databaseService;
    private readonly IEngineRepository _engineRepository;
    private readonly IMarketRepository _marketRepository;
    private readonly IBoardRepository _boardRepository;
    private readonly IBoardGroupRepository _boardGroupRepository;
    private readonly IDurationRepository _durationRepository;
    private readonly ISecurityTypeRepository _securityTypeRepository;
    private readonly ISecurityGroupRepository _securityGroupRepository;
    private readonly ISecurityCollectionRepository _securityCollectionRepository;
    private HttpClient _httpClient;

    public MoexProvider(
        IMapper mapper, 
        IHttpClientFactory factory,
        IDatabaseService databaseService,
        IEngineRepository engineRepository,
        IMarketRepository marketRepository,
        IBoardRepository boardRepository,
        IBoardGroupRepository boardGroupRepository,
        IDurationRepository durationRepository,
        ISecurityTypeRepository securityTypeRepository,
        ISecurityGroupRepository securityGroupRepository,
        ISecurityCollectionRepository securityCollectionRepository
        )
    {
        _httpClient = factory.CreateClient("moex");
        _mapper = mapper;
        _databaseService = databaseService;
        _engineRepository = engineRepository;
        _marketRepository = marketRepository;
        _boardRepository = boardRepository;
        _boardGroupRepository = boardGroupRepository;
        _durationRepository = durationRepository;
        _securityTypeRepository = securityTypeRepository;
        _securityGroupRepository = securityGroupRepository;
        _securityCollectionRepository = securityCollectionRepository;
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }

    public async Task LoadMoexIndex(CancellationToken ct)
    {
        if (ct.IsCancellationRequested) return;

        var response = await _httpClient.GetFromJsonAsync<IndexResponse>("iss/index.json", ct);
        if (response is null)
        {
            throw new InvalidOperationException(nameof(IndexResponse));
        }

        await _databaseService.BeginTransactionAsync();

        try
        {
            if (response.Engines is not null)
            {
                var engines = MoexResponseHelper.GetModels<Engines, Engine>(response.Engines);
                foreach (var engineModel in engines)
                {
                    ct.ThrowIfCancellationRequested();
                    await _engineRepository.AddOrUpdate(engineModel);
                }
            }

            if (response.Markets is not null)
            {
                var markets = MoexResponseHelper.GetModels<Markets, Market>(response.Markets);
                foreach (var marketModel in markets)
                {
                    ct.ThrowIfCancellationRequested();
                    await _marketRepository.AddOrUpdate(marketModel);
                }
            }

            if (response.Boards is not null)
            {
                var boards = MoexResponseHelper.GetModels<Boards, Board>(response.Boards);
                foreach (var boardModel in boards)
                {
                    ct.ThrowIfCancellationRequested();
                    await _boardRepository.AddOrUpdate(boardModel);
                }
            }

            if (response.BoardGroups is not null)
            {
                var boardGroups = MoexResponseHelper.GetModels<BoardGroups, BoardGroup>(response.BoardGroups);
                foreach (var boardGroupModel in boardGroups)
                {
                    ct.ThrowIfCancellationRequested();
                    await _boardGroupRepository.AddOrUpdate(boardGroupModel);
                }
            }

            if (response.Durations is not null)
            {
                var durations = MoexResponseHelper.GetModels<Durations, Duration>(response.Durations);
                foreach (var durationModel in durations)
                {
                    ct.ThrowIfCancellationRequested();
                    await _durationRepository.AddOrUpdate(durationModel);
                }
            }

            if (response.SecurityTypes is not null)
            {
                var securityTypes = MoexResponseHelper.GetModels<SecurityTypes, Model.Entities.Settings.SecurityType>(response.SecurityTypes);
                foreach (var securityTypeModel in securityTypes)
                {
                    ct.ThrowIfCancellationRequested();
                    await _securityTypeRepository.AddOrUpdate(securityTypeModel);
                }
            }

            if (response.SecurityGroups is not null)
            {
                var securityGroups = MoexResponseHelper.GetModels<SecurityGroups, Model.Entities.Settings.SecurityGroup>(response.SecurityGroups);
                foreach (var securityGroupModel in securityGroups)
                {
                    ct.ThrowIfCancellationRequested();
                    await _securityGroupRepository.AddOrUpdate(securityGroupModel);
                }
            }

            if (response.SecurityCollections is not null)
            {
                var securityCollections = MoexResponseHelper.GetModels<SecurityCollections, Model.Entities.Settings.SecurityCollection>(response.SecurityCollections);
                foreach (var securityCollectionModel in securityCollections)
                {
                    ct.ThrowIfCancellationRequested();
                    await _securityCollectionRepository.AddOrUpdate(securityCollectionModel);
                }
            }

            await _databaseService.SaveAsync();

            await _databaseService.CommitTransactionAsync();
        }
        catch
        {
            await _databaseService.RollbackTransactionAsync();
        }
    }

    public async Task<List<SecurityModel>> GetSecurityList(IEnumerable<string> secIdNewList)
    {
        var securityModelList = new List<SecurityModel>();

        foreach (var secId in secIdNewList)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<SecuritiesResponse>($"iss/securities.json?q={secId}");
                if (response?.Securities == null)
                {
                    continue;
                }

                var securities = MoexResponseHelper.GetModels<Securities, Security>(response.Securities);
                var security = securities.FirstOrDefault(s => s.SecId == secId);

                var model = security is not null
                        ? _mapper.Map<SecurityModel>(security)
                        : new SecurityModel { SecId = secId, SecTypeId = 3 };

                securityModelList.Add(model);
            }            
            catch (Exception)
            {

            }
        }

        return securityModelList;
    }
}