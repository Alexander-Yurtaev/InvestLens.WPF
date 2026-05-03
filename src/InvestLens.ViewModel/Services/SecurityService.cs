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
    private readonly IMoexProvider _moexProvider;
    private readonly IMapper _mapper;

    public SecurityService(
        IDatabaseService databaseService,
        ISecurityRepository repository, 
        IMoexProvider moexProvider,
        IMapper mapper)
    {
        _databaseService = databaseService;
        _repository = repository;
        _moexProvider = moexProvider;
        _mapper = mapper;
    }

    public async Task UpdateSecurities(List<string> secIdImportList)
    {
        var secIdDbList = await _repository.GetSecIdListAsync();
        var secIdNewList = secIdImportList.Except(secIdDbList);

        var newSecurityModelList = await _moexProvider.GetSecurityList(secIdNewList);

        var newSecurityList = _mapper.Map<List<Security>>(newSecurityModelList);

        await _repository.AddRangeAsync(newSecurityList);
        await _databaseService.SaveAsync();
    }
}