using InvestLens.DataAccess.Repositories;
using InvestLens.DataAccess.Services;
using InvestLens.Model.Entities;

namespace InvestLens.ViewModel.Services;

public class SecurityService : ISecurityService
{
    private readonly ISecurityRepository _repository;

    public SecurityService(ISecurityRepository repository)
    {
        _repository = repository;
    }

    public async Task UpdateSecurities(List<string> secIdImportList)
    {
        var secIdDbList = await _repository.GetSecIdListAsync();
        var secIdNewList = secIdImportList.Except(secIdDbList);

        var newSecurityList = secIdNewList.Select(s => new Security(s));

        await _repository.AddRangeAsync(newSecurityList);
        await _repository.SaveAsync();
    }
}