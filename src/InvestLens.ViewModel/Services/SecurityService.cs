using AutoMapper;
using InvestLens.DataAccess.Repositories;
using InvestLens.DataAccess.Services;
using InvestLens.Model;
using InvestLens.Model.Entities;
using OxyPlot.Series;

namespace InvestLens.ViewModel.Services;

public class SecurityService : ISecurityService
{
    private readonly IDatabaseService _databaseService;
    private readonly ISecurityRepository _repository;
    private readonly IMoexService _moexService;
    private readonly IMapper _mapper;
    private readonly IWindowManager _windowManager;

    public SecurityService(
        IDatabaseService databaseService,
        ISecurityRepository repository, 
        IMoexService moexService,
        IMapper mapper,
        IWindowManager windowManager)
    {
        _databaseService = databaseService;
        _repository = repository;
        _moexService = moexService;
        _mapper = mapper;
        _windowManager = windowManager;
    }

    public async Task UpdateSecurities(List<string> secIdImportList, CancellationToken ct)
    {
        try
        {
            var secIdDbList = await _repository.GetSecIdListAsync(ct);
            var secIdNewList = secIdImportList.Except(secIdDbList);

            var newSecurityModelList = await _moexService.GetSecurityList(secIdNewList, ct);

            var newSecurityList = _mapper.Map<List<Security>>(newSecurityModelList);
            foreach (var security in newSecurityList)
            {
                security.IsLoaded = true;
            }

            await _databaseService.BeginTransactionAsync();
            
            await _repository.AddRangeAsync(newSecurityList, ct);

            foreach (var security in newSecurityList.Where(s => !string.IsNullOrEmpty(s.MarketpriceBoardid)))
            {
                await _moexService.LoadHistory(security, ct);
            }
            
            await _databaseService.SaveAsync();
            await _databaseService.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _databaseService.RollbackTransactionAsync();
            _windowManager.ShowErrorDialog($"Ошибка при обновлении ценнсых бумаг: {ex.Message}");
        }
    }
}