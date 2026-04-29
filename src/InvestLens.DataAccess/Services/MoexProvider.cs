using InvestLens.Model;

namespace InvestLens.DataAccess.Services;

public class MoexProvider : IMoexProvider
{
    public async Task<List<SecurityModel>> GetSecurityList(IEnumerable<string> secIdNewList)
    {
        var securityList = secIdNewList
            .Select(s => new SecurityModel(s, Model.Enums.SecurityType.None))
            .ToList();
        
        return await Task.FromResult(securityList);
    }
}