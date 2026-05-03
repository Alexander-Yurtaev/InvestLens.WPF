using InvestLens.DataAccess.Services;
using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class SecurityGroupRepository : BaseRepository, ISecurityGroupRepository
{
    public SecurityGroupRepository(IDatabaseService databaseService, IAuthManager authManager) 
        : base(databaseService, authManager)
    {
    }
}