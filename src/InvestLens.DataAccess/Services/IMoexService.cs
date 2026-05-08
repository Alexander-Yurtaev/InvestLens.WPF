using InvestLens.Model;
using InvestLens.Model.Entities;

namespace InvestLens.DataAccess.Services;

public interface IMoexService
{
    MoexCache MoexDictionaries { get; }
    Task<List<SecurityModel>> GetSecurityList(IEnumerable<string> secIdNewList, CancellationToken ct);
    Task LoadHistory(Security security, CancellationToken ct);
    Task LoadMoexIndex(CancellationToken ct);
}