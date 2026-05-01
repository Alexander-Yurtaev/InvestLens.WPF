using InvestLens.Model;

namespace InvestLens.DataAccess.Services;

public interface IMoexProvider : IDisposable
{
    Task<List<SecurityModel>> GetSecurityList(IEnumerable<string> secIdNewList);
    Task LoadMoexIndex();
}