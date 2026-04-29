using InvestLens.Model.Entities;
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess.Repositories;

public class SecurityRepository : BaseRepository, ISecurityRepository
{
    public SecurityRepository(InvestLensDataContext dataContext, 
        IAuthManager authManager) : base(dataContext, authManager)
    {
    }

    public async Task AddRangeAsync(IEnumerable<Security> newSecurityList)
    {
        await DataContext.Securities.AddRangeAsync(newSecurityList);
    }

    public async Task<List<string>> GetSecIdListAsync()
    {
        var result = await DataContext.Securities
            .Select(s => s.SecId)
            .ToListAsync();

        return result;
    }

    public async Task<List<Security>> GetUnloadedSecurityListAsync()
    {
        var result = await DataContext.Securities
            .Where(s => !s.IsLoaded)
            .ToListAsync();

        return result;
    }
}