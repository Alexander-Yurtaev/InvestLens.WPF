using AutoMapper;
using InvestLens.Shared.DataAccess.Repositories;
using InvestLens.Shared.DataAccess.Services;
using InvestLens.Shared.Model;
using InvestLens.Shared.Model.Entities;
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

            _windowManager.ShowIsBusy("Получаем список ЦБ...");

            var newSecurityModelList = await _moexService.GetSecurityList(secIdNewList, ct);

            var newSecurityList = _mapper.Map<List<Security>>(newSecurityModelList);
            foreach (var security in newSecurityList)
            {
                security.IsLoaded = true;
            }

            await _databaseService.BeginTransactionAsync();

            _windowManager.ShowIsBusy("Добавляем список ЦБ...");

            await _repository.AddRangeAsync(newSecurityList, ct);

            _windowManager.ShowIsBusy("Получаем исторические данные...");

            using var semaphorSlim = new SemaphoreSlim(1);
            var tasks = new List<Task>();

            foreach (var security in newSecurityList.Where(s => !string.IsNullOrEmpty(s.MarketpriceBoardid)))
            {
                await semaphorSlim.WaitAsync(ct);

                var task = Task.Run(async () => {
                    try
                    {
                        await _moexService.LoadHistory(security, ct);
                    }
                    finally
                    {
                        semaphorSlim.Release();
                    }
                }, ct);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            _windowManager.ShowIsBusy("");

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