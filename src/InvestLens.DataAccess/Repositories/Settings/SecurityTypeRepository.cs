using InvestLens.DataAccess.Services;
using InvestLens.Model.Entities.Settings;
using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class SecurityTypeRepository : BaseRepository, ISecurityTypeRepository
{
    public SecurityTypeRepository(IDatabaseService databaseService, IAuthManager authManager) 
        : base(databaseService, authManager)
    {
    }

    public List<SecurityType> GetAll()
    {
        return DatabaseService.DataContext.SecurityTypes.ToList();
    }
}