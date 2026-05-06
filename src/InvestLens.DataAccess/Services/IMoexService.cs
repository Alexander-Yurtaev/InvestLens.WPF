using InvestLens.Model;

namespace InvestLens.DataAccess.Services;

public interface IMoexService
{
    MoexCache MoexDictionaries { get; }
    Task<List<SecurityModel>> GetSecurityList(IEnumerable<string> secIdNewList, CancellationToken ct);
    Task LoadMoexIndex(CancellationToken ct);
}