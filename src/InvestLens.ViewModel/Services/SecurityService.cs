using AutoMapper;
using InvestLens.DataAccess.Repositories;
using InvestLens.DataAccess.Services;
using InvestLens.Model;
using InvestLens.Model.Entities;

namespace InvestLens.ViewModel.Services;

public class SecurityService : ISecurityService
{
    private readonly IDatabaseService _databaseService;
    private readonly ISecurityRepository _repository;
    private readonly IMoexService _moexProvider;
    private readonly IMapper _mapper;
    private readonly IWindowManager _windowManager;

    public SecurityService(
        IDatabaseService databaseService,
        ISecurityRepository repository, 
        IMoexService moexProvider,
        IMapper mapper,
        IWindowManager windowManager)
    {
        _databaseService = databaseService;
        _repository = repository;
        _moexProvider = moexProvider;
        _mapper = mapper;
        _windowManager = windowManager;
    }

    public async Task UpdateSecurities(List<string> secIdImportList, CancellationToken ct)
    {
        try
        {
            var secIdDbList = await _repository.GetSecIdListAsync();
            var secIdNewList = secIdImportList.Except(secIdDbList);

            var newSecurityModelList = await _moexProvider.GetSecurityList(secIdNewList, ct);

            var newSecurityList = _mapper.Map<List<Security>>(newSecurityModelList);
            foreach (var security in newSecurityList)
            {
                security.IsLoaded = true;
            }

            await _repository.AddRangeAsync(newSecurityList);
            await _databaseService.SaveAsync();
        }
        catch (Exception ex)
        {
            _windowManager.ShowErrorDialog($"Ошибка при обновлении ценнсых бумаг: {ex.Message}");
        }
    }
}