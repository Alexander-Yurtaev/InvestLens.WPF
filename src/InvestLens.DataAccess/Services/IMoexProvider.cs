using InvestLens.Model;

namespace InvestLens.DataAccess.Services;

public interface IMoexProvider
{
    Task<List<SecurityModel>> GetSecurityList(IEnumerable<string> secIdNewList);
}