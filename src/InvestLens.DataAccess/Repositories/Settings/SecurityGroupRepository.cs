using InvestLens.DataAccess.Services;
using InvestLens.Model.Entities.Settings;
using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class SecurityGroupRepository : BaseRepository, ISecurityGroupRepository
{
    public SecurityGroupRepository(IDatabaseService databaseService, IAuthManager authManager) 
        : base(databaseService, authManager)
    {
    }

    public List<SecurityGroup> GetAll()
    {
        return DatabaseService.DataContext.SecurityGroups.ToList();
    }
}