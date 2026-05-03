using InvestLens.DataAccess.Services;
using InvestLens.Model.Entities;
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess.Repositories;

public class SecurityRepository : BaseRepository, ISecurityRepository
{
    public SecurityRepository(IDatabaseService databaseService, 
        IAuthManager authManager) : base(databaseService, authManager)
    {
    }

    public async Task AddRangeAsync(IEnumerable<Security> newSecurityList)
    {
        await DatabaseService.DataContext.Securities.AddRangeAsync(newSecurityList);
    }

    public async Task<List<string>> GetSecIdListAsync()
    {
        var result = await DatabaseService.DataContext.Securities
            .Select(s => s.SecId)
            .ToListAsync();

        return result;
    }

    public async Task<List<Security>> GetUnloadedSecurityListAsync()
    {
        var result = await DatabaseService.DataContext.Securities
            .Where(s => !s.IsLoaded)
            .ToListAsync();

        return result;
    }
}