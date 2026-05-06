using InvestLens.DataAccess.Services;
using InvestLens.Model.Entities.Settings;
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess.Repositories;

public class SecurityTypeRepository : BaseRepository, ISecurityTypeRepository
{
    public SecurityTypeRepository(IDatabaseService databaseService, IAuthManager authManager) 
        : base(databaseService, authManager)
    {
    }

    public async Task<List<SecurityType>> GetAllAsync()
    {
        return await DatabaseService.DataContext.SecurityTypes.ToListAsync();
    }
}