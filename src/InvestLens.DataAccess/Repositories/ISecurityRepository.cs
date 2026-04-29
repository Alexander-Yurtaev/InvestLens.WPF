using InvestLens.Model.Entities;

namespace InvestLens.DataAccess.Repositories;

public interface ISecurityRepository : IBaseRepository
{
    Task AddRangeAsync(IEnumerable<Security> newSecurityList);
    Task<List<string>> GetSecIdListAsync();
    Task<List<Security>> GetUnloadedSecurityListAsync();
}
