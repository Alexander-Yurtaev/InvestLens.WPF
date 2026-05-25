using InvestLens.Model.Entities;

namespace InvestLens.DataAccess.Repositories;

public interface ISecurityRepository : IBaseRepository
{
    Task AddRangeAsync(IEnumerable<Security> newSecurityList, CancellationToken ct);
    Task<List<string>> GetSecIdListAsync(CancellationToken ct);
    Task<List<Security>> GetLoadedSecurityListAsync(CancellationToken ct);
    Task<List<Security>> GetUnloadedSecurityListAsync(CancellationToken ct);
}
